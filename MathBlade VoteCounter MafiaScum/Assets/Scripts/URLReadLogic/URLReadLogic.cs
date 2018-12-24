using System;
using HtmlAgilityPack;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Unity.Jobs;
using Unity.Collections;
using System.Text;
using Assets.Scripts.SupportClasses;
using System.Collections.Generic;
using Assets.Scripts.Support_Scripts;
using UnityEngine;

namespace Assets.Scripts.URLReadLogic
{
    public static class URLReadLogic
    {

        public static HtmlDocument GetHTMLDocumentFromURL(string validURL)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
                var web = new HtmlWeb();
                web.AutoDetectEncoding = false;
                web.OverrideEncoding = Encoding.GetEncoding("UTF-8");

                return web.Load(validURL, "GET");

            }
            catch
            {

                System.Console.WriteLine("An error occurred scraping the document. Try again later.");
                return null;

            }
        }


        private static bool MyRemoteCertificateValidationCallback(System.Object sender,
            X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            bool isOk = true;
            // If there are errors in the certificate chain,
            // look at each error to determine the cause.
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                for (int i = 0; i < chain.ChainStatus.Length; i++)
                {
                    if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                    {
                        continue;
                    }
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                        break;
                    }
                }
            }
            return isOk;
        }

        public static List<Post> GetAllPosts(int numberOfPages, int urlStart, int postsPerPage, string validURL, List<string> moderatorNames, List<Player> players, List<Replacement> replacements)
        {


            List<Post> finalPosts = new List<Post>();
            List<URLJobLogic.GrabDataFromOnePageJob> jobs = new List<URLJobLogic.GrabDataFromOnePageJob>();
            List<JobHandle> jobHandles;
           

            //This is max amount of space each job can take
            
        
            jobHandles =  new List<JobHandle>(); 
            for (int i = 0; i <= numberOfPages; i++)
			{
				int desiredStart = (postsPerPage * i) + urlStart;
				if (desiredStart > (numberOfPages * postsPerPage))
				{

					continue;
				}
				int firstPostOnPage = (postsPerPage * i) + urlStart;

				string urlForThisPage = validURL + "&start=" + firstPostOnPage;

				URLJobLogic.GrabDataFromOnePageJob job = URLJobLogic.CreateAJob(urlForThisPage, players, replacements, moderatorNames);
				jobs.Add(job);
                JobHandle handle = job.Schedule();

                jobHandles.Add(handle);

                


			}


            //foreach(JobHandle handle in jobHandles)
            //{
            //    handle.Complete();
            //}
            NativeArray<JobHandle> jobHandlesNativeArray = new NativeArray<JobHandle>(jobHandles.ToArray(), Allocator.Temp);
            JobHandle allJobHandles = JobHandle.CombineDependencies(jobHandlesNativeArray);

            URLJobLogic.ListenForAllJobsCompletion masterJob = new URLJobLogic.ListenForAllJobsCompletion();

            masterJob.Schedule(allJobHandles);

            allJobHandles.Complete();
            jobHandlesNativeArray.Dispose();



            try
            {

                foreach (URLJobLogic.GrabDataFromOnePageJob job in jobs)
                {
                   
                    try
                    {
                        NativeArray<byte> bytesFromJob = job.PostBytes;
                        
                        List<Post> voteList = bytesFromJob.ObjectFromNativeArrayBytes<List<Post>>();
                        finalPosts.AddRange(voteList);
                       
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                   


                }
            }
            catch (Exception streamException)
            {
                Debug.Log("Error in converting data.");
            }
            finally
            {
                foreach (URLJobLogic.GrabDataFromOnePageJob job in jobs)
                {

                  

                    job.ValidURLBytes.Dispose();
                    job.PlayersBytes.Dispose();
                    job.ReplacementBytes.Dispose();
                    job.ModeratorNamesBytes.Dispose();
                    job.PostBytes.Dispose();

                }
            }


            return finalPosts;
			
        }

        

    }



    public static class URLJobLogic
    {

      
        private const int MAX_BYTE_ARRAY_LENGTH = 314159;

        public static GrabDataFromOnePageJob CreateAJob(string validURL, List<Player> players, List<Replacement> replacements, List<string> moderatorNames)
        {
                

                GrabDataFromOnePageJob onePageJob = new GrabDataFromOnePageJob();

                onePageJob.ValidURLBytes = new NativeArray<byte>(validURL.Length, Allocator.Temp);
                onePageJob.ValidURLBytes.CopyFrom(Encoding.UTF8.GetBytes(validURL));


                byte[] playersBytes = players.ObjectToBytes();
                onePageJob.PlayersBytes = new NativeArray<byte>(playersBytes.Length, Allocator.Temp);
                onePageJob.PlayersBytes.CopyFrom(playersBytes);

                byte[] replacementsBytes = replacements.ObjectToBytes();
                onePageJob.ReplacementBytes = new NativeArray<byte>(replacementsBytes.Length, Allocator.Temp);
                onePageJob.ReplacementBytes.CopyFrom(replacementsBytes);

                byte[] moderatorNamesBytes = moderatorNames.ObjectToBytes();
                onePageJob.ModeratorNamesBytes = new NativeArray<byte>(moderatorNamesBytes.Length, Allocator.Temp);
                onePageJob.ModeratorNamesBytes.CopyFrom(moderatorNamesBytes);

                onePageJob.PostBytes = new NativeArray<byte>(MAX_BYTE_ARRAY_LENGTH, Allocator.TempJob);
               

               
                
                

                return onePageJob;
        }



        public struct GrabDataFromOnePageJob : IJob
        {           
            //Inputs needed
            public NativeArray<byte> ValidURLBytes;
            public NativeArray<byte> PlayersBytes;
            public NativeArray<byte> ReplacementBytes;
            public NativeArray<byte> ModeratorNamesBytes;
            public int MaxBytesForVector;


            //Output given
            public NativeArray<byte> PostBytes;
            //  public bool HasVotesToAdd;

            

            public void Execute()
            {
                //Convert back to useable objects
                string validURL = Encoding.UTF8.GetString(ValidURLBytes.ToArray());

				List<Player> players = PlayersBytes.ObjectFromNativeArrayBytes<List<Player>> ();
                List<Replacement> replacements = ReplacementBytes.ObjectFromNativeArrayBytes<List<Replacement>>();
                List<String> moderatorNames = ModeratorNamesBytes.ObjectFromNativeArrayBytes<List<String>>();  

                HtmlDocument doc = URLReadLogic.GetHTMLDocumentFromURL(validURL);                

                List<Post> postsFromPage = RunScrubLogic.RunScrubLogic.RunScrubOnADoc(doc, players, replacements, moderatorNames);

                byte[] postBytes = postsFromPage.ObjectToBytes<List<Post>>();

                if (postBytes.Length > MAX_BYTE_ARRAY_LENGTH)
                {
                    throw new System.ArgumentOutOfRangeException("Too long a list post object allocate more space.");
                }
                else if (postBytes.Length <= MAX_BYTE_ARRAY_LENGTH)
                {
                    byte[] byteArrayProperLength = new byte[MAX_BYTE_ARRAY_LENGTH];
                    for (int i = 0; i < postBytes.Length; i++)
                    {

                        byteArrayProperLength[i] = postBytes[i];
                    }
                    PostBytes.CopyFrom(byteArrayProperLength);
                    
                }





            }

           

                

        }
        
        //Workaround job to make sure all jobs complete.
        public struct ListenForAllJobsCompletion : IJob
        {
            

            public void Execute()
            {
               
            }
        }
    }

   
}

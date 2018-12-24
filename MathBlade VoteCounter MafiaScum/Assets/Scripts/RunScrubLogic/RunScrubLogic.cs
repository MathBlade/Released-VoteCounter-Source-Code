using System;
using System.Collections.Generic;

using HtmlAgilityPack;
using System.Diagnostics;
using Assets.Scripts.SupportClasses;
using Assets.Scripts.Support_Scripts;
using System.Text.RegularExpressions;



namespace Assets.Scripts.RunScrubLogic
{
    public static class RunScrubLogic
    {
        private const int MAX_USERNAME_LENGTH = 25;


        private const string VOTE_COUNT_SPOILER_NAME = "Spoiler: VoteCount Settings";


        public static string ScrubPostForSettings(string url, int postNumberInput)
        {

            try
            {

                url = "https://forum.mafiascum.net/viewtopic.php?" + url + "&start=" + postNumberInput;
                //return "Url: " + url + " Post Number Input: " + postNumberInput;

                Regex reg = new Regex("^p[0-9]", RegexOptions.IgnoreCase);
                HtmlDocument doc = URLReadLogic.URLReadLogic.GetHTMLDocumentFromURL(url);
                if ((doc == null) || doc.DocumentNode == null || doc.DocumentNode.SelectSingleNode("//div") == null)
                {
                    throw new ArgumentNullException("Could not connect with mafiascum. Try again later.");
                }
                HtmlNode div = doc.DocumentNode.SelectSingleNode("//div");
                HtmlNode postbody = div.SelectSingleNode(".//div[contains(@class,'postbody')]");

                string divInnerHtml = div.InnerHtml;
                HtmlNode content = postbody.SelectSingleNode(".//div[contains(@class,'content')]");
                HtmlNodeCollection spoilerTags = content.SelectNodes(".//div[contains(@class,'quotetitle')]");
                HtmlNodeCollection spoilerContentTags = content.SelectNodes(".//div[contains(@class,'quotecontent')]");

                //string displayString = "";
                int i = 0;
                //bool settingsFound = false;
                if (spoilerTags == null)
                {
                    throw new ArgumentNullException("No spoiler tags in given post. Please verify URL and post number.");
                }
                foreach (HtmlNode spoilerNode in spoilerTags)
                {

                    HtmlNode spoilerHeaderTextNode = spoilerNode.SelectSingleNode(".//b");
                    if (spoilerHeaderTextNode != null)
                    {
                        if (spoilerNode.SelectSingleNode(".//b").InnerText.Trim().Equals(VOTE_COUNT_SPOILER_NAME))
                        {

                            break;


                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        i++;
                    }

                }

                if (i < spoilerContentTags.Count)
                {

                    return spoilerContentTags[i].SelectSingleNode(".//div").InnerHtml.Trim();
                }
                else
                {
                    throw new ArgumentNullException("Could not find vote counter settings. Check spoiler tag name.");
                }

            }
            catch (Exception e)
            {
                throw new ArgumentNullException("Could load settings data. Check input.");

            }



        }


        public static List<Post> BuildPosts(string validURL, List<Player> players, List<string> moderatorNames, List<Replacement> replacements)
        {


            HtmlDocument doc = URLReadLogic.URLReadLogic.GetHTMLDocumentFromURL(validURL);


            HtmlNode pagination = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'pagination')]");

            int numberOfPages = int.MinValue;
            try
            {
                var links = pagination.SelectNodes(".//a[@href]");
                var lastLink = links[links.Count - 1];
                numberOfPages = Int32.Parse(lastLink.InnerText.Trim());
            }
            catch
            {
                numberOfPages = 1;
            }
            int urlStart = 0;
            if (!validURL.Contains("&ppp=200"))
            {
                validURL = validURL + "&ppp=200";
            }
            if (validURL.Contains("&start"))
            {
                int indexOfnextAmp = validURL.IndexOf("&", validURL.IndexOf("&start=") + 1);
                if (indexOfnextAmp > -1)
                {
                    indexOfnextAmp += 1;
                    int indexOfStart = validURL.IndexOf("&start=");
                    int startLength = "&start=".Length;
                    int strLength = validURL.Length;
                    int start = indexOfStart + startLength;
                    int stop = indexOfnextAmp - (indexOfStart + startLength + 1);
                    string substring = validURL.Substring(start, stop);
                    urlStart = Int32.Parse(substring);
                }
                else
                {
                    urlStart = Int32.Parse(validURL.Substring(validURL.IndexOf("&start=") + "&start=".Length));
                }
            }
            int postsPerPage = 200;


            return URLReadLogic.URLReadLogic.GetAllPosts(numberOfPages,urlStart,postsPerPage,validURL,moderatorNames,players,replacements);

            // votesOnThread = RunScrubOnADoc(doc, players, replacements, moderatorNames, timings);
            // CoroutineReadAUrl(numberOfPages, urlStart, postsPerPage, validURL, votesOnThread, moderatorNames, players, replacements, timings);



        }


        public static List<Post> RunScrubOnADoc(HtmlDocument doc, List<Player> players, List<Replacement> replacements, List<string> moderatorNames)
        {
            List<Post> postsOnThisPage = new List<Post>();
            try
            {

                HtmlNodeCollection divs = doc.DocumentNode.SelectNodes("//div");
                Regex reg = new Regex("^p[0-9]", RegexOptions.IgnoreCase);


                


                foreach (HtmlNode div in divs)
                {
                    string id = div.GetAttributeValue("id", string.Empty);
                    if (reg.IsMatch(id))
                    {


                        HtmlNode postProfile = div.SelectSingleNode(".//dl[contains(@class,'postprofile')]");
                        string postAuthor = postProfile.SelectSingleNode(".//a").InnerHtml;
                        //displayString += postAuthor;
                        //We don't care about votes the mod has made.

                        bool wasPostByModerator = false;
                        foreach (string moderatorName in moderatorNames)
                        {
                            if (Player.makeNameFriendly(postAuthor).Equals(Player.makeNameFriendly(moderatorName)))
                            {
                                wasPostByModerator = true;
                                break;
                            }
                        }

                        if (wasPostByModerator == true)
                        {
                            continue;
                        }




                        HtmlNode postbody = div.SelectSingleNode(".//div[contains(@class,'postbody')]");
                        string pbInnerText = postbody.InnerText;
                        string pbInnerHtml = postbody.InnerHtml;
                        int postNumber = Int32.Parse(postbody.SelectSingleNode(".//strong").InnerHtml.Replace("#", ""));


                        Player playerVoting = Player.FindPlayerByNameUserAid(players, postAuthor);
                        if (playerVoting == null)
                            Player.FindPlayerByNameUserAidReplacementsLoop(players, postAuthor, replacements);
                        if (playerVoting == null)
                        {
                            playerVoting = PlayerNameWordSearch(players, replacements, Player.makeNameFriendly(postAuthor));
                        }
                        //Required for UTF8 issues. For some reason the encoding/decoding on the byte array fucks everything to hell. Wasn't an issue before.
                        if (playerVoting == null)
                            playerVoting = players.returnClosestPlayerLevensheinDistance(postAuthor);

                       // if (playerVoting != null)
                        //{
                           // playerVoting.addPostNumber(postNumber);
                       // }


                        HtmlNode authorNode = postbody.SelectSingleNode(".//p[contains(@class,'author')]");
                        HtmlNode firstURL = authorNode.SelectSingleNode(".//a");
                        string hrefValue = firstURL.Attributes["href"].Value;
                        string directPostLocation = hrefValue.Substring(hrefValue.IndexOf("#")).Replace("#p", "#");
                        string bbCode = "[url=https://forum.mafiascum.net/viewtopic.php?p=" + directPostLocation.Replace("#", "") + directPostLocation + "]" + postNumber + "[/url]";

                        string dateTextStart = authorNode.InnerText;
                        dateTextStart = dateTextStart.Replace("Post&nbsp;#" + postNumber + "&nbsp;\n\t\t\t\t    \n\t\t\t\t\t(ISO)&nbsp;\n\t\t\t\t    \n\t\t\t      &raquo; ", "").Trim();

                        DateTime timeOfPost;
                        bool isValidDate = DateTime.TryParse(dateTextStart, out timeOfPost);
                        if (!isValidDate)
                            throw new ArgumentException("Bad date provided for post: " + postNumber);

                        Post post = null;
                        if (playerVoting != null)
                        {
                            

                            post = new Post(postNumber, playerVoting, timeOfPost, bbCode);
                        }

                        HtmlNode content = postbody.SelectSingleNode(".//div[contains(@class,'content')]");

                        var quotes = content.SelectNodes(".//blockquote");

                        HtmlNode contentMinusQuotes = content;
                        if (quotes != null)
                        {
                            foreach (var quote in quotes)
                            {
                                try
                                {

                                    quote.Remove();
                                }
                                catch
                                {


                                }
                            }
                        }

                        HtmlNodeCollection spoilerTags = contentMinusQuotes.SelectNodes(".//div[contains(@class,'quotetitle')]");
                        HtmlNodeCollection spoilerContentTags = contentMinusQuotes.SelectNodes(".//div[contains(@class,'quotecontent')]");
                        if (spoilerTags != null)
                        {

                            try
                            {
                                foreach (HtmlNode spoiler in spoilerTags)
                                {
                                    spoiler.Remove();
                                }
                                foreach (HtmlNode spoilerContent in spoilerContentTags)
                                {
                                    spoilerContent.Remove();
                                }
                            }
                            catch
                            {

                            }
                        }


                        try
                        {
                           
                            post.ExtractVoteFromPost(players, contentMinusQuotes, replacements);
                            postsOnThisPage.Add(post);

                        }
                        catch (Exception e)
                        {
                            List<string> playerNames = new List<string>();
                            foreach (Player player in players)
                            {
                                playerNames.Add(player.Name);
                            }

                            UnityEngine.Debug.Log(playerVoting == null ? "Unable to get original player posting. Check for typos in settings." : "Some other error occurred in getting the vote. ");
                            
                        }



                    }
                }

            }
            catch(Exception e2)
            {


                UnityEngine.Debug.Log("Major unknown error occurred. Run me in debug. ");

            }

            return postsOnThisPage;
        }


        public static Vote GetVoteFromContent(Post post, HtmlNode content, List<Player> players, List<Replacement> replacements)
        {
            bool unvoteOccurred = false;
            bool boldVote = false;
            string playerVotedName = ExtractPlayerNameVotedForFromVoteTags(content, out unvoteOccurred);
            bool unvoteOccurredFromVoteTags = unvoteOccurred;
            bool finalUnvoteOccurred = unvoteOccurredFromVoteTags;
            
            if (playerVotedName == null)
            {
                playerVotedName = ExtractPlayerNameVotedForFromBoldTags(content, unvoteOccurredFromVoteTags, out finalUnvoteOccurred);
                if (playerVotedName != null)
                {
                    boldVote = true;
                }
            }

            if (playerVotedName != null && !finalUnvoteOccurred)
            {
                playerVotedName = RemoveBBTags(playerVotedName);
                if (playerVotedName.Length > MAX_USERNAME_LENGTH)
                {
                    return null;
                }
                return BuildVoteFromPostPlayerName(post, playerVotedName, players, replacements, boldVote);
            }
            else if (finalUnvoteOccurred)
            {
                return new Vote(post.PlayerPosting, new Player("UNVOTE: "), post.PostNumber, post.TimeOfPost, boldVote, null);
            }
            else
            {
                return null;
            }

        }

        private static Vote BuildVoteFromPostPlayerName(Post post, string playerName, List<Player> players, List<Replacement> replacements, bool isBoldVote)
        {
            Player playerVoted = GetPlayerFromContent(playerName, players, replacements);
            if (playerVoted == null)
                return null;
            return new Vote(post.PlayerPosting, playerVoted, post.PostNumber, post.TimeOfPost, isBoldVote, null);
            
        }

        private static Player GetPlayerFromContent(string playerName, List<Player> players, List<Replacement> replacements)
        {
            Player playerVoted = null;
           

           
            string playerFriendlyName = Player.makeNameFriendly(playerName);
            if (playerName != null)
            {
                if (playerName == null)
                    playerVoted  = Player.FindPlayerByName(players, playerFriendlyName);

                if (playerVoted == null)
                    playerVoted = IfNoLynchReturnNoLynchPlayer(playerFriendlyName);

                if (playerVoted == null)
                    playerVoted = Player.FindPlayerByNameUserAidReplacementsLoop(players, playerName, replacements);


                if (playerVoted == null)
                    playerVoted = players.checkAbbreviation(playerFriendlyName);

                if (playerVoted == null)
                    playerVoted = Player.FindPlayerByNameUserAidReplacementsLoopStartsWith(players, playerName, replacements);

                if (playerVoted == null)
                    playerVoted = Player.FindPlayerByNameUserAidReplacementsLoopStartsWith6(players, playerName, replacements);

                if (playerVoted == null)
                    playerVoted = PlayerNameWordSearch(players, replacements, playerFriendlyName);

                if (playerVoted == null)
                    playerVoted = players.returnClosestPlayerLevensheinDistance(playerName);
            }
            return playerVoted;
        }

       

        private static Player PlayerNameWordSearch(List<Player> players, List<Replacement> replacements, string friendlyPlayerName)
        {
            List<Player> playersWithWord = new List<Player>();
            bool wordInName = false;
            foreach (Player player in players)
            {
                if (player.WordsInName != null)
                {
                    if (!wordInName)
                    {
                        foreach (string word in player.WordsInName)
                        {
                            if (!playersWithWord.Contains(player) && Player.makeNameFriendly(player.Name).ContainsIgnoreCase(friendlyPlayerName) && friendlyPlayerName.ContainsIgnoreCase(word))
                            {
                                playersWithWord.Add(player);

                            }
                            else if (!playersWithWord.Contains(player))
                            {
                                foreach (Replacement replacement in replacements)
                                {
                                    string newPlayerName = replacement.OldPlayerName;
                                    foreach (Replacement rep2 in replacements)
                                    {
                                        newPlayerName = rep2.performReplacement(newPlayerName, players);
                                    }


                                    if (!playersWithWord.Contains(player) && Player.makeNameFriendly(newPlayerName).ContainsIgnoreCase(Player.makeNameFriendly(player.Name)) && replacement.OldPlayerName.ContainsIgnoreCase(word) && friendlyPlayerName.ContainsIgnoreCase(word))
                                    {
                                        playersWithWord.Add(player);
                                        break;

                                    }

                                }

                            }


                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if (playersWithWord.Count == 1)
            {
                return playersWithWord[0];
                
            }

            return null;
        }

        private static Player IfNoLynchReturnNoLynchPlayer(string playerFriendlyName)
        {
            Player playerVoted = null;
            string wagonNoLynch = Player.makeNameFriendly(Wagon.NO_LYNCH);
            if (playerFriendlyName.Equals(wagonNoLynch))
                playerVoted = Wagon.NO_LYNCH_PLAYER;

            return playerVoted;
        }

        private static string RemoveBBTags(string playerName)
        {
            if (playerName != null && playerName.Contains("]") && (playerName.Contains("[")))
            {
                List<int> foundRightIndexes = new List<int>();
                List<int> foundLeftIndexes = new List<int>();
                for (int i = playerName.IndexOf(']'); i < -1; i = playerName.IndexOf(']', i + 1))
                {
                    foundRightIndexes.Add(i);
                }
                for (int i = playerName.IndexOf('['); i < -1; i = playerName.IndexOf('[', i + 1))
                {
                    foundLeftIndexes.Add(i);
                }
                if (foundLeftIndexes.Count != foundRightIndexes.Count)
                {
                    //playername is bad
                    playerName = null;
                }
                else
                {
                    for (int i = 0; i < foundLeftIndexes.Count; i++)
                    {
                        int openTag = foundLeftIndexes[i];
                        int closeTag = foundRightIndexes[i];
                        if (closeTag < openTag)
                        {

                            playerName = null;
                        }
                        playerName = playerName.Substring(0, openTag) + playerName.Substring(closeTag + 1);
                    }
                }
            }

            return playerName;
        }

        private static string ExtractPlayerNameVotedForFromVoteTags(HtmlNode content, out bool unvoteOccurred)
        {
            var votes = content.SelectNodes(".//span[contains(@class,'bbvote')]");

            unvoteOccurred = false;
            string playerName = null;
            HtmlNode lastVote = null;
            string unvoteName = "";
            if (votes != null)
            {
                lastVote = votes[votes.Count - 1];
                int voteIndex = 2;
                unvoteOccurred = false;

                while (lastVote.InnerHtml.Contains("UNVOTE"))
                {
                    unvoteOccurred = true;
                    unvoteName = lastVote.InnerHtml;
                    if (votes.Count >= voteIndex)
                    {
                        lastVote = votes[votes.Count - voteIndex];
                        voteIndex++;
                    }
                    else
                    {
                        lastVote = null;
                        break;
                    }
                }
                if ((lastVote == null) && (unvoteOccurred))
                {
                    playerName = unvoteName;
                }
            }
            else
                lastVote = null;


            if (lastVote != null)
            {
                playerName = null;
                playerName = lastVote.InnerHtml.Replace("VOTE: ", "");
                playerName = playerName.Replace("<br>", "");
                playerName = playerName.Replace("<br >", "");
            }

            return playerName;
        }

        private static string ExtractPlayerNameVotedForFromBoldTags(HtmlNode content, bool unvoteOccurredFromVoteTags, out bool unvoteOccurred)
        {
            
            string playerName = null;
            unvoteOccurred = false;
            string unvoteName = null;
            if (!unvoteOccurredFromVoteTags)
            {
               HtmlNode lastBoldVote;

                var boldTagTexts = content.SelectNodes(".//span[contains(@class,'noboldsig')]");
                if (boldTagTexts != null)
                {


                    lastBoldVote = boldTagTexts[boldTagTexts.Count - 1];
                    int bvi = boldTagTexts.Count - 1;
                    int boldVoteIndex = 2;
                    unvoteOccurred = false;

                    do
                    {
                        if (lastBoldVote.InnerHtml.ContainsIgnoreCase("vote"))
                        {
                            break;
                        }
                        else
                        {
                            bvi = bvi - 1;
                            if (bvi > -1)
                            {
                                lastBoldVote = boldTagTexts[bvi];
                            }
                            else
                            {
                                lastBoldVote = null;
                            }
                        }
                    }
                    while (lastBoldVote != null && !lastBoldVote.InnerHtml.ContainsIgnoreCase("vote"));

                    if (lastBoldVote != null)
                    {
                        string[] splitBySemiColon = lastBoldVote.InnerHtml.Split(';');
                        lastBoldVote.InnerHtml = splitBySemiColon[splitBySemiColon.Length - 1];
                        if (lastBoldVote.InnerHtml.IndexOf("<br>") > -1)
                        {
                            lastBoldVote.InnerHtml = lastBoldVote.InnerHtml.Substring(lastBoldVote.InnerHtml.IndexOf("<br>") + "<br>".Length);
                        }
                        if (lastBoldVote.InnerHtml.IndexOf("<br/>") > -1)
                        {
                            lastBoldVote.InnerHtml = lastBoldVote.InnerHtml.Substring(lastBoldVote.InnerHtml.IndexOf("<br/>") + "<br/>".Length);
                        }
                        if (lastBoldVote.InnerHtml.IndexOf("<br >") > -1)
                        {
                            lastBoldVote.InnerHtml = lastBoldVote.InnerHtml.Substring(lastBoldVote.InnerHtml.IndexOf("<br >") + "<br >".Length);
                        }
                        if (lastBoldVote.InnerHtml.IndexOf("<br />") > -1)
                        {
                            lastBoldVote.InnerHtml = lastBoldVote.InnerHtml.Substring(lastBoldVote.InnerHtml.IndexOf("<br />") + "<br />".Length);
                        }
                        if (lastBoldVote.InnerHtml.IndexOf(",") > -1)
                        {
                            lastBoldVote.InnerHtml = lastBoldVote.InnerHtml.Substring(lastBoldVote.InnerHtml.IndexOf(",") + ",".Length);
                        }


                        while (lastBoldVote.InnerHtml.ContainsIgnoreCase("UNVOTE"))
                        {
                            unvoteOccurred = true;
                            unvoteName = lastBoldVote.InnerText;
                            if (boldTagTexts.Count >= boldVoteIndex)
                            {




                                boldVoteIndex++;
                            }
                            else
                            {
                                lastBoldVote = null;
                                break;
                            }
                        }
                    }
                    if ((lastBoldVote == null) && (unvoteOccurred))
                    {
                        playerName = unvoteName;
                    }
                }
                else
                    lastBoldVote = null;

                if ((lastBoldVote != null) && (lastBoldVote.InnerText.Trim().ContainsIgnoreCase("vote") == true) && ((lastBoldVote.InnerText.Trim().Substring(0, 4).ContainsIgnoreCase("vote")) || (lastBoldVote.InnerText.Trim().Substring(0, 6).ContainsIgnoreCase("unvote"))) && (!lastBoldVote.InnerHtml.Contains("<")))
                {
                    playerName = null;
                    if (lastBoldVote.InnerText.Trim().ContainsIgnoreCase("Unvote"))
                    {
                        playerName = null;
                        unvoteOccurred = true;                      
                    }
                    else
                    {
                        playerName = lastBoldVote.InnerText.Replace("VOTE: ", "");
                        playerName = playerName.Replace("VOTE ", "");
                        playerName = playerName.Replace("vote: ", "");
                        playerName = playerName.Replace("vote ", "");
                        playerName = playerName.Replace("Vote: ", "");
                        playerName = playerName.Replace("Vote ", "");

                        playerName = playerName.Replace("VOTE:", "");
                        playerName = playerName.Replace("vote:", "");
                        playerName = playerName.Replace("Vote:", "");


                        playerName = playerName.Replace("<br>", "");
                        playerName = playerName.Replace("<br >", "");
                        playerName = playerName.Trim();

                    }

                }

               
            }

            return playerName;
        }

        
    }


    
}

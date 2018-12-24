using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SupportClasses;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using Unity.Collections;

namespace Assets.Scripts.Support_Scripts
{
    public static class ExtensionMethods
    {
        #region My logic extension methods

        //This is required because content size fitter doesn't work with beta unity if inside panel groupings.
        public static void SetInputFieldTextHackyResize(InputField inputText, string newText, bool displayVisually)
        {

            


            //inputText.text = newText;
            if (displayVisually)
            {
                inputText.text = newText;
            }            
            else
            {
                string fileNameBeforePrefix = "resultsData";
                string extension = ".txt";

                fileNameBeforePrefix = FileWriting.WriteString(fileNameBeforePrefix, extension, newText);
                SetInputFieldTextHackyResize(inputText, "The text was too long for Unity to display. Open filename " + Application.dataPath + "/" + fileNameBeforePrefix + extension, true);
                return;
            }
          

            
            
            



            //inputText.text = newText;

            Canvas.ForceUpdateCanvases();
            Text[] texts = inputText.GetComponentsInChildren<Text>();
            if (texts != null)
            {
                foreach (Text text in texts)
                {
                    if (text != null)
                    {
                        //Not sure why this all of a sudden gives wrong line counts when addition of new panel. +10 for buffer.
                        int lineCount = text.cachedTextGenerator.lineCount + 10;

                        RectTransform t = text.rectTransform;
                        Vector2 dimension = t.sizeDelta;
                        dimension.y = (16 * lineCount) + 15;
                        t.sizeDelta = dimension;
                    }
                }
            }
        }

        public static Player GetPlayer(this List<Player> players, Player aPlayer)
        {
            foreach (Player player in players)
            {
                if (aPlayer.Name.Equals(player))
                    return player;
            }
            return null;
        }
        public static int numAlive(this List<Player> players)
        {
            int playersAlive = 0;
            foreach (Player player in players)
            {
                if ((player.IsAlive == true) && (!player.IsDead))
                {
                    playersAlive++;
                }
            }

            return playersAlive;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            return Contains(source, toCheck, StringComparison.OrdinalIgnoreCase);
        }
        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        public static byte[] ObjectToBytes<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);

                return stream.ToArray();
            }
        }

        private static T ObjectFromBytes<T>(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }



        private static T ObjectFromBytesRemoveNulls<T>(byte[] bytes)
        {

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }


        public static T BytesToObject<T>(this byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {


                var binForm = new BinaryFormatter();
                stream.Write(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(stream);
                return (T)obj;
            }
        }






        public static T ObjectFromNativeArrayBytes<T>(this NativeArray<byte> bytes)
        {
            return ObjectFromBytes<T>(bytes.ToArray());
        }

        public static T ObjectFromNativeArrayBytesRemoveNulls<T>(this NativeArray<byte> bytes)
        {
            return ObjectFromBytesRemoveNulls<T>(bytes.ToArray());
        }



        public static Player checkAbbreviation(this List<Player> players, string abbreviationToFind)
        {
            List<Player> playersWithAbbreviation = new List<Player>();

            foreach (Player player in players)
            {
                if (player.Abbreviations != null)
                {
                    foreach (string abbreviation in player.Abbreviations)
                    {
                        if (!playersWithAbbreviation.Contains(player) && abbreviation.ToLower().Equals(abbreviationToFind.ToLower()))
                        {
                            playersWithAbbreviation.Add(player);
                        }
                    }
                }
            }

            if (playersWithAbbreviation.Count != 1)
            {
                return null;
            }
            else
            {
                return playersWithAbbreviation[0];
            }
        }

        public static Player returnClosestPlayerLevensheinDistance(this List<Player> players, string nameToFind)
        {
            string closestPlayerName = returnClosestPlayerNameLevensheinDistance(players, nameToFind);
            if (closestPlayerName != Wagon.NO_LYNCH)
            {
                return Player.FindPlayerByName(players, closestPlayerName);
            }
            else
            {
                return Wagon.NO_LYNCH_PLAYER;
            }
        }

        public static string returnClosestPlayerNameLevensheinDistance(this List<Player> players, string nameToFind)
        {
            List<string> playerNames = new List<string>();
            foreach (Player player in players)
            {
                playerNames.Add(player.Name);
            }
            playerNames.Add(Wagon.NO_LYNCH);

            return playerNames.returnClosestStringLevensheinDistance(nameToFind);
        }

        public static string returnClosestStringLevensheinDistance(this List<string> stringList, string stringToFind)
        {
            int indexOfString = returnIndexOfClosestString(stringList, stringToFind);
            if (indexOfString > -1)
                return stringList[indexOfString];
            else
                return null;

        }

        private static int returnIndexOfClosestString(this List<string> stringList, string stringToFind)
        {
            int[] distanceInts = levenshteinDistanceInts(stringList, stringToFind);

            int min = distanceInts.Min();

            int countOfMin = 0;
            int indexToReturn = -314;
            for (int i = 0; i < distanceInts.Count(); i++)
            {
                if (distanceInts[i] == min)
                {
                    countOfMin++;
                    indexToReturn = i;
                }
            }



            if (countOfMin == 1)
                return indexToReturn;
            else
                return -314159;


        }

        private static int[] levenshteinDistanceInts(this List<string> stringList, string stringToFind)
        {
            int[] distanceInts = new int[stringList.Count];

            for (int i = 0; i < stringList.Count; i++)
            {
                distanceInts[i] = stringList[i].LevenshteinDistance(stringToFind);

            }

            return distanceInts;
        }


        public static int LevenshteinDistance(this string aString, string stringToCompareTo)
        {

            if (aString == null && stringToCompareTo == null)
            {
                return 0;
            }
            else if (aString == null)
            {
                return stringToCompareTo.Length;
            }
            else if (stringToCompareTo == null)
            {
                return aString.Length;
            }
            else
            {
                int n = aString.Length;
                int m = stringToCompareTo.Length;
                int[,] d = new int[n + 1, m + 1];


                // Step 1
                if (n == 0)
                {
                    return m;
                }

                if (m == 0)
                {
                    return n;
                }

                // Step 2 -- Create zero distance arrays. 
                for (int i = 0; i <= n; d[i, 0] = i++)
                {
                }

                for (int j = 0; j <= m; d[0, j] = j++)
                {
                }

                // Step 3 do the looping to find the distance. 
                for (int i = 1; i <= n; i++)
                {
                    //Step 4
                    for (int j = 1; j <= m; j++)
                    {
                        // Step 5
                        int cost = (stringToCompareTo[j - 1] == aString[i - 1]) ? 0 : 1;

                        // Step 6
                        d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                            d[i - 1, j - 1] + cost);
                    }
                }
                // Step 7
                return d[n, m];
            }
        }
        #endregion

    }




}

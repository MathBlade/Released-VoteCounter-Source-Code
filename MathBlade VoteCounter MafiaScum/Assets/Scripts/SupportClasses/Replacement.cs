using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class Replacement
    {

        string oldPlayerName;
        string newPlayerName;
        string[] oldPlayerHydraNames;
        string[] newPlayerHydraNames;


        public Replacement(string _oldPlayerName, string _newPlayerName)
        {

            if (_oldPlayerName.Contains("{"))
            {
                int indexOfLeftCurly = _oldPlayerName.IndexOf("{");
                int indexOfRightCurly = _oldPlayerName.IndexOf("}");
                string hydranameString = _oldPlayerName.Substring(0, indexOfLeftCurly);
                string hydrapartnersString = _oldPlayerName.Substring(indexOfLeftCurly + 1, indexOfRightCurly - indexOfLeftCurly - 1);
                oldPlayerName = hydranameString.Trim();
                oldPlayerHydraNames = hydrapartnersString.Split('+');

            }
            else
            {
                oldPlayerName = _oldPlayerName;
            }

            if (_newPlayerName.Contains("{"))
            {
                int indexOfLeftCurly = _newPlayerName.IndexOf("{");
                int indexOfRightCurly = _newPlayerName.IndexOf("}");
                string hydranameString = _newPlayerName.Substring(0, indexOfLeftCurly);
                string hydrapartnersString = _newPlayerName.Substring(indexOfLeftCurly + 1, indexOfRightCurly - indexOfLeftCurly - 1);
                newPlayerName = hydranameString.Trim();
                newPlayerHydraNames = hydrapartnersString.Split('+');

            }
            else
            {
                newPlayerName = _newPlayerName;
            }

        }

        public string OldPlayerName { get { return oldPlayerName; } }

        public string NewPlayerName { get { return newPlayerName; } }

        public string[] OldPlayerHydraNames { get { return oldPlayerHydraNames; } }

        public string[] NewPlayerHydraNames { get { return newPlayerHydraNames; } }

        public string performReplacement(string textToPerformReplacement, List<Player> players)
        {
            foreach (Player player in players)
            {
                if (player.Name.Equals(oldPlayerName))
                {
                    player.Name = newPlayerName;

                    //Handle abbreviations
                    string abbreviation;
                    if (newPlayerName != null && newPlayerName.Contains(" "))
                    {
                        abbreviation = Player.buildAbbreviationFromName(newPlayerName, Convert.ToChar(" "));
                    }
                    else if (newPlayerName != null && newPlayerName.Trim().Contains("_"))
                    {
                        abbreviation = Player.buildAbbreviationFromName(newPlayerName, Convert.ToChar("_"));
                    }
                    else
                    {
                        abbreviation = Player.checkCaps(newPlayerName);
                    }
                    if (abbreviation != null)
                    {
                        if (player.Abbreviations == null)
                        {
                            player.Abbreviations = new List<string>();
                        }
                        player.Abbreviations.Add(abbreviation);
                    }



                    //Hydra names

                    if (NewPlayerHydraNames != null && NewPlayerHydraNames.Length > 0)
                    {
                        if (player.HydraPartners == null)
                        {
                            player.HydraPartners = NewPlayerHydraNames;
                        }
                        else
                        {


                            //Handle hydra names
                            List<string> alreadyExistingPartnerNames = new List<string>();
                            alreadyExistingPartnerNames.AddRange(player.HydraPartners);

                            List<string> finalNamesList = new List<string>();

                            bool existsAlready = false;
                            foreach (string hydraName in NewPlayerHydraNames)
                            {
                                existsAlready = false;
                                foreach (string oldName in alreadyExistingPartnerNames)
                                {
                                    if (Player.makeNameFriendly(oldName).Equals(Player.makeNameFriendly(hydraName)))
                                    {
                                        existsAlready = true;
                                    }
                                }

                                if (!existsAlready)
                                {
                                    finalNamesList.Add(hydraName);
                                }
                            }

                            finalNamesList.AddRange(alreadyExistingPartnerNames);

                            player.HydraPartners = finalNamesList.ToArray();


                            //Handle replacements

                            List<String> wordsInReplacement = Dictionary.AllWordsInString(newPlayerName);
                            existsAlready = false;
                            foreach (string hydraName in NewPlayerHydraNames)
                            {
                                foreach (string oldName in alreadyExistingPartnerNames)
                                {
                                    if (Player.makeNameFriendly(oldName).Equals(Player.makeNameFriendly(hydraName)))
                                    {
                                        existsAlready = true;
                                    }
                                }

                                if (!existsAlready)
                                {
                                    if (player.WordsInName == null)
                                    {
                                        player.WordsInName = new List<string>();
                                    }
                                    List<string> hydraPartnerWords = Dictionary.AllWordsInString(hydraName);
                                    foreach (string aWord in hydraPartnerWords)
                                    {
                                        if (!player.WordsInName.Contains(aWord))
                                        {
                                            player.WordsInName.Add(aWord);
                                        }
                                    }

                                    if (newPlayerName.Contains(" "))
                                    {
                                        string[] potentialNonDictionaryWords = newPlayerName.Split(Convert.ToChar(" "));
                                        foreach (string potentialWord in potentialNonDictionaryWords)
                                        {
                                            if (!player.WordsInName.Contains(potentialWord))
                                            {
                                                player.WordsInName.Add(potentialWord);
                                            }
                                        }
                                    }

                                    if (newPlayerName.Contains("_"))
                                    {
                                        string[] potentialNonDictionaryWords = newPlayerName.Split(Convert.ToChar("_"));
                                        foreach (string potentialWord in potentialNonDictionaryWords)
                                        {
                                            if (!player.WordsInName.Contains(potentialWord))
                                            {
                                                player.WordsInName.Add(potentialWord);
                                            }
                                        }
                                    }
                                }

                                //Abbreviation based on words
                                player.AddAbbreviationByWords(newPlayerName, player.WordsInName);

                            }




                        }
                    }
                }




            }


            return textToPerformReplacement.Replace(oldPlayerName, newPlayerName);
        }

        public static Replacement GoBackwards(List<Player> players, List<Replacement> replacements, string newestPlayer)
        {
            return GoBackwards(players, replacements, newestPlayer, null);
        }

        private static Replacement GoBackwards(List<Player> players, List<Replacement> replacements, string newestPlayer, Replacement lastValue)
        {
            foreach (Replacement replacement in replacements)
            {
                if (replacement.NewPlayerName.Equals(newestPlayer))
                {
                    return GoBackwards(players, replacements, replacement.OldPlayerName, replacement);
                }
            }

            return lastValue;

        }


    }
}

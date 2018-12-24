using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class Player
    {

        string name;
        bool isAlive;
        Player playerCurrentlyVoting;
        List<string> abbreviations;
        string[] hydraPartners;
        bool isLoved = false;
        bool isHated = false;
        bool isTreestumped = false;
        bool isGunner = false;
        List<String> wordsInName;
        int postNumberOfVote;        
        DateTime timeOfLastPost;


        private const string LovedIndicator = "l";
        private const string HatedIndicator = "h";
        private const string TreestumpedIndicator = "t";
        private const string GunnerIndicator = "g";

        public Player(string _name)
        {
            name = _name.Trim();
            PostNumbers = new List<int>();
            timeOfLastPost = new DateTime(1986, 3, 14); //Before anyone could be posting.
            string remainingName = name;
            if (name.Contains("{"))
            {
                int indexOfLeftCurly = name.IndexOf("{");
                int indexOfRightCurly = name.IndexOf("}");
                //string hydranameString = name.Substring (0, indexOfLeftCurly);

                string hydrapartnersString = name.Substring(indexOfLeftCurly + 1, indexOfRightCurly - indexOfLeftCurly - 1);
                //name = hydranameString.Trim ();
                hydraPartners = hydrapartnersString.Split('+');

                remainingName = name.Replace("{" + hydrapartnersString + "}", "");

            }

            if (remainingName.Contains("/"))
            {
                int indexOfFirstSlash = name.IndexOf("/");
                int indexOfSecondSlash = name.LastIndexOf("/");
                if (indexOfSecondSlash == -1)
                {
                    System.Console.WriteLine("Player name not formatted correctly. Received: " + _name);
                    throw new Exception("Player name not formatted correctly. Received: " + _name);
                }
                else
                {
                    string playerIndicator = name.Substring(indexOfFirstSlash + 1, indexOfSecondSlash - indexOfFirstSlash - 1);
                    if (playerIndicator.Length != 1)
                    {
                        System.Console.WriteLine("Player name not formatted correctly. Received: " + _name);
                        throw new Exception("Player name not formatted correctly. Received: " + _name);
                    }


                    remainingName = remainingName.Replace("/" + playerIndicator + "/", "");

                    switch (playerIndicator.ToLower())
                    {

                        case (LovedIndicator):
                            System.Console.WriteLine("Player " + remainingName + " is loved.");
                            isLoved = true;
                            break;
                        case (HatedIndicator):
                            System.Console.WriteLine("Player " + remainingName + " is hated.");
                            isHated = true;
                            break;
                        case (TreestumpedIndicator):
                            System.Console.WriteLine("Player " + remainingName + " is treestumped");
                            isTreestumped = true;
                            break;
                        case (GunnerIndicator):
                            System.Console.WriteLine("Player " + remainingName + " is a gunner.");
                            isGunner = true;
                            break;
                        default:
                            System.Console.WriteLine("Player name not formatted correctly. Received: " + _name);
                            throw new Exception("Player name not formatted correctly. Received: " + _name);

                    }


                }
            }


            name = remainingName;

            isAlive = true;
            playerCurrentlyVoting = null;
            string abbreviation;
            if (name != null && name.Contains(" "))
            {
                abbreviation = buildAbbreviationFromName(name, Convert.ToChar(" "));
            }
            else if (name != null && name.Trim().Contains("_"))
            {
                abbreviation = buildAbbreviationFromName(name, Convert.ToChar("_"));
            }
            else
            {
                abbreviation = checkCaps(name);
            }

            if (abbreviation != null)
            {
                abbreviations = new List<string>();
                abbreviations.Add(abbreviation);
            }

            wordsInName = Dictionary.AllWordsInString(name);
            if (hydraPartners != null)
            {
                foreach (string hydraPartnerName in hydraPartners)
                {
                    List<string> hydraPartnerWords = Dictionary.AllWordsInString(hydraPartnerName);
                    foreach (string aWord in hydraPartnerWords)
                    {
                        if (!wordsInName.Contains(aWord))
                        {
                            wordsInName.Add(aWord);
                        }
                    }
                }
            }

            if (name.Contains(" "))
            {
                string[] potentialNonDictionaryWords = name.Split(Convert.ToChar(" "));
                foreach (string potentialWord in potentialNonDictionaryWords)
                {
                    if (!wordsInName.Contains(potentialWord))
                    {
                        wordsInName.Add(potentialWord);
                    }
                }
            }

            if (name.Contains("_"))
            {
                string[] potentialNonDictionaryWords = name.Split(Convert.ToChar("_"));
                foreach (string potentialWord in potentialNonDictionaryWords)
                {
                    if (!wordsInName.Contains(potentialWord))
                    {
                        wordsInName.Add(potentialWord);
                    }
                }
            }


            this.AddAbbreviationByWords(name, wordsInName);
            postNumberOfVote = -1;



        }

        public void addPostNumber(int postNumber)
        {

            if (!PostNumbers.Contains(postNumber))
            {
                PostNumbers.Add(postNumber);
            }
        }

        public void AddAbbreviationByWords(string name, List<string> words)
        {
            string newAbbreviation = AbbreviationByWords(name, words);
            if (newAbbreviation != null)
            {
                if (this.Abbreviations == null)
                {
                    this.Abbreviations = new List<string>();
                }
                if (!this.Abbreviations.Contains(newAbbreviation))
                {
                    this.Abbreviations.Add(newAbbreviation);
                }
            }
        }

        private static string AbbreviationByWords(string name, List<string> words)
        {
            string nameToCheck = (name + "").ToLower();
            List<string> nameBrokenUpByWords = new List<string>();
            bool allNames = true;
            while (nameToCheck.Length > 0)
            {
                char[] charArray = nameToCheck.ToCharArray();
                char firstChar = charArray[0];
                string firstWord = null;

                foreach (string word in words)
                {
                    if (makeNameFriendly(nameToCheck).StartsWith(word))
                    {
                        if (firstWord == null)
                        {
                            firstWord = word;
                        }
                        else
                        {
                            if (word.Length > firstWord.Length)
                            {
                                firstWord = word;
                            }
                        }
                    }
                }
                if (firstWord != null && firstWord.Length > 0)
                {
                    nameBrokenUpByWords.Add(firstWord);
                    nameToCheck = nameToCheck.Replace(firstWord, "");

                }
                else
                {
                    allNames = false;
                    nameToCheck = "";

                }
            }

            if (allNames == true && nameBrokenUpByWords != null && nameBrokenUpByWords.Count > 1)
            {
                string additionalAbbreviation = "";
                foreach (string word in nameBrokenUpByWords)
                {
                    char[] charArray = word.ToCharArray();
                    char firstChar = charArray[0];
                    additionalAbbreviation += firstChar;
                }

                return additionalAbbreviation;

            }
            else
            {
                return null;
            }
        }

        public List<String> WordsInName { get { return wordsInName; } set { wordsInName = value; } }

        public bool IsLoved { get { return isLoved; } }
        public bool IsHated { get { return isHated; } }
        public bool IsTreestumped { get { return isTreestumped; } }
        public bool IsGunner { get { return isGunner; } }

        public string[] HydraPartners { get { return hydraPartners; } set { hydraPartners = value; } }

        public static string checkCaps(string name)
        {
            string trimmedString = name.Trim();
            int firstLetterIndex = -1;
            List<char> abbreviationChars = new List<char>();

            for (int i = 0; i < trimmedString.Length; i++)
            {
                if (Char.IsLetter(trimmedString[i]))
                {
                    firstLetterIndex = i;
                    abbreviationChars.Add(trimmedString[i]);

                    break;
                }
                else if (Char.IsDigit(trimmedString[i]))
                {
                    abbreviationChars.Add(trimmedString[i]);
                }
            }

            for (int j = firstLetterIndex + 1; j < trimmedString.Length; j++)
            {
                char charAtIndex = trimmedString[j];
                if (Char.IsLetter(charAtIndex) && Char.IsUpper(charAtIndex))
                {
                    if (Char.IsLetter(trimmedString[j - 1]) && Char.IsLower(trimmedString[j - 1]))
                    {
                        abbreviationChars.Add(charAtIndex);
                    }


                }
                else if (Char.IsDigit(charAtIndex))
                {
                    abbreviationChars.Add(charAtIndex);
                }
            }

            string abbreviationString = "";
            char[] abbreviationCharArray = abbreviationChars.ToArray();
            foreach (char aChar in abbreviationCharArray)
            {
                abbreviationString += aChar;
            }
            if (abbreviationString.Length < 2)
            {
                return null;
            }
            return abbreviationString;

        }

        public static string buildAbbreviationFromName(string name, char divider)
        {
            List<int> indexes = new List<int>();
            indexes.Add(0);
            string nameTrimmed = name.Trim();
            int index = nameTrimmed.IndexOf(divider);
            while (index >= 0)
            {
                if (name.Length > (index + 1))
                {
                    indexes.Add(index + 1);
                }
                index = nameTrimmed.IndexOf(divider, index + 1);
            }

            char[] nameTrimmedCharArray = nameTrimmed.ToCharArray();
            string abbreviation = "";
            foreach (int anIndex in indexes)
            {
                abbreviation += nameTrimmedCharArray[anIndex];
            }

            return abbreviation;
        }



        public List<string> Abbreviations
        {
            get
            {
                return abbreviations;
            }
            set
            {
                abbreviations = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public static Player FindPlayerByName(List<Player> players, string playerName)
        {
            if (playerName == null)
            {
                return null;
            }
            foreach (Player player in players)
            {
                if (player.Name.Equals(playerName))
                {
                    return player;
                }
                if (player.HydraPartners != null)
                {
                    foreach (string hydraPartnerName in player.HydraPartners)
                    {
                        if (makeNameFriendly(hydraPartnerName).Equals(makeNameFriendly(playerName)))
                        {
                            return player;
                        }
                    }
                }
            }

            return null;
        }

        public static Player FindPlayerByNameUserAid(List<Player> players, string playerName)
        {
            string userFriendlyName = makeNameFriendly(playerName);
            foreach (Player player in players)
            {
                if (makeNameFriendly(player.Name).Equals(userFriendlyName))
                {
                    return player;
                }
                if (player.HydraPartners != null)
                {
                    foreach (string hydraPartnerName in player.HydraPartners)
                    {
                        if (makeNameFriendly(hydraPartnerName).Equals(makeNameFriendly(playerName)))
                        {
                            return player;
                        }
                    }
                }
            }

            return null;
        }

        public static Player FindPlayerByNameUserAidReplacementsLoop(List<Player> players, string playerName, List<Replacement> replacements)
        {
            string playerNameToFind = playerName;
            foreach (Replacement replacement in replacements)
            {

                playerNameToFind = replacement.performReplacement(playerNameToFind, players);
            }

            string currentPlayerName = "";
            foreach (Player player in players)
            {

                if (makeNameFriendly(playerNameToFind).Equals(makeNameFriendly(player.Name)))
                {
                    return player;
                }

                if (player.HydraPartners != null)
                {
                    foreach (string hydraPartnerName in player.HydraPartners)
                    {
                        if ((makeNameFriendly(player.Name).Equals(makeNameFriendly(hydraPartnerName)) || (makeNameFriendly(playerNameToFind).Equals(makeNameFriendly(hydraPartnerName)))))
                        {
                            return player;
                        }
                    }
                }

                foreach (Replacement replacement in replacements)
                {

                    currentPlayerName = replacement.performReplacement(player.Name, players);
                    if (makeNameFriendly(currentPlayerName).Equals(makeNameFriendly(playerNameToFind)))
                    {
                        return player;
                    }

                    if (replacement.OldPlayerHydraNames != null)
                    {
                        foreach (string hydraPartnerName in replacement.OldPlayerHydraNames)
                        {
                            if (makeNameFriendly(player.Name).Equals(makeNameFriendly(hydraPartnerName)))
                            {
                                return player;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public static Player FindPlayerByNameUserAidReplacementsLoopStartsWith6(List<Player> players, string playerName, List<Replacement> replacements)
        {
            if (playerName.Length >= 6)
            {
                return FindPlayerByNameUserAidReplacementsLoopStartsWith(players, playerName.Substring(0, 6), replacements);
            }
            else
            {
                return null;
            }
        }

        public static Player FindPlayerByNameUserAidReplacementsLoopStartsWith(List<Player> players, string playerName, List<Replacement> replacements)
        {

            string playerNameToFind = playerName;
            foreach (Replacement replacement in replacements)
            {

                playerNameToFind = replacement.performReplacement(playerNameToFind, players);
            }

            string currentPlayerName = "";
            foreach (Player player in players)
            {

                if (makeNameFriendly(player.Name).StartsWith(makeNameFriendly(playerNameToFind)))
                {
                    return player;
                }
                foreach (Replacement replacement in replacements)
                {

                    string correspondingPlayer = replacement.OldPlayerName;
                    foreach (Replacement rep2 in replacements)
                    {
                        correspondingPlayer = rep2.performReplacement(correspondingPlayer, players);
                    }

                    if (!correspondingPlayer.Equals(player.Name))
                    {
                        break;
                    }

                    if (makeNameFriendly(replacement.OldPlayerName).StartsWith(makeNameFriendly(playerNameToFind)) && playerNameToFind.Length > 2)
                    {
                        return player;
                    }

                    currentPlayerName = replacement.performReplacement(player.Name, players);
                    if (makeNameFriendly(currentPlayerName).StartsWith(makeNameFriendly(playerNameToFind)))
                    {
                        return player;
                    }

                    if (replacement.OldPlayerHydraNames != null)
                    {
                        foreach (string hydraPartnerName in replacement.OldPlayerHydraNames)
                        {
                            if (makeNameFriendly(player.Name).StartsWith(makeNameFriendly(hydraPartnerName)))
                            {
                                return player;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public static string makeNameFriendly(string name)
        {
            string nameLowerCase = name.ToLower();
            string lowerCaseAndNoSpaces = nameLowerCase.Replace(" ", "");
            string lowerCaseNoSpacesNoUnderscores = lowerCaseAndNoSpaces.Replace("_", "");
            string lowerCaseNoDashes = lowerCaseNoSpacesNoUnderscores.Replace("-", "");

            return lowerCaseNoDashes;
        }



        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
            set
            {
                isAlive = value;
            }
        }

        public int PostNumberOfVote
        {
            get
            {
                return postNumberOfVote;
            }
            set
            {
                postNumberOfVote = value;
            }
        }


        public bool IsDead
        {
            get
            {
                return !isAlive;
            }
            set
            {
                isAlive = (!(value));
            }
        }

        public Player PlayerCurrentlyVoting
        {
            get
            {
                return playerCurrentlyVoting;
            }
            set
            {
                playerCurrentlyVoting = value;
            }
        }

        public List<int> getPostNumbersLaterThatPost(int firstPostOfDay, int lastPostOfDay)
        {
            List<int> postNumbersSincePost = new List<int>();
            foreach (int post in PostNumbers)
            {
                if (post >= firstPostOfDay)
                {
                    postNumbersSincePost.Add(post);
                }
            }


            return postNumbersSincePost;
        }

        public int getNumberOfPostsInDay(int firstPostOfDay, int lastPostOfDay)
        {
            return getPostNumbersLaterThatPost(firstPostOfDay, lastPostOfDay).Count;
        }

        public DateTime TimeOfLastPost { get { return timeOfLastPost; } set { timeOfLastPost = value; } }

        public List<int> PostNumbers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SupportClasses;
using UnityEngine;

namespace Assets.Scripts.Support_Scripts
{
    public static class VoteCountMainWorkClass
    {
        //Constants formatting
        public static char COMMA = ',';
        public static char COLON = ':';

        public static string GetCurrentVoteCount(bool isRestCall, string url, string playerList, string replacementList, string moderatorNames, string dayStartList, string deadListText, string dayviggedText, string priorVCNumberInput, string colorCode, string optionalFlavorText, string optionalDeadlineText, string optionalVoteOverrideData, bool alphabeticalSort = true, bool simple = true, bool lSort = true, bool cleanDay = false, bool displayAllVCs = false, String _prodTimer = null, string _fontOverride = null, bool _areaTagsOn = true, string _dividerOverride = null, bool _showLLevel = false, bool _showZeroCountWagons = false)
        {

            VoteServiceObject vso = BuildBaseVoteObject(isRestCall, url, playerList, replacementList, moderatorNames, colorCode, dayStartList, deadListText, dayviggedText, priorVCNumberInput, optionalFlavorText, optionalDeadlineText, optionalVoteOverrideData, _prodTimer, _fontOverride, _areaTagsOn, _dividerOverride, _showLLevel, _showZeroCountWagons);
            if (vso.ErrorMessage != null)
            {
                return vso.ErrorMessage;
            }
            else
            {
                try
                {
                    return BuildHistoryLogic.BuildVoteCount(BuildHistoryLogic.BuildHistoryObject(vso, alphabeticalSort, simple, lSort, cleanDay, displayAllVCs));
                }
                catch (Exception e)
                {
                   


                    return "An error occurred while processing the votes. Please check input data or give MathBlade a PM with the thread and post number.";// + "MESSAGE: " + message + "INNER EXCEPTION: " + innerExceptionMessage + "STACK TRACE: " + e.StackTrace + "url=" + host;

                }
            }

        }


       

        public static string GetJSONVotes(bool isRestCall, string url, string playerList, string replacementList, string moderatorNamesInput, string colorCode, string dayStartList = null, string deadListText = null, string dayviggedList = null, string priorVCNumberInput = null, string optionalFlavorText = null, string optionalDeadlineText = null, string optionalVoteOverrideData = null, string playerName = null, string _prodTimer = null, string _fontOverride = null, bool _areaTagsOn = true, string _dividerOverride = null, bool _showLLevel = false, bool _showZeroCountWagons = false)
        {

            VoteServiceObject vso = BuildBaseVoteObject(isRestCall, url, playerList, replacementList, moderatorNamesInput, colorCode, dayStartList, deadListText, dayviggedList, priorVCNumberInput, optionalFlavorText, optionalDeadlineText, optionalVoteOverrideData, _prodTimer, _fontOverride, _areaTagsOn, _dividerOverride, _showLLevel, _showZeroCountWagons);
            if (vso.ErrorMessage != null)
            {
                return vso.ErrorMessage;
            }
            else
            {
                if (!string.IsNullOrEmpty(playerName))
                {
                    Player player = Player.FindPlayerByNameUserAidReplacementsLoop(vso.Players, playerName, vso.Replacements);
                    if (player != null)
                    {
                        return vso.VoteStringByPlayer(player);
                    }
                    else
                    {
                        return "Player named " + playerName + " not found. Please check data entry.";
                    }
                }
                else
                {
                    return vso.AllVotesString;
                }
            }

        }


        private static VoteServiceObject BuildBaseVoteObject(bool isRestCall, VoteScrubInformationObject vsio)
        {
            return BuildBaseVoteObject(isRestCall, vsio.UrlOfGame, vsio.PlayerTextInput, vsio.ReplacementTextInput, vsio.ModeratorNamesInput, vsio.ColorCode, vsio.DayNumbersInput, vsio.DeadListInput, vsio.DayviggedInput, vsio.PriorVCNumberInput, vsio.FlavorInput, vsio.DeadLineInput, vsio.VoteOverridesInput, vsio.ProdTimer, vsio.FontOverride, vsio.AreaTagsOn, vsio.DividerOverride, vsio.ShowLLevel, vsio.ShowZeroCountWagons);
        }

        private static VoteServiceObject BuildBaseVoteObject(bool isRestCall, string url, string playerList, string replacementList, string moderatorNamesInput, string colorCode, string dayStartList = null, string deadListText = null, string _dayviggedListText = null, string priorVCNumberInput = null, string optionalFlavorText = null, string optionalDeadlineText = null, string optionalVoteOverrideData = null, String _prodTimer = null, string _fontOverride = null, bool _areaTagsOn = true, string _dividerOverride = null, bool _showLLevel = false, bool _showZeroCountWagons = false, bool _hardReset = false)
        {

           //Input variables
           string playerText;
           string replacementText;
           string dayPostStartList;
           string deadPlayerListText;
           string dayviggedListText;
           string resurrectedListText;
           string moderatorNamesText;
           string priorVCNumberInputText;
           string voteOverrideText;
           string prodTimerString;


            //Variables used in data crunching.
            string urlOfGame;
            List<Player> players;
            List<Replacement> replacements;
            List<string> moderatorNames;
            List<int> dayStartPostNumbers;
            List<List<Player>> nightkilledPlayers;
            List<DayviggedPlayer> dayViggedPlayers;
            List<ResurrectedPlayer> resurrectedPlayers;
            int priorVCNumber;
            string flavorText;
            string deadlineCode;
            string colorHashCode;
            string fontOverride;
            List<Vote> votesByOverride;
            ProdTimer prodTimer;
            bool areaTagsOn;
            string dividerOverride;
            bool showLLevel;
            bool showZeroCountWagons;
            bool hardReset;


            try
            {
                urlOfGame = url;
                playerText = playerList;
                replacementText = replacementList;
                moderatorNamesText = moderatorNamesInput;
                dayPostStartList = dayStartList;
                deadPlayerListText = deadListText;
                priorVCNumberInputText = priorVCNumberInput;
                
                
                voteOverrideText = optionalVoteOverrideData;
                colorHashCode = colorCode;
                prodTimerString = _prodTimer;
                fontOverride = _fontOverride;
                areaTagsOn = _areaTagsOn;
                dividerOverride = _dividerOverride;
                showLLevel = _showLLevel;
                showZeroCountWagons = _showZeroCountWagons;
                dayviggedListText = _dayviggedListText;
                hardReset = _hardReset;

                //string errorsInGivenData = validateAllUserInputs();

                //TODO resurrectedListText
                resurrectedListText = null;
                string errorsInGivenData = validateAllUserInputs(playerText, replacementText, dayPostStartList, deadPlayerListText, dayviggedListText, resurrectedListText, moderatorNamesText, priorVCNumberInputText, optionalFlavorText, optionalDeadlineText, voteOverrideText, prodTimerString, out players, out replacements, out moderatorNames, out dayStartPostNumbers, out nightkilledPlayers, out dayViggedPlayers, out resurrectedPlayers, out priorVCNumber, out flavorText, out deadlineCode, out votesByOverride, out prodTimer);

               
                if (errorsInGivenData != null)
                {
                    return new VoteServiceObject(null, "Invalid inputs provided. Ending processing. Reason: " + errorsInGivenData);
                }


                if (validateURL(urlOfGame) == true)
                {
                    if (!urlOfGame.Contains("&ppp=200"))
                    {
                        urlOfGame = urlOfGame + "&ppp=200";
                    }

                    votesByOverride = Day.parseVoteText(players, voteOverrideText, replacements);



                    return new VoteServiceObject(urlOfGame, players, replacements, moderatorNames, dayStartPostNumbers, nightkilledPlayers, dayViggedPlayers, resurrectedPlayers, colorHashCode, priorVCNumber, flavorText, deadlineCode, votesByOverride, true, prodTimer, fontOverride, areaTagsOn, dividerOverride, showLLevel, showZeroCountWagons, false);
                }
                else
                {
                    System.Console.WriteLine("Invalid URL given in. Ending processing.");

                    return new VoteServiceObject(null, "Invalid URL given in. Ending processing.");
                }
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    Debug.Log(e.InnerException.StackTrace);
                }
                else
                {
                    Debug.Log(e.StackTrace);
                }
                return new VoteServiceObject(null, "Some weird stuff happened.");
                //return new VoteServiceObject(null, "InputData: " + GetVotesDebug(urlOfGame, playerList, replacementList, moderatorNamesInput, dayStartList, deadListText, priorVCNumberInput, optionalFlavorText, optionalDeadlineText, optionalVoteOverrideData, _prodTimer) + " An unexpected error occurred. Please PM MathBlade with input data or temporarily hand count votes." + " MESSAGE: " + (e.Message != null ? e.Message : "No message") + " STACK TRACE: " + e.StackTrace);
            }
        }


        private static string validateAllUserInputs(string playerText, string replacementText, string dayPostStartList, string deadPlayerListText, string dayviggedListText, string resurrectedListText, string moderatorNamesText, string priorVCNumberText, string flavorInput, string deadLineInput, string voteOverrideText, string prodTimerString, out List<Player> players, out List<Replacement> replacements, out List<string> moderatorNames, out List<int> dayStartPostNumbers, out List<List<Player>> nightkilledPlayers, out List<DayviggedPlayer> dayViggedPlayers, out List<ResurrectedPlayer> resurrectedPlayers,out int priorVCNumber, out string flavorText, out string deadlineCode, out List<Vote> votesByOverride, out ProdTimer prodTimer)
        {
            string errorList = null;
            List<string> allErrors = new List<string>();


            List<Player> playersDone = null;
            List<Replacement> replacementsDone = null;
            List<int> dayStartPostNumbersDone = null;
            List<List<Player>> nightkilledPlayersDone = null;
            List<DayviggedPlayer> dayViggedPlayersDone = null;
            List<ResurrectedPlayer> resurrectedPlayersDone = null;
            List<string> moderatorNamesDone = null;
            int priorVCNumberDone = -314;
            string flavorTextDone = null;
            string deadlineCodeDone = null;
            List<Vote> votesByOverrideDone = null;
            ProdTimer prodTimerDone = null;

            try
            {
                
               
                errorList = null;

                allErrors.Add(validatePlayerList(playerText, out playersDone));
                if (playersDone == null)
                {
                    allErrors.Add("No players found.");
                }
                else
                {
                    allErrors.Add(validateReplacementsList(replacementText, out replacementsDone));
                    allErrors.Add(validateDayPostNumbers(dayPostStartList, out dayStartPostNumbersDone));
                    allErrors.Add(validateDeaths(deadPlayerListText, playersDone, replacementsDone, out nightkilledPlayersDone));
                    allErrors.Add(validateDayviggedPlayers(dayviggedListText, playersDone, replacementsDone, out dayViggedPlayersDone));
                    allErrors.Add(validateResurrectedPlayers(resurrectedListText, playersDone, replacementsDone, out resurrectedPlayersDone));
                    allErrors.Add(validateModeratorNames(moderatorNamesText, out moderatorNamesDone));
                    allErrors.Add(validatePriorVCNumber(priorVCNumberText, out priorVCNumberDone));
                    allErrors.Add(validateFlavorText(flavorInput, out flavorTextDone));
                    allErrors.Add(validateDeadline(deadLineInput, out deadlineCodeDone));
                    allErrors.Add(validateVoteOverrideData(voteOverrideText, playersDone, replacementsDone, out votesByOverrideDone));
                    allErrors.Add(validateProdTimer(prodTimerString, out prodTimerDone));
                }

                string finalErrorMessage = string.Empty;
                foreach(string errorString in allErrors)
                {
                    if (errorString != null)
                    {
                        finalErrorMessage = finalErrorMessage + errorString + "\n";
                    }
                }
                if (finalErrorMessage.Equals(string.Empty))
                {
                    players = playersDone;
                    replacements = replacementsDone;
                    dayStartPostNumbers = dayStartPostNumbersDone;
                    nightkilledPlayers = nightkilledPlayersDone;
                    dayViggedPlayers = dayViggedPlayersDone;
                    resurrectedPlayers = resurrectedPlayersDone;
                    moderatorNames = moderatorNamesDone;
                    priorVCNumber = priorVCNumberDone;
                    flavorText = flavorTextDone;
                    deadlineCode = deadlineCodeDone;
                    votesByOverride = votesByOverrideDone;
                    prodTimer = prodTimerDone;
                    return null;
                }
                else
                {
                    players = null;
                    replacements = null;
                    dayStartPostNumbers = null;
                    nightkilledPlayers = null;
                    dayViggedPlayers = null;
                    resurrectedPlayers = null;
                    moderatorNames = null;
                    priorVCNumber = -314;
                    flavorText = null;
                    deadlineCode = null;
                    votesByOverride = null;
                    prodTimer = null;
                    return finalErrorMessage.Trim();
                }
                

            }
            catch (Exception e)
            {
                players = null;
                replacements = null;
                dayStartPostNumbers = null;
                nightkilledPlayers = null;
                dayViggedPlayers = null;
                resurrectedPlayers = null;
                moderatorNames = null;
                priorVCNumber = -314;
                flavorText = null;
                deadlineCode = null;
                votesByOverride = null;
                prodTimer = null;
                return "An error occured in validating the data: " + e.Message;
            }


            

        }

        public static void myUtilityMethod(Action<string> textAppender)
        {
            if (textAppender != null) { textAppender("Hi Worldo!"); }
        }

    

        public static string validateText(string text)
        {

            if (text != null && text.Length > 0)
            {
                text = text.Trim();
                return text;
            }
            else
            {
                return null;
            }
        }

        public static string validatePlayerList(string playerText, out List<Player> players)
        {
            string playerListInput = playerText;
            if (playerListInput == null)
            {
                players = null;
                return "You need to provide a comma separated list of players.";
            }
            
            string playerListInputValidatedExists = validateText(playerText);


            string[] playerCommaSplitList = playerListInputValidatedExists.Split(COMMA);
            players = new List<Player>();

            foreach (string playerName in playerCommaSplitList)
            {
                try
                {
                    players.Add(new Player(playerName.Trim()));
                }
                catch (Exception ex)
                {
                    var stringMessage = ex.Message;
                    if (Dictionary.Instance() == null)
                    {
                        return "Could not load dictionary for word recognition: " + Application.dataPath;
                    }
                    else
                    {
                        return "Player Name: " + playerName + " was not formatted correctly.";
                    }
                }
            }

            if ((players.Count == 0) || (players.Count == 1))
            {

                System.Console.WriteLine("ERROR: You must provide a comma delimited list of players.");
                return "ERROR: You must provide a comma delimited list of players.";
            }



            return null;

        }

        public static string validateReplacementsList(string replacementText, out List<Replacement> replacements)
        {
            try
            {
                replacements = new List<Replacement>();
                if (replacementText == null || replacementText.Length == 0)
                {
                    return null;
                }
                

                string[] replacementsCommaSplitList = replacementText.Split(COMMA);

                string[] replacementArray = null;

                Replacement replacement = null;
                foreach (string replacementGroup in replacementsCommaSplitList)
                {
                    if (replacementGroup.Length > 0)
                    {
                        replacementArray = replacementGroup.Split(COLON);
                        replacement = new Replacement(replacementArray[0].Trim(), replacementArray[1].Trim());
                        replacements.Add(replacement);
                    }

                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("ERROR: There was a problem with the replacement list. Please check your format and try again. ");
                replacements = null;
                return "ERROR: There was a problem with the replacement list. Please check your format and try again. " + e.StackTrace;
            }

            return null;
        }

        public static string validateDayPostNumbers(string dayPostStartList, out List<int> dayStartPostNumbers)
        {
            dayStartPostNumbers = new List<int>();
            if (dayPostStartList == null)
            {
                dayStartPostNumbers.Add(0);
                return null;
            }
            
            string dayStartPostNumberText = validateText(dayPostStartList);
            dayStartPostNumbers = new List<int>();
            if (dayStartPostNumberText == null)
            {
                dayStartPostNumbers.Add(0);
                return null;
            }
            string[] postNumberList = dayStartPostNumberText.Split(COMMA);


            int lastNewPostNumber = -2;
            bool isValidList = true;
            foreach (string aPostNumberString in postNumberList)
            {
                int newPostNumber = -1;
                bool isANumber = int.TryParse(aPostNumberString.Trim(), out newPostNumber);
                if (isANumber && newPostNumber > -1 && newPostNumber > lastNewPostNumber)
                {
                    dayStartPostNumbers.Add(newPostNumber);
                    lastNewPostNumber = newPostNumber;
                }
                else
                {
                    isValidList = false;
                    break;
                }
            }

            if (!isValidList)
            {

                System.Console.WriteLine("ERROR: Check the input of your post day start numbers;");
                return ("ERROR: Check the input of your post day start numbers; ");
            }
            else if (dayStartPostNumbers.Count == 0)
            {
                dayStartPostNumbers.Add(0);
            }

            return null;

        }

        public static string validateDeaths(string deadPlayerListText, List<Player> players, List<Replacement> replacements, out List<List<Player>> nightkilledPlayers)
        {

            if ((deadPlayerListText == null || deadPlayerListText.Length == 0))
            {
                nightkilledPlayers = new List<List<Player>>();
                return null;
            }
           
            string[] deadTextSplit = deadPlayerListText.Split(Convert.ToChar(","));

           
            string[] deadPlayerStringSplit = null;
            string playerName = null;
            int nightOfDeath = -1;
            nightkilledPlayers = new List<List<Player>>();

           
            if (deadTextSplit.Length > 0)
            {
                foreach (string deadPlayerString in deadTextSplit)
                {
                    if (deadPlayerString.Length > 0)
                    {
                        deadPlayerStringSplit = deadPlayerString.Split(Convert.ToChar("-"));
                        if (deadPlayerStringSplit.Length != 2)
                        {
                            throw new ArgumentException("You must provide deaths in a format player name then a dash then the night number. Example: MathBlade-1 input found was " + String.Join("", deadPlayerStringSplit) + ".");
                        }
                        playerName = deadPlayerStringSplit[0].Trim();
                        nightOfDeath = Int32.Parse(deadPlayerStringSplit[1].Trim());

                        while (nightkilledPlayers.Count < (nightOfDeath + 1))
                        {
                            nightkilledPlayers.Add(new List<Player>());
                        }

                        Player playerKilled = Player.FindPlayerByNameUserAid(players, playerName);
                        if (playerKilled != null)
                        {
                            nightkilledPlayers[nightOfDeath - 1].Add(playerKilled);

                        }
                        else
                        {

                            playerKilled = Player.FindPlayerByNameUserAidReplacementsLoop(players, playerName, replacements);
                            if (playerKilled != null)
                            {
                                nightkilledPlayers[nightOfDeath - 1].Add(playerKilled);
                            }
                            else
                            {
                                System.Console.WriteLine("Error finding player killed. Please check spelling for players killed. Bad input: " + playerName);
                                return ("Error finding player killed. Please check spelling for players killed. Bad input: " + playerName);
                            }
                        }



                    }
                }
            }


            return null;
        }

        public static string validateDayviggedPlayers(string dayviggedListText, List<Player> players, List<Replacement> replacements, out List<DayviggedPlayer> dayViggedPlayers)
        {

            if (dayviggedListText == null || dayviggedListText.Length == 0)
            {
                dayViggedPlayers = new List<DayviggedPlayer>();
                return null;
            }
            
            string[] dayvigsTextSplit = dayviggedListText.Split(Convert.ToChar(","));

        
            string[] deadPlayerStringSplit = null;
            string playerName = null;
            int postNumber = -1;
            dayViggedPlayers = new List<DayviggedPlayer>();

            

            if (dayvigsTextSplit.Length > 0)
            {
                foreach (string dayvigString in dayvigsTextSplit)
                {
                    if (dayvigString.Length > 0)
                    {
                        deadPlayerStringSplit = dayvigString.Split(Convert.ToChar("-"));
                        playerName = deadPlayerStringSplit[0].Trim();
                        postNumber = Int32.Parse(deadPlayerStringSplit[1].Trim());



                        Player playerKilled = Player.FindPlayerByNameUserAid(players, playerName);
                        if (playerKilled != null)
                        {
                            dayViggedPlayers.Add(new DayviggedPlayer(playerKilled, postNumber));

                        }
                        else
                        {

                            playerKilled = Player.FindPlayerByNameUserAidReplacementsLoop(players, playerName, replacements);
                            if (playerKilled != null)
                            {
                                dayViggedPlayers.Add(new DayviggedPlayer(playerKilled, postNumber));
                            }
                            else
                            {
                                System.Console.WriteLine("Error finding player dayvigged. Please check spelling for players killed. Bad input: " + playerName);
                                return ("Error finding player dayvigged. Please check spelling for players killed. Bad input: " + playerName);
                            }
                        }



                    }
                }
            }


            return null;
        }

        public static string validateResurrectedPlayers(string resurrectedListText, List<Player> players, List<Replacement> replacements, out List<ResurrectedPlayer> resurrectedPlayers)
        {

            if (resurrectedListText == null || resurrectedListText.Length == 0)
            {
                resurrectedPlayers = new List<ResurrectedPlayer>();
                return null;
            }
            
            string[] resurrectedTextSplit = resurrectedListText.Split(Convert.ToChar(","));

            string[] alivePlayerStringSplit = null;
            string playerName = null;
            int postNumber = -1;
            resurrectedPlayers = new List<ResurrectedPlayer>();           

            if (resurrectedTextSplit.Length > 0)
            {
                foreach (string resString in resurrectedTextSplit)
                {
                    if (resString.Length > 0)
                    {
                        alivePlayerStringSplit = resString.Split(Convert.ToChar("-"));
                        playerName = alivePlayerStringSplit[0].Trim();
                        postNumber = Int32.Parse(alivePlayerStringSplit[1].Trim());



                        Player playerResd = Player.FindPlayerByNameUserAid(players, playerName);
                        if (playerResd != null)
                        {
                            resurrectedPlayers.Add(new ResurrectedPlayer(playerResd, postNumber));

                        }
                        else
                        {

                            playerResd = Player.FindPlayerByNameUserAidReplacementsLoop(players, playerName, replacements);
                            if (playerResd != null)
                            {
                                resurrectedPlayers.Add(new ResurrectedPlayer(playerResd, postNumber));
                            }
                            else
                            {
                                System.Console.WriteLine("Error finding player resurrected. Please check spelling for players killed. Bad input: " + playerName);
                                return ("Error finding player resurrected. Please check spelling for players killed. Bad input: " + playerName);
                            }
                        }



                    }
                }
            }


            return null;
        }

        public static string validateModeratorNames(string moderatorNamesText, out List<string> moderatorNames)
        {

          
            string moderatorNamesString = validateText(moderatorNamesText);



            if (moderatorNamesString == null)
            {
                System.Console.WriteLine("No moderator specified. This is a required field.");
                moderatorNames = null;
                return ("No moderator specified. This is a required field.");
            }

            string[] moderatorNamesArray = moderatorNamesString.Split(COMMA);
            moderatorNames = new List<string>();
            foreach (string name in moderatorNamesArray)
            {
                moderatorNames.Add(name);
            }

            if (moderatorNames.Count == 0)
            {
                System.Console.WriteLine("No moderator specified. This is a required field.");
                moderatorNames = null;
                return ("No moderator specified. This is a required field.");
            }

            return null;
        }


        public static string validatePriorVCNumber(string priorVCNumberInputText, out int priorVCNumber)
        {
           
            string vcNumberText = priorVCNumberInputText;
            if (vcNumberText == null || vcNumberText.Length == 0)
            {
                priorVCNumber = 0;
                return null;
            }

            return (int.TryParse(vcNumberText, out priorVCNumber) ? null : "Error you did not provide a number for prior vote count number.");
        }


        public static string validateFlavorText(string flavorInput, out string flavorText)
        {

            if (flavorInput == null || flavorInput.Trim().Length == 0)
            {
                flavorText = "This is an automated vote count generated by a tool written by MathBlade. It goes much smoother with exact votes but will try to detect bold votes and misspellings. If you have issues during this beta, please get MathBlade.";
            }
            else
            {
                flavorText = flavorInput;
            }

            return null;
        }

        public static string validateDeadline(string deadLineInput, out string deadlineCode)
        {
            
            if (deadLineInput == null || deadLineInput.Length == 0)
            {
                deadlineCode = "(FIX ME)";
            }
            else
            {
                deadlineCode = deadLineInput;
            }

            return null;
        }

        public static string validateProdTimer(string prodTimerString, out ProdTimer prodTimer)
        {
          
            if (prodTimerString == null || prodTimerString.Length == 0)
            {
                prodTimer = null;
                return null;
            }
            else
            {
                string[] parameters = prodTimerString.Split(',');
                int currentTimer = 0;
                int[] countdownvalues = new int[4];
                int i = 0;

                foreach (string aTimer in parameters)
                {
                    bool successfulParse = int.TryParse(aTimer, out currentTimer);
                    if (!successfulParse)
                    {
                        prodTimer = null;
                        return "Failure to parse prod timer";
                    }
                    else
                    {
                        countdownvalues[i] = currentTimer;
                        i++;
                    }

                }

                prodTimer = new ProdTimer(countdownvalues[0], countdownvalues[1], countdownvalues[2], countdownvalues[3]);
                return null;
            }
        }

        public static string validateVoteOverrideData(string voteOverrideText, List<Player> players, List<Replacement> replacements, out List<Vote> votesByOverride)
        {
           
            if (voteOverrideText == null || voteOverrideText.Trim().Length == 0)
            {
                votesByOverride = new List<Vote>();
                return null;
            }

            try
            {
                votesByOverride = Day.parseVoteText(players, voteOverrideText, replacements);

            }

            catch (Exception e)
            {
                System.Console.WriteLine("Error occurred in processing override. Check format. Exception: " + e.StackTrace);
                votesByOverride = null;
                return ("||" + voteOverrideText + "|| " + " Error occurred in processing override. Check format. Exception: " + e.StackTrace);
            }

            return null;
        }




        public static bool validateURL(string url)
        {
            if ((url == null) || (url.Length == 0))
            {
                return false;
            }

            return true;

        }

    }

    
}

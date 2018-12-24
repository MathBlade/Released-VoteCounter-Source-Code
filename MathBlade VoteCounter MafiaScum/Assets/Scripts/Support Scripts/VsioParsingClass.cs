using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.SupportClasses;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Assets.Scripts.RunScrubLogic
{


    public class VsioParsingClass
    {
        #region private constants

        private const int MAX_BYTE_ARRAY_LENGTH = 314159;


        private const string URL_OF_GAME_PROMPT = "url=";
        private const string PLAYER_TEXT_PROMPT = "playerList=";
        private const string REPLACEMENTS_LIST_PROMPT = "replacementList=";
        private const string MOD_LIST_PROMPT = "moderatorNames=";
        private const string DAY_NUMBERS_PROMPT = "dayStartNumbers=";
        private const string DEAD_LIST_PROMPT = "deadList=";
        private const string DEADLINE_PROMPT = "deadline=";
        private const string FLAVOR_PROMPT = "flavor=";
        private const string VOTE_OVERRIDES_PROMPT = "voteOverrides=";
        private const string ALPHA_SORT_PROMPT = "alphaSort=";
        private const string SIMPLE_SORT_PROMPT = "simple=";
        private const string L_SORT_PROMPT = "lSort=";
        private const string CLEAN_DAY_PROMPT = "cleanDay=";
        private const string DISPLAY_ALL_VCs_PROMPT = "displayAllVCs=";
        private const string VOTE_NUMBER_INPUT = "priorVCNumber=";
        private const string COLOR_HASH_CODE = "color=";
        private const string PROD_TIMER = "prodTimer=";
        private const string FONT_OVERRIDE = "fontOverride=";
        private const string AREA_TAGS = "showAreaTags=";
        private const string DIVIDER_STRING = "dividerOverride=";
        private const string SHOW_L_LEVEL = "showLLEVEL=";
        private const string SHOWZEROCOUNTWAGONS = "showZeroCountWagons=";
        private const string DAY_VIGGED_PLAYERS_PROMPT = "dayviggedPlayers=";
        private const string RESURRECTED_PLAYERS_PROMPT = "resurrectedPlayers=";
        private const string HARD_RESET = "hardReset=";


        private static string[] ALL_PROMPTS = new string[] { VOTE_NUMBER_INPUT, URL_OF_GAME_PROMPT, PLAYER_TEXT_PROMPT, DEADLINE_PROMPT, REPLACEMENTS_LIST_PROMPT, MOD_LIST_PROMPT, DAY_NUMBERS_PROMPT, DEAD_LIST_PROMPT, FLAVOR_PROMPT, VOTE_OVERRIDES_PROMPT, ALPHA_SORT_PROMPT, SIMPLE_SORT_PROMPT, L_SORT_PROMPT, CLEAN_DAY_PROMPT, DISPLAY_ALL_VCs_PROMPT, COLOR_HASH_CODE, PROD_TIMER, FONT_OVERRIDE, AREA_TAGS, DIVIDER_STRING, SHOW_L_LEVEL, SHOWZEROCOUNTWAGONS, DAY_VIGGED_PLAYERS_PROMPT, RESURRECTED_PLAYERS_PROMPT, HARD_RESET };
        #endregion


        public static VoteScrubInformationObject GetSettingsScrubObject(bool isRestCall, string innerText)
        {
            List<string> promptsFound = new List<string>();
            List<string> valuesFound = new List<string>();

            string remainingString = innerText;

            List<string> promptsLeft = new List<string>();
            promptsLeft.AddRange(ALL_PROMPTS);


            bool workLeftToDo = true;
            List<string> usedPrompts = new List<string>();
            List<string> values = new List<string>();
            string originalText = (string)innerText.Clone();


            while (workLeftToDo == true)
            {
                workLeftToDo = promptLoop(ref innerText, ref promptsLeft, ref usedPrompts, ref values);
            }

            string priorVCNumberInput = getRequiredValue(VOTE_NUMBER_INPUT, usedPrompts, values);
            string urlOfGame = System.Net.WebUtility.UrlDecode(getRequiredValue(URL_OF_GAME_PROMPT, usedPrompts, values)).Replace("&amp;", "&");
            string playerTextInput = getRequiredValue(PLAYER_TEXT_PROMPT, usedPrompts, values);
            string replacementTextInput = getOptionalValue(REPLACEMENTS_LIST_PROMPT, null, usedPrompts, values);
            string moderatorNamesInput = getRequiredValue(MOD_LIST_PROMPT, usedPrompts, values);
            string dayNumbersInput = getOptionalValue(DAY_NUMBERS_PROMPT, null, usedPrompts, values);
            string deadListInput = getOptionalValue(DEAD_LIST_PROMPT, null, usedPrompts, values);
            string deadLineInput = getOptionalValue(DEADLINE_PROMPT, null, usedPrompts, values);
            string dayviggedInput = getOptionalValue(DAY_VIGGED_PLAYERS_PROMPT, null, usedPrompts, values);
            string resurrectedInput = getOptionalValue(RESURRECTED_PLAYERS_PROMPT, null, usedPrompts, values);
            string flavorInput = getOptionalValue(FLAVOR_PROMPT, null, usedPrompts, values);
            string voteOverridesInput = getOptionalValue(VOTE_OVERRIDES_PROMPT, null, usedPrompts, values);
            string alphaSortInput = getOptionalValue(ALPHA_SORT_PROMPT, "true", usedPrompts, values);
            string simpleInput = getOptionalValue(SIMPLE_SORT_PROMPT, "true", usedPrompts, values);
            string lSortInput = getOptionalValue(L_SORT_PROMPT, "true", usedPrompts, values);
            string cleanDayInput = getOptionalValue(CLEAN_DAY_PROMPT, "false", usedPrompts, values);
            string displayAllVCsInput = getOptionalValue(DISPLAY_ALL_VCs_PROMPT, "false", usedPrompts, values);
            string colorCode = getOptionalValue(COLOR_HASH_CODE, "#00BFFF", usedPrompts, values);
            string prodTimerString = getOptionalValue(PROD_TIMER, null, usedPrompts, values);
            string fontOverrideString = getOptionalValue(FONT_OVERRIDE, null, usedPrompts, values);
            string areaTagsOnString = getOptionalValue(AREA_TAGS, Boolean.TrueString, usedPrompts, values);


            bool areaTagsOn = true;
            bool.TryParse(areaTagsOnString, out areaTagsOn);

            string dividerOverrideString = getOptionalValue(DIVIDER_STRING, " ~ ", usedPrompts, values);

            string showLLevelString = getOptionalValue(SHOW_L_LEVEL, Boolean.FalseString, usedPrompts, values);
            bool showLLevel = false;
            bool.TryParse(showLLevelString, out showLLevel);

            string showZeroCountWagonString = getOptionalValue(SHOWZEROCOUNTWAGONS, Boolean.FalseString, usedPrompts, values);
            bool showZeroCountWagons = true;
            bool.TryParse(showZeroCountWagonString, out showZeroCountWagons);

            string hardResetString = getOptionalValue(HARD_RESET, Boolean.FalseString, usedPrompts, values);
            bool doHardReset = false;
            bool.TryParse(hardResetString, out doHardReset);


            if (voteOverridesInput != null)
            {
                try
                {
                    HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();                   
                    htmlDocument.LoadHtml(voteOverridesInput);
                    voteOverridesInput = System.Net.WebUtility.UrlDecode(htmlDocument.DocumentNode.SelectSingleNode(".//code").InnerText);
                }
                catch
                {
                    voteOverridesInput = "";

                }
            }

            return new VoteScrubInformationObject(priorVCNumberInput, urlOfGame, playerTextInput, dayNumbersInput, replacementTextInput, moderatorNamesInput, deadListInput, deadLineInput, flavorInput, voteOverridesInput, alphaSortInput, simpleInput, lSortInput, cleanDayInput, displayAllVCsInput, colorCode, prodTimerString, fontOverrideString, areaTagsOn, dividerOverrideString, showLLevel, showZeroCountWagons, dayviggedInput, resurrectedInput, doHardReset);
        }

        private static string getRequiredValue(string prompt, List<string> promptsUsed, List<string> values)
        {

            int indexOfRequiredItem = promptsUsed.IndexOf(prompt);
            if (indexOfRequiredItem == -1)
            {
                throw new ArgumentNullException();
            }

            return values[indexOfRequiredItem].Replace("<br>", "");
        }

        private static string getOptionalValue(string prompt, string defaultIfMissing, List<string> promptsUsed, List<string> values)
        {
            int indexOfRequiredItem = promptsUsed.IndexOf(prompt);
            if (indexOfRequiredItem == -1)
            {
                return defaultIfMissing;
            }

            return values[indexOfRequiredItem].Replace("<br>", "");
        }

        private static bool promptLoop(ref string remainingString, ref List<string> prompts, ref List<string> usedPrompts, ref List<string> values)
        {

            foreach (string prompt in prompts)
            {
                if (remainingString.StartsWith(prompt))
                {
                    prompts.Remove(prompt);
                    usedPrompts.Add(prompt);
                    string currentPrompt = prompt;
                    remainingString = remainingString.Substring(prompt.Length);
                    List<int> indexesOfOtherPrompts = new List<int>();
                    foreach (string differentPrompt in prompts)
                    {
                        int index = remainingString.IndexOf(differentPrompt);
                        if (index > -1)
                        {
                            indexesOfOtherPrompts.Add(index);

                        }
                        else
                        {
                            indexesOfOtherPrompts.Add(int.MaxValue);
                        }
                    }

                    string value;
                    if (indexesOfOtherPrompts.Count > 0)
                    {
                        int minIndex = indexesOfOtherPrompts.Min();

                        if (minIndex > -1 && minIndex != int.MaxValue)
                        {
                            value = remainingString.Substring(0, minIndex);
                            values.Add(value);
                            remainingString = remainingString.Substring(minIndex);
                            return true;
                        }
                        else
                        {
                            value = remainingString;
                            values.Add(value);
                            return false;
                        }
                    }
                    else
                    {
                        value = remainingString;
                        values.Add(value);
                        return false;
                    }



                }
            }

            return false;
        }

        private VoteScrubInformationObject ScrubInnerText(bool isRestCall, string innerText)
        {
            VoteScrubInformationObject vsio = null;
            try
            {
                vsio = GetSettingsScrubObject(isRestCall, innerText);
                return vsio;
            }
            catch (Exception e)
            {
                return null;
            }



        }

        public static string GetSettingsString(string threadshortened, int postNumber)
        {
            //Result storage array       
            NativeArray<byte> settingsResult = new NativeArray<byte>(MAX_BYTE_ARRAY_LENGTH, Allocator.TempJob);

            try
            {
                //Create the job          
                VoteCounterSettingsJob voteCounterSettingsJob = new VoteCounterSettingsJob();
                voteCounterSettingsJob.ThreadShortened = new NativeArray<byte>(threadshortened.Length, Allocator.Temp);
                voteCounterSettingsJob.ThreadShortened.CopyFrom(Encoding.ASCII.GetBytes(threadshortened));
                voteCounterSettingsJob.PostNumber = postNumber;
                voteCounterSettingsJob.result = settingsResult;

                //Schedule the job
                JobHandle settingsHandle = voteCounterSettingsJob.Schedule();

                // Wait for the job to complete
                settingsHandle.Complete();


                // All copies of the NativeArray point to the same memory, you can access the result in "your" copy of the NativeArray


                string settingsString = Encoding.UTF8.GetString(settingsResult.ToArray());

                // Free the memory allocated by the result array      
                voteCounterSettingsJob.ThreadShortened.Dispose();
                settingsResult.Dispose();

                return settingsString;
            }
            catch (System.Exception e)
            {
                Debug.Log("Exception occurred " + e.StackTrace);

                return "Error executing settings job";
            }



        }

        private struct VoteCounterTestJob : IJob
        {
            public float a;
            public float b;
            public NativeArray<float> result;

            public void Execute()
            {
                result[0] = a + b;
            }
        }

        private struct VoteCounterSettingsJob : IJob
        {
            public NativeArray<byte> ThreadShortened;
            public int PostNumber;
            public NativeArray<byte> result;


            public void Execute()
            {
                string threadShortenedString = Encoding.UTF8.GetString(ThreadShortened.ToArray());

                //314159
                byte[] byteArray = Encoding.UTF8.GetBytes(RunScrubLogic.ScrubPostForSettings(threadShortenedString, PostNumber));
                if (byteArray.Length > MAX_BYTE_ARRAY_LENGTH)
                {
                    throw new System.ArgumentOutOfRangeException("Too long a settings string");
                }
                else if (byteArray.Length <= MAX_BYTE_ARRAY_LENGTH)
                {
                    byte[] byteArrayProperLength = new byte[MAX_BYTE_ARRAY_LENGTH];
                    for (int i = 0; i < byteArray.Length; i++)
                    {

                        byteArrayProperLength[i] = byteArray[i];
                    }
                    result.CopyFrom(byteArrayProperLength);

                }

            }

        }
    }
}

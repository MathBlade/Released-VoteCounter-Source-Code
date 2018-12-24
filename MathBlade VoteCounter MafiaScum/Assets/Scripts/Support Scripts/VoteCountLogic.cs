using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine.UI;
using Assets.Scripts.RunScrubLogic;
using Assets.Scripts.SupportClasses;
using Assets.Scripts.Support_Scripts;
using System;

public class VoteCountLogic  {

    
	public static void GetVoteCount(string threadshortened, int postNumber, InputField resultsField)
	{

        

        if (resultsField == null)
            return;

        

        string settingsString = VsioParsingClass.GetSettingsString(threadshortened, postNumber);
        
        ExtensionMethods.SetInputFieldTextHackyResize(resultsField, GetVoteCountFromSettingsString(settingsString), true);   

    }


    public static void GetAllJSONVotes(string threadshortened, int postNumber, string playerName, InputField resultsField)
    {



        if (resultsField == null)
            return;



        string settingsString = VsioParsingClass.GetSettingsString(threadshortened, postNumber);

        ExtensionMethods.SetInputFieldTextHackyResize(resultsField, GetJSONVotesFromString(settingsString, playerName), false);

    }

    private static string GetJSONVotesFromString(string settingsString, string playerName = null)
    {
        try
        {
            VoteScrubInformationObject vsio = ScrubInnerText(settingsString);

            if (string.IsNullOrEmpty(playerName))
            {
                return VoteCountMainWorkClass.GetJSONVotes(true, vsio.UrlOfGame, vsio.PlayerTextInput, vsio.ReplacementTextInput, vsio.ModeratorNamesInput, vsio.ColorCode, vsio.DayNumbersInput,vsio.DeadListInput,vsio.DayviggedInput, vsio.PriorVCNumberInput,vsio.FlavorInput, vsio.DeadLineInput, vsio.VoteOverridesInput, null);
            }
            else
            {
                return VoteCountMainWorkClass.GetJSONVotes(true, vsio.UrlOfGame, vsio.PlayerTextInput, vsio.ReplacementTextInput, vsio.ModeratorNamesInput, vsio.ColorCode, vsio.DayNumbersInput,vsio.DeadListInput,vsio.DayviggedInput, vsio.PriorVCNumberInput,vsio.FlavorInput, vsio.DeadLineInput, vsio.VoteOverridesInput, playerName);
            }

        }
        catch (System.Exception e)
        {
            return "Check data and try again. If that fails get MathBlade if he is still supporting this. Message for Math: " + e.Message;
        }
    }



    private static string GetVoteCountFromSettingsString(string settingsString)
    {
        try
        {
            VoteScrubInformationObject vsio = ScrubInnerText(settingsString);
            
            return VoteCountMainWorkClass.GetCurrentVoteCount(true, vsio.UrlOfGame, vsio.PlayerTextInput, vsio.ReplacementTextInput, vsio.ModeratorNamesInput, vsio.DayNumbersInput, vsio.DeadListInput, vsio.DayviggedInput, vsio.PriorVCNumberInput, vsio.ColorCode, vsio.FlavorInput, vsio.DeadLineInput, vsio.VoteOverridesInput, Boolean.Parse(vsio.AlphaSortInput), Boolean.Parse(vsio.SimpleInput), Boolean.Parse(vsio.LSortInput), Boolean.Parse(vsio.CleanDayInput), Boolean.Parse(vsio.DisplayAllVCsInput), vsio.ProdTimer, vsio.FontOverride, vsio.AreaTagsOn, vsio.DividerOverride, vsio.ShowLLevel, vsio.ShowZeroCountWagons);
        }
        catch (System.Exception e)
        {
            return "Check data and try again. If that fails get MathBlade if he is still supporting this. Message for Math: " + e.Message;
        }
    }

    private static VoteScrubInformationObject ScrubInnerText(string innerText)
    {
        VoteScrubInformationObject vsio = null;
        try
        {
            vsio = VsioParsingClass.GetSettingsScrubObject(true, innerText);
            return vsio;
        }
        catch (System.Exception e)
        {
            return null;
        }



    }


   


}

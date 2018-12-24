using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.SupportClasses;
using Assets.Scripts.Support_Scripts;

public class AllVotesJSONPanel : EntryPanel {

    public InputField PlayerTextField;

    public override void PerformAction()
    {
        GetAllJSONVotes();
    }


    public void GetAllJSONVotes()
    {
        string errorMessage = null;
        int postNumber = -1;
        string playerName = null;

        if (ResultField == null)
            return;

        if (WordsFile == null)
            return;

        if (PlayerTextField != null)
            playerName = PlayerTextField.text.Trim();



        if (ThreadShortenedInputField == null)
        {
            errorMessage = AddErrorStringText(errorMessage, "You need to specify a thread where your settings are.");
        }

        if (PostNumberInputField == null)
        {
            errorMessage = AddErrorStringText(errorMessage, "You need to specify a postnumber.");

        }
        else
        {

            bool postNumberIsANumber = int.TryParse(PostNumberInputField.text.Trim(), out postNumber);

            if (!postNumberIsANumber)
            {
                errorMessage = AddErrorStringText(errorMessage, "Post number needs to be a number.");
            }
            else if (postNumber < 0)
            {
                errorMessage = AddErrorStringText(errorMessage, "Invalid post number specified.");
            }
        }

        if (errorMessage != null)
            ExtensionMethods.SetInputFieldTextHackyResize(ResultField, errorMessage, true);
        //ResultField.text = errorMessage;
        else
        {
            //ResultField.text = "Validation complete";
            //VoteCountLogic.GetVoteCount(ThreadShortenedInputField.text, postNumber, ResultField);
            VoteCountLogic.GetAllJSONVotes(ThreadShortenedInputField.text, postNumber, playerName, ResultField);
        }






    }
}

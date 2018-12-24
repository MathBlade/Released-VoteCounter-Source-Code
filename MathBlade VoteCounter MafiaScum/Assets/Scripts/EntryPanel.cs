using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.SupportClasses;
using Assets.Scripts.Support_Scripts;


public class EntryPanel : MonoBehaviour {

	public InputField ThreadShortenedInputField;
	public InputField PostNumberInputField;
	public InputField ResultField;
    public TextAsset WordsFile;
    

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void ResetInputData()
	{
		if (ThreadShortenedInputField != null)
			ThreadShortenedInputField.text = string.Empty;
		if (PostNumberInputField != null)
			PostNumberInputField.text = string.Empty;

     

    }

    public virtual void PerformAction()
    {
        DoVoteCount();
    }
    public void DoVoteCount()
	{
		string errorMessage = null;
		int postNumber = -1;

		if (ResultField == null)
			return;

        if (WordsFile == null)
            return;

       



        if (ThreadShortenedInputField == null) {
			errorMessage = AddErrorStringText (errorMessage, "You need to specify a thread where your settings are.");
		} 

		if (PostNumberInputField == null) {
			errorMessage = AddErrorStringText (errorMessage, "You need to specify a postnumber.");

		} else {

			bool postNumberIsANumber = int.TryParse (PostNumberInputField.text.Trim (), out postNumber);

			if (!postNumberIsANumber) {
				errorMessage = AddErrorStringText (errorMessage, "Post number needs to be a number.");
			} else if (postNumber < 0) {
				errorMessage = AddErrorStringText (errorMessage, "Invalid post number specified.");
			}
		}

        if (errorMessage != null)
            ExtensionMethods.SetInputFieldTextHackyResize(ResultField, errorMessage, true);
        //ResultField.text = errorMessage;
        else
        {
            //ResultField.text = "Validation complete";
            VoteCountLogic.GetVoteCount(ThreadShortenedInputField.text, postNumber, ResultField);
        }





			
	}

	protected string AddErrorStringText(string errorMessageSoFar, string textToAdd)
	{
		if (textToAdd == null)
			return errorMessageSoFar;
		
		if (errorMessageSoFar != null) {
			errorMessageSoFar = "\n" + textToAdd;
		} else {
			errorMessageSoFar = textToAdd;
		}

		return errorMessageSoFar;
	}
}

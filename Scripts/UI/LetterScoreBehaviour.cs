using UnityEngine;
using UnityEngine.UI;

public class LetterScoreBehaviour : MonoBehaviour {

    //If it's active to change
    public bool active;

    //Actual Letter
    public string letter;

    //Dictionary
    private string possibleLetters = "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z";

    //Letters from dictionary to an array
    private string[] letterString;

    //Letter Number (0 - 26)
    private int numberLetter = 0;
    
    private Text text;

    // if state changed
    private bool lastState = false;

	// Use this for initialization
	void Start () {
       
        text = GetComponent<Text>();

        letterString = possibleLetters.Split(' '); // From dictionary to Array

        refreshText();
	}

    #region delegates
    private void enableInteraction()
    {
        disableInteraction();
        InputManager.inputSelectUpDelegate += letterUp;
        InputManager.inputSelectDownDelegate += letterDown;
    }


    private void disableInteraction()
    {
        InputManager.inputSelectUpDelegate -= letterUp;
        InputManager.inputSelectDownDelegate -= letterDown;
    }
    #endregion

    // Update is called once per frame
    void Update () {

        if(lastState != active)
        {
            lastState = active;

            if (active) enableInteraction();
            else disableInteraction();
        }

		//more transparency if selected
        if(active)
        {
            verifyChange();
            text.color = new Color(1f, 1f, 1f, 1f);
        } else
        {
            text.color = new Color(1f, 1f, 1f, 0.5f);
        }

	}

    private void verifyChange()
    {
       refreshText();
    }

    //change letter to Up
    private void letterUp()
    {
        if (numberLetter == letterString.Length - 1) numberLetter = 0;
        else numberLetter++;
    }

    //change letter to Down
    private void letterDown()
    {
        if (numberLetter == 0) numberLetter = letterString.Length - 1;
        else numberLetter--;
    }
    
    private void refreshText()
    {
        letter = letterString[numberLetter];
        text.text = letter;
    }
}

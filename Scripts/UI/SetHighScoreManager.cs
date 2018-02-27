using UnityEngine;
using UnityEngine.UI;

public class SetHighScoreManager : MonoBehaviour {

    //Score to be saved
    [SerializeField]
    private Text actualScore;

    //Letters to save player name
    [SerializeField]
    private LetterScoreBehaviour[] letters;

    //which letter is selected
    private int letterNumber;

    [SerializeField]
    private GameStates gameState;

    [SerializeField]
    private HighScoreList highScoreList;

    #region delegates
    private void enableInteraction()
    {
        disableInteraction();
        InputManager.inputFireDelegate += SelectName;
        InputManager.inputSelectLeftDelegate += previousLetter;
        InputManager.inputSelectRightDelegate += nextLetter;
    }

    private void disableInteraction()
    {
        InputManager.inputFireDelegate -= SelectName;
        InputManager.inputSelectLeftDelegate -= previousLetter;
        InputManager.inputSelectRightDelegate -= nextLetter;
    }
    #endregion


    private void previousLetter()
    {
        if (letterNumber == 0) letterNumber = 2;
        else letterNumber--;
        refreshActiveLetter();
    }

    private void nextLetter()
    {
        if (letterNumber == 2) letterNumber = 0;
        else letterNumber++;
        refreshActiveLetter();

    }

    //Select name and set Score to Rank
    private void SelectName()
    {
        //Disable all letters
        foreach(LetterScoreBehaviour letter in letters) { letter.active = false; }

        string namePlayer = letters[0].letter + letters[1].letter + letters[2].letter; //Save to a string
        Score.setHighScore(namePlayer); //Set score
        highScoreList.refreshScores(); //Refresh list
        gameState.changeState(GameState.GameOver); //Go to gameover screen
    }


    private void refreshActiveLetter()
    {
        foreach (LetterScoreBehaviour letter in letters) letter.active = false;

        letters[letterNumber].active = true;
    }

    private void OnEnable()
    {
        enableInteraction();

        letterNumber = 0;

        actualScore.text = "Score : " + Score.score;

        refreshActiveLetter();
    }

    private void OnDisable()
    {
        disableInteraction();
    }

}

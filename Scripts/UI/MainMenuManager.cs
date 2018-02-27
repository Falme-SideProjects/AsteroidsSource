using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    [SerializeField]
    private GameStates gameStates;

    //Top record text (#1)
    [SerializeField]
    private Text recordText;
    

    private void enableInteraction()
    {
        disableInteraction();
        InputManager.inputFireDelegate += startGame;
        InputManager.inputSelectEscDelegate += exitGame;
    }

    private void disableInteraction()
    {
        InputManager.inputFireDelegate -= startGame;
        InputManager.inputSelectEscDelegate -= exitGame;
    }


	// Use this for initialization
	void Start ()
    {
        refreshRecord();
    }
    
    //Quit Application
    private void exitGame()
    {
        ExitGame.QuitGame();
    }

    private void startGame()
    {
        gameStates.changeState(GameState.Ready);
    }

    private void OnEnable()
    {
        enableInteraction();
        refreshRecord();
    }

    private void OnDisable()
    {
        disableInteraction();

    }

    private void refreshRecord()
    {
        if (Score.highscores != null)
        {
            if (Score.highscores.Count > 0)
                recordText.text = "" + Score.highscores[0].value;
            else
                refreshRecord();
        }
    }
}

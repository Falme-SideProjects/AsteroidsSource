using UnityEngine;

public enum GameState { MainMenu, Ready, Gameplay, SetHighScore, GameOver }

public class GameStates : MonoBehaviour {

    //Where you are in the game?
    [SerializeField]
    public static GameState actualGameState;

    //Which level?
    public static short level;

    //Screens
    [SerializeField]
    private GameObject mainMenu, ready, gameplay, highscore, gameOver;
    
    private ElementsManager elementsManager;

    public HighScoreList highScoreList;

    //If it's paused or not
    public static bool paused = false;

	// Use this for initialization
	void Start () {

        elementsManager = GetComponent<ElementsManager>();
        actualGameState = GameState.MainMenu; //Init game at MainMenu
        elementsManager.startDemo(); //Play Background Main Menu

    }
	
    //when changed State, Do :
    public void changeState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                elementsManager.startDemo();
                gameplay.SetActive(false);
                gameOver.SetActive(false);
                mainMenu.SetActive(true);

                break;
            case GameState.Ready:
                elementsManager.clearScreen();
                mainMenu.SetActive(false);
                gameplay.SetActive(false);
                ready.SetActive(true);
                Invoke("gotoGameplay", 2f);
            break;
            case GameState.Gameplay:
                level = 0;
                Score.score = 0;
                Score.nextLife = 10000;
                Lifes.lives = 3;
                elementsManager.startGameplay();
                ready.SetActive(false);
                gameplay.SetActive(true);
            break;
            case GameState.SetHighScore:
                gameplay.SetActive(false);
                highscore.SetActive(true);
                break;
            case GameState.GameOver:
                level = 0;
                Score.score = 0;
                Score.nextLife = 10000;
                highScoreList.refreshScores();
                highscore.SetActive(false);
                gameplay.SetActive(false);
                gameOver.SetActive(true);
                Invoke("gotoMainMenu", 5f);
            break;
        }

        actualGameState = newState;
    }

    //Redirect
    public void gotoGameplay() { changeState(GameState.Gameplay); }

    public void gotoMainMenu() { changeState(GameState.MainMenu); }

}

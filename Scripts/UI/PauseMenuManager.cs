using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour {

    [SerializeField]
    private GameStates gameStates;

    [SerializeField]
    private GameplayManager gameplayManager;

    //UI that have buttons for mobile
    [SerializeField]
    private GameObject mobileControl;

    //Items in Pause Menu 
    [SerializeField]
    private List<ItemPauseMenuBehaviour> itemPauseMenuBehaviour;

    //actual option
    private int numberOption = 0;

    #region delegates
    private void enableInteraction()
    {
        disableInteraction();
        InputManager.inputFireDelegate += selectOption;
        InputManager.inputSelectUpDelegate += previousOption;
        InputManager.inputSelectDownDelegate += nextOption;
    }

    private void disableInteraction()
    {
        InputManager.inputFireDelegate -= selectOption;
        InputManager.inputSelectUpDelegate -= previousOption;
        InputManager.inputSelectDownDelegate -= nextOption;
    }
    #endregion


    private void refreshOption()
    {
        foreach (ItemPauseMenuBehaviour item in itemPauseMenuBehaviour) { item.active = false; }
        itemPauseMenuBehaviour[numberOption].active = true;
    }

    private void previousOption()
    {
        if (numberOption > 0) numberOption--;
        else numberOption = itemPauseMenuBehaviour.Count - 1;

        refreshOption();
    }

    private void nextOption()
    {
        if (numberOption < itemPauseMenuBehaviour.Count - 1) numberOption++;
        else numberOption = 0;
        refreshOption();
    }

    //What will happen when select each one
    private void selectOption()
    {

        switch (numberOption)
        {
            case 0:
                closePause();
                break;
            case 1:
                resetGame();
                break;
            case 2:
                exitGame();
                break;
        }

    }

    public void resetGame()
    {
        closePause();
        gameStates.changeState(GameState.Ready);
    }

    public void exitGame()
    {
        closePause();
        gameStates.changeState(GameState.MainMenu);
    }

    private void closePause()
    {
        gameObject.SetActive(false);
    }

    //On open pause, set this
    private void setOpen()
    {
        numberOption = 0;
        refreshOption();
    }

    private void OnEnable()
    {
        enableInteraction();
        setOpen();

        //if is mobile, hide controls
        if (InputManager.staticSystem == GameSystem.Mobile)
        {
            mobileControl.SetActive(false);
        }

        InputManager.inputSelectEscDelegate += closePause;
        Time.timeScale = 0; //Slow time to 0
        GameStates.paused = true; //set paused
        gameplayManager.stopAudio(); //Pause audio
    }

    private void OnDisable()
    {
        disableInteraction();

        //if is mobile, show controls
        if (InputManager.staticSystem == GameSystem.Mobile)
        {
            mobileControl.SetActive(true);
        }

        InputManager.inputSelectEscDelegate -= closePause;
        Time.timeScale = 1;
        GameStates.paused = false;
        gameplayManager.resumeAudio(); //Resume audio
    }
}

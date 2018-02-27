using System.Collections.Generic;
using UnityEngine;

public enum GameSystem { Desktop, Mobile }

public class InputManager : MonoBehaviour {

    //application system (Windows or Android)
    [SerializeField]
    private GameSystem gameSystem;

    //Where can access the System
    public static GameSystem staticSystem;

    //Delegates to call inputs
    #region Delegates
    public delegate void InputDelegate();
    public static InputDelegate inputFireDelegate;
    public static InputDelegate inputTurnLeftDelegate;
    public static InputDelegate inputTurnRightDelegate;
    public static InputDelegate inputThrustDelegate;
    public static InputDelegate inputHyperspaceDelegate;

    public static InputDelegate inputSelectLeftDelegate;
    public static InputDelegate inputSelectRightDelegate;
    public static InputDelegate inputSelectUpDelegate;
    public static InputDelegate inputSelectDownDelegate;
    public static InputDelegate inputSelectEscDelegate;
    #endregion

    //For mobile and Hold Button
    private bool leftUp = true;
    private bool rightUp = true;
    private bool thrustUp = true;

    //Show or hide items depending on System
    public List<GameObject> mobileUIItems;
    public List<GameObject> desktopUIItems;
    

    void Awake()
    {   
        //Define
        staticSystem = gameSystem;

        if (gameSystem == GameSystem.Desktop)
        {
            hideElements(mobileUIItems);
            showElements(desktopUIItems);
        }
        else if (gameSystem == GameSystem.Mobile)
        {
            hideElements(desktopUIItems);
            showElements(mobileUIItems);
        }
    }


    // Update is called once per frame
    void Update () {
        if(gameSystem == GameSystem.Desktop)
        {
            //Simple Movements
            if (Input.GetKeyDown(KeyCode.Z)) { Hyperspace(); }
            if (Input.GetKeyDown(KeyCode.Space)) { Fire(); }
            if (Input.GetKey(KeyCode.LeftArrow)) { TurnLeft(); }
            if (Input.GetKey(KeyCode.RightArrow)) { TurnRight(); }
            if (Input.GetKey(KeyCode.UpArrow)) { Thrust(); }

            //UI Mostly
            if (Input.GetKeyDown(KeyCode.RightArrow)) { SelectRight(); }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) { SelectLeft(); }
            if (Input.GetKeyDown(KeyCode.UpArrow)) { SelectUp(); }
            if (Input.GetKeyDown(KeyCode.DownArrow)) { SelectDown(); }
            if (Input.GetKeyDown(KeyCode.Escape)) { SelectEsc(); }

        } else if (gameSystem == GameSystem.Mobile)
        {
            if (!leftUp) TurnLeft();
            if (!rightUp) TurnRight();
            if (!thrustUp) Thrust();
        }
    }

    //Hide any Element GameObject
    private void hideElements(List<GameObject> elements)
    {
        foreach(GameObject elem in elements)
        {
            elem.SetActive(false);
        }
        
    }

    //show any Element GameObject
    private void showElements(List<GameObject> elements)
    {
        foreach (GameObject elem in elements)
        {
            elem.SetActive(true);
        }

    }

    #region HoldForMobile
    public void UpLeft()
    {
        leftUp = true;
    }


    public void DownLeft()
    {
        leftUp = false;
    }

    public void UpRight()
    {
        rightUp = true;
    }


    public void DownRight()
    {
        rightUp = false;
    }

    public void UpThrust()
    {
        thrustUp = true;
    }


    public void DownThrust()
    {
        thrustUp = false;
    }
    #endregion

    #region Fire
    public void Fire()
    {
        if (inputFireDelegate != null)
            inputFireDelegate();
    }
    #endregion

    #region TurnLeft
    public void TurnLeft()
    {
        if (inputTurnLeftDelegate != null)
            inputTurnLeftDelegate();

    }
    #endregion

    #region TurnRight
    public void TurnRight()
    {
        if (inputTurnRightDelegate != null)
            inputTurnRightDelegate();

    }
    #endregion

    #region Thrust
    public void Thrust()
    {
        if(inputThrustDelegate != null)
            inputThrustDelegate();
    }
    #endregion

    #region Hyperspace
    public void Hyperspace()
    {
        if (inputHyperspaceDelegate != null)
            inputHyperspaceDelegate();

    }
    #endregion





    #region SelectLeft
    public void SelectLeft()
    {
        if (inputSelectLeftDelegate != null)
            inputSelectLeftDelegate();

    }
    #endregion

    #region SelectRight
    public void SelectRight()
    {
        if (inputSelectRightDelegate != null)
            inputSelectRightDelegate();

    }
    #endregion

    #region SelectUp
    public void SelectUp()
    {
        if (inputSelectUpDelegate != null)
            inputSelectUpDelegate();

    }
    #endregion

    #region SelectDown
    public void SelectDown()
    {
        if (inputSelectDownDelegate != null)
            inputSelectDownDelegate();

    }
    #endregion

    #region SelectEsc
    public void SelectEsc()
    {
        if (inputSelectEscDelegate != null)
            inputSelectEscDelegate();

    }
    #endregion


}

using UnityEngine;
using UnityEngine.InputSystem;

public class XRInputHelper : MonoBehaviour
{
    #region fields

    //input actions for all used inputs
    InputAction explodeAction;
    InputAction explodeAllAction;
    InputAction infoAction;
    InputAction menuAction;

    //make the class available as pseudo-static
    public static XRInputHelper instance;

    #endregion fields



    #region unity methods

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        //set input actions according to defined input sheet
        explodeAction = InputSystem.actions.FindAction("Explode");
        explodeAllAction = InputSystem.actions.FindAction("Explode All");
        infoAction = InputSystem.actions.FindAction("Info");
        menuAction = InputSystem.actions.FindAction("Main Menu");
    }

    #endregion unity methods



    #region access to inputs

    public bool explodeActionPressedThisFrame
    {
        get
        {
            return explodeAction.WasPressedThisFrame();
        }
    }

    public bool infoActionPressedThisFrame
    {
        get
        {
            return infoAction.WasPressedThisFrame();
        }
    }

    public bool explodeAllActionPressedThisFrame
    {
        get
        {
            return explodeAllAction.WasPressedThisFrame();
        }
    }

    public bool menuActionPressedThisFrame
    {
        get
        {
            return menuAction.WasPressedThisFrame();
        }
    }

    #endregion access to inputs
}

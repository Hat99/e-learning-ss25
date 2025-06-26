using UnityEngine;
using UnityEngine.InputSystem;

public class XRInputHelper : MonoBehaviour
{
    InputAction explodeAction;
    InputAction explodeAllAction;
    InputAction infoAction;

    public static XRInputHelper instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        explodeAction = InputSystem.actions.FindAction("Explode");
        explodeAllAction = InputSystem.actions.FindAction("Explode All");
        infoAction = InputSystem.actions.FindAction ("Info");
    }

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
}

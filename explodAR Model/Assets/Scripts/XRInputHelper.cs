using UnityEngine;
using UnityEngine.InputSystem;

public class XRInputHelper : MonoBehaviour
{
    InputAction explodeAction;
    InputAction infoAction;

    public static XRInputHelper instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        explodeAction = InputSystem.actions.FindAction("Explode");
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
}

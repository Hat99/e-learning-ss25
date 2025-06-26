using UnityEngine;
using UnityEngine.InputSystem;

public class XRInputHelper : MonoBehaviour
{
    InputAction explodeAction;
    InputAction explodeAllAction;
    InputAction infoAction;

    InputAction trackingTest;
    InputAction trackingTestB;

    public static XRInputHelper instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        explodeAction = InputSystem.actions.FindAction("Explode");
        explodeAllAction = InputSystem.actions.FindAction("Explode All");
        infoAction = InputSystem.actions.FindAction ("Info");
        trackingTest = InputSystem.actions.FindAction("Is Tracked Left");
        trackingTestB = InputSystem.actions.FindAction("Is Tracked Right");
    }

    private void Update()
    {
        if (trackingTest.WasPressedThisFrame())
        {
            Debug.Log("Hover detected from left");
        }
        if (trackingTestB.WasPressedThisFrame())
        {
            Debug.Log("Hover detected from right");
        }
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

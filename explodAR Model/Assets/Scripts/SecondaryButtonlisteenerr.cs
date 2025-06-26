using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SecondaryButtonListener : MonoBehaviour
{
    public InputActionReference secondaryButtonAction;
    public MonoBehaviour targetScript; // Your second script
    private IButtonAction action;
    public XRRayInteractor rayInteractor;

    void OnEnable()
    {
        action = targetScript as IButtonAction;
        secondaryButtonAction.action.performed += OnSecondaryButtonPressed;
        secondaryButtonAction.action.Enable();
    }

    void OnDisable()
    {
        secondaryButtonAction.action.performed -= OnSecondaryButtonPressed;
        secondaryButtonAction.action.Disable();
    }
    void OnSecondaryButtonPressedtest(InputAction.CallbackContext context)
    {
        if (rayInteractor is XRRayInteractor ray && ray.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            Debug.Log("Ray hit: " + hit.transform.name);
            action?.Execute();
        }
    }

    void OnSecondaryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Secondary button pressed!");
        action?.Execute();
    }
}

using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine;

public class DualButtonAction : MonoBehaviour
{
    public InputActionReference selectAction;   // Grip
    public InputActionReference activateAction; // Trigger
    public XRRayInteractor rayInteractor;

    [SerializeField] private MonoBehaviour gripActionScript;    // Must implement IButtonAction
    [SerializeField] private MonoBehaviour triggerActionScript; // Must implement IButtonAction

    private IButtonAction gripAction;
    private IButtonAction triggerAction;

    void OnEnable()
    {
        gripAction = gripActionScript as IButtonAction;
        triggerAction = triggerActionScript as IButtonAction;

        selectAction.action.performed += OnGrip;
        activateAction.action.performed += OnTrigger;
        selectAction.action.Enable();
        activateAction.action.Enable();
    }

    void OnDisable()
    {
        selectAction.action.performed -= OnGrip;
        activateAction.action.performed -= OnTrigger;
        selectAction.action.Disable();
        activateAction.action.Disable();
    }

    void OnGrip(InputAction.CallbackContext ctx)
    {
        if (TryGetPointedObject(out GameObject target) && gripAction != null)
        {
            gripAction.Execute();
        }
    }

    void OnTrigger(InputAction.CallbackContext ctx)
    {
        if (TryGetPointedObject(out GameObject target) && triggerAction != null)
        {
            triggerAction.Execute();
        }
    }

    private bool TryGetPointedObject(out GameObject target)
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            target = hit.transform.gameObject;
            return true;
        }

        target = null;
        return false;
    }
}

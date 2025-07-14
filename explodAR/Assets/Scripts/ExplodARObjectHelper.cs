using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ExplodARObjectHelper : MonoBehaviour
{
    #region fields

    //references to the explodable and info scripts on the same object (if they're used)
    public Explodable explodable;
    public Info info;

    //an outline to show explodable objects while hovering over them
    private Outline _outline;

    #endregion fields



    #region unity methods

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //add an outline to the object (using Quick Outline plugin by Chris Nolet)
        _outline = gameObject.AddComponent<Outline>();
        _outline.OutlineMode = Outline.Mode.OutlineAll;

        _outline.OutlineWidth = ExplodARController.instance.outlineWidth;
        _outline.OutlineColor = ExplodARController.instance.outlineColor;

        _outline.enabled = false;

        //add a mesh collider to make object interactible
        gameObject.AddComponent<MeshCollider>();

        //add XR simple interactible script
        XRSimpleInteractable simpleInteractable = gameObject.AddComponent<XRSimpleInteractable>();
        simpleInteractable.firstHoverEntered.AddListener(delegate { SetOutlineEnabled(true); });
        simpleInteractable.lastHoverExited.AddListener(delegate { SetOutlineEnabled(false); });
    }

    // Update is called once per frame
    void Update()
    {
        //listen for explode / info inputs if the object is selected
        if (_outline.enabled)
        {
            //trigger functions if the scripts are on the object
            if (explodable != null && XRInputHelper.instance.explodeActionPressedThisFrame)
            {
                explodable.ToggleExplosion();
            }
            if (info != null && XRInputHelper.instance.infoActionPressedThisFrame)
            {
                info.ToggleInfo();
            }
        }
    }

    #endregion unity methods



    #region methods

    //sets status of object outline
    public void SetOutlineEnabled(bool value)
    {
        //enable the outline only if the object can explode or has info to display
        //(otherwise objects aren't interactable and this script shouldn't even be on them)
        bool hasInfo = info != null;
        bool canExplode = explodable != null && explodable.CanExplode();
        _outline.enabled = value && (hasInfo || canExplode);
    }

    #endregion methods
}

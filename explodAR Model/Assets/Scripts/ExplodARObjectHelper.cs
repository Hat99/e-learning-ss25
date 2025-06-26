using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ExplodARObjectHelper : MonoBehaviour
{
    public Explodable explodable;
    public Info info;
    //an outline to show explodable objects while hovering over them
    private Outline _outline;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //add an outline to the object
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

    public void SetOutlineEnabled(bool value)
    {
        //enable the outline only if the object can explode or has info to display
        bool hasInfo = info != null;
        bool canExplode = explodable != null && explodable.CanExplode();
        _outline.enabled = value && (hasInfo || canExplode);
    }
}

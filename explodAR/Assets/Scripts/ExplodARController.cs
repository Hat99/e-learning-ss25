using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ExplodARController : MonoBehaviour
{
    #region fields

    //event for triggering all explosions
    public UnityEvent<bool> explodeAllEvent = new UnityEvent<bool>();

    [Tooltip("How many seconds explosions take")]
    public float explosionDuration;

    [Tooltip("The information box template object")]
    public GameObject infoTemplate;

    //parameters for outlines
    public float outlineWidth;
    public Color outlineColor;

    //global explosion state
    private bool _exploded;

    //make the class available as pseudo-static
    public static ExplodARController instance;

    #endregion fields



    #region unity methods

    private void Awake()
    {
        if (explodeAllEvent == null)
            explodeAllEvent = new UnityEvent<bool>();

        instance = this;   
    }

    private void Update()
    {
        //listen for hotkeys (keyboard only, not really needed anymore)
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            ExplodeAll();
        }
        if (Keyboard.current[Key.R].wasPressedThisFrame)
        {
            ImplodeAll();
        }

        //listen for explode all input
        if (XRInputHelper.instance.explodeAllActionPressedThisFrame)
        {
            ToggleExplosion();
        }
    }

    #endregion unity methods



    #region event invocations

    //invoke explode all event
    public void ExplodeAll()
    {
        _exploded = true;
        explodeAllEvent.Invoke(_exploded);
    }

    //invoke implode all event
    public void ImplodeAll()
    {
        _exploded = false;
        explodeAllEvent.Invoke(_exploded);
    }

    //toggle current global explosion state
    public void ToggleExplosion()
    {
        _exploded = !_exploded;
        explodeAllEvent.Invoke(_exploded);
    }

    #endregion event incovations
}

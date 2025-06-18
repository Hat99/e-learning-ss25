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
        //listen for hotkeys
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            ExplodeAll();
        }
        if (Keyboard.current[Key.R].wasPressedThisFrame)
        {
            ImplodeAll();
        }
    }

    #endregion unity methods



    #region event invocations

    public void ExplodeAll()
    {
        explodeAllEvent.Invoke(true);
    }

    public void ImplodeAll()
    {
        explodeAllEvent.Invoke(false);
    }

    #endregion event incovations
}

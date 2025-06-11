using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ExplodARController : MonoBehaviour
{
    public UnityEvent<bool> explodeAllEvent = new UnityEvent<bool>();

    [Tooltip("How many seconds explosions take")]
    public float explosionDuration;

    [Tooltip("The information box template object")]
    public GameObject infoTemplate;

    public static ExplodARController instance;
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

    public void ExplodeAll()
    {
        explodeAllEvent.Invoke(true);
    }

    public void ImplodeAll()
    {
        explodeAllEvent.Invoke(false);
    }

}

using TMPro;
using UnityEngine;

public class ExplodARController : MonoBehaviour
{
    [Tooltip("A template to use as a button for exploding objects")]
    public GameObject explodeButtonTemplate;
    [Tooltip("A template to use as a button for opening an object's info box")]
    public GameObject infoButtonTemplate;

    [Tooltip("How far the button should be placed from its object")]
    public float buttonDistanceFromObject;

    [Tooltip("How many seconds explosions take")]
    public float explosionDuration;

    

    public static ExplodARController instance;
    private void Awake()
    {
        instance = this;   
    }
}

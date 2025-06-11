using TMPro;
using UnityEngine;

public class InfoTemplate : MonoBehaviour
{

    [Tooltip("The title text object")]
    public TextMeshProUGUI infoTemplateTitle;

    [Tooltip("The container for the info boxes")]
    public GameObject infoContainer;

    [Tooltip("The info content text object")]
    public TextMeshProUGUI infoTemplateTextContent;

    public static InfoTemplate instance;
    private void Awake()
    {
        instance = this;
    }
}

using TMPro;
using UnityEngine;

public class InfoTemplate : MonoBehaviour
{
    [Tooltip("Set automatically")]
    public GameObject infoTemplate;

    [Tooltip("The title text object")]
    public TextMeshProUGUI infoTemplateTitle;

    [Tooltip("The info content text object")]
    public TextMeshProUGUI infoTemplateTextContent;

    public static InfoTemplate instance;
    private void Awake()
    {
        instance = this;
        infoTemplate = gameObject;
    }
}

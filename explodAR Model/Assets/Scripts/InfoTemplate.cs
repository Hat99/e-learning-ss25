using TMPro;
using UnityEngine;

public class InfoTemplate : MonoBehaviour
{
    #region fields

    [Tooltip("The title text object")]
    public TextMeshProUGUI infoTemplateTitle;

    [Tooltip("The container for the info boxes")]
    public GameObject infoContainer;

    [Tooltip("The header for info boxes")]
    public TextMeshProUGUI infoTemplateHeader;

    [Tooltip("The info content text object")]
    public TextMeshProUGUI infoTemplateTextContent;

    //make the class available as pseudo-static
    public static InfoTemplate instance;

    #endregion fields



    #region unity methods

    private void Awake()
    {
        instance = this;
    }

    #endregion unity methods
}

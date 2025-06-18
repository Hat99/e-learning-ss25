using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{
    #region infoObject

    //data definition for a block of information to be displayed in the info box
    [Serializable]
    public class InfoObject
    {
        public enum Type
        {
            paragraph,
            media
        }

        [Tooltip("Which type of info block")]
        public Type type;

        [Tooltip("The header of the section")]
        public string header;

        //unimplemented!
        [Tooltip("Wether or not the section is collapsible")]
        public bool collapsible;

        [Tooltip("The information text or media source")]
        public string text;
    }

    #endregion infoObject



    #region fields

    [Tooltip("The title of the info box")]
    public string title;
    
    [Tooltip("The header and information texts to be displayed (in order)")]
    [SerializeField]
    public List<InfoObject> informationObjects = new List<InfoObject>();

    //the info box object
    private GameObject _infoBox;

    //wether or not the info box is currently active
    private bool _infoShown = false;

    #endregion fields



    #region unity methods

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //instantiate the info box
        _infoBox = Instantiate(ExplodARController.instance.infoTemplate);
        _infoBox.transform.SetParent(transform, true);
        _infoBox.transform.position = transform.position;

        //TODO: make this dynamic
        _infoBox.transform.Translate(new Vector3(0, .75f, 0));
        InfoTemplate info = _infoBox.GetComponent<InfoTemplate>();

        info.infoTemplateTitle.text = title;

        //fill the info box's scroll view
        TextMeshProUGUI tmp;
        foreach (InfoObject obj in informationObjects)
        {
            //set the header if it's used
            if (obj.header != "")
            {
                tmp = Instantiate(info.infoTemplateHeader);
                tmp.text = obj.header;
                tmp.transform.SetParent(info.infoContainer.transform, false);
                tmp.gameObject.SetActive(true);
            }

            //create the info based on type
            switch (obj.type)
            {
                case InfoObject.Type.paragraph:
                    tmp = Instantiate(info.infoTemplateTextContent);
                    tmp.text = obj.text;
                    tmp.transform.SetParent(info.infoContainer.transform, false);
                    tmp.gameObject.SetActive(true);
                    break;
                case InfoObject.Type.media:
                    //unimplemented!
                    break;
                default:
                    Debug.Log("Unimplemented type");
                    break;
            }
        }
    }

    #endregion unity methods



    #region methods

    //toggles the info box active or inactive
    public void ToggleInfo()
    {   
        _infoShown = !_infoShown;
        if (_infoShown)
        {
            SetInfoBoxRotationToView();
        }
        _infoBox.SetActive(_infoShown);
    }

    //aligns the info box to face the camera
    private void SetInfoBoxRotationToView()
    {
        _infoBox.transform.rotation = Camera.main.transform.rotation;
    }

    #endregion methods
}

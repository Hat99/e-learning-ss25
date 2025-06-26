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

    public Vector3 infoOffset = new Vector3(0, 0.75f, 0);

    //the info box object
    private GameObject _infoBox;

    //wether or not the info box is currently active
    private bool _infoShown = false;

    #endregion fields



    #region unity methods

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ensure that the object has an explodARObjectHelper
        ExplodARObjectHelper helper = gameObject.GetComponent<ExplodARObjectHelper>();
        if (helper == null)
        {
            helper = gameObject.AddComponent<ExplodARObjectHelper>();
        }
        //register this script with the helper
        helper.info = this;

        //instantiate the info box
        _infoBox = Instantiate(ExplodARController.instance.infoTemplate);
        _infoBox.transform.SetParent(transform, true);
        _infoBox.transform.position = transform.position;

        //TODO: make this dynamic
        _infoBox.transform.Translate(infoOffset);
        InfoTemplate info = _infoBox.GetComponent<InfoTemplate>();

        info.infoTemplateTitle.text = title;

        string text = "";
        foreach(InfoObject obj in informationObjects)
        {
            if(obj.header != "")
            {
                text += obj.header + "\n";
            }
            if(obj.text != "")
            {
                text += obj.text + "\n\n";
            }
        }

        TextMeshProUGUI tmp = Instantiate(info.infoTemplateTextContent);
        tmp.text = text;
        tmp.transform.SetParent(info.infoContainer.transform, false);
        tmp.gameObject.SetActive(true);

        //fill the info box's scroll view
        //TextMeshProUGUI tmp;
        //foreach (InfoObject obj in informationObjects)
        //{
        //    //set the header if it's used
        //    if (obj.header != "")
        //    {
        //        tmp = Instantiate(info.infoTemplateHeader);
        //        tmp.text = obj.header;
        //        tmp.transform.SetParent(info.infoContainer.transform, false);
        //        tmp.gameObject.SetActive(true);
        //    }

        //    //create the info based on type
        //    switch (obj.type)
        //    {
        //        case InfoObject.Type.paragraph:
        //            tmp = Instantiate(info.infoTemplateTextContent);
        //            tmp.text = obj.text;
        //            tmp.transform.SetParent(info.infoContainer.transform, false);
        //            tmp.gameObject.SetActive(true);
        //            break;
        //        case InfoObject.Type.media:
        //            //unimplemented!
        //            break;
        //        default:
        //            Debug.Log("Unimplemented type");
        //            break;
        //    }
        //}
    }

    #endregion unity methods

    #region controller
    public class GripAction : MonoBehaviour, IButtonAction
    {
        public void Execute()
        {
            Debug.Log("Grip action executed!");
            // Your logic here
        }
    }

    #endregion controller

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
        Quaternion rotation = Camera.main.transform.rotation;
        rotation.x = 0;
        rotation.z = 0;
        _infoBox.transform.rotation = rotation;
    }

    #endregion methods
}

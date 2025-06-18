using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Info : MonoBehaviour
{
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

        [Tooltip("Wether or not the section is collapsible")]
        public bool collapsible;

        [Tooltip("The information text or media source")]
        public string text;
    }

    [Tooltip("The title of the info box")]
    public string title;
    
    [Tooltip("The header and information texts to be displayed (in order)")]
    [SerializeField]
    public List<InfoObject> informationObjects = new List<InfoObject>();

    private GameObject _infoBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _infoBox = Instantiate(ExplodARController.instance.infoTemplate);
        _infoBox.transform.SetParent(transform, true);
        _infoBox.transform.position = transform.position;
        _infoBox.transform.Translate(new Vector3(0, .75f, 0));
        InfoTemplate info = _infoBox.GetComponent<InfoTemplate>();

        info.infoTemplateTitle.text = title;
        TextMeshProUGUI tmp;
        foreach (InfoObject obj in informationObjects)
        {
            if (obj.header != "")
            {
                tmp = Instantiate(info.infoTemplateHeader);
                tmp.text = obj.header;
                tmp.transform.SetParent(info.infoContainer.transform, false);
                tmp.gameObject.SetActive(true);
            }
            switch (obj.type)
            {
                case InfoObject.Type.paragraph:
                    tmp = Instantiate(info.infoTemplateTextContent);
                    tmp.text = obj.text;
                    tmp.transform.SetParent(info.infoContainer.transform, false);
                    tmp.gameObject.SetActive(true);
                    break;
                case InfoObject.Type.media:
                    break;
                default:
                    Debug.Log("Unimplemented type");
                    break;
            }
        }
    }

    // Update is called once per frame
    private bool _infoShown = false;
    void Update()
    {
        //if (Keyboard.current[Key.I].wasPressedThisFrame)
        //{
        //    infoShown = !infoShown;
        //    ShowInfo(infoShown);
        //}
    }

    public void ToggleInfo()
    {   
        _infoShown = !_infoShown;
        _infoBox.SetActive(_infoShown);
    }

}

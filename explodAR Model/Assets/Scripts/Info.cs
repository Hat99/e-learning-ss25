using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

        private TextMeshProUGUI _headerText;
        private TextMeshProUGUI _contentText;
    }

    [Tooltip("The title of the info box")]
    public string title;
    
    [Tooltip("The header and information texts to be displayed (in order)")]
    [SerializeField]
    public List<InfoObject> informationObjects = new List<InfoObject>();

    private GameObject _infoBox;
    private TextMeshProUGUI _titleText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _infoBox = Instantiate(InfoTemplate.instance.infoTemplate);
        InfoTemplate info = _infoBox.GetComponent<InfoTemplate>();

        //_titleText = Instantiate(ExplodARController.instance.infoTemplateTitle);
        _titleText.text = title;

        _infoBox.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

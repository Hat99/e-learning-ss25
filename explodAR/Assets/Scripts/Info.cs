using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    [Serializable]
    public class InfoObject
    {
        [Tooltip("The header of the section")]
        public string header;

        [Tooltip("Wether or not the section is collapsible")]
        public bool collapsible;

        [Tooltip("A media source from the assets to use")]
        public string mediaSource;

        [Tooltip("The information text or media description")]
        public string text;
    }

    [Tooltip("The title of the info box")]
    public string title;
    
    [Tooltip("The header and information texts to be displayed (in order)")]
    [SerializeField]
    public List<InfoObject> informationObjects = new List<InfoObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DebugInfo();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DebugInfo()
    {
        Debug.Log(title);
        foreach(InfoObject info in informationObjects)
        {
            Debug.Log(info.header);
            Debug.Log(info.collapsible);
            Debug.Log(info.mediaSource);
            Debug.Log(info.text);
        }
    }
}

using UnityEngine;

public class MenuToggler : MonoBehaviour
{
    public GameObject menu;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (XRInputHelper.instance.menuActionPressedThisFrame)
        {
            menu.SetActive(!menu.activeSelf);
            menu.transform.position = player.transform.position;
            menu.transform.localPosition += player.transform.forward;
            menu.transform.rotation = player.transform.rotation;
        }
    }
}

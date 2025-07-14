using UnityEngine;

public class MenuToggler : MonoBehaviour
{
    #region fields

    [Tooltip("the gameobject containing the main menu canvas")]
    public GameObject menu;

    [Tooltip("the gameobject to use when referencing current player position and rotation (e.g. camera)")]
    public GameObject player;

    #endregion fields



    #region unity methods

    // Update is called once per frame
    void Update()
    {
        if (XRInputHelper.instance.menuActionPressedThisFrame)
        {
            menu.SetActive(!menu.activeSelf);

            //set position and rotation to match player camera
            menu.transform.position = player.transform.position;
            menu.transform.localPosition += player.transform.forward;
            menu.transform.rotation = player.transform.rotation;
        }
    }

    #endregion unity methods
}

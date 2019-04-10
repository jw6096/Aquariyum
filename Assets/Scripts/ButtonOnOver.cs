using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnOver : MonoBehaviour
{
    public Image description;

    public void OnMouseEnter()
    {
        description.enabled = true;
        Debug.Log("enter");
    }
    public void OnMouseExit()
    {
        description.enabled = false;
    }
}

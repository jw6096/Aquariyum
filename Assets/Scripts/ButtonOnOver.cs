using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonOnOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable == true)
        {
            description.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        description.SetActive(false);
    }
}

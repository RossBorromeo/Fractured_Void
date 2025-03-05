using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  // attach it to each window 
    public GameObject InfoBox;// for the alters info box
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InfoBox != null)
        {
            InfoBox.SetActive(true); // Show the info panel when hovering over the window
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InfoBox != null)
        {
            InfoBox.SetActive(false); // Hide the info panel when not hovering
        }
    }
}

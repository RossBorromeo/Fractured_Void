using System;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI.Table;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine;

public class InteractPromptArea: MonoBehaviour
{
    public Canvas canvas; // Reference to the prompt canvas
    public GameObject item; // Reference to the door object

    void Start()
    {
        if (canvas != null)
            canvas.gameObject.SetActive(false); // Hide canvas at the start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if player enters the interaction zone
        {
            if (canvas != null)
                canvas.gameObject.SetActive(true); // Show prompt
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canvas != null)
                canvas.gameObject.SetActive(false); // Hide prompt when player leaves
        }
    }
}

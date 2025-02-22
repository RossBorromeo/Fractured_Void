using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    public Canvas canvas; // Reference to the prompt canvas
    public GameObject key; // Reference to the key object

    void Start()
    {
        if (canvas != null)
            canvas.gameObject.SetActive(false); // Hide canvas at the start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player enters
        {
            if (canvas != null && key != null && key.activeSelf)
            {
                canvas.gameObject.SetActive(true); // Show the canvas if the key is active
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canvas != null)
                canvas.gameObject.SetActive(false); // Hide canvas when player exits trigger
        }
    }

    private void LateUpdate()
    {
        // Ensure the canvas hides when the key is deactivated
        if (key != null && !key.activeSelf && canvas != null && canvas.gameObject.activeSelf)
        {
            //Debug.Log("Key is inactive, hiding canvas");
            canvas.gameObject.SetActive(false);
        }
    }
}

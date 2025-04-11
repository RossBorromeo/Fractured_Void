using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTrigger : MonoBehaviour
{
    public Canvas promptCanvas;
    public Canvas letterCanvas; 
    public GameObject paperPlane ; // The plane object 

    private bool playerInTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (letterCanvas != null)
            {
                letterCanvas.gameObject.SetActive(true);
            }

            if (paperPlane != null)
            {
                paperPlane.SetActive(false);
                promptCanvas.gameObject.SetActive(false);
            }


            playerInTrigger = false; // Optional: to prevent repeated activations
        }
    }
}

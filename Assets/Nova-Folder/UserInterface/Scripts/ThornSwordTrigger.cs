using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornSwordTrigger : MonoBehaviour
{
   
    public Canvas promptCanvas;
    public GameObject thornSword; // The thorn object 

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

            if (thornSword != null)
            {
                thornSword.SetActive(false);
                promptCanvas.gameObject.SetActive(false);
            }


            playerInTrigger = false; //  prevent repeated activations
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTrigger : MonoBehaviour
{

    public Canvas letterCanvas; 
    public GameObject paperPlane ; // The plane object 

   
    private void OnTriggerEnter(Collider other)
    {
        // check if player enters and presses key
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player")) 
        {
            // Make the canvas visible
            if (letterCanvas!= null)
            {
                letterCanvas.gameObject.SetActive(true); // Show the canvas
            }

            
            if (paperPlane != null)
            {
                // Destroy the object
                //Destroy(paperPlane);
 
               paperPlane.SetActive(false); 
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintButton : MonoBehaviour
{
   // shows the hint button when the player enters the triggerbox
    public GameObject hintButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hintButton != null)
        {
            hintButton.SetActive(true);
        }
    }
}


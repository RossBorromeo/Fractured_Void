using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskTrigger : MonoBehaviour
{
    public TMP_Text taskText;  //  task UI text in the Inspector
   
    private bool taskActivated = false; // ensures task is only triggered once

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !taskActivated)
        {
            ActivateTask();
            taskActivated = true; // Prevents multiple triggers
        }
    }

    private void ActivateTask()
    {
        if (taskText != null)
        {
           
            taskText.gameObject.SetActive(true); // Make sure the text is visible
        }
        
    }
}


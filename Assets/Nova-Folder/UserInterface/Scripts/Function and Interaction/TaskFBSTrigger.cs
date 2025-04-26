using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFBSTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the flag in TaskCompletionManagerFBS
            TaskCompletionManagerFBS.Instance.hasEnteredDialogueFBSTrigger = true;

            // Optionally, destroy or disable this trigger so it doesn't fire again
            Destroy(gameObject);
        }
    }
}
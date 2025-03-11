using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string[] dialogueLines; // set these in Inspector
    public string npcName; // assign "Oliver" or "Mabel" in Inspector
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Return))
        {
            DialogueManager.Instance.StartDialogue(dialogueLines, npcName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}


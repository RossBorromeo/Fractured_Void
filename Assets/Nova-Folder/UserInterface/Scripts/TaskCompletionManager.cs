using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskCompletionManager : MonoBehaviour
{
    // TextBoxes for the bedroom level
    // for assigning the ui text boxes with the bedroom task prompts 
    public TMP_Text keyTaskText;
    public TMP_Text doorTaskText;


    // TextBoxes for garden level




    public string keyID = "KeyOne";
    public Animator doorAnimator;       // assign the Animator of the door
    private bool keyCollected = false;
    private bool doorOpened = false;

    void Start()
    {
        if (doorAnimator == null)
        {
            Debug.LogError("Door Animator is not assigned in TaskCompletionManager!");
        }
    }
    void Update()
    {
        // checking if the key has been collected
        if (!keyCollected && PlayerInventory.Instance.HasKey(keyID))
        {
            keyCollected = true;
            UpdateTaskText(keyTaskText, "Find the key");
        }

        if (!doorOpened && doorAnimator != null)
        {
            // Check if the current animation state is "BedDoorOpening"
            AnimatorStateInfo stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("BedDoorOpening") && stateInfo.normalizedTime >= 0.5f)
            {
                Debug.Log("Current Animation State: " + doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("BedDoorOpening"));
                doorOpened = true;
                UpdateTaskText(doorTaskText, "open the DOOr");
            }
        }
        
        // checking if the door is opened (collider is disabled)
        // GameObject door = GameObject.Find(doorID);
        //if (!doorOpened && door != null && !door.GetComponent<Collider>().enabled)
        // {
        //     doorOpened = true;
        //      UpdateTaskText(doorTaskText, "open Door");
        //  }

    }
    private void UpdateTaskText(TMP_Text taskText, string taskName)
    {
        if (taskText != null)
        {
            taskText.text = "<s>" + taskName + "</s>"; // applies strikethrough format to the tasks
        }
    }
}

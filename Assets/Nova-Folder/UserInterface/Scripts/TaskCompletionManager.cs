using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskCompletionManager : MonoBehaviour
{
    public static TaskCompletionManager Instance; // singleton

    // TextBoxes for the bedroom level
    // for assigning the ui text boxes with the bedroom task prompts 
    public TMP_Text keyTaskText;
    public TMP_Text doorTaskText;


    // TextBoxes for garden level
    public TMP_Text findRoseTaskText;
    public TMP_Text findFlowersTaskText;
    public TMP_Text placeFlowersTaskText;


    // bedroom

    public string keyID = "KeyOne";
    public Animator doorAnimator;       // assign the Animator of the door
    private bool keyCollected = false;
    private bool doorOpened = false;

    // garden
   public bool flowersCollected = false;
   public bool flowersPlaced = false;
   public bool roseFound = false;

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
            // check if the current animation state is "BedDoorOpening"
            AnimatorStateInfo stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("BedDoorOpening") && stateInfo.normalizedTime >= 0.5f)
            {
                Debug.Log("Current Animation State: " + doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("BedDoorOpening"));
                doorOpened = true;
                UpdateTaskText(doorTaskText, "open the DOOr");
            }
        }
        
        if (flowersCollected == true)
        {
            UpdateTaskText(findFlowersTaskText, "Collect All Seasonal flowers");
        }
        if (flowersPlaced == true)
        {
            UpdateTaskText(placeFlowersTaskText, "Put flowers  in the right place");
        }
        if (roseFound == true)
        {
            UpdateTaskText(findRoseTaskText, "Find Rose");
        }


    }
    public void UpdateTaskText(TMP_Text taskText, string taskName)
    {
        if (taskText != null)
        {
            taskText.text = "<s>" + taskName + "</s>"; // applies strikethrough format to the tasks
        }
    }
}

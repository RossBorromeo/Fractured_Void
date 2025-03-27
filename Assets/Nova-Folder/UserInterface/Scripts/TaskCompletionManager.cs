using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskCompletionManager : MonoBehaviour
{
    public static TaskCompletionManager Instance; // Singleton instance

    // TextBoxes for the bedroom level
    // for assigning the UI text boxes with the bedroom task prompts 
    public TMP_Text keyTaskText;
    public TMP_Text doorTaskText;

    // TextBoxes for the garden level
    public TMP_Text findRoseTaskText;
    public TMP_Text findFlowersTaskText;
    public TMP_Text placeFlowersTaskText;

    // bedroom variables
    public string keyID = "KeyOne";  // Unique ID for the key
    public Animator doorAnimator;    // Assign the Animator of the door
    private bool keyCollected = false; // Tracks if the key has been collected
    private bool doorOpened = false;  // Tracks if the door has been opened

    // garden variables
    public bool flowersCollected = false;  // Tracks if all flowers have been collected
    public bool flowersPlaced = false;     // Tracks if flowers have been placed correctly
    public bool roseFound = false;         // Tracks if Rose has been found

    private void Awake()
    {
        // Singleton //  ensures only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (doorAnimator == null)
        {
            //Debug.LogError("Door Animator is not assigned in TaskCompletionManager!");
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

        // checking if the door has been opened
        if (!doorOpened && doorAnimator != null)
        {
            // check if the current animation state is "BedDoorOpening"
            AnimatorStateInfo stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("BedDoorOpening") && stateInfo.normalizedTime >= 0.5f)
            {
                Debug.Log("Current Animation State: " + doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("BedDoorOpening"));
                doorOpened = true;
                UpdateTaskText(doorTaskText, "Open the door");
            }
        }

        // checking if flowers have been collected
        if (flowersCollected == true)
        {
            UpdateTaskText(findFlowersTaskText, "Collect All Seasonal flowers");
        }

        // checking if flowers have been placed
        if (flowersPlaced == true)
        {
            UpdateTaskText(placeFlowersTaskText, "Put flowers in the right place");
        }

        // checking if Rose has been found
        if (roseFound == true)
        {
            UpdateTaskText(findRoseTaskText, "Find Rose");
        }
    }

    // Function to update and cross out completed tasks in the UI
    public void UpdateTaskText(TMP_Text taskText, string taskName)
    {
        if (taskText != null)
        {
            taskText.text = "<s>" + taskName + "</s>"; // applies strikethrough format to the tasks
        }
    }
}

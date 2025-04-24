using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskCompletionManagerRoseGarden : MonoBehaviour
{

    public static TaskCompletionManagerRoseGarden Instance; // Singleton instance

    // TextBoxes for the garden level
    public TMP_Text findRoseTaskText;
    public TMP_Text findFlowersTaskText;
    public TMP_Text placeFlowersTaskText;

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

    private void Update()
    {
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

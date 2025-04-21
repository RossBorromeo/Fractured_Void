using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TaskCompletionManagerFBS : MonoBehaviour
{

    public static TaskCompletionManagerFBS Instance; // Singleton instance

    // TextBoxes for the flashback scene
    [Header("Flashback Journal Task Texts:")]
    public TMP_Text talkToRoseTaskText;
    public TMP_Text planeTaskText;
    public TMP_Text roseGiftTaskText;
    [Space]

    // Scene variables
    private bool hasTalked = false;  // Tracks if rose has been talked to 
    private bool hasRead = false;     // Tracks if letter has been read
    private bool giftFound = false;  // Tracks if Rose's gift has been found

   
    [Space]

    [Header("Triggers for Journal:")]
    public GameObject dialogueTriggerBox;// dialogue triggered
    public GameObject planeCollider;
    public GameObject thornSword;

    public bool hasEnteredDialogueFBSTrigger = false;
    

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
        if (!hasTalked && hasEnteredDialogueFBSTrigger)
        {
            hasTalked = true;
            UpdateTaskText(talkToRoseTaskText, "Talk to Rose");
        }

        if (planeCollider != null && !planeCollider.activeInHierarchy && !hasRead)
        {
            hasRead = true;
            UpdateTaskText(planeTaskText, "Paper Plane?");
        }
       
        if (!giftFound && ThornSwordTrigger.Instance != null && ThornSwordTrigger.Instance.pickedUpSword)
        {
            giftFound = true;
            UpdateTaskText(roseGiftTaskText, "Rose's Gift");
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

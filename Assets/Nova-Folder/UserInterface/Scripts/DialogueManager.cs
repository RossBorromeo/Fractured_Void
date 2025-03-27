using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor.Rendering;

public class DialogueManager : MonoBehaviour
{
    

    public TextMeshProUGUI oliverDialogueText; // Oliver's TextBox
    public TextMeshProUGUI mabelDialogueText; // Mabel's TextBox

    public GameObject oliverDialogueBox; // Oliver's UI DialogueBox
    public GameObject mabelDialogueBox; // Mabel's UI DialogueBox

    public GameObject oliverBust; // Oliver's UI Bust 
    public GameObject mabelBust; // Mabel's UI Bust 

    private Queue<DialogueLine> dialogueQueue = new Queue<DialogueLine>();// Stores dialogue lines
    private bool isDialogueActive = false;// Track if dialogue is running
    private System.Action onDialogueEndCallback;// stores the callback for the pause 

    private PlayerMovement playerMovement; // reference to player movement script

    public static DialogueManager Instance; // singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // finds player and gets the PlayerMovement script
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }

    }

    public void StartDialogue(List<DialogueLine> lines, System.Action onDialogueEnd=null)
    {

        onDialogueEndCallback = onDialogueEnd;// stores the callback
        dialogueQueue.Clear();

        foreach (DialogueLine line in lines)
        {
            dialogueQueue.Enqueue(line);
        }

        isDialogueActive = true;
        PausePlayerMovement(); // Pause movement when dialogue starts

        DisplayNextLine();
    }

    void Update()
    {
        if (isDialogueActive && (Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.Mouse0)))
        {
            DisplayNextLine();
        }
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = dialogueQueue.Dequeue();
        
        //hides all Dialogue elements at first 
        mabelDialogueBox.SetActive(false);
        oliverDialogueBox.SetActive(false);
        oliverBust.SetActive(false);
        mabelBust.SetActive(false);

        //shows all the correct character's dialogue box and busts
        if (currentLine.speakerName == "Oliver")
        {
            oliverDialogueBox.SetActive(true);  // show Oliver's dialogue UI, including text
            oliverBust.SetActive(true);
            oliverDialogueText.text = currentLine.text;
        }
        else if (currentLine.speakerName == "Mabel")
        {
            mabelDialogueBox.SetActive(true);
            mabelBust.SetActive(true);
            mabelDialogueText.text = currentLine.text;
        }
        


    }

    public void EndDialogue()
    {
        mabelDialogueBox.SetActive(false);
        oliverDialogueBox.SetActive(false);
        mabelBust.SetActive(false);
        oliverBust.SetActive(false);
        isDialogueActive = false;

        ResumePlayerMovement(); // resumes movement when dialogue ends

        if (onDialogueEndCallback != null)
        {  
            onDialogueEndCallback?.Invoke();// calls the function to resume the game 
        }

    }

    private void PausePlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.SetMovementEnabled(false); // freezes player
        }
    }

    private void ResumePlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.SetMovementEnabled(true); // unfreezes player
        }
    }


}

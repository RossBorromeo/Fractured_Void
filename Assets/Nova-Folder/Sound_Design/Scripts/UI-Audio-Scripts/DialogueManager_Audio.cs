using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager_Audio : MonoBehaviour
{
    [Header("Speaker Text boxes:")]
    public TextMeshProUGUI oliverDialogueText; // Oliver's TextBox
    public TextMeshProUGUI mabelDialogueText; // Mabel's TextBox
    public TextMeshProUGUI roseDialogueText;  // Rose's TextBox

    [Header("Speaker Dialogue boxes:")]
    public GameObject oliverDialogueBox; // Oliver's UI DialogueBox
    public GameObject mabelDialogueBox;  // Mabel's UI DialogueBox
    public GameObject roseDialogueBox;   // Rose's UI DialogueBox

    [Header("Speaker Bust Image:")]
    public GameObject oliverBust; // Oliver's UI Bust 
    public GameObject mabelBust;  // Mabel's UI Bust 
    public GameObject roseBust;   // Rose's UI Bust 

    [Header("Audio Settings:")]
    public AudioSource dialogueAudioSource; // Audio source for dialogue
    public AudioClip oliverSound;           // Sound for Oliver
    public AudioClip mabelSound;            // Sound for Mabel
    public AudioClip roseSound;             // Sound for Rose

    private Queue<DialogueLine_Audio> dialogueQueue = new Queue<DialogueLine_Audio>();
    private bool isDialogueActive = false; // Track if dialogue is running
    private System.Action onDialogueEndCallback; // Stores the callback for the pause
    private PlayerMovement playerMovement; // Reference to player movement script

    public static DialogueManager_Audio Instance; // Singleton

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

        // Finds player and gets the PlayerMovement script
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    public void StartDialogue(List<DialogueLine_Audio> lines, System.Action onDialogueEnd = null)
    {
        onDialogueEndCallback = onDialogueEnd; // Stores the callback
        dialogueQueue.Clear();

        foreach (DialogueLine_Audio line in lines)
        {
            dialogueQueue.Enqueue(line);
        }

        isDialogueActive = true;
        PausePlayerMovement(); // Pause movement when dialogue starts

        DisplayNextLine();
    }

    void Update()
    {
        if (isDialogueActive && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0)))
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

        DialogueLine_Audio currentLine = dialogueQueue.Dequeue();

        // Hides all dialogue elements at first
        mabelDialogueBox.SetActive(false);
        oliverDialogueBox.SetActive(false);
        roseDialogueBox.SetActive(false);

        oliverBust.SetActive(false);
        mabelBust.SetActive(false);
        roseBust.SetActive(false);

        // Shows the correct character's dialogue box and busts
        if (currentLine.speakerName == "Oliver")
        {
            oliverDialogueBox.SetActive(true);  // Show Oliver's dialogue UI, including text
            oliverBust.SetActive(true);
            oliverDialogueText.text = currentLine.text;

            // Play Oliver's specific sound
            PlayCharacterSound(oliverSound);
        }
        else if (currentLine.speakerName == "Mabel")
        {
            mabelDialogueBox.SetActive(true);
            mabelBust.SetActive(true);
            mabelDialogueText.text = currentLine.text;

            // Play Mabel's specific sound
            PlayCharacterSound(mabelSound);
        }
        else if (currentLine.speakerName == "Rose")
        {
            roseDialogueBox.SetActive(true);
            roseBust.SetActive(true);
            roseDialogueText.text = currentLine.text;

            // Play Rose's specific sound
            PlayCharacterSound(roseSound);
        }

        // Play the audio clip for the current line
        if (currentLine.audioClip is AudioClip audioClip && dialogueAudioSource != null)
        {
            dialogueAudioSource.clip = audioClip;
            dialogueAudioSource.Play();
        }
    }

    public void EndDialogue()
    {
        mabelDialogueBox.SetActive(false);
        oliverDialogueBox.SetActive(false);
        roseDialogueBox.SetActive(false);

        roseBust.SetActive(false);
        mabelBust.SetActive(false);
        oliverBust.SetActive(false);

        isDialogueActive = false;

        // Stop any currently playing audio
        if (dialogueAudioSource != null)
        {
            dialogueAudioSource.Stop();
        }

        ResumePlayerMovement(); // Resumes movement when dialogue ends

        if (onDialogueEndCallback != null)
        {
            onDialogueEndCallback?.Invoke(); // Calls the function to resume the game
        }
    }

    private void PausePlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.SetMovementEnabled(false); // Freezes player
        }
    }

    private void ResumePlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.SetMovementEnabled(true); // Unfreezes player
        }
    }

    private void PlayCharacterSound(AudioClip clip)
    {
        if (dialogueAudioSource != null && clip != null)
        {
            dialogueAudioSource.clip = clip;
            dialogueAudioSource.Play();
        }
    }
}
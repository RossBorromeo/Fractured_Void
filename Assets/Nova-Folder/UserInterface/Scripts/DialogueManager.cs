using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // singleton

    public TextMeshProUGUI oliverDialogueText; // Oliver's TextBox
    public TextMeshProUGUI mabelDialogueText; // Mabel's TextBox

    public GameObject oliverDialogueBox; // Oliver's UI DialogueBox
    public GameObject mabelDialogueBox; // Mabel's UI DialogueBox

    private Queue<string> dialogueQueue = new Queue<string>(); // Stores dialogue lines
    private string currentSpeaker = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(string[] lines, string speaker)
    {
        dialogueQueue.Clear();

        foreach (string line in lines)
        {
            dialogueQueue.Enqueue(line);
        }

        currentSpeaker = speaker;
        ShowCorrectDialogueBox();
        DisplayNextLine();
    }

    private void ShowCorrectDialogueBox()
    {
        // hide both dialogue boxes first
        oliverDialogueBox.SetActive(false);
        mabelDialogueBox.SetActive(false);

        // show the correct dialogue box based on the character
        if (currentSpeaker == "Oliver")
        {
            oliverDialogueBox.SetActive(true);
        }
        else if (currentSpeaker == "Mabel")
        {
            mabelDialogueBox.SetActive(true);
        }
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = dialogueQueue.Dequeue();

        if (currentSpeaker == "Oliver")
        {
            oliverDialogueText.text = line;
        }
        else if (currentSpeaker == "Mabel")
        {
            mabelDialogueText.text = line;
        }
    }

    public void EndDialogue()
    {
        oliverDialogueBox.SetActive(false);
        mabelDialogueBox.SetActive(false);
    }

    void Update()
    {
        if ((oliverDialogueBox.activeSelf || mabelDialogueBox.activeSelf) && Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextLine();
        }
    }
}
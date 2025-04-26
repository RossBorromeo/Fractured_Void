using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource dialogueAudioSource; // The audio source to play dialogue audio
    public AudioClip oliverAudioClip; // Audio clip for Oliver
    public AudioClip mabelAudioClip; // Audio clip for Mabel
    public AudioClip roseAudioClip; // Audio clip for Rose

    private DialogueManager dialogueManager; // Reference to the DialogueManager
    private string lastSpeaker = ""; // Tracks the last speaker to detect changes

    void Start()
    {
        // Find the DialogueManager instance in the scene
        dialogueManager = DialogueManager.Instance;
    }

    void Update()
    {
        if (dialogueManager != null)
        {
            // Check the current speaker
            string currentSpeaker = GetCurrentSpeaker();
            if (currentSpeaker != lastSpeaker)
            {
                PlaySpeakerAudio(currentSpeaker);
                lastSpeaker = currentSpeaker; // Update the last speaker
            }
        }
    }

    private string GetCurrentSpeaker()
    {
        // Check which dialogue box is active and return the corresponding speaker's name
        if (dialogueManager.oliverDialogueBox.activeSelf)
        {
            return "Oliver";
        }
        else if (dialogueManager.mabelDialogueBox.activeSelf)
        {
            return "Mabel";
        }
        else if (dialogueManager.roseDialogueBox.activeSelf)
        {
            return "Rose";
        }
        return "";
    }

    private void PlaySpeakerAudio(string speaker)
    {
        if (dialogueAudioSource == null) return;

        // Select the appropriate audio clip based on the speaker
        AudioClip clipToPlay = null;
        if (speaker == "Oliver")
        {
            clipToPlay = oliverAudioClip;
        }
        else if (speaker == "Mabel")
        {
            clipToPlay = mabelAudioClip;
        }
        else if (speaker == "Rose")
        {
            clipToPlay = roseAudioClip;
        }

        // Play the audio clip if it's not null
        if (clipToPlay != null)
        {
            dialogueAudioSource.clip = clipToPlay;
            dialogueAudioSource.Play();
        }
    }
}

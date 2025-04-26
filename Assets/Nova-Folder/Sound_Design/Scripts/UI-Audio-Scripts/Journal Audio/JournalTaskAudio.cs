using UnityEngine;

public class JournalTaskAudio : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource component to play sound effects
    public AudioClip taskCompleteSound; // Sound effect for task completion

    private bool keyTaskCompleted = false; // Tracks if the key task was completed
    private bool doorTaskCompleted = false; // Tracks if the door task was completed

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in JournalTaskAudio!");
        }

        if (taskCompleteSound == null)
        {
            Debug.LogError("TaskCompleteSound is not assigned in JournalTaskAudio!");
        }
    }

    private void Update()
    {
        // Check if the key task is completed
        if (!keyTaskCompleted && TaskCompletionManager.Instance != null && TaskCompletionManager.Instance.keyTaskText != null)
        {
            if (TaskCompletionManager.Instance.keyTaskText.text.Contains("<s>")) // Check for strikethrough
            {
                keyTaskCompleted = true;
                PlayTaskCompleteSound();
            }
        }

        // Check if the door task is completed
        if (!doorTaskCompleted && TaskCompletionManager.Instance != null && TaskCompletionManager.Instance.doorTaskText != null)
        {
            if (TaskCompletionManager.Instance.doorTaskText.text.Contains("<s>")) // Check for strikethrough
            {
                doorTaskCompleted = true;
                PlayTaskCompleteSound();
            }
        }
    }

    // Method to play the task completion sound effect
    private void PlayTaskCompleteSound()
    {
        if (audioSource != null && taskCompleteSound != null)
        {
            audioSource.PlayOneShot(taskCompleteSound);
        }
    }
}

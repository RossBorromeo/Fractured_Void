using UnityEngine;

public class JournalTaskAudioFBS : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource component to play sound effects
    public AudioClip taskCompleteSound; // Sound effect for task completion

    private bool talkToRoseCompleted = false; // Tracks if the "Talk to Rose" task was completed
    private bool paperPlaneCompleted = false; // Tracks if the "Paper Plane?" task was completed
    private bool roseGiftCompleted = false; // Tracks if the "Rose's Gift" task was completed

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in JournalTaskAudioFBS!");
        }

        if (taskCompleteSound == null)
        {
            Debug.LogError("TaskCompleteSound is not assigned in JournalTaskAudioFBS!");
        }
    }

    private void Update()
    {
        // Check if the "Talk to Rose" task is completed
        if (!talkToRoseCompleted && TaskCompletionManagerFBS.Instance != null && TaskCompletionManagerFBS.Instance.talkToRoseTaskText != null)
        {
            if (TaskCompletionManagerFBS.Instance.talkToRoseTaskText.text.Contains("<s>")) // Check for strikethrough
            {
                talkToRoseCompleted = true;
                PlayTaskCompleteSound();
            }
        }

        // Check if the "Paper Plane?" task is completed
        if (!paperPlaneCompleted && TaskCompletionManagerFBS.Instance != null && TaskCompletionManagerFBS.Instance.planeTaskText != null)
        {
            if (TaskCompletionManagerFBS.Instance.planeTaskText.text.Contains("<s>")) // Check for strikethrough
            {
                paperPlaneCompleted = true;
                PlayTaskCompleteSound();
            }
        }

        // Check if the "Rose's Gift" task is completed
        if (!roseGiftCompleted && TaskCompletionManagerFBS.Instance != null && TaskCompletionManagerFBS.Instance.roseGiftTaskText != null)
        {
            if (TaskCompletionManagerFBS.Instance.roseGiftTaskText.text.Contains("<s>")) // Check for strikethrough
            {
                roseGiftCompleted = true;
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

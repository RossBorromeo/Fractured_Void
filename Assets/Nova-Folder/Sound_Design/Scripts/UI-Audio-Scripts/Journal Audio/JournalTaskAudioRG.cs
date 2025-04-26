using UnityEngine;

public class JournalTaskAudioRG : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource component to play sound effects
    public AudioClip taskCompleteSound; // Sound effect for task completion

    private bool flowersCollectedCompleted = false; // Tracks if the "Collect All Seasonal Flowers" task was completed
    private bool flowersPlacedCompleted = false; // Tracks if the "Put Flowers in the Right Place" task was completed
    private bool roseFoundCompleted = false; // Tracks if the "Find Rose" task was completed

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in JournalTaskAudioRG!");
        }

        if (taskCompleteSound == null)
        {
            Debug.LogError("TaskCompleteSound is not assigned in JournalTaskAudioRG!");
        }
    }

    private void Update()
    {
        // Check if the "Collect All Seasonal Flowers" task is completed
        if (!flowersCollectedCompleted && TaskCompletionManagerRoseGarden.Instance != null && TaskCompletionManagerRoseGarden.Instance.findFlowersTaskText != null)
        {
            if (TaskCompletionManagerRoseGarden.Instance.findFlowersTaskText.text.Contains("<s>")) // Check for strikethrough
            {
                flowersCollectedCompleted = true;
                PlayTaskCompleteSound();
            }
        }

        // Check if the "Put Flowers in the Right Place" task is completed
        if (!flowersPlacedCompleted && TaskCompletionManagerRoseGarden.Instance != null && TaskCompletionManagerRoseGarden.Instance.placeFlowersTaskText != null)
        {
            if (TaskCompletionManagerRoseGarden.Instance.placeFlowersTaskText.text.Contains("<s>")) // Check for strikethrough
            {
                flowersPlacedCompleted = true;
                PlayTaskCompleteSound();
            }
        }

        // Check if the "Find Rose" task is completed
        if (!roseFoundCompleted && TaskCompletionManagerRoseGarden.Instance != null && TaskCompletionManagerRoseGarden.Instance.findRoseTaskText != null)
        {
            if (TaskCompletionManagerRoseGarden.Instance.findRoseTaskText.text.Contains("<s>")) // Check for strikethrough
            {
                roseFoundCompleted = true;
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

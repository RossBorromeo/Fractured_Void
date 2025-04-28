using UnityEngine;

public class Puzzle2Trigger : MonoBehaviour
{
    public AudioSource marigoldAudioSource; // Assign the audio source in the inspector
    public GameObject triggerZone; // Assign the trigger zone in the inspector
    private bool marigoldCollected = false;

    private void Update()
    {
        // Check if the Marigold has been collected (destroyed in the scene)
        if (!marigoldCollected && GameObject.Find("Marigold") == null)
        {
            marigoldCollected = true;

            if (marigoldAudioSource != null)
            {
                marigoldAudioSource.Play();
                Debug.Log("Marigold collected! Audio source activated.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (marigoldCollected && other.CompareTag("Player")) // Ensure the player is the one triggering
        {
            if (marigoldAudioSource != null)
            {
                marigoldAudioSource.Stop();
                Debug.Log("Player passed through the trigger. Audio source stopped.");
            }

            if (triggerZone != null)
            {
                Destroy(triggerZone); // Optionally destroy the trigger zone
                Debug.Log("Trigger zone destroyed.");
            }
        }
    }
}

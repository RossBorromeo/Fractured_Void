using UnityEngine;

public class PlayAudioOnTrigger : MonoBehaviour
{
    public AudioClip fallAudio; // Public field for the audio clip
    private AudioSource audioSource; // Private field for the AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("[PlayAudioOnTrigger] Missing AudioSource component!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            if (audioSource != null && fallAudio != null)
            {
                audioSource.PlayOneShot(fallAudio); // Play the fall audio
            }
        }
    }
}


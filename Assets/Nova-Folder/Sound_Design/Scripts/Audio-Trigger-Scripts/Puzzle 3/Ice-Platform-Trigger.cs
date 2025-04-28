using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class IcePlatformTrigger : MonoBehaviour
{
    [Header("Audio Settings")]
    [Tooltip("The sound to play when the player lands on the platform.")]
    [SerializeField] private AudioClip landingSound;

    private AudioSource audioSource;

    private void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource
        if (landingSound != null)
        {
            audioSource.clip = landingSound;
            audioSource.playOnAwake = false; // Ensure the sound doesn't play on start
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Play the landing sound
            if (landingSound != null)
            {
                audioSource.Play();
            }
        }
    }
}

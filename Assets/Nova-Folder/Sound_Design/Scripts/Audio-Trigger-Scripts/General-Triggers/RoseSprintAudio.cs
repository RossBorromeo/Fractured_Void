using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RoseMovementAudio : MonoBehaviour
{
    [Header("Audio Settings")]
    [Tooltip("The sound to play while Rose is moving.")]
    [SerializeField] private AudioClip movementSound;

    private AudioSource audioSource;
    private Vector3 lastPosition;
    private bool isMoving = false;

    private void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource
        if (movementSound != null)
        {
            audioSource.clip = movementSound;
            audioSource.loop = true; // Loop the movement sound
        }

        // Initialize the last position
        lastPosition = transform.position;
    }

    private void Update()
    {
        // Check if the GameObject is moving
        isMoving = Vector3.Distance(transform.position, lastPosition) > 0.01f;

        if (isMoving)
        {
            // Start playing the sound if not already playing
            if (!audioSource.isPlaying && movementSound != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Stop the sound if the GameObject stops moving
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // Update the last position
        lastPosition = transform.position;
    }

    private void OnDestroy()
    {
        // Stop the audio when the GameObject is destroyed
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

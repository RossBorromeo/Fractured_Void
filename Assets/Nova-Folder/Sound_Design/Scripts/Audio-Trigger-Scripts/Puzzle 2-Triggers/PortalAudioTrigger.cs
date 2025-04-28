using UnityEngine;

[RequireComponent(typeof(PortalTeleport))]
public class PortalAudioTrigger : MonoBehaviour
{
    [Tooltip("Assign a single sound effect to be played when the portal is used.")]
    [SerializeField] private AudioClip portalSound;

    [Tooltip("Assign the portal that will stop the audio when the player enters it.")]
    [SerializeField] private GameObject stopTriggerPortal;

    [Tooltip("Reference to the AudioSource from Puzzle2Trigger to stop.")]
    [SerializeField] private AudioSource puzzle2AudioSource;

    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure an AudioSource component is attached to the portal
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Validate that a sound effect is assigned
        if (portalSound == null)
        {
            Debug.LogWarning($"No portal sound effect assigned on {gameObject.name}!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Check if this is the stop trigger portal
        if (stopTriggerPortal != null && gameObject == stopTriggerPortal)
        {
            StopPuzzle2Audio();
        }
        else
        {
            // Play the sound effect every time the player enters the portal
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (portalSound == null) return;

        // Play the assigned sound effect
        audioSource.PlayOneShot(portalSound);
    }

    private void StopPuzzle2Audio()
    {
        // Stop the Puzzle2Trigger's AudioSource if it's assigned
        if (puzzle2AudioSource != null && puzzle2AudioSource.isPlaying)
        {
            puzzle2AudioSource.Stop();
            Debug.Log("Puzzle2Trigger's audio source stopped by PortalAudioTrigger.");
        }
    }
}

using UnityEngine;

public class PillarAudioTrigger : MonoBehaviour
{
    public AudioSource audioSource; // Expose AudioSource for manual assignment in the Inspector
    public AudioClip flowerPlacedSound; // Single sound effect for all flower placements

    private PlayerFlowerInteration_Audio_UI flowerInteraction;

    // Flag to track if audio has already been played for a placement
    private bool audioPlayedForCurrentPlacement = false;

    private void Start()
    {
        // Find the PlayerFlowerInteration_Audio_UI script in the scene
        flowerInteraction = FindObjectOfType<PlayerFlowerInteration_Audio_UI>();

        if (flowerInteraction == null)
        {
            Debug.LogError("PlayerFlowerInteration_Audio_UI script not found in the scene.");
        }

        // Check if AudioSource is assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned. Please assign it in the Inspector.");
        }
    }

    private void Update()
    {
        if (flowerInteraction == null || audioSource == null) return;

        // Check if any flower is placed and play the sound once per placement
        if (!audioPlayedForCurrentPlacement &&
            (flowerInteraction.placedTulip || flowerInteraction.placedMarigold || flowerInteraction.placedAster || flowerInteraction.placedPoinsettia))
        {
            PlaySound(flowerPlacedSound);
            audioPlayedForCurrentPlacement = true; // Mark as played for the current placement
        }

        // Reset the flag if no flowers are being placed (to allow for the next placement)
        if (!flowerInteraction.placedTulip && !flowerInteraction.placedMarigold && !flowerInteraction.placedAster && !flowerInteraction.placedPoinsettia)
        {
            audioPlayedForCurrentPlacement = false;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

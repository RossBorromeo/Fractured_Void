using UnityEngine;

public class PillarAudioTrigger : MonoBehaviour
{
    public AudioSource audioSource; // Expose AudioSource for manual assignment in the Inspector
    public AudioClip flowerPlacedSound; // Single sound effect for all flower placements

    private PlayerFlowerInteration_Audio_UI flowerInteraction;

    // Flags to track if audio has already been played for each flower placement
    private bool tulipAudioPlayed = false;
    private bool marigoldAudioPlayed = false;
    private bool asterAudioPlayed = false;
    private bool poinsettiaAudioPlayed = false;

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

        // Check if the Tulip is placed and play the sound if it hasn't been played yet
        if (flowerInteraction.placedTulip && !tulipAudioPlayed)
        {
            PlaySound(flowerPlacedSound);
            tulipAudioPlayed = true; // Mark as played
        }

        // Check if the Marigold is placed and play the sound if it hasn't been played yet
        if (flowerInteraction.placedMarigold && !marigoldAudioPlayed)
        {
            PlaySound(flowerPlacedSound);
            marigoldAudioPlayed = true; // Mark as played
        }

        // Check if the Aster is placed and play the sound if it hasn't been played yet
        if (flowerInteraction.placedAster && !asterAudioPlayed)
        {
            PlaySound(flowerPlacedSound);
            asterAudioPlayed = true; // Mark as played
        }

        // Check if the Poinsettia is placed and play the sound if it hasn't been played yet
        if (flowerInteraction.placedPoinsettia && !poinsettiaAudioPlayed)
        {
            PlaySound(flowerPlacedSound);
            poinsettiaAudioPlayed = true; // Mark as played
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

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GateAudioTrigger : MonoBehaviour
{
    [Tooltip("The audio clip to play when the game starts.")]
    [SerializeField] private AudioClip gameAudio;

    private AudioSource audioSource;

    private void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Play the audio if the clip is assigned
        if (gameAudio != null)
        {
            audioSource.clip = gameAudio;
            audioSource.playOnAwake = false; // Ensure it doesn't play automatically
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("GameAudio clip is not assigned in the Inspector.", this);
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GateAudioTrigger : MonoBehaviour
{
    [Tooltip("The audio clip to play when the gate is triggered.")]
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

        // Ensure the audio clip is assigned
        if (gameAudio == null)
        {
            Debug.LogWarning("GameAudio clip is not assigned in the Inspector.", this);
        }
    }

    // Public method to play the audio
    public void TriggerGateAudio()
    {
        if (gameAudio != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameAudio);
            Debug.Log("Gate audio triggered.");
        }
    }
}

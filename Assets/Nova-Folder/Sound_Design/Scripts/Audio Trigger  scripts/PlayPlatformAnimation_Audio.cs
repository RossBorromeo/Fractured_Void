using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPlatformAnimation_Audio : MonoBehaviour
{
    public Animator platformAnimator; // Reference to the Animator component
    public AudioSource platformAudioSource; // Reference to the AudioSource component
    public AudioClip platformSoundEffect; // Reference to the AudioClip for the sound effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player entered the trigger
        {
            Debug.Log("Player entered trigger - Playing platform animation and sound effect");
            platformAnimator.Play("Platform Extension"); // Play the animation
            platformAudioSource.PlayOneShot(platformSoundEffect); // Play the sound effect
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPlatformAnimation_Audio : MonoBehaviour
{
    public Animator platformAnimator; // Reference to the Animator component
    public AudioSource platformAudioSource; 
    public AudioClip platformSoundEffect; 

    private bool hasPlayedSound = false; // Tracks if sound has been played

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayedSound) 
        {
            Debug.Log("Player entered trigger - Playing platform animation and sound effect");
            platformAnimator.Play("Platform Extension"); 
            platformAudioSource.PlayOneShot(platformSoundEffect);
            hasPlayedSound = true; 
        }
    }
}
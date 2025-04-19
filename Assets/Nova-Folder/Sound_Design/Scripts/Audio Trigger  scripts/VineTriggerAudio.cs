using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineTriggerAudio : MonoBehaviour
{
    public CreepingVines vines; // Assign the vine GameObject in Inspector
    public AudioClip activationSound; // Assign the sound effect in Inspector
    private AudioSource audioSource;

    private void Start()
    {
        // Ensure an AudioSource is attached to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && vines != null)
        {
            vines.ActivateVines(); // Activate the vines when the player enters

            // Play the activation sound
            if (activationSound != null)
            {
                audioSource.PlayOneShot(activationSound);
            }
        }
    }
}

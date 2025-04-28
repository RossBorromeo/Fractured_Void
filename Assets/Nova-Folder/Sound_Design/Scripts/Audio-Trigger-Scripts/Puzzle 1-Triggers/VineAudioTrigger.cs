using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAudioTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource vineAudioSource; // Reference to the audio source for vine sound
    [SerializeField] private AudioSource musicAudioSource; // Reference to the audio source for music
    [SerializeField] private GameObject asterFlower; // Reference to the Aster flower object
    [SerializeField] private Collider vineTrigger; // Reference to the Vine trigger collider

    private bool isPlayerInTrigger = false;

    void Start()
    {
        if (vineAudioSource == null)
        {
            Debug.LogError("Vine AudioSource is not assigned in the inspector.");
        }

        if (musicAudioSource == null)
        {
            Debug.LogError("Music AudioSource is not assigned in the inspector.");
        }

        if (asterFlower == null)
        {
            Debug.LogError("Aster flower is not assigned in the inspector.");
        }

        if (vineTrigger == null)
        {
            Debug.LogError("Vine trigger is not assigned in the inspector.");
        }
    }

    void Update()
    {
        // Check if the Aster flower is collected (destroyed)
        if (asterFlower == null && musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the Vine trigger
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;

            if (!vineAudioSource.isPlaying)
            {
                vineAudioSource.Play();
            }

            if (!musicAudioSource.isPlaying)
            {
                musicAudioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the Vine trigger
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;

            if (vineAudioSource.isPlaying)
            {
                vineAudioSource.Stop();
            }
        }
    }
}

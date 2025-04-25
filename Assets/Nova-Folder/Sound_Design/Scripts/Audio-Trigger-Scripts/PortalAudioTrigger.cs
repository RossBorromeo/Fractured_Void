using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAudioTrigger : MonoBehaviour
{
    public AudioSource audioSource; // Assign this in the Unity Editor

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing. Please assign it in the Inspector.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && audioSource != null)
        {
            audioSource.Play();
        }
    }
}
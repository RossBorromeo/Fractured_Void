using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlaySound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            AudioManager.PlaySound(SoundType.Footsteps, audioSource);
        }
        else
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }
    }
}

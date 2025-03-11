using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlaySound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            AudioManager.PlaySound(SoundType.SFX, audioSource);
        }
        else
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }
    }
}

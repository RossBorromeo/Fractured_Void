using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlaySound()
    {
        AudioManager.PlaySound(SoundType.Footsteps);
    }
}
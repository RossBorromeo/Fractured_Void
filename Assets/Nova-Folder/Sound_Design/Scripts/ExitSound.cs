using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSound : StateMachineBehaviour
{
    [SerializeField] private SoundType sound;

    [SerializeField, Range(0, 1)] private float volume = 1;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.PlaySound(sound, null, volume);
    }
}

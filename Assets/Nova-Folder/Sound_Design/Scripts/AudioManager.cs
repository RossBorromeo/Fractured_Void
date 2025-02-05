using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum SoundType
{
    Jump,
    Attack,
    Footsteps
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundLists;
    private static AudioManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, AudioSource source, float volume = 1)
    {
        AudioClip[] clips = instance.soundLists[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume);
    }

#if UNITY_EDITOR

    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundLists, names.Length);
        for (int i = 0; i < soundLists.Length; i++)
            soundLists[i].names = names[i];
    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string names;
    [SerializeField] private AudioClip[] sounds;
}
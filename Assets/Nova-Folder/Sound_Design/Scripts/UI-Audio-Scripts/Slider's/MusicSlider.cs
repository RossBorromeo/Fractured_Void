using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // Reference to the AudioMixer
    [SerializeField] private Slider musicSlider;   // Reference to the UI Slider

    private const string MusicVolumeParam = "MusicVolume"; // Name of the parameter in the AudioMixer

    void Start()
    {
        // Initialize the slider value based on the current AudioMixer setting
        if (audioMixer != null && musicSlider != null)
        {
            float currentVolume;
            if (audioMixer.GetFloat(MusicVolumeParam, out currentVolume))
            {
                musicSlider.value = Mathf.Pow(10, currentVolume / 20); // Convert dB to linear
            }

            // Add listener to handle slider value changes
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        if (audioMixer != null)
        {
            float volumeInDb = Mathf.Log10(sliderValue) * 20; // Convert linear to dB
            audioMixer.SetFloat(MusicVolumeParam, volumeInDb);
        }
    }
}

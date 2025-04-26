using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // Reference to the AudioMixer
    [SerializeField] private Slider volumeSlider;  // Reference to the UI Slider

    private const string MasterVolumeParam = "MasterVolume"; // Name of the parameter in the AudioMixer

    void Start()
    {
        // Initialize the slider value based on the current AudioMixer setting
        if (audioMixer != null && volumeSlider != null)
        {
            float currentVolume;
            if (audioMixer.GetFloat(MasterVolumeParam, out currentVolume))
            {
                volumeSlider.value = Mathf.Pow(10, currentVolume / 20); // Convert dB to linear
            }

            // Add listener to handle slider value changes
            volumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }
    }

    public void SetMasterVolume(float sliderValue)
    {
        if (audioMixer != null)
        {
            float volumeInDb = Mathf.Log10(sliderValue) * 20; // Convert linear to dB
            audioMixer.SetFloat(MasterVolumeParam, volumeInDb);
        }
    }
}

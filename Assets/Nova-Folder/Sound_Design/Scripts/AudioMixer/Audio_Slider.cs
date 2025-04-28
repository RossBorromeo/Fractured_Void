using UnityEngine;
using UnityEngine.UI;

public class Audio_Slider : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider mainSlider; // Slider for Main volume
    [SerializeField] private Slider sfxSlider; // Slider for SFX volume
    [SerializeField] private Slider ambienceSlider; // Slider for Ambience volume
    [SerializeField] private Slider musicSlider; // Slider for Music volume

    [Header("Audio Sources")]
    [SerializeField] private AudioSource mainAudioSource; // AudioSource for Main
    [SerializeField] private AudioSource sfxAudioSource; // AudioSource for SFX
    [SerializeField] private AudioSource ambienceAudioSource; // AudioSource for Ambience
    [SerializeField] private AudioSource musicAudioSource; // AudioSource for Music

    private const string MainVolumeKey = "MainVolume";
    private const string SfxVolumeKey = "SfxVolume";
    private const string AmbienceVolumeKey = "AmbienceVolume";
    private const string MusicVolumeKey = "MusicVolume";

    void Start()
    {
        // Load saved volume settings
        if (mainSlider != null && mainAudioSource != null)
        {
            float mainVolume = PlayerPrefs.GetFloat(MainVolumeKey, mainAudioSource.volume);
            mainSlider.value = mainVolume;
            mainAudioSource.volume = mainVolume;
            mainSlider.onValueChanged.AddListener(value => AdjustVolume(mainAudioSource, value, MainVolumeKey));
        }

        if (sfxSlider != null && sfxAudioSource != null)
        {
            float sfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey, sfxAudioSource.volume);
            sfxSlider.value = sfxVolume;
            sfxAudioSource.volume = sfxVolume;
            sfxSlider.onValueChanged.AddListener(value => AdjustVolume(sfxAudioSource, value, SfxVolumeKey));
        }

        if (ambienceSlider != null && ambienceAudioSource != null)
        {
            float ambienceVolume = PlayerPrefs.GetFloat(AmbienceVolumeKey, ambienceAudioSource.volume);
            ambienceSlider.value = ambienceVolume;
            ambienceAudioSource.volume = ambienceVolume;
            ambienceSlider.onValueChanged.AddListener(value => AdjustVolume(ambienceAudioSource, value, AmbienceVolumeKey));
        }

        if (musicSlider != null && musicAudioSource != null)
        {
            float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, musicAudioSource.volume);
            musicSlider.value = musicVolume;
            musicAudioSource.volume = musicVolume;
            musicSlider.onValueChanged.AddListener(value => AdjustVolume(musicAudioSource, value, MusicVolumeKey));
        }
    }

    private void AdjustVolume(AudioSource audioSource, float value, string key)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
            PlayerPrefs.SetFloat(key, value); // Save the volume setting
        }
    }

    void OnDestroy()
    {
        // Remove listeners to avoid memory leaks
        if (mainSlider != null) mainSlider.onValueChanged.RemoveAllListeners();
        if (sfxSlider != null) sfxSlider.onValueChanged.RemoveAllListeners();
        if (ambienceSlider != null) ambienceSlider.onValueChanged.RemoveAllListeners();
        if (musicSlider != null) musicSlider.onValueChanged.RemoveAllListeners();
    }
}

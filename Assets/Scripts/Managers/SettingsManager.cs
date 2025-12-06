using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider musicSlider;
    public Toggle fullscreenToggle;
    public AudioSource musicAudioSource;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("MasterVolume", 0.8f);
        float music = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        bool fullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        if (volumeSlider != null) volumeSlider.value = volume;
        if (musicSlider != null) musicSlider.value = music;
        if (fullscreenToggle != null) fullscreenToggle.isOn = fullscreen;

        ApplyVolume(volume);
        ApplyMusic(music);
        Screen.fullScreen = fullscreen;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnVolumeChanged(float value)
    {
        ApplyVolume(value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void OnMusicChanged(float value)
    {
        ApplyMusic(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void OnFullscreenChanged(bool isOn)
    {
        Screen.fullScreen = isOn;
        PlayerPrefs.SetInt("Fullscreen", isOn ? 1 : 0);
    }

    void ApplyVolume(float value)
    {
        AudioListener.volume = Mathf.Clamp01(value);
    }

    void ApplyMusic(float value)
    {
        if (musicAudioSource == null) return;
        musicAudioSource.volume = Mathf.Clamp01(value);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI volumeValueText;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown languageDropdown;
    public Toggle fullscreenToggle;

    private float newVolume;
    private int newResolutionIndex;
    private int newLanguageIndex;
    private bool newFullscreen;

    private int[] widths = { 1920, 2560, 1280, 640 };
    private int[] heights = { 1080, 1440, 720, 480 };

    private void Awake()
    {
        SettingsData SD = SaveSystem.LoadSettings();

        volumeSlider.value = SD.sfxVolume;
        fullscreenToggle.isOn = SD.fullscreen;
        resolutionDropdown.value = SD.resolutionIndex;
        languageDropdown.value = SD.languageIndex;

        newVolume = SD.sfxVolume;
        newResolutionIndex = SD.resolutionIndex;
        newLanguageIndex = SD.languageIndex;
        newFullscreen = SD.fullscreen;

        ApplyChanges(false);

        volumeValueText.text = newVolume.ToString();
    }

    public void OnVolumeChanged(float v)
    {
        newVolume = v;
        volumeValueText.text = v.ToString();
    }

    public void OnResolutionChanged(int index)
    {
        newResolutionIndex = index;
    }

    public void OnLanguageChanged(int index)
    {
        newLanguageIndex = index;
    }

    public void OnFullscreenChanged(bool isOn)
    {
        newFullscreen = isOn;
    }

    public void ApplyChanges(bool save)
    {
        AudioListener.volume = newVolume / 100f;

        Screen.fullScreen = newFullscreen;

        Screen.SetResolution(
            widths[newResolutionIndex],
            heights[newResolutionIndex],
            newFullscreen
        );

        LocalizationManager.Language lang =
            newLanguageIndex == 1 ?
                LocalizationManager.Language.English :
                LocalizationManager.Language.Russian;

        LocalizationManager.Instance.LoadLanguage(lang);
        EventManager.OnLanguageChanged.Invoke(this);

        if (save)
            SaveSettingsAfterApply();
    }

    private void SaveSettingsAfterApply()
    {
        SaveSystem.SaveSettings(new SettingsData
        {
            sfxVolume = newVolume,
            fullscreen = newFullscreen,
            resolutionIndex = newResolutionIndex,
            languageIndex = newLanguageIndex
        });
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Outdoors");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

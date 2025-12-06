using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public enum Language
    {
        English,
        Russian
    }

    public Language CurrentLanguage = Language.English;

    private Dictionary<string, string> localizedText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        LoadLanguage(CurrentLanguage);
    }


    public void LoadLanguage(Language lang)
    {
        string fileName = lang == Language.English ? "en" : "ru";
        TextAsset jsonFile = Resources.Load<TextAsset>("Localization/" + fileName);

        if (jsonFile == null)
        {
            Debug.LogError("Localization file not found: " + fileName);
            return;
        }

        localizedText = new Dictionary<string, string>();

        string json = jsonFile.text.Trim();

        // Находим все пары "ключ":"значение" в корне
        var matches = System.Text.RegularExpressions.Regex.Matches(
            json,
            "\"([^\"]+)\"\\s*:\\s*\"([^\"]*)\""
        );

        foreach (System.Text.RegularExpressions.Match m in matches)
        {
            string key = m.Groups[1].Value;
            string value = m.Groups[2].Value;

            localizedText[key] = value;
        }

        Debug.Log("Loaded " + localizedText.Count + " entries from " + fileName);
    }


    public string GetText(string key)
    {
        if (localizedText != null && localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        else
        {
            Debug.LogWarning("Missing localization key: " + key);
            return key;
        }
    }
}
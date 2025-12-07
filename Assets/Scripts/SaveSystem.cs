using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/save.json";
    private static string settingsPath = Application.persistentDataPath + "/settings.json";

    // ---------- GAME SAVE ----------
    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public static SaveData LoadGame()
    {
        if (!File.Exists(savePath))
            return new SaveData();

        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<SaveData>(json);
    }

    // ---------- SETTINGS SAVE ----------
    public static void SaveSettings(SettingsData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(settingsPath, json);
    }

    public static SettingsData LoadSettings()
    {
        if (!File.Exists(settingsPath))
            return new SettingsData(); // создаём с настройками по умолчанию

        string json = File.ReadAllText(settingsPath);
        return JsonUtility.FromJson<SettingsData>(json);
    }
}

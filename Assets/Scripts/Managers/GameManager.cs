using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SaveData data;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        data = SaveSystem.LoadGame();
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(data);
    }

    public void LoadGame()
    {
        data = SaveSystem.LoadGame();
    }
}

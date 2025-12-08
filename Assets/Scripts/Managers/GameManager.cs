using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SaveData data;
    public int money;
    public int currentDay = 1;

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

    private void OnEnable()
    {
        EventManager.OnDayEnd += HandleDayEnd;
        EventManager.OnMoneyChanged += HandleMoneyChanged;
    }

    private void OnDisable()
    {
        EventManager.OnDayEnd -= HandleDayEnd;
        EventManager.OnMoneyChanged -= HandleMoneyChanged;
    }

    private void HandleMoneyChanged(Component sender, int amount)
    {
        money = amount;
        data.money = money;
    }

    private void HandleDayEnd(Component sender)
    {
        currentDay++;
        data.currentDay = currentDay;
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(data);
    }

    public void LoadGame()
    {
        data = SaveSystem.LoadGame();
        money = data.money;
        currentDay = data.currentDay;
    }
}

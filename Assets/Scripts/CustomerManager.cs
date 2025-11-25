using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;
    public Transform servicePoint;
    public string[] customerPhrases;
    public Button serveButton;
    public Button goToGardenButton;
    public TextMeshProUGUI serveButtonText;

    private GameObject activeCustomer;
    private int customersServed = 0;
    private int maxCustomersPerDay = 5;
    private bool dayFinished = false;

    void Start()
    {
        UpdateButtonText();
        SpawnNewCustomer();
    }

    public void SpawnNewCustomer()
    {
        if (activeCustomer != null) return;
        if (dayFinished) return;

        if (customersServed >= maxCustomersPerDay)
        {
            EndDay();
            return;
        }

        activeCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        activeCustomer.transform.position = servicePoint.position;

        TextMeshProUGUI text = activeCustomer.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null && customerPhrases.Length > 0)
        {
            text.text = customerPhrases[Random.Range(0, customerPhrases.Length)];
        }
    }

    public void ServeCustomer()
    {
        if (dayFinished)
        {
            StartNextDay();
            return;
        }

        if (activeCustomer == null) return;

        Destroy(activeCustomer);
        activeCustomer = null;

        customersServed++;

        if (customersServed < maxCustomersPerDay)
            Invoke("SpawnNewCustomer", 0.5f);
        else
            EndDay();
    }

    private void EndDay()
    {
        dayFinished = true;
        goToGardenButton.interactable = true;
        UpdateButtonText();
        Debug.Log("День завершён!");
    }

    private void StartNextDay()
    {
        customersServed = 0;
        dayFinished = false;
        goToGardenButton.interactable = false;
        UpdateButtonText();
        Debug.Log("Новый день начался!");

        SpawnNewCustomer();
    }

    private void UpdateButtonText()
    {
        if (dayFinished)
            serveButtonText.text = "Следующий день";
        else
            serveButtonText.text = "Обслужить";
    }
}

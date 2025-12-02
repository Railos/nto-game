using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;
    public Transform servicePoint;
    public List<CustomerObject> customerPool = new List<CustomerObject>();
    public Button serveButton;
    public Button goToGardenButton;
    public TextMeshProUGUI serveButtonText;

    private GameObject activeCustomer;
    private CustomerObject activeCustomerObject;
    private int customersServed = 0;
    private int maxCustomersPerDay = 5;
    private bool dayFinished = false;
    private TextMeshProUGUI text;

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

        activeCustomerObject = customerPool[customersServed];
        activeCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        activeCustomer.transform.position = servicePoint.position;

        text = activeCustomer.GetComponentInChildren<TextMeshProUGUI>();
        text.text = activeCustomerObject.entryDialogues[Random.Range(0, activeCustomerObject.entryDialogues.Length)];
    }

    public void ServeCustomer()
    {
        if (dayFinished)
        {
            for (int i = customerPool.Count - 1; i > 0; i--) {
                int j = Random.Range(0, i + 1);
                var temp = customerPool[i];
                customerPool[i] = customerPool[j];
                customerPool[j] = temp;
            }
            StartNextDay();
            return;
        }

        if (activeCustomer == null) return;

        text.text = activeCustomerObject.satisfiedDialogues[Random.Range(0, activeCustomerObject.satisfiedDialogues.Length)];

        Invoke(nameof(FinishServing), 2f);
    }

    private void FinishServing()
    {
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
    }

    private void StartNextDay()
    {
        customersServed = 0;
        dayFinished = false;
        goToGardenButton.interactable = false;
        UpdateButtonText();

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

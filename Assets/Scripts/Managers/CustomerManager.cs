using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    [Header("UI")]
    public Image portraitImage;
    public TextMeshProUGUI dialogueText;
    public Button serveButton;
    public Button goToGardenButton;
    public TextMeshProUGUI serveButtonText;

    [Header("Data")]
    public List<CustomerObject> customerPool = new List<CustomerObject>();

    private CustomerObject activeCustomerObject;
    private int customersServed = 0;
    private int maxCustomersPerDay = 5;
    private bool dayFinished = false;
    private bool waitingForNextCustomer = false;

    void Start()
    {
        ShuffleCustomerPool();
        goToGardenButton.interactable = false;
        UpdateButtonText();
        SpawnNewCustomer();
    }

    void SpawnNewCustomer()
    {
        if (dayFinished) return;
        if (customersServed >= maxCustomersPerDay)
        {
            EndDay();
            return;
        }

        activeCustomerObject = customerPool[customersServed];

        if (portraitImage != null)
        {
            if (activeCustomerObject.portrait != null)
                portraitImage.sprite = activeCustomerObject.portrait;

            portraitImage.rectTransform.anchoredPosition =
                activeCustomerObject.spawnPosition;
        }

        if (dialogueText != null && activeCustomerObject.entryDialogues.Length > 0)
        {
            dialogueText.text = activeCustomerObject
                .entryDialogues[Random.Range(0, activeCustomerObject.entryDialogues.Length)];
        }

        waitingForNextCustomer = false;
    }

    public void ServeCustomer()
    {
        if (dayFinished)
        {
            ShuffleCustomerPool();
            StartNextDay();
            return;
        }

        if (activeCustomerObject == null) return;

        if (!waitingForNextCustomer)
        {
            if (dialogueText != null && activeCustomerObject.satisfiedDialogues.Length > 0)
            {
                dialogueText.text = activeCustomerObject
                    .satisfiedDialogues[Random.Range(0, activeCustomerObject.satisfiedDialogues.Length)];
            }

            waitingForNextCustomer = true;
        }
        else
        {
            FinishServing();
        }
    }

    void FinishServing()
    {
        customersServed++;
        waitingForNextCustomer = false;

        if (customersServed < maxCustomersPerDay)
            SpawnNewCustomer();
        else
            EndDay();
    }

    void EndDay()
    {
        dayFinished = true;
        goToGardenButton.interactable = true;
        UpdateButtonText();
    }

    void StartNextDay()
    {
        customersServed = 0;
        dayFinished = false;
        goToGardenButton.interactable = false;
        UpdateButtonText();
        SpawnNewCustomer();
    }

    void UpdateButtonText()
    {
        if (dayFinished)
            serveButtonText.text = "Следующий день";
        else
            serveButtonText.text = "Обслужить";
    }

    void ShuffleCustomerPool()
    {
        for (int i = customerPool.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = customerPool[i];
            customerPool[i] = customerPool[j];
            customerPool[j] = temp;
        }
    }
}

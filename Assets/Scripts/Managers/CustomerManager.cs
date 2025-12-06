using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    private enum DialogueState
    {
        WaitingOrder,    // сразу после прихода клиента показан заказ
        ChoosingAnswer,  // игрок выбирает вариант
        Reacting         // показана реакция, ждём следующего гостя
    }

    [Header("UI")]
    public Image portraitImage;
    public TextMeshProUGUI dialogueText;
    public Button serveButton;
    public Button goToGardenButton;
    public TextMeshProUGUI serveButtonText;
    public Button[] answerButtons;
    public TextMeshProUGUI[] answerTexts;

    [Header("Answer Colors")]
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color defaultAnswerColor = Color.white;

    [Header("Data")]
    public List<CustomerObject> customerPool = new List<CustomerObject>();

    private CustomerObject activeCustomerObject;
    private int customersServed = 0;
    private int maxCustomersPerDay = 5;
    private bool dayFinished = false;
    private DialogueState state = DialogueState.WaitingOrder;

    void Start()
    {
        ShuffleCustomerPool();
        goToGardenButton.interactable = false;

        if (serveButton != null)
            serveButton.onClick.AddListener(OnServeButtonClick);

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

        // сразу показываем заказ
        if (dialogueText != null)
            dialogueText.text = activeCustomerObject.orderText;

        state = DialogueState.WaitingOrder;
        serveButtonText.text = "Приготовить";

        ResetAnswerButtons();
        HideUnusedAnswerButtons();
    }

    void OnServeButtonClick()
    {
        if (dayFinished)
        {
            StartNextDay();
            return;
        }

        if (activeCustomerObject == null) return;

        switch (state)
        {
            case DialogueState.WaitingOrder:
                ShowQuestionWithAnswers();
                break;

            case DialogueState.ChoosingAnswer:
                // ничего, ждём выбор варианта
                break;

            case DialogueState.Reacting:
                FinishServing();
                break;
        }
    }

    void ShowQuestionWithAnswers()
    {
        state = DialogueState.ChoosingAnswer;

        if (dialogueText != null)
            dialogueText.text = activeCustomerObject.questionText;

        ResetAnswerButtons();

        if (answerButtons == null || answerTexts == null) return;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < activeCustomerObject.answerOptions.Length)
            {
                int index = i;

                if (answerButtons[i] == null) continue;

                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));

                if (i < answerTexts.Length && answerTexts[i] != null)
                    answerTexts[i].text = activeCustomerObject.answerOptions[i];
            }
            else
            {
                if (answerButtons[i] != null)
                    answerButtons[i].gameObject.SetActive(false);
            }
        }

        serveButtonText.text = "Выберите ответ";
    }

    void OnAnswerSelected(int index)
    {
        bool isCorrect = index == activeCustomerObject.correctAnswerIndex;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] == null) continue;

            Image img = answerButtons[i].GetComponent<Image>();
            if (img == null) continue;

            if (i == index)
                img.color = isCorrect ? correctColor : wrongColor;
            else
                img.color = defaultAnswerColor;
        }

        string[] pool = isCorrect
            ? activeCustomerObject.satisfiedDialogues
            : activeCustomerObject.disappointedDialogues;

        if (pool != null && pool.Length > 0 && dialogueText != null)
        {
            dialogueText.text = pool[Random.Range(0, pool.Length)];
        }

        state = DialogueState.Reacting;
        serveButtonText.text = "Следующий гость";
    }

    void ResetAnswerButtons()
    {
        if (answerButtons == null) return;

        foreach (Button b in answerButtons)
        {
            if (b == null) continue;

            b.onClick.RemoveAllListeners();

            Image img = b.GetComponent<Image>();
            if (img != null)
                img.color = defaultAnswerColor;
        }
    }

    void HideUnusedAnswerButtons()
    {
        if (answerButtons == null) return;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] != null)
                answerButtons[i].gameObject.SetActive(false);
        }
    }

    void FinishServing()
    {
        customersServed++;

        if (customersServed < maxCustomersPerDay)
        {
            SpawnNewCustomer();
        }
        else
        {
            EndDay();
        }
    }

    void EndDay()
    {
        dayFinished = true;
        goToGardenButton.interactable = true;
        serveButtonText.text = "Следующий день";
        state = DialogueState.Reacting;
    }

    void StartNextDay()
    {
        customersServed = 0;
        dayFinished = false;
        goToGardenButton.interactable = false;
        ShuffleCustomerPool();
        SpawnNewCustomer();
    }

    void ShuffleCustomerPool()
    {
        for (int i = customerPool.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            CustomerObject temp = customerPool[i];
            customerPool[i] = customerPool[j];
            customerPool[j] = temp;
        }
    }
}

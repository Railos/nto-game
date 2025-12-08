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
    public GameObject goToGardenButton;
    public TextMeshProUGUI serveButtonText;
    public Button[] answerButtons;
    public TextMeshProUGUI[] answerTexts;
    public TextMeshProUGUI moneyText;

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
    
    public Cup cup;
    public List<RecipeSO> recipes;

    public void CheckCup()
    {
        foreach (var recipe in recipes)
        {
            if (IsMatchingRecipe(recipe))
            {
                Debug.Log("Успех! Подходит рецепт: " + recipe.recipeName);
                cup.Clear();
                ShowQuestionWithAnswers();
                return;
            }
        }

        Debug.Log("Неверный рецепт! Клиент недоволен.");
        cup.Clear();
        WrongCup();
    }

    private bool IsMatchingRecipe(RecipeSO recipe)
    {
        // Сравниваем количество
        if (cup.ingredients.Count != recipe.requiredIngredients.Count)
        {
            Debug.Log("Количество ингредиентов не совпадает.");
            return false;
        }

        // Проверяем, что каждый нужный ингредиент есть в стакане
            foreach (var ing in recipe.requiredIngredients)
            {
                if (!cup.ingredients.Contains(ing))
                {
                    Debug.Log("Отсутствует ингредиент: " + ing.displayName);
                    return false;
                } 
            }

        return true;
    }

    void Start()
    {
        ShuffleCustomerPool();
        goToGardenButton.SetActive(false);

        SpawnNewCustomer();
    }

    public void cupReadyToServe()
    {
        serveButtonText.text = "Обслужить";
    }

    public void cupUnreadyToServe()
    {
        serveButtonText.text = "Чай не готов";
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
        if (!cup.readyToServe)
            serveButtonText.text = "Чай не готов";
        else
            serveButtonText.text = "Обслужить";

        ResetAnswerButtons();
        HideUnusedAnswerButtons();
    }

    public void OnServeButtonClick()
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
                if (cup.readyToServe)
                    CheckCup();
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
        int newMoney = GameManager.Instance.money + activeCustomerObject.wantedRecipe.moneyGain;
        moneyText.text = newMoney.ToString();
        EventManager.OnMoneyChanged.Invoke(this, newMoney);

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

    void WrongCup()
    {
        state = DialogueState.Reacting;

        string[] pool = activeCustomerObject.disappointedDialogues;

        dialogueText.text = pool[Random.Range(0, pool.Length)];

        serveButtonText.text = "Следующий гость";
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
        goToGardenButton.SetActive(true);
        serveButtonText.text = "Следующий день";
        state = DialogueState.Reacting;
    }

    void StartNextDay()
    {
        customersServed = 0;
        dayFinished = false;
        goToGardenButton.SetActive(false);
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

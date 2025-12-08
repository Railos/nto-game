using UnityEngine;
using System.Collections.Generic;

public class Cup : MonoBehaviour
{
    public List<IngredientSO> ingredients = new List<IngredientSO>();
    public CustomerManager customerManager;
    private Animator animator;
    public bool readyToServe = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (IngredientSelector.selectedIngredient != null)
        {
            ingredients.Add(IngredientSelector.selectedIngredient.ingredient);
            Debug.Log("Добавлено: " + IngredientSelector.selectedIngredient.ingredient.displayName);

            IngredientSelector.selectedIngredient.light2d.SetActive(false);
            IngredientSelector.selectedIngredient = null;
        }
        else
        {
            if (!readyToServe)
            {
                animator.CrossFade("CupToCustomer", 0f);
                readyToServe = true;
                customerManager.cupReadyToServe();
            }
            else
            {
                animator.CrossFade("CupToWorkplace", 0f);
                readyToServe = false;
                customerManager.cupUnreadyToServe();
            }

        }
    }

    public void Clear()
    {
        ingredients.Clear();
        animator.CrossFade("CupIdle", 0f);
        readyToServe = false;
    }
}

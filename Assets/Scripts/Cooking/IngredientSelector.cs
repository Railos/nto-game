using UnityEngine;

public class IngredientSelector : MonoBehaviour
{
    public static IngredientInteractable selectedIngredient;

    public static void Select(IngredientInteractable ingredient)
    {
        if (selectedIngredient != null) selectedIngredient.light2d.SetActive(false);
        selectedIngredient = ingredient;
    }
}

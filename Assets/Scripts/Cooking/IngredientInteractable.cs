using UnityEngine;

public class IngredientInteractable : MonoBehaviour
{
    public IngredientSO ingredient;
    public GameObject light2d;

    private void OnMouseDown()
    {
        IngredientSelector.Select(this);
        light2d.SetActive(true);
    }
}

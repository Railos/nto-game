using UnityEngine;

[CreateAssetMenu(menuName = "Ingredient")]
public class IngredientSO : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
}

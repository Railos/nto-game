using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Recipe")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public List<IngredientSO> requiredIngredients;
    public int moneyGain;
}

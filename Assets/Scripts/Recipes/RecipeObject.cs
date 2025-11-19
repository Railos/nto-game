using UnityEngine;

[CreateAssetMenu]
public class RecipeObject : ScriptableObject
{
    public string name;
    public ItemObject[] items;
    public float price;

}

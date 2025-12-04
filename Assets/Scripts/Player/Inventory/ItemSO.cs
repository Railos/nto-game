using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public enum ItemType
    {
        seeds,
        teaweeds,
        quests,
        ingredients
    }

    public string itemName;
    public ItemType itemType;
    public int amount;
    public bool isStackable;
    public Sprite sprite;
}

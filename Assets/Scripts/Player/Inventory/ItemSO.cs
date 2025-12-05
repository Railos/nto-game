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
    public bool isStackable;
    public Sprite sprite;

    [Header("Seeds")]
    public float growTime;
    public ItemSO plant;
    public int amountPlants;
}

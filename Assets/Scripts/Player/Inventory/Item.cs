using System;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        plant1,
        plant2,
        plant3
    }

    public string name;
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.plant1: return ItemAssets.Instance.plant1;
            case ItemType.plant2: return ItemAssets.Instance.plant2;
            case ItemType.plant3: return ItemAssets.Instance.plant3;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.plant1:
            case ItemType.plant2:
            case ItemType.plant3:
                return true;
        }
    }
}

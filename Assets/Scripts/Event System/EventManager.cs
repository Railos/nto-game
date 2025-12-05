using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityAction<Component> OnGardenSlotOpen;
    public static UnityAction<Component, Item> OnGardenSlotCollect;
    public static UnityAction<Component, Item> OnSeedSelect;
    public static UnityAction<Component, Item> OnSeedCollect;
}

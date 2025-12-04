using UnityEngine;

public class GardeningSlot : InteractableObject
{
    public Item currentPlant;

    public GameObject vacantIcon;

    public override void Interact()
    {
        base.Interact();
        EventManager.OnGardenSlotOpen.Invoke(this);
        vacantIcon.SetActive(false);
    }
}

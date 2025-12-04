using UnityEngine;

public class GardeningSlot : InteractableObject
{
    public Item currentPlant;
    public bool isVacant = true;
    public GameObject vacantIcon;

    public override void Interact()
    {
        base.Interact();
        isVacant = false;
        EventManager.OnGardenSlotOpen.Invoke(this);
    }

    private void Update()
    {
        if (isVacant)
        {
            vacantIcon.SetActive(true);
        }
        else
        {
            vacantIcon.SetActive(false);
        }
    }
}

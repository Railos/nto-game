using UnityEngine;

public class GardeningSlot : InteractableObject
{
    public Item currentPlant;
    public bool isVacant = true;
    public bool finishedGrowing = false;
    public GameObject vacantIcon;
    public GameObject waitIcon;
    public GameObject finishIcon;
    public GameObject growingPlantVisual;

    public override void Interact()
    {
        base.Interact();
        if (isVacant)
            EventManager.OnGardenSlotOpen.Invoke(this);
        if (finishedGrowing)
            CollectFinishedPlant();
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

    public void StartGrowingPlant()
    {
        isVacant = false;
        waitIcon.SetActive(true);
        growingPlantVisual.SetActive(true);
        Invoke(nameof(FinishGrowingPlant), currentPlant.item.growTime);
    }

    private void FinishGrowingPlant()
    {
        Debug.Log("Finished growing ts");
        waitIcon.SetActive(false);
        finishIcon.SetActive(true);
        finishedGrowing = true;
    }
    private void CollectFinishedPlant()
    {
        isVacant = true;
        finishIcon.SetActive(false);
        growingPlantVisual.SetActive(false);
        EventManager.OnSeedCollect.Invoke(this, currentPlant);
        currentPlant = null;
        finishedGrowing = false;
    }
}

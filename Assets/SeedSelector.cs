using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeedSelector : MonoBehaviour
{

    private Inventory inventory;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;

    public GameObject seedMenu;
    public bool isOpen = false;

    private void OnEnable()
    {
        EventManager.OnGardenSlotOpen += OpenMenu;
    }

    private void OnDisable()
    {
        EventManager.OnGardenSlotOpen -= OpenMenu;
    }

    public void OpenMenu(Component component)
    {
        if (seedMenu.activeSelf)
        {
            CloseMenu();
            component.gameObject.GetComponent<GardeningSlot>().isVacant = true;
            return;
        }
        seedMenu.SetActive(true);
        isOpen = true;
        RefreshInventoryItems();
    }

    public void CloseMenu()
    {
        seedMenu.SetActive(false);
        isOpen = false;
    }
    
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 110f;
        foreach (Item item in inventory.GetItemList())
        {
            if (item.item.itemType != ItemSO.ItemType.seeds)
            {
                continue;
            }
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.item.sprite;
            TextMeshProUGUI textAmount = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if (item.amount <= 1)
            {
                textAmount.text = "";
            }
            else
            {
                textAmount.text = item.amount.ToString();
            }
            
            x++;
            if (x > 9)
            {
                y--;
                x = 0;
            }
        }
    }
}

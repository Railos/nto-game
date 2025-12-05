using UnityEngine;

public class Player : MonoBehaviour
{

    public bool isInventoryOpen = false;
    private Inventory inventory;
    public Ui_Inventory ui_Inventory;
    public SeedSelector seedSelector;

    private void Awake()
    {
        inventory = new Inventory();
        ui_Inventory.SetInventory(inventory);
        seedSelector.SetInventory(inventory);
    }

    private void OnEnable()
    {
        EventManager.OnSeedCollect += CollectPlantFromGardenSlot;
        EventManager.OnSeedSelect += SelectSeed;
    }

    private void OnDisable()
    {
        EventManager.OnSeedCollect -= CollectPlantFromGardenSlot;
        EventManager.OnSeedSelect -= SelectSeed;
    }

    private void SelectSeed(Component component, Item item) {
        Item z = new Item()
        {
            item = item.item,
            amount = 1
        };
        inventory.RemoveItem(z);
    }

    private void CollectPlantFromGardenSlot(Component component, Item seed)
    {
        Item plant = new Item()
        {
            item = seed.item.plant,
            amount = seed.item.amountPlants
        };
        inventory.AddItem(plant);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            Item item = itemWorld.GetItem();
            inventory.AddItem(item);
            ui_Inventory.SetInventory(inventory);
            Destroy(itemWorld.gameObject);
        }
    }

    private void Update()
    {
        if (PlayerInput.Instance.isInventoryButtonPressed())
        {
            isInventoryOpen = !isInventoryOpen;
            ui_Inventory.gameObject.SetActive(isInventoryOpen);
            if (isInventoryOpen)
            {
                seedSelector.CloseMenu();
            }
        }
    }
}

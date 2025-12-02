using UnityEngine;

public class Player : MonoBehaviour
{

    public bool isInventoryOpen = false;
    private Inventory inventory;
    public Ui_Inventory ui_Inventory;

    private void Awake()
    {
        inventory = new Inventory();
        ui_Inventory.SetInventory(inventory);
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
        }
    }
}

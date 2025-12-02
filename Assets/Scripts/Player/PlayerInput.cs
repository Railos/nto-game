using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    public Controls controls;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        controls = new Controls();
        controls.Enable();
    }

    public Vector2 GetMoveInput()
    {
        return controls.Player.Move.ReadValue<Vector2>();
    }

    public bool isInventoryButtonPressed()
    {
        return controls.Player.Inventory.triggered;
    }
}

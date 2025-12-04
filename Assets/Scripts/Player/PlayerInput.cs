using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    private static PlayerInput _instance;
    private Controls controls;
    
    public static PlayerInput Instance => _instance;
    public Controls Controls => controls;
    
    private void Awake()
    {
        // 1. FIRST: Initialize Controls for EVERY instance (even duplicates)
        controls = new Controls();
        
        // 2. THEN: Handle singleton logic
        if (_instance != null && _instance != this)
        {
            // This is a duplicate - disable its controls before destroying
            DisableThisInstancesControls();
            Destroy(gameObject);
            return;
        }
        
        // 3. This is the first/main instance
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Enable controls for the main instance
        EnableControls();
    }
    
    private void EnableControls()
    {
        if (controls == null)
        {
            Debug.LogWarning("Controls not initialized!");
            return;
        }
        
        // Enable the controls
        controls.Enable();
        
        // Or enable specific action maps if you prefer:
        // controls.Player.Enable();
        // controls.UI.Enable();
    }
    
    private void DisableThisInstancesControls()
    {
        if (controls == null)
        {
            Debug.LogWarning("No controls to disable on duplicate!");
            return;
        }
        
        // CRITICAL: Disable ALL action maps
        controls.Disable();
        
        // Also disable each action map individually for safety
        foreach (var actionMap in controls.asset.actionMaps)
        {
            if (actionMap.enabled)
            {
                actionMap.Disable();
            }
        }
        
        // If you know which action maps you enabled, disable them specifically:
        // controls.Player.Disable();
        // controls.UI.Disable();
    }
    
    private void OnDestroy()
    {
        // Only cleanup if this is the actual singleton instance
        if (_instance == this)
        {
            if (controls != null)
            {
                controls.Disable();
                // Optional: controls.Dispose();
            }
            _instance = null;
        }
    }

    public Vector2 GetMoveInput()
    {
        return controls.Player.Move.ReadValue<Vector2>();
    }

    public bool isInventoryButtonPressed()
    {
        return controls.Player.Inventory.triggered;
    }

    public bool isPauseButtonPressed()
    {
        return controls.Player.Pause.triggered;
    }
}

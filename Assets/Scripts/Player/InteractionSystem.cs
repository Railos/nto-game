using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public static InteractionSystem Instance { get; private set; }
    
    [Header("Hover Object")]
    [SerializeField] private GameObject hoverPrefab;
    [SerializeField] private float hoverOffset = 0.5f;
    
    private GameObject currentHoverObject;
    private InteractableObject currentInteractableObject;
    private Camera mainCamera;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    void Start()
    {
        mainCamera = Camera.main;
        
        if (hoverPrefab != null)
        {
            currentHoverObject = Instantiate(hoverPrefab, Vector3.zero, Quaternion.identity);
            currentHoverObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Hover prefab is not assigned!");
        }
    }
    
    void Update()
    {
        if (PauseMenuManager.Instance != null && PauseMenuManager.Instance.isPaused)
        {
            HideHoverObject();
            return;
        }

        if (currentHoverObject != null && currentHoverObject.activeSelf)
        {
            UpdateHoverPosition();
            if (Input.GetMouseButtonDown(0)) currentInteractableObject.Interact();
        }
    }
    
    void UpdateHoverPosition()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentHoverObject.transform.position = mousePosition + Vector2.up * hoverOffset;
    }
    
    // Called by InteractionObject when mouse enters
    public void ShowHoverObject(InteractableObject z)
    {
        if (currentHoverObject != null)
        {
            currentHoverObject.SetActive(true);
            currentInteractableObject = z;
        }
    }
    
    // Called by InteractionObject when mouse exits
    public void HideHoverObject()
    {
        if (currentHoverObject != null)
        {
            currentHoverObject.SetActive(false);
        }
    }
}
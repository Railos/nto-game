using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerObject data;

    private OrderTextBar textBar;

    private void Awake()
    {
        textBar = GetComponent<OrderTextBar>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformOrder();
        }
    }

    public void PerformOrder()
    {
        textBar.DisplayText(data.entryDialogues[Random.Range(0, data.entryDialogues.Length)]);
    }
}

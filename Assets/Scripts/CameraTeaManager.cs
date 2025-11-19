using UnityEngine;

public class CameraTeaManager : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void GoToCook()
    {
        animator.CrossFade("CameraToTea", 0);
    }

    public void GoToCustomers()
    {
        animator.CrossFade("CameraToCustomers", 0);
    }
}

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{

    void OnMouseEnter()
    {
        if (!PauseMenuManager.Instance.isPaused)
            InteractionSystem.Instance?.ShowHoverObject(this);
    }

    void OnMouseExit()
    {
        InteractionSystem.Instance?.HideHoverObject();
    }

    public virtual void Interact()
    {
        
    }
}
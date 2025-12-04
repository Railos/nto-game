using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToScene : InteractableObject
{
    public override void Interact()
    {
        SceneManager.LoadScene("Shop");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToCafe : MonoBehaviour
{
    public void GoToShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void GoToGarden()
    {
        SceneManager.LoadScene("Outdoors");
    }
}

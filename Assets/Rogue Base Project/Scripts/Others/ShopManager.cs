using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private GameObject shopUI;

    public void OpenShop()
    {
        shopUI.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }
}

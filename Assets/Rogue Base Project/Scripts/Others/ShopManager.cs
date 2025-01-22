using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private int baseHealthPrice = 10;

    [SerializeField]
    private int baseSwordPrice = 20;

    [SerializeField]
    private int baseBowPrice = 30;

    [SerializeField]
    private float priceIncreaseMultiplier = 1.5f;

    private int currentHealthPrice;
    private int currentSwordPrice;
    private int currentBowPrice;

    [SerializeField]
    Text healthPriceText;

    [SerializeField]
    Text swordPriceText;

    [SerializeField]
    Text bowPriceText;

    [SerializeField]
    private GameObject shopUI;

    PlayerControler player;

    void Start()
    {
        PlayerControler player = FindFirstObjectByType<PlayerControler>();
        // Initialize current prices
        currentHealthPrice = baseHealthPrice;
        currentSwordPrice = baseSwordPrice;
        currentBowPrice = baseBowPrice;
        UpdatePriceTexts();
    }

    private void UpdatePriceTexts()
    {
        healthPriceText.text = currentHealthPrice.ToString();
        swordPriceText.text = currentSwordPrice.ToString();
        bowPriceText.text = currentBowPrice.ToString();
    }

    public void PurchaseHealth()
    {
        if (player != null && player.coins >= currentHealthPrice)
        {
            player.coins -= currentHealthPrice;
            // Add health to player here
            Debug.Log("Health purchased");

            // Increase price for next purchase
            currentHealthPrice = Mathf.RoundToInt(currentHealthPrice * priceIncreaseMultiplier);
            UpdatePriceTexts();
        }
    }

    public void PurchaseSword()
    {
        if (player != null && player.coins >= currentSwordPrice)
        {
            player.coins -= currentSwordPrice;
            // Give sword upgrade here
            Debug.Log("Sword purchased");

            currentSwordPrice = Mathf.RoundToInt(currentSwordPrice * priceIncreaseMultiplier);
            UpdatePriceTexts();
        }
    }

    public void PurchaseBow()
    {
        if (player != null && player.coins >= currentBowPrice)
        {
            player.coins -= currentBowPrice;
            Debug.Log("Bow purchased");

            currentBowPrice = Mathf.RoundToInt(currentBowPrice * priceIncreaseMultiplier);
            UpdatePriceTexts();
        }
    }

    public void OpenShop()
    {
        player.enabled = false;
        shopUI.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    public void CloseShop()
    {
        player.enabled = true;
        shopUI.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }
}

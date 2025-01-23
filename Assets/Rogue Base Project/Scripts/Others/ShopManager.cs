using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Title("Prices")]
    [SerializeField]
    private int baseHealthPrice = 10;

    [SerializeField]
    private int baseSwordPrice = 20;

    [SerializeField]
    private int baseBowPrice = 30;

    [SerializeField]
    private int basePotionPrice = 40;

    [SerializeField]
    private float priceIncreaseMultiplier = 1.5f;

    private int currentHealthPrice;
    private int currentSwordPrice;
    private int currentBowPrice;
    private int currentPotionPrice;

    [Title("Coin Texts")]
    [SerializeField]
    private Text playerCoinsText;

    [SerializeField]
    Text healthPriceText;

    [SerializeField]
    Text swordPriceText;

    [SerializeField]
    Text bowPriceText;

    [SerializeField]
    Text potionPriceText;
    [Title("Shop UI")]
    [SerializeField]
    private GameObject shopUI;
    [SerializeField]

    PlayerControler player;
    [Title("Max Upgrade Times")]
    [SerializeField]
    int maxUpgradeTimes = 5;
    int bowUpgradeTimes = 0;
    int swordUpgradeTimes = 0;
    int HealthUpgradeTimes = 0;
    [SerializeField]
    int maxPotions = 3;
    [Title("Buttons")]
    [SerializeField]
    private Button healthButton;
    [SerializeField]
    private Button swordButton;
    [SerializeField]
    private Button bowButton;
    [SerializeField]
    private Button potionButton;
    [Title("Text Colors")]

    private Color normalTextColor = Color.yellow;
    private Color insufficientFundsColor = Color.red;

    void Start()
    {
        PlayerControler player = FindFirstObjectByType<PlayerControler>();
        // Initialize current prices
        currentHealthPrice = baseHealthPrice;
        currentSwordPrice = baseSwordPrice;
        currentBowPrice = baseBowPrice;
        currentPotionPrice = basePotionPrice;
        UpdatePriceTexts();
    }

    private void UpdatePriceTexts()
    {
        playerCoinsText.text = player.coins.ToString();

        // Health update
        healthPriceText.text = currentHealthPrice.ToString();
        healthPriceText.color = player.coins >= currentHealthPrice ? normalTextColor : insufficientFundsColor;
        healthButton.interactable = player.coins >= currentHealthPrice && HealthUpgradeTimes < maxUpgradeTimes;

        // Sword update
        swordPriceText.text = currentSwordPrice.ToString();
        swordPriceText.color = player.coins >= currentSwordPrice ? normalTextColor : insufficientFundsColor;
        swordButton.interactable = player.coins >= currentSwordPrice && swordUpgradeTimes < maxUpgradeTimes;

        // Bow update
        bowPriceText.text = currentBowPrice.ToString();
        bowPriceText.color = player.coins >= currentBowPrice ? normalTextColor : insufficientFundsColor;
        bowButton.interactable = player.coins >= currentBowPrice && bowUpgradeTimes < maxUpgradeTimes;

        // Potion update
        potionPriceText.text = currentPotionPrice.ToString();
        potionPriceText.color = player.coins >= currentPotionPrice ? normalTextColor : insufficientFundsColor;
        potionButton.interactable = player.coins >= currentPotionPrice && player.potionAmount < maxPotions;
    }

    public void PurchaseHealth()
    {
        if (player != null && player.coins >= currentHealthPrice && HealthUpgradeTimes < maxUpgradeTimes)
        {
            HealthUpgradeTimes++;
            player.coins -= currentHealthPrice;
            // Add health to player here
            Debug.Log("Health purchased");
            player.GetComponent<Health>().maxHealth += 10;

            // Increase price for next purchase
            currentHealthPrice = Mathf.RoundToInt(currentHealthPrice * priceIncreaseMultiplier);
            UpdatePriceTexts();
        }
    }

    public void PurchaseSword()
    {
        if (player != null && player.coins >= currentSwordPrice && swordUpgradeTimes < maxUpgradeTimes)
        {
            swordUpgradeTimes++;
            player.coins -= currentSwordPrice;
            // Give sword upgrade here
            Debug.Log("Sword purchased");
            player.SwordDamage += 5;

            currentSwordPrice = Mathf.RoundToInt(currentSwordPrice * priceIncreaseMultiplier);
            UpdatePriceTexts();
        }
    }

    public void PurchasePotion()
    {
        if (player != null && player.coins >= currentPotionPrice && player.potionAmount < maxPotions)
        {
            player.coins -= currentPotionPrice;
            // Give sword upgrade here
            Debug.Log("potion purchased");
            player.potionAmount += 1;

            currentSwordPrice = Mathf.RoundToInt(currentPotionPrice);
            UpdatePriceTexts();
        }
    }

    public void PurchaseBow()
    {
        if (player != null && player.coins >= currentBowPrice && bowUpgradeTimes < maxUpgradeTimes)
        {
            bowUpgradeTimes++;
            player.coins -= currentBowPrice;
            Debug.Log("Bow purchased");
            player.BowDamage += 5;
            currentBowPrice = Mathf.RoundToInt(currentBowPrice * priceIncreaseMultiplier);
            UpdatePriceTexts();
        }
    }

    public void OpenShop()
    {
        GameObject.FindFirstObjectByType<Inventory>().GetComponent<Inventory>().enabled = false;
        UpdatePriceTexts();
        player.enabled = false;
        shopUI.SetActive(true);
        shopUI.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    public void CloseShop()
    {
        GameObject.FindFirstObjectByType<Inventory>().GetComponent<Inventory>().enabled = true;
        player.enabled = true;
        shopUI.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }
}

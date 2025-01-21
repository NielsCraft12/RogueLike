using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    Text coinText; // Currency indicator.

    [SerializeField]
    Image healthImage; // Health bar.

    public int coins = 0; // Amount of coins collected.

    GameObject player; // Player object.

    private float health;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            health = player.GetComponent<Health>().currentHealth;
        }
        // Restart the scene when you press.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        healthImage.fillAmount = health / 100;
        coins = player.GetComponent<PlayerControler>().coins; // Update the coin count.
        coinText.text = coins.ToString(); // Update the coin text.
    }
}

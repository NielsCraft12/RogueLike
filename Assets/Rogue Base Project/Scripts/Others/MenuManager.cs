using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu;
    //GameObject pauseMenu;
    [SerializeField]
    public GameObject gameOverMenu;
    [SerializeField]
    GameObject hud;
    [SerializeField]
    public GameObject shopMenu;


    PlayerControler playerControler;

    WaveManager waveManager;

    private void Awake()
    {
        waveManager = GameObject.FindFirstObjectByType<WaveManager>();
        playerControler = GameObject.FindFirstObjectByType<PlayerControler>();
        playerControler.enabled = false;
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        hud.SetActive(false);
        Time.timeScale = 0; // Pause the game
    }

    public void GameOver()
    {
        Time.timeScale = 0; // Pause the game
        playerControler.enabled = false;
        hud.SetActive(false);
        shopMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void StartButton()
    {
        Time.timeScale = 1; // Resume the game
        GameObject.FindFirstObjectByType<PlayerControler>().enabled = true;
        hud.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Resume the game
        playerControler.enabled = true;
        gameOverMenu.SetActive(false);
        hud.SetActive(true);
        playerControler.coins = 0;
        playerControler.GetComponent<Health>().maxHealth = 100;
        playerControler.GetComponent<Health>().currentHealth = 100;
        playerControler.BowDamage = 10;
        playerControler.SwordDamage = 10;
        playerControler.potionAmount = 0;
        playerControler.transform.position = new Vector3(-40, -7.9f, 0);
        playerControler.animator.Rebind();
        waveManager.currentWave = 0;
        for (int i = 0; i < waveManager.allEnemies.Count; i++)
        {
            Destroy(waveManager.allEnemies[i]);
        }

    }

    public void MainMenu()
    {
        Time.timeScale = 0; // Resume the game
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        hud.SetActive(false);
    }


    public void QuitGame()
    {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
             Application.Quit();
#elif (UNITY_WEBGL)
             Application.OpenURL("itch url ");
#endif
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] Text coinText;                     // Currency indicator.
    [SerializeField] Image healthImage;                 // Health bar.

    private int coins = 0;                              // Amount of coins collected.


    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // Restart the scene when you press.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

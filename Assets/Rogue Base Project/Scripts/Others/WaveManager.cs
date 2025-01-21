using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private int currentWave;

    [SerializeField]
    private int maxEnemys;

    [SerializeField]
    private int currentEnemies;

    [SerializeField]
    private bool isWaveActive;
    private bool isShopOpen;

    [SerializeField]
    [Required]
    private List<GameObject> Enemys = new List<GameObject>();

    [SerializeField]
    private float spawnInterval = 2f;

    [SerializeField]
    [Required]
    private Transform[] spawnPoints;

    [SerializeField]
    private ShopManager shopManager; // Reference to your shop manager

    void Start()
    {
        currentWave = 0;
        StartNewWave();
    }

    void Update()
    {
        if (isWaveActive && currentEnemies <= 0)
        {
            EndWave();
        }
    }

    private void StartNewWave()
    {
        currentWave++;
        maxEnemys = CalculateEnemyCount();
        currentEnemies = maxEnemys;
        isWaveActive = true;
        isShopOpen = false;
        StartCoroutine(SpawnEnemies());
    }

    private void EndWave()
    {
        isWaveActive = false;
        OpenShop();
    }

    private void OpenShop()
    {
        isShopOpen = true;
        shopManager.OpenShop();
    }

    public void ContinueToNextWave()
    {
        if (isShopOpen)
        {
            shopManager.CloseShop();
            isShopOpen = false;
            StartNewWave();
        }
    }

    private IEnumerator SpawnEnemies()
    {
        int enemiesSpawned = 0;

        while (enemiesSpawned < maxEnemys)
        {
            SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("There are no enemy spawn points!");
            return;
        }

        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        int randomEnemy = Random.Range(0, Enemys.Count);

        GameObject enemy = Instantiate(
            Enemys[randomEnemy],
            spawnPoints[randomSpawnPoint].position,
            Quaternion.identity
        );
        //  enemy.GetComponent<Enemy>()?.Initialize(this);
    }

    private int CalculateEnemyCount()
    {
        return 5 + (currentWave * 2); // Adjust this formula as needed
    }

    public void OnEnemyDeath()
    {
        currentEnemies--;
    }
}

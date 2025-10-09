using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    public GameObject thiefPrefab; // The thief prefab with EnemyFlee script
    public Transform[] spawnPoints; // Array of spawn positions
    public int[] enemiesPerWave; // Number of enemies per wave (e.g., set in Inspector: 3,5,7)
    public float timeBetweenWaves = 5f; // Delay before next wave starts after previous ends

    private int currentWave = 0;
    private int activeEnemies = 0;
    private bool waveActive = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (enemiesPerWave.Length > 0)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        waveActive = true;
        int numEnemies = enemiesPerWave[currentWave];
        activeEnemies = numEnemies;

        for (int i = 0; i < numEnemies; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(thiefPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(0.5f); // Slight delay between spawns
        }
    }

    void Update()
    {
        if (waveActive && activeEnemies <= 0)
        {
            waveActive = false;
            currentWave++;
            if (currentWave < enemiesPerWave.Length)
            {
                StartCoroutine(StartNextWave());
            }
            else
            {
                // All waves complete - add win condition or log
                Debug.Log("All waves completed!");
            }
        }
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave());
    }

    public void EnemyCaught()
    {
        activeEnemies--;
    }

    // Getter for current wave (0-based index)
    public int GetCurrentWave()
    {
        return currentWave;
    }

    // Getter for total number of waves
    public int GetTotalWaves()
    {
        return enemiesPerWave.Length;
    }
}
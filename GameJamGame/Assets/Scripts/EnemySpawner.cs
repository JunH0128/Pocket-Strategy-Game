using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;


    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Enemy Type Settings")]
    [SerializeField] private int fastEnemyStartWave = 3;
    [SerializeField] private int BossStartWave = 1;
    [SerializeField] private float fastEnemySpawnChance = 0.3f;
    [SerializeField] private int miniBossPerWave = 1;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private int BossesLeftToSpawn;
    private bool isSpawning = false;
    private bool waitingForPlayerToStart = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        waitingForPlayerToStart = true;
    }

    public void StartNextWave()
    {
        if (waitingForPlayerToStart && !isSpawning)
        {

        waitingForPlayerToStart = false;
        StartCoroutine(StartWave());
        }
    }   

    private void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();

        if (currentWave >= BossStartWave)
        {
            BossesLeftToSpawn = miniBossPerWave;
        }
        else
        {
            BossesLeftToSpawn = 0;
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        waitingForPlayerToStart = true;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    public bool IsWaitingToStart()
    {
        return waitingForPlayerToStart;
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];

        if (BossesLeftToSpawn > 0 && enemiesLeftToSpawn <= BossesLeftToSpawn)
        {
            if (enemyPrefabs.Length > 2 && enemyPrefabs[2] != null)
            {
                prefabToSpawn = enemyPrefabs[2];
                BossesLeftToSpawn--;
                Debug.Log("Spawning MiniBoss");
            }

        }
        else if (currentWave >= fastEnemyStartWave && Random.value < fastEnemySpawnChance)
        {
            if (enemyPrefabs.Length > 1 && enemyPrefabs[1] != null)
            {
                prefabToSpawn = enemyPrefabs[1];
                Debug.Log("Spawning Fast Enemy");
            }
        }
        else
        {
            Debug.Log("Spawning Regular Enemy");
        }
      
        Instantiate(prefabToSpawn, EnemyManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}

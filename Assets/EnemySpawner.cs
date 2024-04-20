using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool IsActive = true;
    public GameObject enemyPrefab; // Il prefab del nemico da spawnare
    public Transform spawnPoint; // Il punto di spawn
    public int numberOfEnemiesToSpawn = 1; // Numero di nemici da spawnare
    public float spawnRate = 10f; // Frequenza di spawn
    private int enemiesSpawned = 0; // Contatore dei nemici spawnati
    public float nextSpawnTime = 0f; // Tempo prossimo spawn

    private void Start()
    {
        nextSpawnTime = Time.time + spawnRate;
    }
    void Update()
    {
        // Controlla se è il momento di spawnare un nemico
        if (Time.time >= nextSpawnTime && enemiesSpawned < numberOfEnemiesToSpawn && IsActive)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        // Controlla che il prefab del nemico e il punto di spawn siano impostati
        if (enemyPrefab != null && spawnPoint != null)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemiesSpawned++;
        }
        else
        {
            Debug.LogWarning("Prefab nemico o punto di spawn non impostati!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool IsActive = true;
    public bool DoesHaveParentTile = true;
    public GameObject enemyPrefab; // Il prefab del nemico da spawnare
    public Transform spawnPoint; // Il punto di spawn
    public int numberOfEnemiesToSpawn = 1; // Numero di nemici da spawnare
    public float spawnRate = 10f; // Frequenza di spawn
    private int enemiesSpawned = 0; // Contatore dei nemici spawnati
    public float nextSpawnTime = 0f; // Tempo prossimo spawn
    private BasicTile parentTile;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnRate;
        if (transform.parent != null)
        {
            parentTile = transform.parent.GetComponent<BasicTile>();
            if (parentTile == null)
            {
                Debug.Log("Parent Tile not found");
            }
        }
        
    }
    void Update()
    {
        if (DoesHaveParentTile)
        {
            // Controlla se è il momento di spawnare un nemico
            if (Time.time >= nextSpawnTime && enemiesSpawned < numberOfEnemiesToSpawn && IsActive && !(parentTile.isLocked) && !(parentTile.isControlledByPlayer))
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + spawnRate;
            }  
        }
        else if (!DoesHaveParentTile && Time.time >= nextSpawnTime && enemiesSpawned < numberOfEnemiesToSpawn && IsActive)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }

        if (enemiesSpawned >= numberOfEnemiesToSpawn)
        {
            gameObject.SetActive(false);
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

    public void ResetSpawn()
    {
        gameObject.SetActive(true);
        // Resetta il contatore dei nemici spawnati
        enemiesSpawned = 0;

        // Resetta il tempo prossimo spawn
        nextSpawnTime = Time.time + spawnRate;

        // Riattiva lo spawner se non è già attivo

        IsActive = true;
    }
}

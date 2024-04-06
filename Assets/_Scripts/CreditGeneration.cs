using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditGeneration : MonoBehaviour
{
    public bool isActive;
    public GameObject creditPrefab; // Prefab degli oggetti crediti
    public Transform spawnPoint; // Punto di spawn dei crediti
    public float coinGenerationTimer = 5; // Intervallo di tempo in secondi per generare una moneta
    public int CoinsPool = 10;
    private Coroutine coinGenerationCoroutine;

    private void Start()
    {
        PlayerManager.credits++;
        SpawnCredit();
        coinGenerationCoroutine = StartCoroutine(GenerateCoins());
    }


    private IEnumerator GenerateCoins()
    {
        while (CoinsPool > 0)
        {
            yield return new WaitForSeconds(coinGenerationTimer);
            SpawnCredit();
            //AddCoin();
        }

        yield break;
    }

    private void SpawnCredit()
    {
        Instantiate(creditPrefab, spawnPoint.position, Quaternion.identity);
        CoinsPool -= 1;
    }


    public void UpdateCoinGenerationTimer(float newTimerValue)
    {
        coinGenerationTimer = newTimerValue;
        if (coinGenerationCoroutine != null)
        {
            StopCoroutine(coinGenerationCoroutine);
        }
        coinGenerationCoroutine = StartCoroutine(GenerateCoins());
    }

    public void ReduceTimer() { coinGenerationTimer /= 2; }
}

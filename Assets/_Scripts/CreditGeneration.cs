using com.cyborgAssets.inspectorButtonPro;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditGeneration : MonoBehaviour
{
    private bool isActive;
    
    public GameObject creditPrefab; // Prefab degli oggetti crediti
    public Transform spawnPoint; // Punto di spawn dei crediti
    public float coinGenerationTimer = 5; // Intervallo di tempo in secondi per generare una moneta
    private int totalCoins;
    public int CoinsPool = 10;
    public int respawnTime = 10;
    private int startingTime;
    private Coroutine coinGenerationCoroutine;
    private TMP_Text coinText;

    public delegate void RespawnCompleted();
    public event RespawnCompleted OnRespawnCompleted;

    private void Start()
    {
        StartingGeneration();
    }


    private IEnumerator GenerateCoins()
    {
        while (CoinsPool > 0)
        {
            isActive = true;
            yield return new WaitForSeconds(coinGenerationTimer);
            SpawnCredit();
            
            //AddCoin();
        }
        isActive = false;
        StartCoroutine(StartRespawnCountdown());
        yield break;
    }

    private void SpawnCredit()
    {
        Instantiate(creditPrefab, spawnPoint.position, Quaternion.identity);
        CoinsPool -= 1;
        coinText.text = CoinsPool.ToString() + " / " + totalCoins;
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

    public bool IsActive()
    {
        return isActive;
    }
    public void SetActive(bool value)
    {
        isActive = value;
    }

    [ProButton]
    public void RestartCoinGeneration()
    {
        CoinsPool = totalCoins;
        coinGenerationCoroutine = StartCoroutine(GenerateCoins());
    }

    private IEnumerator StartRespawnCountdown()
    {
        // Avvia il conteggio alla rovescia
        while (respawnTime > 0)
        {
            //Debug.Log("Respawn in: " + respawnTime + " seconds");
            yield return new WaitForSeconds(1f);
            respawnTime--;
        }
        
        coinText.text = totalCoins + " / " + totalCoins;
        yield break;
    }

    public void ResetRespawnTime()
    {
        respawnTime = startingTime;
    }

    public void StartingGeneration()
    {
        PlayerManager.credits++;

        coinGenerationCoroutine = StartCoroutine(GenerateCoins());
        coinText = GetComponentInChildren<TMP_Text>();
        totalCoins = CoinsPool;
        startingTime = respawnTime;
        SpawnCredit();
    }
}

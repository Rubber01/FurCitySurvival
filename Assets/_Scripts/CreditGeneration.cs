using com.cyborgAssets.inspectorButtonPro;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CreditGeneration : MonoBehaviour
{
    private bool isActive;
    
    public GameObject creditPrefab; // Prefab degli oggetti crediti
    public Transform spawnPoint; // Punto di spawn dei crediti
    public float coinGenerationTimer = 5; // Intervallo di tempo in secondi per generare una moneta
    public float regenerationTimer = 5;
    private int totalCoins;
    public int CoinsPool = 10;
    public int respawnTime = 10;
    private int startingTime;
    public Coroutine coinGenerationCoroutine;
    public Coroutine regenerationCoroutine;
    private TMP_Text coinText;
    //public bool inCooldown = false;

    public delegate void RespawnCompleted();
    public event RespawnCompleted OnRespawnCompleted;

    private void Start()
    {
        coinText = GetComponentInChildren<TMP_Text>();
        
        totalCoins = CoinsPool;
        CoinsPool = 0;
        coinText.text = CoinsPool.ToString() + " / " + totalCoins;
        regenerationCoroutine = StartCoroutine(CoinPoolRegeneration());
    }


    private IEnumerator GenerateCoins()
    {
        while (CoinsPool > 0)
        {
            isActive = true;
            yield return new WaitForSeconds(coinGenerationTimer);
            SpawnCredit();
            
        }
        
        yield break;
    }

    private void SpawnCredit()
    {
        Instantiate(creditPrefab, spawnPoint.position, Quaternion.identity);
        CoinsPool -= 1;
        coinText.text = CoinsPool.ToString() + " / " + totalCoins.ToString();
    }


    public bool IsActive()
    {
        return isActive;
    }
    public void SetActive(bool value)
    {
        isActive = value;
    }

    //[ProButton]
    //public void RestartCoinGeneration()
    //{
    //    CoinsPool = totalCoins;
    //    coinGenerationCoroutine = StartCoroutine(GenerateCoins());
    //}

    //private IEnumerator StartRespawnCountdown()
    //{
    //    // Avvia il conteggio alla rovescia
    //    while (respawnTime > 0)
    //    {
    //        inCooldown = true;
    //        //Debug.Log("Respawn in: " + respawnTime + " seconds");
    //        yield return new WaitForSeconds(1f);
    //        respawnTime--;
    //    }
    //    inCooldown = false;

    //    //CoinsPool = totalCoins;
        
    //    coinText.text = CoinsPool.ToString() + " / " + totalCoins;
    //    yield break;
    //}

    public void ResetRespawnTime()
    {
        respawnTime = startingTime;
    }

    public void StartingGeneration()
    {
        //PlayerManager.credits++;
        
        

        coinGenerationCoroutine = StartCoroutine(GenerateCoins());
        
        //totalCoins = CoinsPool;
        //startingTime = respawnTime;
        //SpawnCredit();
        
    }

    //public void StartCooldown()
    //{
    //    StartCoroutine(StartRespawnCountdown());
        
    //}

    public void StopCoinGeneration()
    {
        if (coinGenerationCoroutine != null)
        {
            StopCoroutine(coinGenerationCoroutine);
            coinGenerationCoroutine = null; // Resetta il riferimento alla coroutine
            isActive = false;
        }
    }

    private IEnumerator CoinPoolRegeneration()
    {
        while (true)
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!Regen coin: " + CoinsPool + " | " + totalCoins);
            if (CoinsPool < totalCoins)
            {
                CoinsPool = CoinsPool + 1;
            }
            coinText.text = CoinsPool.ToString() + " / " + totalCoins;
            yield return new WaitForSeconds(regenerationTimer);
        }
    }
}

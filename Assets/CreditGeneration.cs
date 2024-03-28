using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditGeneration : MonoBehaviour
{
    public bool isActive;
    //private LevelManager levelManager;
    

    
    public float coinGenerationTimer = 5; // Intervallo di tempo in secondi per generare una moneta
    private Coroutine coinGenerationCoroutine;

    private void Start()
    {
        // Avvia la coroutine per generare le monete
        /*
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.AddCoins(1);
        */
        PlayerManager.credits++;
        coinGenerationCoroutine = StartCoroutine(GenerateCoins());
    }


    private IEnumerator GenerateCoins()
    {
        while (true)
        {
            yield return new WaitForSeconds(coinGenerationTimer);
            AddCoin();
        }
    }

    private void AddCoin()
    {
        /*
        Debug.Log("LvlMngr coins: "+ levelManager._GameData.Coins);
        levelManager.AddCoins(1);
        */
        PlayerManager.credits++;
    }

    // Metodo per aggiornare la coroutine quando il timer cambia
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

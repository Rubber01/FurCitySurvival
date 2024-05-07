using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class WinCondition : MonoSingleton<WinCondition>
{
    public TMP_Text timerText;
    public TMP_Text Victory_Label;
    public GameObject victoryPanel;
    public TMP_Text victoryTimer;
    private int totalRaidableBuisiness;
    public LoseBuisiness loseBuisiness;

    public int BuildingsOwned;

    //public TileDictionary hexData;

    private bool gameEnded = false;
    private float timer;


    private void Start()
    {
        timer = 0f;

        totalRaidableBuisiness = loseBuisiness.raidManager.Length;


        //hexData = HexGrid.Instance.hexData;
        //if (totalRaidableBuildings == CountRaidableTiles(16))
        //{

        //}
        //else
        //{
        //    totalRaidableBuildings = 11;
        //    Debug.LogWarning("Raidable tagged objects don't match Hexgrid");
        //}

    }

    private void Update()
    {
        if (!gameEnded)
        {
            timer += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        victoryTimer.text = timerText.text;

    }

    public void CheckWinCondition()
    {
        if (totalRaidableBuisiness == BuildingsOwned)
        {
            EndGame();
        }
    }

    [ProButton]
    public void EndGame()
    {
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);

        gameEnded = true;

        // Calcolare il tempo trascorso in base al punteggio di vittoria
        float elapsedTime = timer;
        

        // Assegnare la medaglia in base al tempo
        if (elapsedTime <= 60) // Tempo per la medaglia d'oro
        {
            Victory_Label.text = "Gold Medal!";
            
        }
        else if (elapsedTime <= 120) // Tempo per la medaglia d'argento
        {
            Victory_Label.text = "Silver Medal!";
        }
        else // Tempo per la medaglia di bronzo
        {
            Victory_Label.text = "Bronze Medal!";
        }
    }
}

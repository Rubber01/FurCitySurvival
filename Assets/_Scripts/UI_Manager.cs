using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UI_Manager : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField]
    private GameObject pauseMenuUI;

    public void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(pauseMenuUI);
    }


    void Update()
    {

        if (pauseMenuUI == null) //&& SceneManager.GetActiveScene().name != "MainMenu")
        {
            pauseMenuUI = GameObject.Find("PauseMenu");
        }


        // Controlla se il giocatore preme il tasto "Esc"
        if (SceneManager.GetActiveScene().name != "MainMenu" && Input.GetKeyDown(KeyCode.Escape))
        {
            // Se il gioco è già in pausa, riprendi il gioco
            if (isPaused)
            {
                ResumeGame();
            }
            else // Altrimenti metti il gioco in pausa
            {
                PauseGame();
            }
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName , LoadSceneMode.Single);
        ResumeGame();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.UnloadScene("NomeScena");
    }


    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Fermo il tempo del gioco per metterlo in pausa
        pauseMenuUI.SetActive(true); // Attiva il menu di pausa
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Riprendi il tempo del gioco
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false); // Disattiva il menu di pausa
        }
    }
    

    public void SaveGame() { Debug.Log("Feature TBD"); throw new NotImplementedException(); }
    public void LoadGame() { Debug.Log("Feature TBD"); throw new NotImplementedException(); }
    public void Settings() { Debug.Log("Feature TBD"); throw new NotImplementedException(); }
}

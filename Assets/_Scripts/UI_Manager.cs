using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UI_Manager : MonoSingleton<UI_Manager>
{
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
        SceneManager.LoadScene(levelName);
    }

    public void SaveGame() { Debug.Log("Feature TBD"); throw new NotImplementedException(); }
    public void LoadGame() { Debug.Log("Feature TBD"); throw new NotImplementedException(); }
    public void Settings() { Debug.Log("Feature TBD"); throw new NotImplementedException(); }
}

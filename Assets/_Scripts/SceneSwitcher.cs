using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcher: MonoBehaviour
{
    [SerializeField] private int play;
    [SerializeField] private int tutorial;
    
    public void Play()
    {
        SceneManager.LoadScene(play);
    }
    public void Tutorial()
    {
        SceneManager.LoadScene(tutorial);
    }
    public void ExitFromGame()
    {
        Application.Quit();
    }
}

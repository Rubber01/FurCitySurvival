using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcher: MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(2);
    }
    public void Tutorial()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitFromGame()
    {
        Application.Quit();
    }
}

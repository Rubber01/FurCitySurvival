using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTP : MonoBehaviour
{
    public static bool isGameState=false;
    [SerializeField] private int sceneToLoad;
    [SerializeField] bool disable = false;
    void Update()
    {
        if (disable == true)
        {
            if (Input.touchCount > 0)
            {
                gameObject.SetActive(false);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                gameObject.SetActive(false);
            }
        }
        else if (Input.touchCount > 0)
        {
            isGameState=true;
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            isGameState= true;
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else
            isGameState= false;

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] private int rep;
    private ReputationSystem reputationSystem;
    [SerializeField] private GameObject boxJonny;
    [SerializeField] private int sceneToLoad;
    public void SetLevelSystem(ReputationSystem reputationSystem)
    {
        this.reputationSystem = reputationSystem;
    }
    private void Awake()
    {
        boxJonny = GameObject.Find("BoxJonny");
        
    }

    void Update()
    {
        Debug.Log("Box jonny è abilitato? Button " + boxJonny.transform.GetComponentInChildren<Button>().IsActive());
        Debug.Log("reputationSystem.GetLevelNumber() >= rep " + reputationSystem.GetLevelNumber() + " rep " + rep);
        Debug.Log("Box jonny esiste? "+ boxJonny.GetComponent<UI_Assistant>());

        if (reputationSystem.GetLevelNumber() >= rep && boxJonny != null && boxJonny.GetComponent<UI_Assistant>() != null && boxJonny.transform.GetComponentInChildren<Button>().IsActive() == false)
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);

        }

    }
}

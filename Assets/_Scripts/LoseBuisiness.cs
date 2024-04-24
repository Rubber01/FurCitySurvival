using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseBuisiness : MonoBehaviour
{
    [SerializeField] private RaidManager[] raidManager;
    [SerializeField] private GameObject[] temp;
    public bool chiamato;

    private void Awake()
    {
        raidManager = GameObject.FindObjectsOfType<RaidManager>();
        temp = new GameObject[raidManager.Length];

        if (raidManager.Length > 0)
        {
            for(int i = 0; i<raidManager.Length; i++)
            {
                temp[i]= raidManager[i].GetGameObject();
                Debug.Log("iiii+ " + i);
                Debug.Log("Oggetto con lo script raidManager trovato: " + raidManager[i].gameObject.name);

            }



        }
        else
        {
            
            //Debug.Log("Nessun oggetto con lo script raidManager trovato in scena.");
        }
    }
    //fai il metodo copia come in java
    public void PlayerDeath()
    {
        chiamato = true;
        Debug.Log("PlayerDeath chiamato");
        int k = Random.Range(0, raidManager.Length);
        while (raidManager[k].GetRaided() == false)
        {
            k = Random.Range(0, raidManager.Length);
        }
        
        Debug.Log("PlayerDeath k creato");
        Destroy(raidManager[k].GetGameObject());
        
        Debug.Log("PlayerDeath k distrutto");
        Vector3 newPosition = new Vector3(temp[k].transform.position.x, temp[k].transform.position.y, temp[k].transform.position.z);
        Transform newTransform = new GameObject().transform;
        newTransform.position = newPosition;
        Instantiate(temp[k], newTransform);
        Debug.Log("PlayerDeath k creato");
        temp[k].GetComponent<RaidManager>().enabled = true;
        //temp[k].GetComponent<RaidManager>().Restart();
    }
    

}

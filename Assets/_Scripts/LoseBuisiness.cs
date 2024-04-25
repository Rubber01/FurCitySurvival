using com.cyborgAssets.inspectorButtonPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseBuisiness : MonoBehaviour
{
    [SerializeField] private RaidManager[] raidManager;
    [SerializeField] private GameObject[] temp;
    private ReputationLinker linker;
    public bool chiamato;

    private void Awake()
    {
        raidManager = GameObject.FindObjectsOfType<RaidManager>();
        linker = transform.GetComponent<ReputationLinker>();
        temp = new GameObject[raidManager.Length];

        if (raidManager.Length > 0)
        {
            for (int i = 0; i < raidManager.Length; i++)
            {
                temp[i] = raidManager[i].GetGameObject();
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
    [ProButton]
    public void PlayerDeath()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject.GetComponent<Player>().isDead)
        {
            return;
        }
        playerObject.GetComponent<Player>().isDead = true;

        chiamato = true;
        Debug.Log("PlayerDeath chiamato");
        int k = UnityEngine.Random.Range(0, raidManager.Length);
        while (raidManager[k].GetRaided() == false)
        {
            k = UnityEngine.Random.Range(0, raidManager.Length);
        }

        Debug.Log("PlayerDeath k creato");

        GameObject obj = raidManager[k].GetGameObject();
        if (obj == null)
        {
            Debug.Log("OnDeath - RAID MANAGER == NULL");
        }
        else
        {
            GameObject newTile = Instantiate(temp[k], obj.transform.parent);
            raidManager[k] = newTile.GetComponent<RaidManager>();
            Debug.Log("PlayerDeath k distrutto");
            Debug.Log("PlayerDeath k creato");
            newTile.GetComponent<RaidManager>().enabled = true;
            newTile.GetComponent<BasicTile>().TileControlLost();
            temp[k] = newTile;
            //obj.SetActive(false);
            int indice = Array.IndexOf(linker.raidManager, obj.GetComponent<RaidManager>());
            linker.raidManager[indice] = raidManager[k];
            int indice2 = Array.IndexOf(linker.basicTile, obj.GetComponent<BasicTile>());
            linker.basicTile[indice2] = newTile.GetComponent<BasicTile>();
            raidManager[k].Restart();
            //temp[k].GetComponent<RaidManager>().Restart();
            Destroy(obj);
        }


    }
}

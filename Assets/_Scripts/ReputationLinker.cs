using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationLinker : MonoBehaviour
{
    [SerializeField] private ReputationController reputationController;
    //[SerializeField] private Player player;
    [SerializeField] public RaidManager[] raidManager;
    [SerializeField] public BasicTile[] basicTile;
    [SerializeField] private TriggerTileUnlocker[] tileUnlocker;
    public ReputationSystem reputationSystem;

    private void Awake()
    {
        reputationSystem = new ReputationSystem();
        reputationController.SetLevelSystem(reputationSystem);
        //raidManager.SetLevelSystem(reputationSystem);
        //player.SetLevelSystem(reputationSystem); 
        ReputationSystemAnimated levelSystemAnimated = new ReputationSystemAnimated(reputationSystem);
        reputationController.SetLevelSystemAnimated(levelSystemAnimated);
        tileUnlocker = GameObject.FindObjectsOfType<TriggerTileUnlocker>();
        if (tileUnlocker.Length > 0)
        {
            // Gli oggetti con lo script sono stati trovati
            foreach (TriggerTileUnlocker obj in tileUnlocker)
            {
                // Fai qualcosa con ogni oggetto trovato
                obj.SetLevelSystem(reputationSystem);

                Debug.Log("Oggetto con lo script TriggerTileUnlocker trovato: " + obj.gameObject.name);
            }
        }
        else
        {
            // Nessun oggetto con lo script � stato trovato
            Debug.Log("Nessun oggetto con lo script TriggerTileUnlocker trovato in scena.");
        }
        basicTile = GameObject.FindObjectsOfType<BasicTile>();
        if (basicTile.Length > 0)
        {
            foreach(BasicTile obj in basicTile)
            {
                obj.SetLevelSystem(reputationSystem);
            }
        }
        raidManager = GameObject.FindObjectsOfType<RaidManager>();
        if (raidManager.Length > 0)
        {
            // Gli oggetti con lo script sono stati trovati
            foreach (RaidManager obj in raidManager)
            {
                // Fai qualcosa con ogni oggetto trovato
                obj.SetLevelSystem(reputationSystem);
                //Debug.Log("Oggetto con lo script raidManager trovato: " + obj.gameObject.name);
            }
        }
        else
        {
            // Nessun oggetto con lo script � stato trovato
            //Debug.Log("Nessun oggetto con lo script raidManager trovato in scena.");
        }
    }

    

}

        // Verifica se sono stati trovati oggetti con lo script
        


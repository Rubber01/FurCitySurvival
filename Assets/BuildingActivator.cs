using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingActivator : MonoBehaviour
{

    
    GameObject parentObject;
    public CreditGeneration creditGeneration;
    private BuildingTile buildingTile;
    private HireHenchmen hireHenchmen;
    private void Start()
    {
        parentObject = gameObject.transform.parent.gameObject;
        buildingTile = parentObject.GetComponent<BuildingTile>();
        creditGeneration = parentObject.transform.GetComponentInChildren<CreditGeneration>();

        hireHenchmen = parentObject.transform.GetComponentInChildren<HireHenchmen>();
        
    }

    //da implementare ontriggerStay
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>()  /* && resources */)
        {
            if (creditGeneration != null)
            {
                
                if (buildingTile.isSpawned && !(creditGeneration.IsActive()))
                {
                    //creditGeneration.StartingGeneration();
                    //CreditGeneration creditgeneration = gameObject.GetComponentInChildren<CreditGeneration>();

                    if (creditGeneration.CoinsPool == 0 && creditGeneration.respawnTime == 0)
                    {
                        Debug.Log("Building Activated!");
                        creditGeneration.RestartCoinGeneration();
                        //Debug.Log("credit Generation: " + gameObject.GetComponentInChildren<CreditGeneration>().name);
                        creditGeneration.ResetRespawnTime();
                    }
                }

                
            }
            else if(buildingTile.isSpawned  && hireHenchmen.isActive)//Da specificare il caso in cui l'edificio sia di Recruitment
            {
                
                    
                    hireHenchmen.SpawnAlly();
                

            }
            else
            {
                Debug.LogError("CreditGeneration o HireHenchmen non trovati nel padre dell'oggetto corrente.");
            }
            
        }
    }

    
}

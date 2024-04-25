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
        if (hireHenchmen != null)
        {
            hireHenchmen.StartAllyRegeneration();
        }
    }


    //da implementare ontriggerStay
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>()  /* && resources */)
        {
            if (buildingTile.isSpawned/* && (creditGeneration.IsActive())*/)
            {
                //if (creditGeneration.CoinsPool <= 0 && creditGeneration.inCooldown == false)
                //{
                //    creditGeneration.StartCooldown();
                //    //creditGeneration.ResetRespawnTime();
                //}
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<Player>()  /* && resources */)
        {
            
            if (creditGeneration != null)
            {
                
                if (buildingTile.isSpawned && !(creditGeneration.IsActive()))
                {

                    if (/*creditGeneration.inCooldown == false &&*/ creditGeneration.CoinsPool>0)
                    {
                        creditGeneration.StartingGeneration();
                    }
                }

                
            }
            else if(buildingTile.isSpawned  && hireHenchmen.isActive)//Da specificare il caso in cui l'edificio sia di Recruitment
            {
                    hireHenchmen.SpawnAlly();
            }
            else
            {
                Debug.Log("CreditGeneration o HireHenchmen non trovati nel padre dell'oggetto corrente.");
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>()  /* && resources */)
        {
            if (creditGeneration != null)
            {
                creditGeneration.StopCoinGeneration();
                creditGeneration.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static CreditGeneration;

public class BuildingTile : BasicTile
{
    public Building buildingData;
    //public GameData gameData;

    private int buildingCost;
    public bool isSpawned = false;
    

    private void Awake()
    {
        buildingCost = buildingData.buildingCost;
        //Debug.Log("Building cost: " + buildingCost);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Building Tile: onTriggerEnter" + other);
        Debug.Log(other.gameObject.GetComponent<Player>());
        if (other.gameObject.GetComponent<Player>())
        {
            switch (buildingData.resourceCostType)
            {
                //case ResourceType.MetalScrap:
                //    if (PlayerManager.metalScrapNumber >= buildingCost && !(isSpawned))
                //    {
                //        Debug.Log("triggered building tile by: " + other.gameObject.GetComponent<Player>().name);
                //        PlayerManager.metalScrapNumber -= buildingCost;
                //        gameObject.GetComponent<BuildingPrefabController>().SpawnBuilding(transform.position, transform.rotation);

                //        isSpawned = true;
                //    }
                //    break;
                //case ResourceType.Metal:
                //    if (PlayerManager.metalNumber >= buildingCost && !(isSpawned))
                //    {
                //        Debug.Log("triggered building tile by: " + other.gameObject.GetComponent<Player>().name);
                //        PlayerManager.metalScrapNumber -= buildingCost;
                //        gameObject.GetComponent<BuildingPrefabController>().SpawnBuilding(transform.position, transform.rotation);

                //        isSpawned = true;
                //    }
                //    break;
                //case ResourceType.PlasticWaste:
                //    if (PlayerManager.plasticWasteNumber >= buildingCost && !(isSpawned))
                //    {
                //        Debug.Log("triggered building tile by: " + other.gameObject.GetComponent<Player>().name);
                //        gameObject.GetComponent<BuildingPrefabController>().SpawnBuilding(transform.position, transform.rotation);
                //        PlayerManager.plasticWasteNumber -= buildingCost;
                //        isSpawned = true;
                //    }
                //    break;
                //case ResourceType.Plastic:
                //    if (PlayerManager.plasticNumber >= buildingCost && !(isSpawned))
                //    {
                //        Debug.Log("triggered building tile by: " + other.gameObject.GetComponent<Player>().name);
                //        gameObject.GetComponent<BuildingPrefabController>().SpawnBuilding(transform.position, transform.rotation);
                //        PlayerManager.plasticWasteNumber -= buildingCost;
                //        isSpawned = true;
                //    }
                //    break;
                case ResourceType.Credit:
                    if (PlayerManager.credits >= buildingCost && !(isSpawned))
                    {
                        Debug.Log("triggered building tile by: " + other.gameObject.GetComponent<Player>().name);
                        gameObject.GetComponent<BuildingPrefabController>().SpawnBuilding(transform.position, transform.rotation);
                        PlayerManager.credits -= buildingCost;
                        isSpawned = true;
                    }
                    //Codice tagliato in BuildingActivator.cs
                    break;
                case ResourceType.Henchmen:
                    Debug.Log("------> ResoyrceTypeHenchmen");
                    break;
            }
        }
    }
    

}

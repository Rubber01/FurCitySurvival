using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BuildingTile : MonoBehaviour
{
    public Building buildingData;
    public GameData gameData;

    private int buildingCost;

    private void Awake()
    {
        buildingCost = buildingData.buildingCost;
        Debug.Log("Building cost: " + buildingCost);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (gameData.Coins >= buildingCost)
            {
                Debug.Log("triggered building tile by: " + other.gameObject.GetComponent<Player>().name);
                gameObject.GetComponent<BuildingPrefabController>().SpawnBuilding(transform.position, transform.rotation);
            }
        }
    }
}

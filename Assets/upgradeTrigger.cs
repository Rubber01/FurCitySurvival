using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class upgradeTrigger : MonoBehaviour
{

    private int counter = 0;
    public BuildingPrefabController buildingPrefabController;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("upgrade triggered");
        buildingPrefabController.UpgradeBuilding();
        //buildingPrefabController.ToggleAdditionalMesh(counter,true);
        counter ++;
    }
}

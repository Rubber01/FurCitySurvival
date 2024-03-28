using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBuildingTrigger : MonoBehaviour
{
    [SerializeField] public GenericBuilding building;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("upgrade triggered");

        building.UseBuilding();
        //buildingPrefabController.ToggleAdditionalMesh(counter,true);
    }

    private void OnTriggerStay(Collider other)
    {
        building.UseBuilding();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefineBuildingController : MonoBehaviour, GenericBuilding
{
    [SerializeField] private Building refinementBuilding;

    private short resourceTillFull;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("upgrade triggered");

        UseBuilding();
        //buildingPrefabController.ToggleAdditionalMesh(counter,true);
    }

    private void OnTriggerStay(Collider other)
    {
        UseBuilding();
    }

    public void UseBuilding()
    {
        if (refinementBuilding.resourceType == ResourceType.MetalScrap)
        {
            if (PlayerManager.metalScrapNumber >= refinementBuilding.resourceRequirementNumber)
            {
                PlayerManager.metalScrapNumber -= refinementBuilding.resourceRequirementNumber;
                SpawnRefinedResource();
            }
        }

        if (refinementBuilding.resourceType == ResourceType.PlasticWaste)
        {
            if (PlayerManager.plasticWasteNumber >= refinementBuilding.resourceRequirementNumber)
            {
                PlayerManager.plasticWasteNumber -= refinementBuilding.resourceRequirementNumber;
                SpawnRefinedResource();
            }
        }
    }

    private void SpawnRefinedResource()
    {
        GameObject instantiatedResource = Instantiate(refinementBuilding.refinedResource.resourceModel, transform.position + new Vector3(-2f, 1.5f, 2f), Quaternion.identity);
    }
}

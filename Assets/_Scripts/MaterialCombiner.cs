using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCombiner : MonoBehaviour, GenericBuilding
{
    public bool isActive;
    [SerializeField] private Building combineBuilding;

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
        Debug.Log("Combining?");
        if (combineBuilding.resourceType1 == ResourceType.MetalScrap)
        {

           // Debug.Log("Materials: metalScrap#: " + PlayerManager.metalScrapNumber + "|" + PlayerManager.plasticWasteNumber);

            if (combineBuilding.resourceType2 == ResourceType.PlasticWaste)
            {
                Debug.Log("Materials2 ok!");
                if (PlayerManager.metalScrapNumber >= combineBuilding.resourceType1Number &&
                    PlayerManager.plasticWasteNumber >= combineBuilding.resourceType2Number)
                {
                    PlayerManager.metalScrapNumber -= combineBuilding.resourceType1Number;
                    PlayerManager.plasticWasteNumber -= combineBuilding.resourceType2Number;
                    SpawnCombinedResource();
                }
            }

        }
    }

    private void SpawnCombinedResource()
    {
        Debug.Log("SpawnCombinedResource:" + combineBuilding.combinedResource.resourceModel);
        GameObject instantiatedResource = Instantiate(combineBuilding.combinedResource.resourceModel, transform.position + new Vector3(-1.5f,1.5f,0f),Quaternion.identity); //BUG: spawna l'oggetto ma non rimane nella posizione
        //instantiatedResource.GetComponent<Rigidbody>().velocity = new Vector3(-6, 2, 2);
        
        Rigidbody rb = instantiatedResource.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            
            rb.isKinematic = false;
        }

        instantiatedResource.transform.SetParent(transform, true);
    }

}

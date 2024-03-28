using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTile : BasicTile
{
    public ResourceStack resourceStackData;
    //public GameData gameData;

    public ResourceType resourceType;

    private void Start()
    {
        gameObject.GetComponent<ResourceStackPrefabController>().SpawnResource(transform.position, transform.rotation);
    }
}

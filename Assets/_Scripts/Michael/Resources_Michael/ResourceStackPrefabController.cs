using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStackPrefabController : MonoBehaviour
{
    public ResourceStack resourceStackData;

    private GameObject resourceStackModel;

    public void SpawnResource(Vector3 position, Quaternion rotation)
    {
        resourceStackModel = Instantiate(resourceStackData.resourceStackModel, position, rotation);
        resourceStackModel.transform.rotation = rotation * Quaternion.Euler(90f, 0f, 0f);
        resourceStackModel.transform.SetParent(transform);
    }
}

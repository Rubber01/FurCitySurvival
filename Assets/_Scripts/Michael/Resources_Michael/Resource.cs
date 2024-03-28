using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Credit,
    MetalScrap,
    Metal,
    PlasticWaste,
    Plastic,
    Chip,
    // Aggiungi altri tipi di risorse necessari al tuo gioco
}

[CreateAssetMenu(fileName = "NewResource", menuName = "ScriptableObjects/Resource")]
public class Resource : ScriptableObject
{
    public ResourceType resourceType;
    public string resourceName;
    public int resourceTier; //Resource's refinement tier
    public GameObject resourceModel;
}

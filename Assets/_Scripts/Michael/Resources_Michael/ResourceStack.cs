using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResourceStack", menuName = "ScriptableObjects/ResourceStack")]
public class ResourceStack : ScriptableObject
{

    public ResourceType resourceType;
    public string resourceStackName;
    public int resourceHitPoints;
    public float spawnTime;
    public int currentMiningLevel; // Vari livelli di estrazione risorse che fanno variare il modello
    public GameObject resourceStackModel;

    public Resource resourceUnit;

}

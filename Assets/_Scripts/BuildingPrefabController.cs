using System.Collections;
using System.Collections.Generic;
using UnityEditor.Il2Cpp;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using UnityEngine.UIElements;
using com.cyborgAssets.inspectorButtonPro;

public class BuildingPrefabController : MonoBehaviour
{
    public Building buildingData; // Riferimento allo ScriptableObject dell'edificio
    public int currentLevel = 1;
    private int currentActiveMeshIndex = 0;
    private GameObject buildingmodel;

    private void Start()
    {
        // Verifica che il riferimento allo ScriptableObject sia valido
        if (buildingData != null)
        {
            // Imposta il livello di upgrade in base al valore nello ScriptableObject
            currentLevel = 1;
            buildingData.currentUpgradeLevel = currentLevel;
        }
    }

    public void UpgradeBuilding()
    {
        // Aumenta il livello di upgrade
        buildingData.currentUpgradeLevel++;
        // Attiva la mesh aggiuntiva successiva
        ToggleAdditionalMesh(currentActiveMeshIndex + 1, true);
        buildingData.UpgradeToLevel(buildingData.currentUpgradeLevel);
    }

    [ProButton]
    private void ToggleAdditionalMesh(int index, bool active)
    {
        Debug.Log("index" + index);
        Debug.Log("children: " + buildingmodel.transform.childCount);
        // Controlla se il prefab ha le mesh aggiuntive
        if (buildingmodel.transform.childCount >= 2 && index < buildingmodel.transform.childCount)
        {
            
            // Attiva la mesh aggiuntiva specificata
            buildingmodel.transform.GetChild(index).gameObject.SetActive(active);
            // Aggiorna l'indice della mesh aggiuntiva attualmente attiva
            currentActiveMeshIndex = index;
        }
    }

   
    public void SpawnBuilding(Vector3 position, Quaternion rotation)
    {
         
        
        GameObject newBuilding = Instantiate(buildingData.buildingModel, position, rotation);
        buildingmodel = newBuilding;
        newBuilding.transform.rotation = rotation * Quaternion.Euler(90f, 0f, 0f);
        newBuilding.transform.SetParent(transform);

    }
}

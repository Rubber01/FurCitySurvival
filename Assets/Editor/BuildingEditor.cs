using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Building))]
public class BuildingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Building building = (Building)target;

        // Mostra sempre i campi dati principali
        EditorGUILayout.LabelField("Main Settings", EditorStyles.boldLabel);
        building.buildingType = (BuildingType)EditorGUILayout.EnumPopup("Building Type", building.buildingType);
        building.buildingName = EditorGUILayout.TextField("Building Name", building.buildingName);
        building.resourceCostType = (ResourceType)EditorGUILayout.EnumPopup("Resource Cost Type", building.resourceCostType);
        building.buildingCost = EditorGUILayout.IntField("Building Cost", building.buildingCost);
        building.currentUpgradeLevel = EditorGUILayout.IntField("Current Upgrade Level", building.currentUpgradeLevel);
        building.buildingModel = (GameObject)EditorGUILayout.ObjectField("Building Model", building.buildingModel, typeof(GameObject), true);


        switch (building.buildingType)
        {
            case BuildingType.ResourceRefinery:
                building.showResourceRefineryVariables = EditorGUILayout.Toggle("Show Resource Refinery Variables", building.showResourceRefineryVariables);
                if (building.showResourceRefineryVariables)
                {
                    // Mostra le variabili specifiche per l'edificio di raffinazione delle risorse
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("resourceType"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("refinedResource"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("resourceRequirementNumber"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("resourceRefinementRate"));
                }
                break;
            case BuildingType.CreditGenerator:
                building.showCreditGeneratorVariables = EditorGUILayout.Toggle("Show Credit Generator Variables", building.showCreditGeneratorVariables);
                if (building.showCreditGeneratorVariables)
                {
                    // Mostra le variabili specifiche per l'edificio generatore di crediti
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("creditGenerationRate"));
                }
                break;
            case BuildingType.CombineBuilding:
                building.showCombineBuildingVariables = EditorGUILayout.Toggle("Show Combine Building Variables", building.showCombineBuildingVariables);
                if (building.showCombineBuildingVariables)
                {
                    // Mostra le variabili specifiche per l'edificio di combinazione
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("resourceType1"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("resourceType1Number"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("resourceType2"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("resourceType2Number"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("resultingResourceType"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("combinedResource"));
                }
                break;
            case BuildingType.PubBuilding:
                building.showPubBuildingVariables = EditorGUILayout.Toggle("Show Pub Building Variables", building.showPubBuildingVariables);
                if (building.showPubBuildingVariables)
                {
                    // Mostra le variabili specifiche per l'edificio del pub
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("test"));
                    
                }
                break;
            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

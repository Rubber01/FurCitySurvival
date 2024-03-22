using UnityEngine;
using static UnityEditor.FilePathAttribute;
using UnityEngine.UIElements;

// Enumerazione per i diversi tipi di edifici
public enum BuildingType
{
    ResourceRefinery,
    CreditGenerator,
    CombineBuilding
}

// Enumerazione per i tipi di risorse
public enum ResourceType
{
    Resource1,
    Resource2,
    // Aggiungi altri tipi di risorse necessari al tuo gioco
}

// Classe che rappresenta un tipo generico di edificio
[CreateAssetMenu(fileName = "NewBuilding", menuName = "ScriptableObjects/Building")]
public class Building : ScriptableObject
{
    public BuildingType buildingType;
    public string buildingName;
    public int buildingCost;
    public int currentUpgradeLevel; // Livello di upgrade attuale
    public GameObject buildingModel;

    // Variabili specifiche per l'edificio di raffinazione delle risorse
    [Header("ResourceRefinery")]
    public ResourceType resourceType;
    [SerializeField] private int resourceRefinementRate;

    // Variabili specifiche per l'edificio generatore di crediti
    [Header("CreditGenerator")]
    [SerializeField] private int creditGenerationRate;

    // Variabili specifiche per l'edificio di combinazione
    [Header("CombineBuilding")]
    [SerializeField] private ResourceType resourceType1;
    [SerializeField] private ResourceType resourceType2;
    [SerializeField] private ResourceType resultingResourceType;

    private void OnValidate()
    {
        switch (buildingType)
        {
            case BuildingType.ResourceRefinery:
                // Implementa qui la logica specifica per l'edificio di raffinazione delle risorse
                break;
            case BuildingType.CreditGenerator:
                // Implementa qui la logica specifica per l'edificio generatore di crediti
                break;
            case BuildingType.CombineBuilding:
                // Implementa qui la logica specifica per l'edificio di combinazione
                break;
            default:
                break;
        }
    }

    public void UpgradeToLevel(int _level)
    {
        currentUpgradeLevel += _level;
    }

    
}

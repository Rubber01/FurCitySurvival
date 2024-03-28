using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using Unity.VisualScripting;

// Enumerazione per i diversi tipi di edifici
public enum BuildingType
{
    ResourceRefinery,
    CreditGenerator,
    CombineBuilding
}

// Classe che rappresenta un tipo generico di edificio
[CreateAssetMenu(fileName = "NewBuilding", menuName = "ScriptableObjects/Building")]
public class Building : ScriptableObject
{
    public BuildingType buildingType;
    public string buildingName;
    public ResourceType resourceCostType;
    public int buildingCost;
    public int currentUpgradeLevel; // Livello di upgrade attuale
    public GameObject buildingModel;

    // Variabili specifiche per l'edificio di raffinazione delle risorse
    [Header("ResourceRefinery")]
    public ResourceType resourceType;
    public Resource refinedResource;
    public int resourceRequirementNumber; // Numero di risorse richieste per generare 1 risorsa raffinata
    [SerializeField] private int resourceRefinementRate; // Tempo necessario per convertire in risorsa raffinata

    // Variabili specifiche per l'edificio generatore di crediti
    [Header("CreditGenerator")]
    [SerializeField] private int creditGenerationRate;

    // Variabili specifiche per l'edificio di combinazione
    [Header("CombineBuilding")]
    [SerializeField] public ResourceType resourceType1;
    [SerializeField] public int resourceType1Number;
    [SerializeField] public ResourceType resourceType2;
    [SerializeField] public int resourceType2Number;
    [SerializeField] public ResourceType resultingResourceType;
    [SerializeField] public Resource combinedResource;

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
        currentUpgradeLevel = _level;
    }

    public IEnumerator GenerateCoins()
    {
        while (true)
        {
            yield return new WaitForSeconds(creditGenerationRate); // Aspetta per il tempo specificato

            // Aggiungi una moneta
            AddCoin();

            // Se vuoi aggiornare la grafica o eseguire altre operazioni quando una moneta viene generata, puoi farlo qui
        }
    }

    private void AddCoin()
    {
        // Aggiungi una moneta al contatore dei crediti
        // Assicurati di avere un sistema per tenere traccia dei crediti, potresti avere un singleton di gioco o qualcosa di simile
        // Ad esempio:
        
        LevelManager.Instance.SpawnCoin(buildingModel.transform.position);
    }
}

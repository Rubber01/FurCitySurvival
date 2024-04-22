using AYellowpaper.SerializedCollections;
using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTile : MonoBehaviour
{
    public Vector2Int tileCoords;
    [SerializeField]
    public Vector2Int TileCoords { get { return tileCoords; } set { tileCoords = value; } } //aggiungere le coordinate alla generazione
    public Vector3 tilePosition;
    [SerializeField]
    public Vector3 TilePosition { get { return tilePosition; } set { tilePosition = value; } }
    public bool isLocked;
    public int unlockCost;
    public int RepCost;
    public ResourceType resourceRequiredToUnlock;
    public bool isControlledByPlayer = false;
    public bool isRaidable = true;

    
    private MeshCollider meshCollider;
    


    //[SerializedDictionary("Grid Coord", "TileObject")]
    //public SerializedDictionary<Vector2Int, GameObject> neighbourTiles;

    private void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        //gameData = GetComponent<GameData>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Triggered by: " + other.name);
    //    Debug.Log("------------>" + other.gameObject.GetComponent<Player>());
    //    if (other.gameObject.GetComponent<Player>()) 
    //    {
    //        if (isLocked && LevelManager.Instance._GameData.Coins >= unlockCost)
    //        {
    //            UnlockTile();
    //        }
    //    }
    //}

    [ProButton]
    public void UnlockTile()
    {

        
        //Debug.Log("LevelManager + " + LevelManager.Instance._GameData.Coins);

        Debug.Log("Is it locked1? " + isLocked);
        if (isLocked)
        {
            
            //Debug.Log(LevelManager.Instance._GameData.Coins);
            Debug.Log("Tile Unlocked!");
            GameObject childObject = this.transform.GetChild(0).gameObject;
            
            childObject.SetActive(false);
            //LevelManager.Instance._GameData.Coins -= unlockCost;
            isLocked = false;
        }
    }

    public List<BasicTile> adjacentTiles = new List<BasicTile>();
    public GameObject triggerBoxPrefab;

    // Metodo per aggiungere un tile adiacente
    public void AddAdjacentTile(BasicTile tile)
    {
        if (!adjacentTiles.Contains(tile))
        {
            adjacentTiles.Add(tile);
        }
    }

    // Metodo per trovare i tile adiacenti
    [ProButton]
    public void FindAdjacentTiles()
    {
        adjacentTiles.Clear();

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f); // Considera un raggio di 1.1 unità per trovare i tile adiacenti

        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject) // Evita di includere se stesso tra i tile adiacenti
            {
                BasicTile adjacentTile = collider.gameObject.GetComponent<BasicTile>();
                if (adjacentTile != null && adjacentTile.isLocked)
                {
                    adjacentTiles.Add(adjacentTile);

                    // Calcola la posizione del trigger box a metà strada tra questo tile e il tile adiacente
                    Vector3 triggerPosition = (transform.position + adjacentTile.transform.position) / 2f;
                    

                    // Calcola la direzione tra questo tile e il tile adiacente
                    Vector3 direction = (adjacentTile.transform.position - transform.position).normalized;


                    // Calcola la rotazione necessaria per orientare il trigger box nella direzione della distanza tra i due tile
                    Quaternion triggerRotation = Quaternion.LookRotation(direction, Vector3.up);

                    triggerPosition += Vector3.up * 0.2f; //per aggiustare la posizione

                    // Spawn del trigger box
                    GameObject triggerBox = Instantiate(triggerBoxPrefab, triggerPosition, triggerRotation);
                    // Imparenta il trigger box con il tile corrente
                    triggerBox.transform.SetParent(transform);
                    //triggerBox.transform.localScale = new Vector3(0.1f, 1f, 0.1f); // Imposta la scala del trigger box, potrebbe variare a seconda delle tue esigenze
                    triggerBox.GetComponent<TriggerTileUnlocker>().tileToUnlock = adjacentTile;

                }
            }
        }
    }

}

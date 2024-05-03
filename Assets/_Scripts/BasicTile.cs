using AYellowpaper.SerializedCollections;
using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private ReputationSystem reputationSystem;
    private MeshCollider meshCollider;
    private GameObject lockedArea;
    private GameObject building;
    private TextPopup textPopup;
    private MeshController mc;

    public void SetLevelSystem(ReputationSystem reputationSystem)
    {
        Debug.Log("Reputation System chiamato");
        this.reputationSystem = reputationSystem;
    }


    //[SerializedDictionary("Grid Coord", "TileObject")]
    //public SerializedDictionary<Vector2Int, GameObject> neighbourTiles;

    private void Start()
    {
        mc = transform.GetComponentInChildren<MeshController>();
        if (mc != null)
        {
            mc.SwitchMesh(isControlledByPlayer);
            building = mc.gameObject;
        }

        lockedArea = this.transform.GetChild(0).gameObject;
        

        //meshCollider = GetComponent<MeshCollider>();

        if (reputationSystem == null )
        {
            GameObject linker = GameObject.Find("FuncitionUpdater&ReputationLinker");
            SetLevelSystem(linker.GetComponent<ReputationLinker>().reputationSystem);
        }
        

        if (RepCost > reputationSystem.GetLevelNumber())
        {
            textPopup = TextPopup.Create(transform.position, "★" + RepCost);
            textPopup.transform.SetParent(transform, true);
            Color color = new Color(123f / 255f, 138f / 255f, 249f / 255f);
            textPopup.textMesh.color = color;//new Color(123, 138, 249);
            
            if (isLocked)
            {
                lockedArea.transform.parent = null;
                //TileScaler(true, 0.5f);
                Show_Hide_Building(false);
                StartCoroutine(TileScaler(true, 0.5f, 2.0f));
            }
        }
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
            //GameObject childObject = this.transform.GetChild(0).gameObject;
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                lockedArea.SetActive(false);
            }
                
#else
            lockedArea.SetActive(false);
#endif
            //LevelManager.Instance._GameData.Coins -= unlockCost;
            isLocked = false;
            //TileScaler(false, 1);
            Show_Hide_Building(true);
            StartCoroutine(TileScaler(true, 1f, 2.0f));
            if (textPopup != null)
            {
                textPopup.gameObject.SetActive(false);
            }
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

    //[ProButton]
    //public void TileScaler(bool _isReduced, float _scaleFactor)
    //{

    //    // Definisci la scala finale in base a se l'oggetto deve essere rimpicciolito o meno
    //    Vector3 targetScale = _isReduced ? Vector3.one * (100* _scaleFactor) : (Vector3.one * 100);

    //    // Usa Lerp per interpolare gradualmente tra la scala corrente e la scala finale
    //    transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 100f); // Il valore 5f è una velocità di interpolazione arbitraria, puoi regolarla a tuo piacimento

    //}

    public IEnumerator TileScaler(bool _isReduced, float _scaleFactor, float duration)
    {
        // Definisci la scala finale in base a se l'oggetto deve essere rimpicciolito o meno
        Vector3 targetScale = _isReduced ? Vector3.one * (1 * _scaleFactor) : (Vector3.one * 1); //(100 * _scaleFactor) : (Vector3.one * 100);

        float timer = 0f;
        Vector3 initialScale = transform.localScale;

        // Continua ad aggiornare la scala fino a quando non viene raggiunta la scala target
        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, timer / duration);
            yield return null;
        }

        // Assicurati che la scala sia esattamente la scala target alla fine della coroutine
        transform.localScale = targetScale;
    }


    public void TileControlLost()
    {
        
        //temp[k].transform.Find("EnemySpawner").gameObject.SetActive(true);
        isControlledByPlayer = false;
        mc = transform.GetComponentInChildren<MeshController>();
        mc.SwitchMesh(isControlledByPlayer);
        isRaidable = true;
        Transform enemySpawner = transform.Find("EnemySpawner");
        if (enemySpawner != null)
        {
            enemySpawner.gameObject.SetActive(true);

            enemySpawner.GetComponent<EnemySpawner>().ResetSpawn();

        }
        else
        {
            Debug.LogError("ENEMYSPAWNER NOT FOUND!");
        }

        
        Transform buildingActivator = transform.Find("BuildingActivator");
        if (buildingActivator != null)
        {
            buildingActivator.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("BUILDINGACTIVATOR NOT FOUND!");
        }
        

    }
    
    private void Show_Hide_Building(bool _Toshow)
    {
        if (building != null)
        {
            if (_Toshow)
            {
                building.gameObject.SetActive(true);
            }
            else
            {
                building.gameObject.SetActive(false);
            }
            
        }
    }

}

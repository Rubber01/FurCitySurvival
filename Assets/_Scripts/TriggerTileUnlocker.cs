using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerTileUnlocker : MonoBehaviour
{
    private HexGrid hexgrid;
    // Riferimento al componente BasicTile
    public BasicTile tileToUnlock;
    public GameObject tileUnlockedPrefab;
    private void Awake()
    {
        hexgrid = GameObject.Find("Grid").GetComponent<HexGrid>();
    }

    [SerializeField] private TextMeshPro requiredResourceNumber;

    private void Update()
    {
        requiredResourceNumber.text = tileToUnlock.resourceRequiredToUnlock + " " + tileToUnlock.unlockCost.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Unlock");
        // Controlla se il collider con cui si è verificato il contatto è il giocatore
        if (other.CompareTag("Player")) //if (other.CompareTag("Player"))
        {
            //hexgrid.UpdateTile(new KeyValuePair<Vector2Int, Vector3>(tileToUnlock.GetComponent<BasicTile>().TileCoords, tileToUnlock.GetComponent<BasicTile>().TilePosition));

            // Sblocca il tile associato al trigger box
            if (tileToUnlock != null)
            {

                {
                    switch (tileToUnlock.resourceRequiredToUnlock)
                    {
                        case ResourceType.MetalScrap:
                            if (PlayerManager.metalScrapNumber >= tileToUnlock.unlockCost)
                            {
                                PlayerManager.metalScrapNumber -= tileToUnlock.unlockCost;
                                tileToUnlock.UnlockTile();
                                hexgrid.ChangeSpecificTile(tileToUnlock, tileUnlockedPrefab);
                            }
                            break;
                        case ResourceType.Metal:
                            if (PlayerManager.metalNumber >= tileToUnlock.unlockCost)
                            {
                                PlayerManager.metalNumber -= tileToUnlock.unlockCost;
                                tileToUnlock.UnlockTile();
                                hexgrid.ChangeSpecificTile(tileToUnlock, tileUnlockedPrefab);
                            }
                            break;
                        case ResourceType.PlasticWaste:
                            if (PlayerManager.plasticWasteNumber >= tileToUnlock.unlockCost)
                            {
                                PlayerManager.plasticWasteNumber -= tileToUnlock.unlockCost;
                                tileToUnlock.UnlockTile();
                                hexgrid.ChangeSpecificTile(tileToUnlock, tileUnlockedPrefab);
                            }
                            break;
                        case ResourceType.Plastic:
                            if (PlayerManager.plasticNumber >= tileToUnlock.unlockCost)
                            {
                                PlayerManager.plasticNumber -= tileToUnlock.unlockCost;
                                tileToUnlock.UnlockTile();
                                hexgrid.ChangeSpecificTile(tileToUnlock, tileUnlockedPrefab);
                            }
                            break;
                        case ResourceType.Credit:
                            if (PlayerManager.credits >= tileToUnlock.unlockCost)
                            {
                                PlayerManager.credits -= tileToUnlock.unlockCost;
                                tileToUnlock.UnlockTile();
                                hexgrid.ChangeSpecificTile(tileToUnlock, tileUnlockedPrefab);
                            }
                            break;
                    }
                }
               
            }
        }
    }

    
}

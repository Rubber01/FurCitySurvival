using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTile : MonoBehaviour
{
    public bool isLocked;
    public int unlockCost;
    private MeshCollider meshCollider;
    [SerializeField]
    private GameData gameData;

    private void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        //gameData = GetComponent<GameData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);
        Debug.Log("------------>" + other.gameObject.GetComponent<Player>());
        if (other.gameObject.GetComponent<Player>()) 
        {
            if (isLocked && gameData.Coins >= unlockCost)
            {
                UnlockTile();
            }
        }
    }

    [ProButton]
    public void UnlockTile()
    {
        Debug.Log(gameData.Coins);
        
            isLocked = false;
            Debug.Log("Tile Unlocked!");
        GameObject childObject = this.transform.GetChild(0).gameObject;
        childObject.SetActive(false);
        gameData.Coins -= unlockCost;
        
    }
}

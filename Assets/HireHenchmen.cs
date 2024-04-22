using System.Collections;
using TMPro;
using UnityEngine;

public class HireHenchmen : MonoBehaviour
{
    public bool isActive = false;
    
    public GameObject allyPrefab;
    public Transform spawnPoint; // Punto di spawn
    private int totalAllies;
    public int allyPool = 3;
    public int allyCost;
    public int respawnTime = 3;
    private int startingTime;
    private Coroutine allySpawnCoroutine;
    private TMP_Text allyNumText;
    private TMP_Text allyCostText;

    public delegate void RespawnCompleted();
    public event RespawnCompleted OnRespawnCompleted;

    private void Start()
    {
        GameObject obj_allyNumText = GameObject.Find("AllyNumText");
        if (obj_allyNumText != null)
        {
            allyNumText = obj_allyNumText.GetComponent<TMP_Text>();
        }
        //allyNumText = GetComponentInChildren<TMP_Text>();

        GameObject obj = GameObject.Find("AllyCost");
        if (obj != null)
        {
            allyCostText = obj.GetComponent<TMP_Text>();
        }

        allyCostText.text += "\n" + allyCost.ToString();
        //allySpawnCoroutine = StartCoroutine(GenerateAlly());

        totalAllies = allyPool;
        startingTime = respawnTime;

        allyNumText.text = allyPool.ToString() + " / " + totalAllies;
        isActive = true;
        //SpawnAlly();
    }

    //private IEnumerator GenerateAlly()
    //{
    //    while (allyPool > 0)
    //    {
    //        isActive = true;
    //        yield return new WaitForSeconds(respawnTime);
    //        SpawnAlly();

    //    }
    //    isActive = false;
    //    StartCoroutine(StartRespawnCountdown());
    //    yield break;
    //}

    public void SpawnAlly()
    {
        if (PlayerManager.credits >= allyCost && PlayerManager.currentHench < PlayerManager.henchmenSlots)
        {
            
            PlayerManager.credits -= allyCost;
            GameObject newAllyInstance = Instantiate(allyPrefab, spawnPoint.position, Quaternion.identity);
            //allyPrefab.GetComponent<AllyAI>().alreadyAttacked = true;
            allyPool -= 1;
            PlayerManager.currentHench += 1;
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                //Player.Instance.AddAlly(allyPrefab);
                playerObject.GetComponent<Player>().allies.Add(newAllyInstance);
            }
            else
            {
                Debug.LogError("player not found");
            }

            allyNumText.text = allyPool.ToString() + " / " + totalAllies;
            Debug.Log("HenchmenHired!");

            //if ()
            //StartCoroutine(StartRespawnCountdown());
        }

        
    }

    private IEnumerator StartRespawnCountdown()
    {
        // Avvia il conteggio alla rovescia
        while (respawnTime > 0)
        {
            Debug.Log("Respawn in: " + respawnTime + " seconds");
            yield return new WaitForSeconds(1f);
            respawnTime--;
        }

        allyNumText.text = totalAllies + " / " + totalAllies;
        yield break;
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class HireHenchmen : MonoBehaviour
{
    public bool isActive = false;
    
    public GameObject allyPrefab;
    public Transform spawnPoint; // Punto di spawn
    private int totalAllies;
    public int allyPool = 3;
    public int allyCost;
    //public int respawnTime = 3;
    public float regenerationTimer = 5;
    private int startingTime;
    private Coroutine regenAllyCoroutine;
    private TMP_Text allyNumText;
    private TMP_Text allyCostText;

    public delegate void RespawnCompleted();
    public event RespawnCompleted OnRespawnCompleted;

    private void Start()
    {
        Transform childTransform = transform.Find("AllyNumText");
        if (childTransform != null)
        {
            allyNumText = childTransform.GetComponentInChildren<TMP_Text>();
        }
        else
        {
            Debug.Log("AllyNumText not found");
        }

        Transform childTransform2 = transform.Find("AllyCost");
        if (childTransform != null)
        {
            allyCostText = childTransform2.GetComponentInChildren<TMP_Text>();
        }
        else
        {
            Debug.Log("AllyNumText not found");
        }
        

        allyCostText.text ="Recruit cost:"+ "\n" + allyCost.ToString();
        //allySpawnCoroutine = StartCoroutine(GenerateAlly());

        totalAllies = allyPool;
        allyPool = 0;   
        
        //startingTime = respawnTime;

        allyNumText.text = allyPool.ToString() + " / " + totalAllies;
        isActive = true;

        //regenAllyCoroutine = StartCoroutine(AllyRegeneration());

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

    private IEnumerator AllyRegeneration()
    {
        while (true)
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!Regen ally: " + allyPool + " | " + totalAllies);
            if (allyPool < totalAllies)
            {
                allyPool++;
                
            }
            allyNumText.text = allyPool.ToString() + " / " + totalAllies;
            yield return new WaitForSeconds(regenerationTimer);
        }
    }

    public void StartAllyRegeneration()
    {
        regenAllyCoroutine = StartCoroutine(AllyRegeneration());
    }

    public void EmptyAllyPool()
    {
        allyPool = 0;
    }

    //private IEnumerator StartRespawnCountdown()
    //{
    //    // Avvia il conteggio alla rovescia
    //    while (respawnTime > 0)
    //    {
    //        Debug.Log("Respawn in: " + respawnTime + " seconds");
    //        yield return new WaitForSeconds(1f);
    //        respawnTime--;
    //    }

    //    allyNumText.text = totalAllies + " / " + totalAllies;
    //    yield break;
    //}
}

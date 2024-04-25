using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaidManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> previousCollidedObjects = new List<GameObject>();
    [SerializeField] private bool countdownStarted = false;
    [SerializeField] private bool countupStarted = false;
    [SerializeField] private int alliesRequired;
    [SerializeField] private Material materialRaided;
    [SerializeField] private Material materialNotRaided;
    [SerializeField] private Renderer renderer;
    public bool raided = false;
    [SerializeField] private float delay = 2;
    [SerializeField] private float countdownTime = 10f;
    [SerializeField] private float originalCountdownTime = 10f; // Memorizza il tempo di partenza originale
    [SerializeField] private int reputation;
    private ReputationSystem reputationSystem;
    private HealthBar healthBar;
    private BuildingTile tile;
    private Transform buildingActivator;
    public void SetLevelSystem(ReputationSystem reputationSystem)
    {
        this.reputationSystem = reputationSystem;
    }

    private void Start()
    {
        if (raided == false)
        {
            renderer.material = materialNotRaided;
        }
        buildingActivator = transform.Find("BuildingActivator");
        healthBar = HealthBar.Create(new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z - 3f), originalCountdownTime, countdownTime);
        tile = GetComponentInParent<BuildingTile>();
    }
    public bool GetRaided()
    {
        return raided;
    }
    public void SetRaided(bool raided)
    {
         this.raided=raided;
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("allies hitting " + previousCollidedObjects.Count);
        //Debug.LogError("Collision Stay");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ally"))
        {
            if (!previousCollidedObjects.Contains(collision.gameObject))
            {
                previousCollidedObjects.Add(collision.gameObject);

                if (!countdownStarted && previousCollidedObjects.Count - 1 >= alliesRequired)
                {
                    StartCoroutine(CountDown());
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Oggetto " + collision.gameObject.name + " sta uscendo");
        // Verifica se il collider con cui si � colliso appartiene ai layers "Player" o "Ally"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ally"))
        {
            // Verifica se l'oggetto che ha causato la collisione � gi� stato controllato precedentemente
            if (previousCollidedObjects.Contains(collision.gameObject))
            {
                // Aggiungi l'oggetto corrente alla lista degli oggetti precedenti
                previousCollidedObjects.Remove(collision.gameObject);

                // Avvia il conto alla rovescia solo se non � gi� stato avviato
                

            }

        }
        /*if (previousCollidedObjects.Count == 0)
            StartCoroutine(CountUp(delay));*/
    }

    IEnumerator CountDown()
    {
        while (countdownTime > 0 && previousCollidedObjects.Count > 0)
        {
            countupStarted = false;
            countdownStarted = true;
            Debug.Log("Countdown: " + countdownTime.ToString("F1") + " seconds" + " numero di oggetti " + previousCollidedObjects.Count);
            countdownTime -= previousCollidedObjects.Count;
            healthBar.UpdateHealthBar(originalCountdownTime, countdownTime);
            yield return new WaitForSeconds(1f);
        }

    }
    
    IEnumerator CountUp(float delay)
    {
        yield return new WaitForSeconds(delay);
        while (countdownTime < originalCountdownTime)
        {
            countdownStarted = false;
            countupStarted = true;
            Debug.Log("Countup: " + countdownTime.ToString("F1") + " seconds" + " numero di oggetti " + previousCollidedObjects.Count);
            countdownTime++;
            healthBar.UpdateHealthBar(originalCountdownTime, countdownTime);
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator RaidEnd()
    {
        yield return new WaitForSeconds(0.25f);
        healthBar.DestroyHealthBar();
        enabled = false;
        
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    private void Update()
    {
        if (previousCollidedObjects.Count == 0 && countupStarted == false && countdownTime < originalCountdownTime)
        {

            StartCoroutine(CountUp(delay));
        }

        if (countdownTime <= 0 && raided == false)
        {
            raided = true;
            //transform.GetComponentInChildren<CreditGeneration>().SetActive(true);
            if (transform.GetComponentInChildren<CreditGeneration>())
            {
                transform.GetComponentInChildren<CreditGeneration>().enabled = true;
                if (buildingActivator != null)
                {
                    buildingActivator.gameObject.SetActive(true);
                }
            }
            else if (transform.GetComponentInChildren<HireHenchmen>())
            {
                if (buildingActivator != null)
                {
                    buildingActivator.gameObject.SetActive(true);
                    transform.GetComponentInChildren<HireHenchmen>().EmptyAllyPool();
                }
            }

            tile.isRaidable = false;
            tile.isControlledByPlayer = true;
            raided = true;
            renderer.material = materialRaided;
            StartCoroutine(RaidEnd());
            reputationSystem.AddExperience(reputation);
            //buildingActivator = transform.Find("BuildingActivator");

            GameObject playerObject = GameObject.FindWithTag("Player");
            //sacrificio Henchmen ?
            for (int i = 0; i < alliesRequired; i++)
            {
                    Destroy(playerObject.GetComponent<Player>().allies[i]);
            }
            
            transform.GetComponentInChildren<EnemySpawner>().GetComponent<EnemySpawner>().enabled = false;


        }
    }
    public void Restart()
    {
        raided=false;
        countdownStarted = false;
        countupStarted = false;
        countdownTime = originalCountdownTime;
        renderer.material = materialNotRaided;
    }
}

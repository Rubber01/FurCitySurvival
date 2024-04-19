using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> previousCollidedObjects = new List<GameObject>();
    [SerializeField] private bool countdownStarted = false;
    [SerializeField] private bool countupStarted = false;
    private bool raided = false;
    [SerializeField] private float delay = 2;
    [SerializeField] private float countdownTime = 10f; 
    [SerializeField] private float originalCountdownTime=10f; // Memorizza il tempo di partenza originale
    [SerializeField] private int reputation;
    private ReputationSystem reputationSystem;
    private HealthBar healthBar;
    public void SetLevelSystem(ReputationSystem reputationSystem)
    {
        this.reputationSystem = reputationSystem;
    }

    private void Start()
    {
        healthBar=HealthBar.Create(new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z-3f), originalCountdownTime, countdownTime);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ally"))
        {
            if (!previousCollidedObjects.Contains(collision.gameObject))
            {
                previousCollidedObjects.Add(collision.gameObject);

                if (!countdownStarted)
                {
                    StartCoroutine(CountUp());
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Oggetto " + collision.gameObject.name + " sta uscendo");
        // Verifica se il collider con cui si è colliso appartiene ai layers "Player" o "Ally"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ally"))
        {
            // Verifica se l'oggetto che ha causato la collisione è già stato controllato precedentemente
            if (previousCollidedObjects.Contains(collision.gameObject))
            {
                // Aggiungi l'oggetto corrente alla lista degli oggetti precedenti
                previousCollidedObjects.Remove(collision.gameObject);

                // Avvia il conto alla rovescia solo se non è già stato avviato
                
            }
            
        }
        if (previousCollidedObjects.Count == 0)
            StartCoroutine(CountUp());
    }
  
    IEnumerator CountUp()
    {
        if (previousCollidedObjects.Count == 0 && countupStarted == false && countdownTime < originalCountdownTime)
        {

            while (countdownTime < originalCountdownTime)
            {
                countdownStarted = false;
                countupStarted = true;
                Debug.Log("Countup: " + countdownTime.ToString("F1") + " seconds" + " numero di oggetti " + previousCollidedObjects.Count);
                countdownTime++;
                healthBar.UpdateHealthBar(originalCountdownTime, countdownTime);
                yield return new WaitForSeconds(1f);
            }
        }else if (previousCollidedObjects.Count != 0 && countdownStarted==false && countdownTime!=0)
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
    }
    private void Update()
    {
        
         
        if (countdownTime == 0 && raided == false)
        {
            raided = true;
            transform.GetComponentInChildren<CreditGeneration>().SetActive(true);
            reputationSystem.AddExperience(reputation);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStackController : MonoBehaviour
{
    
    [SerializeField] private ResourceStack resourceStack;
    
    private int hitPoints;

    private float timeTillSpawn;

    private float damageTimer;

    private Collider resourceStackCollider;
    [SerializeField] private GameObject resourceMesh;

    [SerializeField] private float xSpawnSpeed;
    [SerializeField] private float ySpawnSpeed;
    [SerializeField] private float zSpawnSpeed;


    private void Start()
    {
        hitPoints = resourceStack.resourceHitPoints;
        resourceStackCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (damageTimer > 0) damageTimer -= Time.deltaTime;

        if (timeTillSpawn > 0)
        {
            timeTillSpawn -= Time.deltaTime;
        }

        if (timeTillSpawn <= 0)
        {
            resourceMesh.SetActive(true);
            resourceStackCollider.enabled = true;
        }

        if (hitPoints == 0 && timeTillSpawn <= 0)
        {
            resourceMesh.SetActive(false);
            resourceStackCollider.enabled = false;
            hitPoints = resourceStack.resourceHitPoints;
            timeTillSpawn = resourceStack.spawnTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DealDamage();
        }
    }

        private void OnTriggerStay(Collider other)
    {
        if (damageTimer <= 0 && other.tag == "Player")
        {
            DealDamage();
        }
    }


    private void DealDamage()
    {
        hitPoints--;
        GameObject instantiatedResource = Instantiate(resourceStack.resourceUnit.resourceModel, transform);
        instantiatedResource.GetComponent<Rigidbody>().velocity = new Vector3(xSpawnSpeed, ySpawnSpeed, zSpawnSpeed);
        damageTimer = 1;
    }
}

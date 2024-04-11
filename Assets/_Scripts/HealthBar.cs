using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private float reduceSpeed = 2;
    private float target = 1;
    private Camera cam;
    public static HealthBar Create(Vector3 position, float maxHealth, float health)
    {
        Debug.Log("healthbar entrato ");
        Transform healthBarTransfrom = Instantiate(GameAssets.i.healthBar, position, Quaternion.identity);
        HealthBar healthBar = healthBarTransfrom.GetComponent<HealthBar>();
        healthBar.UpdateHealthBar(maxHealth, health);

        Debug.Log("healthbar creato");

        return healthBar;
    }
    private GameObject temp;
    private void Start()
    {
        
        temp= GameObject.FindGameObjectWithTag("MainCamera");
        cam = temp.GetComponent<Camera>();
        cam = Camera.main;
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        Debug.Log("Health bar: Max Health=" + maxHealth + " Current Health=" + currentHealth+ " Bar value= " + currentHealth/maxHealth);
        target = currentHealth / maxHealth;
    }
}

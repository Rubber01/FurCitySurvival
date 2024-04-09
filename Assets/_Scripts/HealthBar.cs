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

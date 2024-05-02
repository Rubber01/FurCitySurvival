using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAI : MonoBehaviour
{
    [SerializeField] private int health = 10;
	[SerializeField] private int maxHealth = 10;
	private HealthBar healthBar;

	[SerializeField] private int damage = 2;
	//Attacking
	[SerializeField] private float timeBetweenAttacks;
	[SerializeField] public bool alreadyAttacked = false;
	//States
	[SerializeField] float attackRange, lookRadius = 10f;
	[SerializeField] bool playerInSightRange, playerInAttackRange;
	 
	[SerializeField] Transform target;
	private Transform temp;
	UnityEngine.AI.NavMeshAgent agent;
	[SerializeField] private AnimatorController _animatorController;

	void Start()
	{
		healthBar = GetComponentInChildren<HealthBar>();
		healthBar.UpdateHealthBar(maxHealth, health);
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	void Update()
	{
		if (target == null)
		{
			GameObject p = GameObject.FindGameObjectWithTag("Player");
			Debug.Log("in target null " + p.name);
			target = p.transform;
		}
		// Get the distance to the player
		float distance = Vector3.Distance(target.position, transform.position);
        //Debug.Log("Distanza " + distance);
        if (!alreadyAttacked)
            _animatorController.PlayIdle();
        // If inside the radius
        if (distance <= lookRadius)
		{
			// Move towards the player
			agent.SetDestination(target.position);
            if (!alreadyAttacked)
                _animatorController.PlayRun();
			if (distance <= agent.stoppingDistance /*|| distance <= attackRange*/)
			{
                //FARE CONTROLLO SE è PLAYER
                if (target.CompareTag("Player") == false)
                {
					Debug.Log("chiamo attacco");
					AttackPlayer();
					FaceTarget();
                }else
                {
					_animatorController.PlayIsHappy();
					FaceTarget();
				}

			}
		}
	}
	public void SetTarget(GameObject enemy)
    {
		temp = target;
		target = enemy.transform;
		agent.SetDestination(target.position);
		
		//target = temp;
		
	}
	// Point towards the player
	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
	private void AttackPlayer()
	{
		
		if (!alreadyAttacked)
		{
			//Player.TakeDamage()
			//animazione.start
			Debug.Log("Attacco nemico Ally sto attaccando " + target.name);
			alreadyAttacked = true;
			_animatorController.PlayHit();
			Invoke(nameof(ResetAttack), timeBetweenAttacks);
            if (target != null) // Controllo inserito
            {
                try
                {
					target.GetComponent<EnemyAI>().TakeDamage(damage);
                }
                catch (System.Exception e) // Correzione: "catch" richiede solo il tipo di eccezione
                {
                    Debug.Log(e + "<-----Exception");
                }
            }
        }
	}
	private void ResetAttack()
	{
		_animatorController.PlayStopHit();
		alreadyAttacked = false;
	}
	public void TakeDamage(int damage)
	{
		health -= damage;
		healthBar.UpdateHealthBar(maxHealth, health);
		if (health <= 0)
		{

			Invoke(nameof(DestroyEnemy), 0.5f);
		}
	}
    private void DestroyEnemy()
    {
        _animatorController.PlayIsDying();
		AudioManager.instance.Play("HenchDeath");
        Invoke(nameof(Death), 5);
        agent.speed = 0;
    }
    private void Death()
    {
		AudioManager.instance.Stop("HenchDeath");
        Destroy(gameObject);
    }
}

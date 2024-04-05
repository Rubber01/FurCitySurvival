using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/* Makes enemies follow and attack the player */

public class EnemyAI : MonoBehaviour
{
	[SerializeField] private int health = 10;
	[SerializeField] private int damage = 2;
	//Attacking
	[SerializeField] private float timeBetweenAttacks;
	[SerializeField] bool alreadyAttacked = false;
	//States
	[SerializeField] float attackRange, lookRadius=10f;
	[SerializeField] bool playerInSightRange, playerInAttackRange;

	[SerializeField] Transform target;
	NavMeshAgent agent;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		// Get the distance to the player
		float distance = Vector3.Distance(target.position, transform.position);
		Debug.Log("Distanza "+ distance);
		// If inside the radius
		if (distance <= lookRadius)
		{
			// Move towards the player
			agent.SetDestination(target.position);
			if (distance <= agent.stoppingDistance  || distance<= attackRange)
			{
				// Attack (animazioni e funzione che sottrae da player la vita)
				Debug.Log("chiamo attacco");
				AttackPlayer();
				FaceTarget();
			}
		}
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
		Debug.Log("sono dentro attacco");
		if (!alreadyAttacked)
		{
			//Player.TakeDamage()
			//animazione.start
			Debug.Log("Attacco");
			alreadyAttacked = true;
			target.gameObject.GetComponent<Player>().TakeDamage(damage);
			Invoke(nameof(ResetAttack), timeBetweenAttacks);
		}
	}
	private void ResetAttack()
	{
		alreadyAttacked = false;
	}
	public void TakeDamage(int damage)
	{
		health -= damage;

		if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
	}
	private void DestroyEnemy()
	{
		Destroy(gameObject);
	}
}
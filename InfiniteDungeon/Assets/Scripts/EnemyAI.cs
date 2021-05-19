using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    int currentHealth;

    [SerializeField] private NavMeshAgent agent;

    private Transform player;

    [SerializeField] private LayerMask Ground, Player;

    [SerializeField] private Animator enemyAnimator;

    [SerializeField] private Transform weaponEnd;

    [SerializeField] private float runSpeed;

    [SerializeField] private SphereCollider boneEnd;

    //Patroling
    [SerializeField] private Vector3 walkPoint;
    bool walkPointSet;
    [SerializeField] private float walkPointRange;

    //Attacking
    [SerializeField] private float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSightRange, playerInAttackRange;

    void Start()
    {
        currentHealth = maxHealth;
    }



    private void Awake()
    {
        player = GameObject.Find("Player2").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        //Checks if the player is in this range in order to be able to chase him
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        //Checks if the players is in this range in order to be able to attack him
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange)
            Patrol();
        if (playerInSightRange && !playerInAttackRange)
            Chase();
        if (playerInSightRange && playerInAttackRange)
            Attack();

    }

    private void Patrol()
    {
        //Moves in a certain range, randomly
        enemyAnimator.SetFloat("Speed", 1f);
        if (!walkPointSet)
            SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }

    private void SearchWalkPoint()
    {
        //Calculates the coordinates for the Patrol method
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            walkPointSet = true;
    }

    private void Chase()
    {
        enemyAnimator.SetFloat("Speed", 1f);
        agent.SetDestination(player.position);
    }
    IEnumerator AttackNormal(float time)
    {
        //Timing for the attack move
        yield return new WaitForSeconds(time);
        boneEnd.enabled = true;
        yield return new WaitForSeconds(0.08f);
        boneEnd.enabled = false;
    }
    private void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {

            //Attack code
            //The attack animations runs, then its goes into idle state
            //in order to avoid running if it stays still
            enemyAnimator.SetTrigger("Attack");
            enemyAnimator.SetFloat("Speed", 0f);
            StartCoroutine(AttackNormal(1f));
            alreadyAttacked = true;
            //enemyAnimator.SetTrigger("Idle");
            //Avoid spamming
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {

        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        //For the range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        enemyAnimator.SetTrigger("Death");
        Destroy(this, 0.25f);
        Destroy(gameObject, 2);
    }
}

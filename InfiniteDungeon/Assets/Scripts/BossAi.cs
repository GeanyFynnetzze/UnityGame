using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAi : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 300;

    int currentHealth;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform player;

    [SerializeField] private LayerMask Ground, Player;

    [SerializeField] private Animator enemyAnimator;

    [SerializeField] private SphereCollider weaponEnd;

    [SerializeField] private int attackDamage;

    [SerializeField] private float weaponRange;

    [SerializeField] private Collider weaponCollider;

    [SerializeField]  private Collider playerCollider;

    [SerializeField]  private BossHp healthbar;


    //Attacking
    [SerializeField]private float timeBetweenAttacks;

    [SerializeField] bool alreadyAttacked;

    //States
    [SerializeField] private float sightRange, attackRange;

    [SerializeField] private bool playerInSightRange, playerInAttackRange;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }



    private void Awake()
    {
        player = GameObject.Find("Player2").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

       //If it's in sight range, it starts running after him, if its even closer it can also hit him!
        if (playerInSightRange && !playerInAttackRange)
            Chase();
        if (playerInSightRange && playerInAttackRange)
            Attack();

    }

   

    IEnumerator AttackNormal(float time)
    {
        //In order to time his attack
        yield return new WaitForSeconds(time);
        weaponEnd.enabled = true;
        yield return new WaitForSeconds(1);
        weaponEnd.enabled = false; 
    }

    private void Chase()
    {
        enemyAnimator.SetFloat("speed", 1.0f); 
        agent.SetDestination(player.position);
    }


    private void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

      

        if (!alreadyAttacked)
        {

            //Attack code
            //We play the animation and set the speed to 0 in order to get him in
            //Idle state after the animation, and then we start the coroutine
            enemyAnimator.SetTrigger("attack1");
            enemyAnimator.SetFloat("speed", 0f);
            StartCoroutine(AttackNormal(0.2f));


            //In order to keep him from spamming attacks, balance is needed
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        //Allows us to see the range 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        if (weaponEnd == null)
            return;
        Gizmos.color = Color.magenta;
    }

    public void TakeDamageBoss(int damage)
    {
        //When his health lowers, the slider also gets updated
        enemyAnimator.SetTrigger("take_damage");
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        enemyAnimator.SetTrigger("death");
        Destroy(this, 1f);
        Destroy(gameObject, 2f);
    }


}

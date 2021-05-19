using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private SphereCollider weaponEnd;

    [SerializeField] private AudioManager sound;

    [SerializeField] private int maxHealth=200;

    [SerializeField] private GameObject player;

    [SerializeField]
    private GameObject cam;
    
    public int currentHealth;

    [SerializeField]private PlayerHP healthBar;

    private bool invincible = false;
    [SerializeField]
    private float invincibilityTime = 0.75f;

    [SerializeField] private float timeBetweenAttacks;
    bool alreadyAttacked;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

    }

    IEnumerator SwordAttack(float time)
    {
        yield return new WaitForSeconds(time);
        weaponEnd.enabled = true;
        yield return new WaitForSeconds(0.2f);
        weaponEnd.enabled = false;

    }

    void Attack()
    { 
        if (!alreadyAttacked)
        {
            //Attack code
            animator.SetTrigger("Attack");
            sound.Play("SwordAttack");   
            StartCoroutine(SwordAttack(0.1f));


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    void Die()
    {
        sound.Play("DeathSound");
        Debug.Log("I LOST");
        animator.SetTrigger("Death");
        player.GetComponent<CharacterController3D>().enabled = false;
        player.GetComponent<CharacterCombat>().enabled = false;
        cam.GetComponent<CameraController>().enabled = false;
       
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            currentHealth -= damage;
            sound.Play("HitSound");
            healthBar.SetHealth(currentHealth);
            StartCoroutine(Invulnerability());

            if (currentHealth <= 0)
            {
                Die();
            }
        }

    }

    IEnumerator Invulnerability()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }
}

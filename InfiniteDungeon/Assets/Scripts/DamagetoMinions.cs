using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagetoMinions : MonoBehaviour
{
    [SerializeField] private Transform swordEnd;
    [SerializeField] private int attackDamage;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackRange;
    public void OnTriggerEnter(Collider other)
    {
        EnemyAI minion = other.GetComponent<EnemyAI>();
        if (minion!=null)
        {
            //Collider on the weapon in order to damage the minions
            Collider[] Enemies = Physics.OverlapSphere(swordEnd.position, attackRange, enemyLayers);

            foreach (Collider enemy in Enemies)
            {

                enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (swordEnd == null)
            return;

        Gizmos.DrawWireSphere(swordEnd.position, attackRange);
    }
}

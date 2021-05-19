using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private int attackDamage;

    public void OnTriggerEnter(Collider other)
    {
        if (boss!=null && other.name=="TrollBoss")
        {
            //Collider on the weapon in order to damage the boss
            boss.GetComponent<BossAi>().TakeDamageBoss(attackDamage);
        }
       
    }
}

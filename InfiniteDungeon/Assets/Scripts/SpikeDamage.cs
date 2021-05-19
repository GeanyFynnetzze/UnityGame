using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int attackDamage;


    public void OnTriggerEnter(Collider other)
    {
        if (player != null)
        {
            player.GetComponent<CharacterCombat>().TakeDamage(attackDamage);
        }
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int attackDamage;

    public void OnTriggerEnter(Collider other)
    {
        if (player != null)
        {
            //Collider on the weapon in order to damage the player
            player.GetComponent<CharacterCombat>().TakeDamage(attackDamage);
            FindObjectOfType<AudioManager>().Play("Bonk");
        }
    }
}

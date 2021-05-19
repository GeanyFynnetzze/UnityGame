using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    
    [SerializeField]private Animator chestAnim;
    [SerializeField] private MeshRenderer BossDoor;
    [SerializeField] private BoxCollider Door;
    [SerializeField] private Canvas DoorText;
    private void OnTriggerEnter(Collider other)
    {
        //Opens the chest and also the door to the Boss room
        OpenChest();
        BossDoor.enabled = false;
        Door.enabled = false;
        DoorText.enabled = false;
    }


    private void OpenChest()
    {
        chestAnim.SetTrigger("Open");
    }



}

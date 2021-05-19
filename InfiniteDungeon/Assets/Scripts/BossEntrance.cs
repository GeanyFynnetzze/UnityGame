using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
    public CanvasGroup canvas;
    [SerializeField] private MeshCollider wall;
    [SerializeField] private MeshRenderer door;
    [SerializeField] private AudioManager sound;
    [SerializeField] private AudioSource[] BossMusic;

    //Plays some boss music, shows his HP and also closes the door
    //Fight with honour
    private void Awake()
    {
        sound.Play("TheCave");
    }
    public void OnTriggerEnter(Collider other)
    {
        ShowHp();
        wall.enabled = true;
        door.enabled = true;
        sound.Stop("TheCave");
        sound.Play("BossMusic");
        GetComponent<BoxCollider>().enabled = false;
    }
 
    void ShowHp()
    {
        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
    }
}

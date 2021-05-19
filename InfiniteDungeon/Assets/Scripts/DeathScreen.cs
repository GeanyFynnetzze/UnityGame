using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvas;
    GameObject player;
    [SerializeField] private AudioManager sound;
    private AudioSource[] allAudioSources;
    private void Start()
    {
        player = GameObject.Find("Player2");
    }
    private void FixedUpdate()
    {
        CharacterCombat HpCheck = player.GetComponent<CharacterCombat>();
        if (HpCheck.currentHealth<=0)
        {      
            //Checks for the player's HP and then shows the death screen
            //Reloads the scene
            StopAllAudio();
            StartCoroutine(Show_DeathScreen(1f));
           StartCoroutine(Reload_Scene(6f));
           
        }
    }
    void ShowDeathScreen()
    {
       
        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
    }
    public IEnumerator Show_DeathScreen(float time)
    {
        yield return new WaitForSeconds(time);
         ShowDeathScreen();

    }
    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    public IEnumerator Reload_Scene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   

}

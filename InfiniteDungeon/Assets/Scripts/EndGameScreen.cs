using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;

    private void Start()
    {
        //Shows the final endscreen and then get's you back to the menu
        StartCoroutine(Show_EndScreen(4f));
        StartCoroutine(Load_Menu(7f));
    }

    public IEnumerator Show_EndScreen(float time)
    {
        yield return new WaitForSeconds(time);
        ShowEndScreen();
        
    }
    void ShowEndScreen()
    {

        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
    }

    public IEnumerator Load_Menu(float time)
    {
        yield return new WaitForSeconds(time);
        LoadMenu();
    }
    void LoadMenu()
    {
        SceneManager.LoadScene("Start");
    }

}

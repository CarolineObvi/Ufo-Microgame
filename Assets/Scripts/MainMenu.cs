using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool transitionStage = false;
    public bool menu = false;
    public Animator transition;
    public float transitionTime = 1f;
    
    private void Start()
    {
        if (menu == true)
        {
            Cursor.visible = true;
        }

        if (transitionStage == true)
        {
            LoadNextLevel("Game");
        }
    }
    public void PlayGame()
    {
        LoadNextLevel("Transition Stage");
        //SceneManager.LoadScene("Game");
    }

    public void LoadNextLevel(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }

    IEnumerator LoadLevel(string levelName)
    {
        if (transitionStage == true)
        {
            yield return new WaitForSeconds(4f);
        }
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }
}
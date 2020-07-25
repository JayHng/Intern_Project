using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
    private bool isPaused = false;
    //public GameObject loadingScreen;
    //public Slider slider;
    //public Text progressTxt;

    public GameObject pauseMenu;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex < 1)
        {
            pauseMenu = null;
        }
        else
        {
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
            pauseMenu.SetActive(false);
        }
    }

    public bool IsPaused() => isPaused;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {       
        if(SceneManager.GetActiveScene().buildIndex == 0 && Input.anyKey){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //LoadLevel(1);
        }
    }

    // //Loading Screen
    // public void LoadLevel(int sceneIndex){
    //     StartCoroutine(LoadAsynchronously(sceneIndex));
    // }

    // IEnumerator LoadAsynchronously (int sceneIndex){
    //     AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
    //     loadingScreen.SetActive(true);
        
    //     while(!operation.isDone){
    //         float progress = Mathf.Clamp01(operation.progress / 0.9f);
    //         slider.value = progress;
    //         progressTxt.text = progress * 100f + "%";
                    
    //         yield return null;
    //     }

    // }
}
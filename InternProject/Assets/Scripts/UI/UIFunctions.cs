using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
    private bool isPaused = false;

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

}
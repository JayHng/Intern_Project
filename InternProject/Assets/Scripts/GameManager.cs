using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    AsyncOperation async;

    public string sceneName = "Level1";
    
    bool done;

    private UIFunctions ui;
    public static GameManager gm;
    public Player player;

    void Awake()
    {
        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        ui = FindObjectOfType<UIFunctions>();
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ui.IsPaused())
            { 
                ui.Resume();
            }
            else
            {
                ui.Pause();
            }
        }
        if (async.isDone && !done)
        {
            done = true;
            Time.timeScale = 1;
        }
    }
    
}

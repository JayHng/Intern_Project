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
    public Transform spawnPoint;
    public Controller2D control;

    void Start()
    {
        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        ui = FindObjectOfType<UIFunctions>();
        //spawnPoint = GameObject.FindGameObjectWithTag("StartingPoint").transform;
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

    }

    public void SetStartingPoint()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("StartingPoint").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.gameObject.transform.position = spawnPoint.position;
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


        if (spawnPoint == null)
            SetStartingPoint();

        //Check the last scene
        //if (control.levelToLoad > 4)
        //{
        //    SceneManager.LoadScene(0);
        //}
        if (async.isDone && !done)
        {
            done = true;
            Time.timeScale = 1;
        }
    }
    
}

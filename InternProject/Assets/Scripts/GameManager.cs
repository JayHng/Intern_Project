using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    AsyncOperation async;

    public string sceneName = "Level1";
    
    bool done;

    public UIFunctions ui;

    void Start()
    {
        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        ui = FindObjectOfType<UIFunctions>();
    }

    void Update(){
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
            //WaypointCircuit circuit = FindObjectOfType<WaypointCircuit>();
            //WaypointProgressTracker[] aiCars = FindObjectsOfType<WaypointProgressTracker>();
            //foreach (WaypointProgressTracker car in aiCars)
            //{
            //    car.circuit = circuit;
            //}
            //hider.SetActive(false);

            Time.timeScale = 1;
        }
    }
    
}

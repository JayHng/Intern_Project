using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

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
    void Start(){
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
    public void Respawn(){
        StartCoroutine("RespawnCoroutine");
    }
    public  IEnumerator RespawnCoroutine(){
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        
        ReloadScene();
        player.gameObject.SetActive(true);
        player.gameObject.transform.position = player.respawnPosition;
    }
    public void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

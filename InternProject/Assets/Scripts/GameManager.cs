using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public int points;
    public Text points_Text;
 
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
        points_Text.text = ("" + points);
    }
    public void Respawn(){
        StartCoroutine("RespawnCoroutine");
    }
    public  IEnumerator RespawnCoroutine(){
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        ReloadScene();
        player.gameObject.SetActive(true);
        player.gameObject.transform.position = player.respawnPosition;
    }
    public void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

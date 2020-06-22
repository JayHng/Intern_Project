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

    public int points=0;
    public int highScore=0;
    public Text points_Text;
    public Text highScore_Text;

 
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
        highScore_Text.text = ("" + PlayerPrefs.GetInt("highScore"));
        highScore = PlayerPrefs.GetInt("highScore", 0);

        if(PlayerPrefs.HasKey("points")){
            Scene ActiveScreen = SceneManager.GetActiveScene();
            if(ActiveScreen.buildIndex == 1){
                PlayerPrefs.DeleteKey("points");
                points = 0;
            }else{
                points = PlayerPrefs.GetInt("points");
            }
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

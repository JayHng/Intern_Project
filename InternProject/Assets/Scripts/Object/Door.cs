using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] string nextLevelName;
    bool isLoading;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player" && !isLoading)
        {
            Debug.Log("Door");
            isLoading = true;
            collider2D.GetComponent<Player>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;

            if (!string.IsNullOrWhiteSpace(nextLevelName))
            {
                Invoke("LoadLevel", 1f);
            }
            else
            {
                Debug.Log("Go back to menu");
                SceneManager.LoadScene(0);
            }
        }
    }

    void LoadLevel()
    {
        SceneManager.UnloadSceneAsync(GameManager.gm.sceneName);
        GameManager.gm.sceneName = nextLevelName;
        SceneManager.LoadSceneAsync(nextLevelName, LoadSceneMode.Additive);
        GameManager.gm.player.enabled = true;
    }
}

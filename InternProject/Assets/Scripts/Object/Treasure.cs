using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject coinPartical;
    [SerializeField] GameManager gm;
    
    public Animator transition;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            StartCoroutine("OpenChest");
            
        }
    }

    IEnumerator OpenChest(){
        anim.SetBool("playerTouchChest", true);
        yield return new WaitForSeconds(1.0f);
        gm.points += 100;
        Instantiate(coinPartical, gameObject.transform.position, coinPartical.transform.rotation);
        yield return new WaitForSeconds(2.5f);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2.0f);
        transition.SetTrigger("End");
        SceneManager.LoadScene("EndScene");      
    }
}

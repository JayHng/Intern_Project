using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject coinPartical;
    [SerializeField] GameObject victoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
        coinPartical.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        victoryPanel.SetActive(true);       
    }
}

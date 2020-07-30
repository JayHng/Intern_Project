using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedElectric : MonoBehaviour
{
    [SerializeField] GameObject electric;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine("UnlockedSwitch");
            //electric.SetActive(false);
        }
    }

    IEnumerator UnlockedSwitch(){
        animator.SetBool("playerTouch", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("playerTouch",false);
        electric.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedElectric : MonoBehaviour
{
    [SerializeField] GameObject electric;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameObject.GetComponent<Animation>().Play("Switch_On");

            electric.SetActive(false);
            //electric.SetActive(!electric.gameObject.activeSelf);
        }
    }

}

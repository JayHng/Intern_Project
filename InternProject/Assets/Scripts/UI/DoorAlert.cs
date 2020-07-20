using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAlert : MonoBehaviour
{

    public GameObject doorPanel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            doorPanel.SetActive(true);
            Debug.Log("Panel On");
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            doorPanel.SetActive(false);
            Debug.Log("Panel Off");
        }
    }
}

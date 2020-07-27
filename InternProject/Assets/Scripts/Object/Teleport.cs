using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]private GameObject portal;
    [SerializeField]private Player player;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            StartCoroutine(Teleportation());
        }
    }
    
    IEnumerator Teleportation(){
        yield return new WaitForSeconds(1);
        player.transform.position = new Vector2(portal.transform.position.x,portal.transform.position.y);
    }
}

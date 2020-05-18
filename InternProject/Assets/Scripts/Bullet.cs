using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //to avoid the bullet disappears when hit the "AttackArea" collider
        if(other.isTrigger == false){
            if(other.CompareTag("Player")){
                other.SendMessageUpwards("Damage", 1);
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0){
            Destroy(gameObject);
        }
    }
}

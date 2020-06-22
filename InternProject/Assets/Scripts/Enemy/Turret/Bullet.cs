using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2;
    public Player player;
    AttackDetails attackDetails;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger == false)
        {
            if (other.tag == "Player")
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                //player.DecreasePlayerHP(1);
                other.SendMessageUpwards("Damage", attackDetails);
                Destroy(gameObject);
            }
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}

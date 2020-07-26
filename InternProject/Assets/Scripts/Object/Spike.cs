﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Player player;
    public GameObject panel;

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
        if(other.tag == "Player"){
            player.DecreasePlayerHP(1);
            player.Knockback(this.player.transform.position);
            panel.SetActive(true);
            StartCoroutine("disablePanel");
        }
    }

    
    IEnumerator disablePanel(){
        yield return new WaitForSeconds(1.2f);
        panel.SetActive(false);
    }
}

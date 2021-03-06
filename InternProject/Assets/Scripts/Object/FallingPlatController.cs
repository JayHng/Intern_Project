﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatController : MonoBehaviour
{
    private Rigidbody2D fallPlatRb;
    private Controller2D controller;
    public float fallDelay;

    // Start is called before the first frame update
    public void Start()
    {
        fallPlatRb = gameObject.GetComponent<Rigidbody2D>();  
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Player"){
            StartCoroutine(FallPlatform());
        }

        if(col.collider.tag == "Ground"){
            gameObject.SetActive(false);
        }
    }
    IEnumerator FallPlatform(){                                    
        Debug.Log("Fall");
        yield return new WaitForSeconds(fallDelay);
        fallPlatRb.bodyType = RigidbodyType2D.Dynamic;
        yield return 0;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatController : RaycastController
{
    private Rigidbody2D rb;
    public float fallDelay;
    public Controller2D controller;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();    
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player"){
            controller.isOnGround();
            StartCoroutine(FallPlatform());
        }
    }

    IEnumerator FallPlatform(){
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return 0;
    }
}

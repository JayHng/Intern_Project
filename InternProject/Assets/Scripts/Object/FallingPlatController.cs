using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatController : RaycastController
{
    private Rigidbody2D fallPlatRb;
    public float fallDelay;
    private Controller2D controller;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        fallPlatRb = gameObject.GetComponent<Rigidbody2D>();    
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player")){
            Debug.Log("Fall");
            controller.isOnGround();        
            StartCoroutine(FallPlatform());
        }
    }
    IEnumerator FallPlatform(){                                    
        yield return new WaitForSeconds(fallDelay);
        fallPlatRb.bodyType = RigidbodyType2D.Dynamic;
        yield return 0;
    }
}

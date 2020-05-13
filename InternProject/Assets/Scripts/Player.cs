﻿using UnityEngine;
using UnityEngine.SceneManagement;

//This code belongs to Sebastian Lague
[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;

    float moveSpeed = 6;
    float gravity;
    float jumpVelocity;
    float velocityXSmoothing;
    Controller2D controller;
    Vector3 velocity;

    //This code belongs to me
    public int currentHP;
    public int maxHP = 5;

    // Start is called before the first frame update
    void Start()
    {
        //This code belongs to Sebastian Lague
        controller = GetComponent<Controller2D>();
        gravity = -(2*jumpHeight)/Mathf.Pow(timeToJumpApex,2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);

        //This code belongs to me
        currentHP = maxHP;    
    }
    // Update is called once per frame
    void Update()
    {
        //This code belongs to Sebastian Lague
        if (controller.objectCol.above || controller.objectCol.below){
            velocity.y = 0;
        }
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space) && controller.objectCol.below){
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x= Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,(controller.objectCol.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        //update gravity to the velocity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
    }

    //This code belongs to me
    private void FixedUpdate()
    {
        if (currentHP <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

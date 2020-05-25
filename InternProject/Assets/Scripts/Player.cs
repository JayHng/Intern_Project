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

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float velocityXSmoothing;
    [SerializeField] private Controller2D controller;
    [SerializeField] private Vector3 velocity;

    //This code belongs to me
    public int currentHP;
    public int maxHP = 5;

    //This code belongs to me
    [SerializeField] private bool faceright;

    // Start is called before the first frame update
    void Start()
    {
        //This code belongs to Sebastian Lague
        controller = GetComponent<Controller2D>();
        gravity = -(2*jumpHeight)/Mathf.Pow(timeToJumpApex,2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        //print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);

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
    void FixedUpdate()
    {
        if (currentHP <= 0)
        {
            Death();
        }

        // float inputDir = Input.GetAxisRaw("Horizontal");
        // Debug.Log(inputDir);

        // if(inputDir>0 && !faceright)
        // {
        //     Flip();
        // }
        // if(inputDir<0 && faceright){
        //     Flip();
        // }

    }
    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayerDamage(int dam){
        currentHP -= dam;
    }
    public void Knockback(Vector3 knockDir){
        velocity = new Vector3(0,0,0);
        velocity = new Vector3(knockDir.x * -15, jumpVelocity-7, 0);
    }

    public void Flip(){
        faceright = ! faceright;
        transform.Rotate(0.0f,180.0f,0.0f);
    }
}

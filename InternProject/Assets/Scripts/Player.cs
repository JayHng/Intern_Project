using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour
{

    Vector3 velocity;

    float moveSpeed = 6;
    float gravity = -50;
    Controller2D controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        velocity.x = input.x * moveSpeed;
        //update gravity to the velocity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        
    }

}

using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    Vector3 velocity;

    float moveSpeed = 6;
    float gravity;
    float jumpVelocity;
    Controller2D controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D>();
        gravity = -(2*jumpHeight)/Mathf.Pow(timeToJumpApex,2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }
    // Update is called once per frame
    void Update()
    {
        if(controller.objectCol.above || controller.objectCol.below){
            velocity.y = 0;
        }
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space) && controller.objectCol.below){
            velocity.y = jumpVelocity;
        }

        velocity.x = input.x * moveSpeed;
        //update gravity to the velocity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        
    }

}

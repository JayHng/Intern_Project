using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;
    public int faceDir {get; private set;  }
    public Rigidbody2D rb { get; private set;}
    public Animator anim{ get; private set;}
    public GameObject aliveGO{ get; private set;}
    private Vector2 velocityWorkspace;
    [SerializeField] private Transform wallCheck;
    public virtual void Start() {
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update(){
        stateMachine.currentState.LogicUpdate();
    }
    public virtual void FixedUpdate() {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity){
        velocityWorkspace.Set(faceDir * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall(){
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.isGround);
    }
    
    public virtual void Flip(){
        faceDir *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }
}


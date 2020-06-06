using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;
    public int faceDir {get; private set; }
    public Rigidbody2D rb { get; private set;}
    public Animator anim { get; private set;}
    public GameObject aliveGO { get; private set;}
    private Vector2 velocityWorkspace;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    public virtual void Start() {
        faceDir = 1;

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
        velocityWorkspace.Set(faceDir * velocity, this.rb.velocity.y);
        this.rb.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall(){
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.isGround);
    }
    
    public virtual bool CheckLedge(){
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.isGround);
    }

    public virtual bool CheckPlayerInMinArgoRange(){
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minArgoDistance, entityData.isPlayer);
    }
    public virtual bool CheckPlayerInMaxArgoRange(){
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxArgoDistance, entityData.isPlayer);
    }
    public virtual void Flip(){
        faceDir *= -1;
        aliveGO.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public virtual void OnDrawGizmos() {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * faceDir * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
    }
}


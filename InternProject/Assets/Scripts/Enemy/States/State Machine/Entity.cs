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
    public AnimationToStateMachine animToStateMachine { get; private set;}
    public int lastDamageDir { get; private set;}
    private Vector2 velocityWorkspace;

    
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform groundCheck;
    private float currentHP;
    private  float currentStunResistance;
    private float lastDamageTime;
    protected bool isStunned;
    protected bool isDead;
    public virtual void Start() {
        faceDir = 1;
        currentHP = entityData.maxHP;
        currentStunResistance = entityData.stunResistance;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        animToStateMachine = aliveGO.GetComponent<AnimationToStateMachine>();
        
        stateMachine = new FiniteStateMachine();
    } 

    public virtual void Update(){
        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", rb.velocity.y);
        if(Time.time >= lastDamageTime + entityData.stunRecoveryTime){
            ResetStunResistance();
        }
    }
    public virtual void FixedUpdate() {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity){
        velocityWorkspace.Set(faceDir * velocity, this.rb.velocity.y);
        this.rb.velocity = velocityWorkspace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction){
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall(){
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.isGround);
    }
    
    public virtual bool CheckLedge(){
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.isGround);
    }

    public virtual bool CheckGround(){
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.isGround);
    }
    public virtual bool CheckPlayerInMinArgoRange(){
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minArgoDistance, entityData.isPlayer);
    }
    public virtual bool CheckPlayerInMaxArgoRange(){
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxArgoDistance, entityData.isPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction(){
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.isPlayer);      
    }
    public virtual void DamageHop(float velocity){
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;
    }
    public virtual void ResetStunResistance(){
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }
    public virtual void Damage(AttackDetails attackDetails){
        lastDamageTime = Time.time;

        currentHP -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;

        DamageHop(entityData.damageHopSpeed);

        Instantiate(entityData.hitParticle, aliveGO.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(attackDetails.position.x > aliveGO.transform.position.x){
            lastDamageDir = -1;
        }else{
            lastDamageDir = 1;
        }

        if(currentStunResistance <= 0){
            isStunned = true;
        }
        if(currentHP <= 0){
            isDead = true;
        }
    }
    public virtual void Flip(){
        faceDir *= -1;
        aliveGO.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public virtual void OnDrawGizmos() {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * faceDir * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * faceDir * entityData.minArgoDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxArgoDistance), 0.2f);
    }
}


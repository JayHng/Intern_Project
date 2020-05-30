using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Animator))]
//This code belongs to Bardent(Youtuber)
public class SkeletonController : MonoBehaviour
{
    private enum State{
        Moving, Knockback, Dead
    }
    private State currentState;
    [SerializeField]
    private float groundDistance, wallDistance, movementSpeed, maxHP, knockbackDuration;
    [SerializeField]private Transform groundCheck, wallCheck;
    [SerializeField]private LayerMask IsGround;
    [SerializeField]private Vector2 knockbackSpeed;
    private bool groundDetected, wallDetected;
    private int faceDir, damageDir;
    private GameObject alive;
    private Vector2 movement;
    private Rigidbody2D aliverb;
    private float currentHP, knockbackStartTime;
    public Animator aliveAnim;
    [SerializeField]private GameObject hitParticle, brokenBoneParticle;
    //public Player player;


    public void Start(){
        alive = transform.Find("Alive").gameObject;
        aliverb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = GetComponent<Animator>();
        faceDir = 1;
        currentHP = maxHP;
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //Async to MainScene
    }
    private void Update(){
        switch(currentState){
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }
    //---------------------------------------MOVING STATE------------------------------------------
    private void EnterMovingState(){

    }
    private void UpdateMovingState(){
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, IsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallDistance, IsGround);

        if(!groundDetected || wallDetected){
            Flip();
        }else{
            movement.Set(movementSpeed * faceDir, aliverb.velocity.y);
            aliverb.velocity = movement;
        }

        
    }
    private void ExitMovingState(){

    }

    //-------------------------------------KNOCKBACK STATE-----------------------------------------
    private void EnterKnockbackState(){

        knockbackStartTime=Time.time;
        movement.Set(knockbackSpeed.x * damageDir, knockbackSpeed.y);
        aliverb.velocity = movement;
        aliveAnim.SetBool("SkeKnockback",true);

    }
    private void UpdateKnockbackState(){
        if(Time.time >= knockbackStartTime + knockbackDuration){
            SwitchState(State.Moving);
        }
    }
    private void ExitKnockbackState(){
        aliveAnim.SetBool("SkeKnockback",false);      
    }

    //-----------------------------------------DEAD STATE-------------------------------------------
    private void EnterDeadState(){
        Instantiate(brokenBoneParticle,alive.transform.position, brokenBoneParticle.transform.rotation);
        Destroy(gameObject);
    }
    private void UpdateDeadState(){

    }
    private void ExitDeadState(){

    }

    //-------------------------------------OTHER FUNCTION---------------------------------------------
    private void SwitchState(State state){
        switch(currentState){
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }
        switch(state){
            case State.Moving:
                EnterMovingState();
                break; 
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }
        currentState = state;
    }
    private void Flip(){
        faceDir *= -1;
        alive.transform.Rotate(0.0f, 180.0f,0.0f);
    }
    private void SkeDamage(float[] attackDetails){
        currentHP -= attackDetails[0];
        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f,0.0f, Random.Range(0.0f, 360.0f)));
        if(attackDetails[1] > alive.transform.position.x){
            damageDir = -1;
        }else{
            damageDir =1;
        }
        //Hit particle
        if(currentHP > 0.0f){
            SwitchState(State.Knockback);
        }else if(currentHP <= 0.0f){
            SwitchState(State.Dead);
        }
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallDistance, wallCheck.position.y));
    }
    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.tag=="Player"){
    //         player.PlayerDamage(1);
    //     }
    // }

}


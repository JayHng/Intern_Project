using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Thiscode belongs to Bardent(Youtuber)
public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]private bool combatEnabled;
    [SerializeField]private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]private Transform attack1HitBoxPos;
    [SerializeField]private LayerMask isDamageable;
    private float lastInputTime = Mathf.NegativeInfinity;
    private float[] attackDetails = new float[2];
    private Animator anim;
    private bool isAttacking, inputEntered, isFirstAttack;
    private Controller2D playerController;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        playerController = GetComponent<Controller2D>();
        player=GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }
    private void CheckCombatInput(){
        if(Input.GetKeyDown(KeyCode.Q)){
            if(combatEnabled){
                inputEntered = true;
                lastInputTime = Time.time;
            }
        }
    }
    
    private void CheckAttacks(){
        if(inputEntered){
            if(!isAttacking){
                inputEntered = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }
        if(Time.time >= lastInputTime + inputTimer){
            inputEntered=false;
        }
    }

    private void CheckAttackHitBox(){
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, isDamageable);
        attackDetails[0] = attack1Damage;
        attackDetails[1] = this.transform.position.x;

        foreach(Collider2D collider in detectedObjects){
            collider.transform.parent.SendMessage("SkeDamage", attackDetails);
        }
    }

    private void FinishAttack(){
        anim.SetBool("isAttacking", isAttacking);
        isAttacking=false;
        anim.SetBool("attack1", false);
    }

    private void Damage(float[] attackDetails){
        int dir;
        player.DecreasePlayerHP(1);
        if(attackDetails[1] < this.transform.position.x){
            dir = 1;
        }else{
            dir = -1;
        }
        playerController.PlayerKnockback(dir);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}

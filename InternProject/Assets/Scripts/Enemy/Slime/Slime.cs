using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Entity
{
    public Slime_MoveState moveState {get; private set;}
    public Slime_IdleState idleState {get; private set;}
    public Slime_PlayerDetectedState playerDetectedState {get; private set;}
    public Slime_MeleeAttackState meleeAttackState {get; private set;}
    public Slime_LookForPlayerState lookForPlayerState {get; private set;}
    public Slime_StunState stunState {get; private set;}
    public Slime_DeadState deadState {get; private set;}
    public Slime_DodgeState dodgeState {get; private set;}

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PlayerDetected playerDetectedStateData;
    [SerializeField] private D_MeleeAttack meleeAttackStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] public D_DodgeState dodgeStateData;

    public override void Start() {
        base.Start();

        moveState = new Slime_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Slime_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Slime_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        meleeAttackState = new Slime_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new Slime_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stunState = new Slime_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new Slime_DeadState(this, stateMachine, "dead", deadStateData, this);
        dodgeState = new Slime_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);

        stateMachine.Initialize(moveState);
    }
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if(isDead){
            stateMachine.ChangeState(deadState);
        }
        else if(isStunned && stateMachine.currentState != stunState){
            stateMachine.ChangeState(stunState);
        }
        else if(!CheckPlayerInMinArgoRange()){
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position,meleeAttackStateData.attackRadius);
    }
}

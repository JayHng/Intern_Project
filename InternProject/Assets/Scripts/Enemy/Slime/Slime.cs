using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Entity
{
    public Slime_MoveState moveState {get; private set;}
    public Slime_IdleState idleState {get; private set;}
    public Slime_PlayerDetectedState playerDetectedState {get; private set;}

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PlayerDetected playerDetectedStateData;

    public override void Start() {
        base.Start();

        moveState = new Slime_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Slime_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Slime_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);

        stateMachine.Initialize(moveState);
    }
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

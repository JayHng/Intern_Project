using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected D_DodgeState stateData;

    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxArgoRange;
    protected bool isGrounded;
    protected bool isDodgeOver;
    public DodgeState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(entity1, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxArgoRange = entity.CheckPlayerInMaxArgoRange();
        isGrounded = entity.CheckGround();
    }
    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;
        entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.faceDir);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + stateData.dodgeTime && isGrounded){
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

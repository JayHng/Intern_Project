using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected stateData;
    protected bool isPlayerInMinArgoRange;
    protected bool isPlayerInMaxArgoRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    public PlayerDetectedState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity1, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void DoChecks(){
        base.DoChecks();

        isPlayerInMinArgoRange = entity.CheckPlayerInMinArgoRange();
        isPlayerInMaxArgoRange = entity.CheckPlayerInMaxArgoRange();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }
    public override void Enter()
    {
        base.Enter();

        performLongRangeAction =false;
        entity.SetVelocity(0.0f);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.longRangeActionTime){
            performLongRangeAction = true;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

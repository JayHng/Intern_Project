using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent (Youtuber)
public class ChargeState : State
{
    protected D_ChargeState stateData;
    protected bool isPlayerInMinArgoRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    public ChargeState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity1, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void DoChecks(){
        base.DoChecks();

        isPlayerInMinArgoRange = entity.CheckPlayerInMinArgoRange();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();        
    }
    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        entity.SetVelocity(stateData.chargeSpeed);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + stateData.chargeTime){
            isChargeTimeOver = true;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class MoveState : State
{
    protected D_MoveState stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinArgoRange;
    public MoveState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity1, stateMachine, animBoolName){
        this.stateData = stateData;
    }
    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = this.entity.CheckLedge();
        isDetectingWall = this.entity.CheckWall();
        isPlayerInMinArgoRange = entity.CheckPlayerInMaxArgoRange();
    }
    public override void Enter(){
        base.Enter();
        this.entity.SetVelocity(this.stateData.movementSpeed);
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }
}

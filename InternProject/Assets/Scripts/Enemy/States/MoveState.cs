using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class MoveState : State
{
    protected D_MoveState  stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    public MoveState(Entity entity1, FiniteStateMachine stateMachine1, string animBoolName1, D_MoveState stateData1) : base(entity1, stateMachine1, animBoolName1){
        this.stateData = stateData1;
    }
    public override void Enter(){
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);

        isDetectingWall = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
        
        isDetectingWall = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }

}

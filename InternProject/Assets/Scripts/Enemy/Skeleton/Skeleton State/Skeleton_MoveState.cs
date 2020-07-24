using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class Skeleton_MoveState : MoveState
{
    private Skeleton enemy;

    public Skeleton_MoveState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Skeleton enemy) : base(entity1, stateMachine, animBoolName, stateData){
        this.enemy = enemy;
    }
    public override void DoChecks(){
        base.DoChecks();
    }
    
    public override void Enter(){
        base.Enter();
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();

        if(isPlayerInMinArgoRange){
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if(isDetectingWall || !isDetectingLedge){
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class Enemy1_MoveState : MoveState
{
    private Enemy1 enemy;

    public Enemy1_MoveState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy1 enemy) : base(entity1, stateMachine, animBoolName, stateData){
        this.enemy = enemy;
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
        if(isDetectingWall || !isDetectingLedge){
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }
}

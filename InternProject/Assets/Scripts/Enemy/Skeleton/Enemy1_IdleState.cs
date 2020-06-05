using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// This code belongs to Bardent (Youtuber)
public class Enemy1_IdleState : IdleState
{
    private Enemy1 enemy;

    public Enemy1_IdleState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy1 enemy) : base(entity1, stateMachine, animBoolName, stateData){
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

        if(isIdleTimeOver){
            stateMachine.ChangeState(enemy.moveState);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent (Youtuber)
public class Enemy1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    public Enemy1_PlayerDetectedState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(entity1, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction){
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (performLongRangeAction){
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if (!isPlayerInMaxArgoRange){
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

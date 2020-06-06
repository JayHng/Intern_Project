using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if(!isPlayerInMaxArgoRange){
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

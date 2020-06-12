using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent (Youtuber)
public class Skeleton_ChargeState : ChargeState
{
    protected Skeleton enemy;
    public Skeleton_ChargeState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Skeleton enemy) : base(entity1, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        if(performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if(isDetectingWall || !isDetectingLedge)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if(isChargeTimeOver)
        {
            if(isPlayerInMinArgoRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);      
            }else{
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

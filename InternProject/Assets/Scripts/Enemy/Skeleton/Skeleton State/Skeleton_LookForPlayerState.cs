using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent (Youtuber)
public class Skeleton_LookForPlayerState : LookForPlayerState
{
    private Skeleton enemy;
    public Skeleton_LookForPlayerState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData, Skeleton enemy) : base(entity1, stateMachine, animBoolName, stateData)
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

        if(isPlayerInMinArgoRange){
            stateMachine.ChangeState(enemy.playerDetectedState);
        }else if(isAllTurnsTimeDone){
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_LookForPlayerState : LookForPlayerState
{
    private Enemy1 enemy;
    public Enemy1_LookForPlayerState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData, Enemy1 enemy) : base(entity1, stateMachine, animBoolName, stateData)
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

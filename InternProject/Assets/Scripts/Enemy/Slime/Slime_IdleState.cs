﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_IdleState : IdleState
{
    private Slime enemy;
    public Slime_IdleState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Slime enemy) : base(entity1, stateMachine, animBoolName, stateData)
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
        }
        else if(isIdleTimeOver){
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

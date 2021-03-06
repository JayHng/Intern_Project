﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class LookForPlayerState : State
{
    protected D_LookForPlayer stateData;
    protected bool turnImmediately;
    protected bool isPlayerInMinArgoRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;
    protected int amountOfTurnsDone;
    public LookForPlayerState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData) : base(entity1, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinArgoRange = entity.CheckPlayerInMinArgoRange();
    }

    public override void Enter()
    {
        base.Enter();
        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;

        lastTurnTime = startTime;
        amountOfTurnsDone = 0;

        entity.SetVelocity(0.0f);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(turnImmediately){
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }else if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone){
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if(amountOfTurnsDone >= stateData.amountOfTurn){
            isAllTurnsDone = true;
        }
        if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone){
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTurnImmediately(bool flip){
        turnImmediately = flip;
    }
}

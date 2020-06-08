using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class IdleState : State
{
    protected D_IdleState stateData;
    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinArgoRange;
    protected float idleTime;

    public IdleState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity1, stateMachine, animBoolName){
        this.stateData = stateData;
    }
    public override void DoChecks(){
        base.DoChecks();

        isPlayerInMinArgoRange = entity.CheckPlayerInMinArgoRange();
    }
    public override void Enter(){
        base.Enter();
        entity.SetVelocity(0.0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }
    public override void Exit(){
        base.Exit();

        if(flipAfterIdle){
            entity.Flip();
        }
    }
    public override void LogicUpdate(){
        base.LogicUpdate();

        if(Time.time >= startTime + idleTime){
            isIdleTimeOver = true;
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
        }
    public void SetFlipAfterIdle(bool flip){
        flipAfterIdle = flip;
    }
    private void SetRandomIdleTime(){
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class State
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    public float startTime {get; protected set;}
    protected string animBoolName;
    public State(Entity entity1, FiniteStateMachine stateMachine, string animBoolName){
        this.entity = entity1;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter(){
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit(){
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate(){

    }
    public virtual void PhysicsUpdate(){
        DoChecks();
    }
    public virtual void DoChecks(){

    }
}

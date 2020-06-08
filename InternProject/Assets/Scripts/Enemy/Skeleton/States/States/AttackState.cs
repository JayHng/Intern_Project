using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerInMinArgoRange;

    public AttackState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity1, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinArgoRange = entity.CheckPlayerInMinArgoRange();
    }

    public override void Enter()
    {
        base.Enter();           

        entity.animToStateMachine.attackState = this;
        isAnimationFinished = false;
        entity.SetVelocity(0.0f);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void TriggerAttack(){

    }

    public virtual void FinishAttack(){
        isAnimationFinished = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_DeadState : DeadState
{
    private Skeleton enemy;

    public Skeleton_DeadState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData,Skeleton enemy) : base(entity1, stateMachine, animBoolName, stateData)
    {
        this.enemy=enemy;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected stateData;
    protected bool isPlayerInMinArgoRange;
    protected bool isPlayerInMaxArgoRange;
    public PlayerDetectedState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity1, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0.0f);

        isPlayerInMinArgoRange = entity.CheckPlayerInMinArgoRange();
        isPlayerInMaxArgoRange = entity.CheckPlayerInMaxArgoRange();
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

        isPlayerInMinArgoRange = entity.CheckPlayerInMinArgoRange();
        isPlayerInMaxArgoRange = entity.CheckPlayerInMaxArgoRange();
    }
}

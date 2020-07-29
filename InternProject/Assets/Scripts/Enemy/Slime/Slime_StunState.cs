using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_StunState : StunState
{
    private Slime enemy;
    public Slime_StunState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Slime enemy) : base(entity1, stateMachine, animBoolName, stateData)
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

        if(isStunTimeOver){
            if(isPlayerInMinArgoRange){
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

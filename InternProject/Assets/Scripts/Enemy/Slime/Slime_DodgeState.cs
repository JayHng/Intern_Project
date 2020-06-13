using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_DodgeState : DodgeState
{
    private Slime enemy;
    public Slime_DodgeState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData, Slime enemy) : base(entity1, stateMachine, animBoolName, stateData)
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
        if(isDodgeOver){
            if(isPlayerInMaxArgoRange && performCloseRangeAction){
                stateMachine.ChangeState(enemy.meleeAttackState);
            }else if(!isPlayerInMaxArgoRange){
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

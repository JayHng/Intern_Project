using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_PlayerDetectedState : PlayerDetectedState
{
    private Slime enemy;

    public Slime_PlayerDetectedState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Slime enemy) : base(entity1, stateMachine, animBoolName, stateData)
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

        if(performCloseRangeAction){
            if(Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown){
                stateMachine.ChangeState(enemy.dodgeState);
            }else{
                stateMachine.ChangeState(enemy.meleeAttackState);           
            }
        }
        else if(performLongRangeAction){
            stateMachine.ChangeState(enemy.rangeAttackState);
        }
        else if(!isPlayerInMaxArgoRange){
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent (Youtuber)
public class Skeleton_StunState : StunState
{
    private Skeleton enemy;
    public Skeleton_StunState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Skeleton enemy) : base(entity1, stateMachine, animBoolName, stateData)
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
            if(performCloseRangeAction){
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if(isPlayerInMinArgoRange){
                stateMachine.ChangeState(enemy.chargeState);               
            }
            else{
                enemy.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

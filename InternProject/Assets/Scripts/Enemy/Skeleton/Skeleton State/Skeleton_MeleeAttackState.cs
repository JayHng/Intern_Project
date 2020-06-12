using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent (Youtuber)
public class Skeleton_MeleeAttackState : MeleeAttackState
{
    private Skeleton enemy;

    public Skeleton_MeleeAttackState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, Skeleton enemy) : base(entity1, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished){
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
    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}

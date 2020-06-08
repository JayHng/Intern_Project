using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class DeadState : State
{
    protected D_DeadState stateData;
    public DeadState(Entity entity1, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entity1, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        GameObject.Instantiate(stateData.brokenBonePartical, entity.aliveGO.transform.position, stateData.brokenBonePartical.transform.rotation);
        entity.gameObject.SetActive(false);
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

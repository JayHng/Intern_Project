using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code belongs to Bardent(Youtuber)
public class Enemy1 : Entity
{
   public Enemy1_IdleState idleState { get; private set;}
   public Enemy1_MoveState moveState { get; private set;}
   public Enemy1_PlayerDetectedState playerDetectedState {get; private set; }
   public Enemy1_ChargeState chargeState {get; private set; }
   public Enemy1_LookForPlayerState lookForPlayerState {get; private set;} 

   
   [SerializeField] private  D_IdleState idleStateData;
   [SerializeField] private D_MoveState moveStateData;
   [SerializeField] private D_PlayerDetected playerDetectedData;
   [SerializeField] private D_ChargeState chargeStateData;
   [SerializeField] private D_LookForPlayer lookForPlayerStateData;

   public override void Start(){
       base.Start();
       moveState = new Enemy1_MoveState(this, stateMachine, "move", moveStateData, this);
       idleState = new Enemy1_IdleState(this, stateMachine, "idle", idleStateData, this);
       playerDetectedState = new Enemy1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
       chargeState = new Enemy1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
       lookForPlayerState = new Enemy1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);

       stateMachine.Initialize(moveState);
   }
}

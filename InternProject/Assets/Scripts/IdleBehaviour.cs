﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    public GameObject skeleton;
    // Distance between skeleton and player
    public float skeDistance;
    [SerializeField] private Player playerPos;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
      skeleton = GameObject.FindGameObjectWithTag("Skeleton");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        skeDistance = Vector2.Distance(skeleton.transform.position, this.playerPos.transform.position);

        if(skeDistance < 12){
         animator.SetBool("isFollowing", true);}
        // }else{
        //  animator.SetBool("isFollowing", false);
        // }
      
    //    if(Input.GetKeyDown(KeyCode.E)){
    //        animator.SetBool("isFollowing", true);
    //    }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

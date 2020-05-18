using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : StateMachineBehaviour
{
    [SerializeField] private Player playerPos;
    public GameObject skeleton;
    // Distance between skeleton and player
    public float skeDistance;
    public float speed;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
      skeleton = GameObject.FindGameObjectWithTag("Skeleton");

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      animator.transform.position = Vector2.MoveTowards(animator.transform.position, this.playerPos.transform.position, speed * Time.deltaTime);
      
      skeDistance = Vector2.Distance(skeleton.transform.position, this.playerPos.transform.position);

      if(skeDistance > 12){
        animator.SetBool("isFollowing", false);
      }
      if(skeDistance <= 2){
        animator.SetBool("isAttacking", true);
      }else{
        animator.SetBool("isAttacking", false);
        animator.SetBool("isFollowing", true);
      }
      
        // if(skeDistance < 5){
        //   animator.SetBool("isAttacking", true);
        //   animator.SetBool("isFollowing", false);
        // }else if(skeDistance > 5){         
        //   animator.SetBool("isAttacking", false);
        //   animator.SetBool("isFollowing", true);
        // }

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}

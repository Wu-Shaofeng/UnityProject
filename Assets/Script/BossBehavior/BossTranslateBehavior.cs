using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制BOSS传送行为的动画机状态类
/// 当BOSS处于第二形态时,受击后有几率进入传送状态
/// BOSS会传送至人物旁
/// </summary>
public class BossTranslateBehavior : StateMachineBehaviour
{
    private int randIndex;
    private Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        float origin_Y = animator.transform.position.y;
        float origin_Z = animator.transform.position.z;
        float target_X = player.position.x;
        animator.transform.position = new Vector3(target_X, origin_Y, origin_Z);
        randIndex = Random.Range(0, 3);
        switch (randIndex)
        {
            case 0: animator.SetTrigger("Idle"); break;
            case 1: animator.SetTrigger("Walk"); break;
            case 2: animator.SetTrigger("RemoteAttack");break;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

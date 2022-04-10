using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制BOSS的近战攻击的动画机类
/// </summary>
public class BossAttackBehavior : StateMachineBehaviour
{
    /// <summary>用于决定BOSS下一步行为的随机值</summary>
    private int randIndex;
    /// <summary>玩家位置</summary>
    private Transform player;
    /// <summary>攻击音效</summary>
    [SerializeField] private AudioClip attackSFX;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        randIndex = Random.Range(0, 3);// 通过随机值确认下一步行为是攻击,静止或行走
        switch(randIndex)
        {
            case 0: animator.SetTrigger("Attack");break;
            case 1: animator.SetTrigger("Idle");break;
            case 2: animator.SetTrigger("Walk");break;
        }
        AudioManager.instance.PlayRandomSFX(attackSFX);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.position.x - animator.transform.position.x >= 0)// 根据玩家位置转向
            animator.transform.localScale = new Vector3(-1, 1, 1);
        else
            animator.transform.localScale = new Vector3(1, 1, 1);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
    }
}

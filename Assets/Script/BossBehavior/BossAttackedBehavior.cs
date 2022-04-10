using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制BOSS受击的动画机状态类
/// </summary>
public class BossAttackedBehavior : StateMachineBehaviour
{
    /// <summary>用于确定下一步行为的随机值</summary>
    private int randIndex;
    /// <summary>受击音效</summary>
    [SerializeField] private AudioClip attackedClip;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        randIndex = Random.Range(0, 3);
        switch (randIndex)
        {
            case 0: animator.SetTrigger("Attack"); break;
            case 1: animator.SetTrigger("RemoteAttack"); break;
            case 2: {
                    if (animator.GetBool("SecondState"))// 若BOSS处于第二形态,则进入传送状态
                        animator.SetTrigger("Translate");
                    else
                        animator.SetTrigger("Walk");
                } break;
        }
        AudioManager.instance.PlayRandomSFX(attackedClip);// 播放受击音效
    }
}

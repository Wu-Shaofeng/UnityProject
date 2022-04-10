using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制BOSS远程攻击的动画机类
/// </summary>
public class BossAttackAreaBehavior : StateMachineBehaviour
{
    /// <summary>玩家位置</summary>
    private Transform player;
    /// <summary>动画播放的原始位置</summary>
    private Vector3 originPosition;
    /// <summary>Y轴的偏移量</summary>
    public float offset;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 获取玩家位置
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // 设置远程攻击动画的部分位置,玩家头顶
        animator.transform.position = new Vector3(player.position.x, player.transform.position.y + offset, animator.transform.position.z);
        originPosition = animator.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 固定动画播放位置
        animator.transform.position = originPosition;
    }
}

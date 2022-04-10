using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 死在森林中的旅人，他们的尸骨常常游荡而不得安息
/// </summary>
public class Skeleton : MeleeMonster
{
    ///<summary>怪物受击特效的播放器</summary>
    [Header("子类组件")]
    [SerializeField] private Animator hitAnimator;

    /// <summary>
    /// <para>当怪物受击时，根据传入的玩家方向转身，从而面向玩家</para>
    /// <para>设置触发器，切换动画机的状态</para>
    /// </summary>
    /// <param name="direction">玩家攻击方向</param>
    public new void fendOff(Vector2 direction)
    {
        // 若怪物未死亡
        if (!death)
        {
            // 改变怪物朝向，面向玩家
            transform.localScale = new Vector3(-direction.x, 1, 1);
            // 切换动画机状态
            animator.SetTrigger("Attacked");
            hitAnimator.SetTrigger("Hit");
        }
    }
}

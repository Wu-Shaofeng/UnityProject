using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 森林中的罕见射手，它口中发射的火球将驱逐一切来犯之敌 
/// </summary>
public class Worm : MeleeMonster
{
    ///<summary>怪物受击特效的播放器</summary>
    [Header("子类组件")]
    [SerializeField] private Animator hitAnimator;
    ///<summary>怪物攻击发射的火球预制体</summary>
    [SerializeField] private GameObject fireBall;
    ///<summary>怪物火球发射的起始位置</summary>
    [SerializeField] private GameObject muzzle;
    /// <summary>发射火球的音效</summary>
    [SerializeField] private AudioClip fireSFX;
    /// <summary>
    /// 在muzzle的所在位置发射火球
    /// </summary>
    void  launchFireBall()
    {
        AudioManager.instance.PlayRandomSFX(fireSFX, 3.0f);
        PoolManager.Release(fireBall, muzzle.transform.position, gameObject.transform.rotation, gameObject.transform.localScale);
    }

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

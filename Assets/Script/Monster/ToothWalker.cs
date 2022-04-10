using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 利齿是它的武器，一旦被纠缠就极难甩脱
/// 注: 快跑!!!
/// </summary>
public class ToothWalker : MeleeMonster
{
    /// <summary>攻击音效</summary>
    [SerializeField] private AudioClip attackSFX;
    /// <summary>
    /// 播放攻击音效
    /// </summary>
    public void Bite()
    {
        AudioManager.instance.PlayRandomSFX(attackSFX);
    }
}

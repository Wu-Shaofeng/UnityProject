using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 擅于伪装的老道猎手，平时将自己伪装成景物，等待猎物接近时才给予致命一击
/// 注:小心垃圾堆!!!
/// </summary>
public class Trash : MeleeMonster
{
    /// <summary>
    /// 获取当前播放的动画名
    /// </summary>
    /// <returns>当前播放的动画名</returns>
    public string getCurrentClipInfo()
    {
        return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }

    void Update()
    {
        /// 当怪物处于闲置伏击状态，隐藏血条，起到埋伏效果
        if ((getCurrentClipInfo() == "TrashIdle") && healthBar.gameObject.activeSelf)
            onDisableHealthBar();
        /// 当怪物处于追击状态，显示血条，进入战斗状态
        else if (getCurrentClipInfo() == "TrashChase" && !healthBar.gameObject.activeSelf)
            onEnableHealthBar();
    }
}

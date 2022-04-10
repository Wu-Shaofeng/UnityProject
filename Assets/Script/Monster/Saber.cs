using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 居住在森林中的神秘剑士，他们的一生都投身于战斗，连死亡都是他们的武器
/// </summary>
public class Saber : MeleeMonster
{
    [SerializeField] private AudioClip explosionSFX;
    void ExplosionEvent()
    {
        AudioManager.instance.PlayRandomSFX(explosionSFX);
    }
}

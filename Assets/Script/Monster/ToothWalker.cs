using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����������������һ���������ͼ���˦��
/// ע: ����!!!
/// </summary>
public class ToothWalker : MeleeMonster
{
    /// <summary>������Ч</summary>
    [SerializeField] private AudioClip attackSFX;
    /// <summary>
    /// ���Ź�����Ч
    /// </summary>
    public void Bite()
    {
        AudioManager.instance.PlayRandomSFX(attackSFX);
    }
}

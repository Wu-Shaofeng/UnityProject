using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ס��ɭ���е����ؽ�ʿ�����ǵ�һ����Ͷ����ս�����������������ǵ�����
/// </summary>
public class Saber : MeleeMonster
{
    [SerializeField] private AudioClip explosionSFX;
    void ExplosionEvent()
    {
        AudioManager.instance.PlayRandomSFX(explosionSFX);
    }
}

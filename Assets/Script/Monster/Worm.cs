using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ɭ���еĺ������֣������з���Ļ�������һ������֮�� 
/// </summary>
public class Worm : MeleeMonster
{
    ///<summary>�����ܻ���Ч�Ĳ�����</summary>
    [Header("�������")]
    [SerializeField] private Animator hitAnimator;
    ///<summary>���﹥������Ļ���Ԥ����</summary>
    [SerializeField] private GameObject fireBall;
    ///<summary>������������ʼλ��</summary>
    [SerializeField] private GameObject muzzle;
    /// <summary>����������Ч</summary>
    [SerializeField] private AudioClip fireSFX;
    /// <summary>
    /// ��muzzle������λ�÷������
    /// </summary>
    void  launchFireBall()
    {
        AudioManager.instance.PlayRandomSFX(fireSFX, 3.0f);
        PoolManager.Release(fireBall, muzzle.transform.position, gameObject.transform.rotation, gameObject.transform.localScale);
    }

    /// <summary>
    /// <para>�������ܻ�ʱ�����ݴ������ҷ���ת���Ӷ��������</para>
    /// <para>���ô��������л���������״̬</para>
    /// </summary>
    /// <param name="direction">��ҹ�������</param>
    public new void fendOff(Vector2 direction)
    {
        // ������δ����
        if (!death)
        {
            // �ı���ﳯ���������
            transform.localScale = new Vector3(-direction.x, 1, 1);
            // �л�������״̬
            animator.SetTrigger("Attacked");
            hitAnimator.SetTrigger("Hit");
        }
    }

}

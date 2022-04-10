using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ɭ���е����ˣ����ǵ�ʬ�ǳ����ε������ð�Ϣ
/// </summary>
public class Skeleton : MeleeMonster
{
    ///<summary>�����ܻ���Ч�Ĳ�����</summary>
    [Header("�������")]
    [SerializeField] private Animator hitAnimator;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ƹ�����������󣩲�����Ϊ��״̬����
/// </summary>
public class HideBehavior : StateMachineBehaviour
{
    /// <summary>�������ǰ����</summary>
    private Vector2 forwardVector;
    /// <summary>
    /// <para>�жϹ����Ƿ����ǰ���Ĳ���ֵ</para>
    /// <para>ͨ����ǰ�·���ǰ���������߼�⣬ͨ�������ж��Ƿ�Ӵ�������㼶�����ò���ֵ</para>
    /// <para>����ǰ�������߼�⵽����㼶�����������ǽ�ڣ�����ֵΪ�٣�����ֹͣ</para>
    /// <para>����ǰ�·������߼�⵽����㼶�������ǰ��Ϊ���£�����ֵΪ�٣�����ֹͣ</para>
    /// </summary>
    private bool canForward;
    /// <summary>����㼶</summary>
    [SerializeField] private LayerMask groundLayer;
    /// <summary>�ж��ƶ������߳���</summary>
    [SerializeField] private float walkRayLength;

    /// <summary>
    /// <para>�жϹ����Ƿ�ʼ׷���Ĳ���ֵ</para>
    /// <para>ͨ����ǰ���������߼�⣬ͨ�������Ƿ�Ӵ�����Ҳ㼶�����ò���ֵ</para>
    /// <para>������ֵΪ�棬��������׷��״̬��������ֵΪ�٣�����ﱣ���ƶ�״̬</para>
    /// </summary>
    private bool canChase;
    /// <summary>��Ҳ㼶</summary>
    [SerializeField] private LayerMask playerLayer;
    /// <summary>�ж�׷�������߳���</summary>
    [SerializeField] private float chaseRayLength;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* ʵʱ������ͼ�㣬��canForwardΪ��ʱֹͣǰ������������״̬ */
        canForward = Physics2D.Raycast(animator.transform.position, Vector2.down + forwardVector, walkRayLength, groundLayer) && !(Physics2D.Raycast(animator.transform.position, forwardVector, walkRayLength / 2, groundLayer));
        /* ʵʱ������ͼ�㣬��canChaseΪ����canForwardΪ��ʱ������׷��״̬ */
        canChase = Physics2D.Raycast(animator.transform.position, Vector2.left, chaseRayLength, playerLayer) || Physics2D.Raycast(animator.transform.position, Vector2.right, chaseRayLength, playerLayer);

        if (canChase && canForward)
        {
            animator.SetTrigger("Chase");
        }
    }
}

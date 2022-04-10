using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ƹ������������Ѳ����Ϊ��״̬����
/// </summary>
public class WalkBehavior : StateMachineBehaviour
{
    /// <summary>�����2D����</summary>
    private Rigidbody2D body;
    /// <summary>�������ǰ����</summary>
    private Vector2 forwardVector;
    /// <summary>�����Ѳ���ٶ�</summary>
    [SerializeField]private float speed;
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

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body = animator.gameObject.GetComponent<Rigidbody2D>();
        forwardVector = animator.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* ʵʱ������ͼ�㣬��canForwardΪ��ʱֹͣǰ������������״̬ */
        canForward = Physics2D.Raycast(animator.transform.position, Vector2.down + forwardVector, walkRayLength, groundLayer) && !(Physics2D.Raycast(animator.transform.position, forwardVector, walkRayLength/2, groundLayer));
        /* ʵʱ������ͼ�㣬��canChaseΪ����canForwardΪ��ʱ������׷��״̬ */
        canChase = Physics2D.Raycast(animator.transform.position, forwardVector, chaseRayLength, playerLayer);

        if (!canForward)
        {
            animator.SetTrigger("Idle");
        }
        else if(canChase)
        {
            animator.SetTrigger("Chase");
        }
        else
        {
            body.velocity = animator.transform.localScale.x > 0 ? new Vector2(speed, 0) : new Vector2(-speed, 0);
        }
    }

}

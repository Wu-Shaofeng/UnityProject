using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����BOSSԶ�̹����Ķ�������
/// </summary>
public class BossAttackAreaBehavior : StateMachineBehaviour
{
    /// <summary>���λ��</summary>
    private Transform player;
    /// <summary>�������ŵ�ԭʼλ��</summary>
    private Vector3 originPosition;
    /// <summary>Y���ƫ����</summary>
    public float offset;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ��ȡ���λ��
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // ����Զ�̹��������Ĳ���λ��,���ͷ��
        animator.transform.position = new Vector3(player.position.x, player.transform.position.y + offset, animator.transform.position.z);
        originPosition = animator.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �̶���������λ��
        animator.transform.position = originPosition;
    }
}

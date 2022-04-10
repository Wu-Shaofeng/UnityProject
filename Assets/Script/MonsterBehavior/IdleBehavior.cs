using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ƹ������������������Ϊ��״̬����
/// </summary>
public class IdleBehavior : StateMachineBehaviour
{
    /// <summary>ԭ�����õ���Сʱ��</summary>
    [SerializeField] private float minTime;
    /// <summary>ԭ�����õ����ʱ��</summary>
    [SerializeField] private float maxTime;
    /// <summary>���ü�ʱ��</summary>
    float timer;
    Rigidbody2D body;

    /* ���ü�ʱ����ʱ�� */
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        body = animator.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = Vector2.zero;
    }

    /* ��ʼ����ʱ������ʱΪ0����������״̬ */
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            /* ����ת������Ѳ�� */
            animator.transform.localScale = new Vector3(-animator.transform.localScale.x, 1, 1);
            animator.SetTrigger("Walk");
        }
        
    }
}

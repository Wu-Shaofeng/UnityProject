using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����BOSS�Ľ�ս�����Ķ�������
/// </summary>
public class BossAttackBehavior : StateMachineBehaviour
{
    /// <summary>���ھ���BOSS��һ����Ϊ�����ֵ</summary>
    private int randIndex;
    /// <summary>���λ��</summary>
    private Transform player;
    /// <summary>������Ч</summary>
    [SerializeField] private AudioClip attackSFX;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        randIndex = Random.Range(0, 3);// ͨ�����ֵȷ����һ����Ϊ�ǹ���,��ֹ������
        switch(randIndex)
        {
            case 0: animator.SetTrigger("Attack");break;
            case 1: animator.SetTrigger("Idle");break;
            case 2: animator.SetTrigger("Walk");break;
        }
        AudioManager.instance.PlayRandomSFX(attackSFX);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.position.x - animator.transform.position.x >= 0)// �������λ��ת��
            animator.transform.localScale = new Vector3(-1, 1, 1);
        else
            animator.transform.localScale = new Vector3(1, 1, 1);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
    }
}

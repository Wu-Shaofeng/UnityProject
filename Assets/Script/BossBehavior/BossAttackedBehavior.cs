using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����BOSS�ܻ��Ķ�����״̬��
/// </summary>
public class BossAttackedBehavior : StateMachineBehaviour
{
    /// <summary>����ȷ����һ����Ϊ�����ֵ</summary>
    private int randIndex;
    /// <summary>�ܻ���Ч</summary>
    [SerializeField] private AudioClip attackedClip;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        randIndex = Random.Range(0, 3);
        switch (randIndex)
        {
            case 0: animator.SetTrigger("Attack"); break;
            case 1: animator.SetTrigger("RemoteAttack"); break;
            case 2: {
                    if (animator.GetBool("SecondState"))// ��BOSS���ڵڶ���̬,����봫��״̬
                        animator.SetTrigger("Translate");
                    else
                        animator.SetTrigger("Walk");
                } break;
        }
        AudioManager.instance.PlayRandomSFX(attackedClip);// �����ܻ���Ч
    }
}

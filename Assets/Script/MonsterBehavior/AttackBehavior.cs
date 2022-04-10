using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : StateMachineBehaviour
{
    /// <summary>������Ч</summary>
    [SerializeField] AudioClip attackSFX;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.instance.PlayRandomSFX(attackSFX, 3.0f);// ���Ź�����Ч
    }

}

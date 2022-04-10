using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRemoteAttackBehavior : StateMachineBehaviour
{
    private int randIndex;
    private Animator remoteAttackAnimator;
    [SerializeField] private AudioClip remoteAttackClip;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        remoteAttackAnimator = animator.transform.GetChild(0).GetComponent<Animator>();
        randIndex = Random.Range(0,3);
        switch (randIndex)
        {
            case 0: animator.SetTrigger("RemoteAttack"); break;
            case 1: animator.SetTrigger("Idle"); break;
            case 2: animator.SetTrigger("Walk"); break;
        }
        remoteAttackAnimator.SetTrigger("RemoteAttack");
        AudioManager.instance.PlayRandomSFX(remoteAttackClip);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

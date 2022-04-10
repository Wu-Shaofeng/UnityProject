using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossBasicBehavior : StateMachineBehaviour
{
    private int randIndex;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        randIndex = Random.Range(0, 2);
        switch(randIndex)
        {
            case 0:animator.SetTrigger("Idle");break;
            case 1:animator.SetTrigger("Walk");break;
        }
    }
}

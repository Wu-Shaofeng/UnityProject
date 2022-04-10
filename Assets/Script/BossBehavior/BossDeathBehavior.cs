using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossDeathBehavior : StateMachineBehaviour
{
    [SerializeField] private int scorePoint;
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ScoreManager.instance.GainScore(scorePoint);

        Destroy(animator.gameObject);    
    }
}

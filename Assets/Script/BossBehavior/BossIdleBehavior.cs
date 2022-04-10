using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleBehavior : StateMachineBehaviour
{
    public float minTime;
    public float maxTime;
    private Transform player;
    private float timer;
    private int randIndex;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = Random.Range(minTime, maxTime);
        if (player.position.x - animator.transform.position.x >= 0)
            animator.transform.localScale = new Vector3(-1, 1, 1);
        else
            animator.transform.localScale = new Vector3(1, 1, 1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer<=0)
        {
            randIndex = Random.Range(0, 2);
            switch(randIndex)
            {
                case 0: animator.SetTrigger("Walk");break;
                case 1: animator.SetTrigger("RemoteAttack");break;
            }
            
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}

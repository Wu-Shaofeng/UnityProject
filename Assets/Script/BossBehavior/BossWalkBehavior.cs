using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制BOSS的行走状态的动画机类
/// </summary>
public class BossWalkBehavior : StateMachineBehaviour
{
    /// <summary>行走的最短时间</summary>
    public float minTime;
    /// <summary>行走的最长时间</summary>
    public float maxTime;
    /// <summary>行走速度</summary>
    public float speed;
    /// <summary>射线检测距离</summary>
    public float distance;
    /// <summary>行走时间计时器</summary>
    private float timer;
    /// <summary>玩家位置</summary>
    private Transform player;
    /// <summary>2D刚体</summary>
    private Rigidbody2D body;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 在最长和最短时间内取一个随机值
        timer = Random.Range(minTime, maxTime);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        body = animator.gameObject.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.position.x - animator.transform.position.x >= 0)
            animator.transform.localScale = new Vector3(-1, 1, 1);
        else
            animator.transform.localScale = new Vector3(1, 1, 1);

        if (player.position.x - animator.transform.position.x >= distance)
        {
            body.velocity = new Vector2(speed, 0);
        }
        else if(animator.transform.position.x - player.position.x >= distance)
        {
            body.velocity = new Vector2(-speed, 0);
        }
        else
        {
            animator.SetTrigger("Attack");
        }

        if (timer <= 0)
        {
            animator.SetTrigger("Idle");
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

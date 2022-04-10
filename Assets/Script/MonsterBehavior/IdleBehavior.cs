using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制怪物（动画机对象）闲置行为的状态机类
/// </summary>
public class IdleBehavior : StateMachineBehaviour
{
    /// <summary>原地闲置的最小时间</summary>
    [SerializeField] private float minTime;
    /// <summary>原地闲置的最大时间</summary>
    [SerializeField] private float maxTime;
    /// <summary>闲置计时器</summary>
    float timer;
    Rigidbody2D body;

    /* 设置计时器的时间 */
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        body = animator.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = Vector2.zero;
    }

    /* 开始倒计时，倒计时为0后脱离闲置状态 */
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            /* 怪物转身，继续巡逻 */
            animator.transform.localScale = new Vector3(-animator.transform.localScale.x, 1, 1);
            animator.SetTrigger("Walk");
        }
        
    }
}

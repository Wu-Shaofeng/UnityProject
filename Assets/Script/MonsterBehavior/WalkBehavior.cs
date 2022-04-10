using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制怪物（动画机对象）巡逻行为的状态机类
/// </summary>
public class WalkBehavior : StateMachineBehaviour
{
    /// <summary>怪物的2D刚体</summary>
    private Rigidbody2D body;
    /// <summary>怪物的向前向量</summary>
    private Vector2 forwardVector;
    /// <summary>怪物的巡逻速度</summary>
    [SerializeField]private float speed;
    /// <summary>
    /// <para>判断怪物是否继续前进的布尔值</para>
    /// <para>通过向前下方和前方进行射线检测，通过射线判断是否接触到地面层级来设置布尔值</para>
    /// <para>若向前方的射线检测到地面层级，则怪物碰到墙壁，布尔值为假，怪物停止</para>
    /// <para>若向前下方的射线检测到地面层级，则怪物前方为悬崖，布尔值为假，怪物停止</para>
    /// </summary>
    private bool canForward;
    /// <summary>地面层级</summary>
    [SerializeField] private LayerMask groundLayer;
    /// <summary>判断移动的射线长度</summary>
    [SerializeField] private float walkRayLength;

    /// <summary>
    /// <para>判断怪物是否开始追击的布尔值</para>
    /// <para>通过向前方进行射线检测，通过射线是否接触到玩家层级来设置布尔值</para>
    /// <para>若布尔值为真，则怪物进入追击状态。若布尔值为假，则怪物保持移动状态</para>
    /// </summary>
    private bool canChase;
    /// <summary>玩家层级</summary>
    [SerializeField] private LayerMask playerLayer;
    /// <summary>判断追击的射线长度</summary>
    [SerializeField] private float chaseRayLength;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body = animator.gameObject.GetComponent<Rigidbody2D>();
        forwardVector = animator.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* 实时检测地面图层，当canForward为假时停止前进，进入闲置状态 */
        canForward = Physics2D.Raycast(animator.transform.position, Vector2.down + forwardVector, walkRayLength, groundLayer) && !(Physics2D.Raycast(animator.transform.position, forwardVector, walkRayLength/2, groundLayer));
        /* 实时检测玩家图层，当canChase为真且canForward为假时，进入追击状态 */
        canChase = Physics2D.Raycast(animator.transform.position, forwardVector, chaseRayLength, playerLayer);

        if (!canForward)
        {
            animator.SetTrigger("Idle");
        }
        else if(canChase)
        {
            animator.SetTrigger("Chase");
        }
        else
        {
            body.velocity = animator.transform.localScale.x > 0 ? new Vector2(speed, 0) : new Vector2(-speed, 0);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制怪物（动画机对象）追击行为的状态机类
/// </summary>
public class ChaseBehavior : StateMachineBehaviour
{
    /// <summary>玩家位置</summary>
    private Transform player;
    /// <summary>怪物的2D刚体</summary>
    private Rigidbody2D body;
    /// <summary>怪物的向前向量</summary>
    private Vector2 forwardVector;
    /// <summary>怪物的攻击范围</summary>
    [SerializeField] private float distance;
    /// <summary>怪物的追击速度</summary>
    [SerializeField] private float speed;
    /// <summary>
    /// <para>判断怪物是否继续前进的布尔值</para>
    /// <para>通过向前下方进行射线检测，通过射线是否接触到目标层级来设置布尔值</para>
    /// <para>若布尔值为真，则怪物可以向前移动。若布尔值为假，则怪物停止</para>
    /// </summary>
    [Header("射线检测")]
    private bool canForward;
    /// <summary>目标层级</summary>
    public LayerMask identifyLayer;
    /// <summary>射线长度</summary>
    public float rayLength;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* 获取玩家位置和怪物的2D刚体 */
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        body = animator.gameObject.GetComponent<Rigidbody2D>();
        /* 怪物发现玩家，背景音乐切换至战斗模式音乐 */
        AudioManager.instance.CrossFadeToBattleMode();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* 追击状态时，怪物朝向由玩家位置决定 */
        if (player.position.x - animator.transform.position.x >= 0)
            animator.transform.localScale = new Vector3(1, 1, 1);
        else
            animator.transform.localScale = new Vector3(-1, 1, 1);

        forwardVector = animator.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        canForward = Physics2D.Raycast(animator.transform.position, Vector2.down + forwardVector, rayLength, identifyLayer) && !(Physics2D.Raycast(animator.transform.position, forwardVector, rayLength/2, identifyLayer));
        /* 若玩家在怪物右侧，则怪物向右移动 */
        if (player.position.x - animator.transform.position.x >= distance )
        {
            if (canForward)
                body.velocity = new Vector2(speed, 0);
            /* canForward为假时，怪物停止追击，切换至巡逻状态 */
            else
                animator.SetTrigger("Patrol");
        }
        /* 若玩家在怪物左侧，则怪物向左移动 */
        else if (animator.transform.position.x - player.position.x >= distance)
        {
            if (canForward)
                body.velocity = new Vector2(-speed, 0);
            else
                animator.SetTrigger("Patrol");
        }
        /* 若玩家在怪物攻击范围内，怪物开始攻击 */
        else
        {
            if(animator.gameObject.TryGetComponent<Saber>(out Saber saber))
            {
                if (Random.value > 0.6)
                    animator.SetTrigger("Explosion");
                else
                {
                    body.velocity = Vector2.zero;
                    animator.SetTrigger("Attack");
                }
            }
            else
            {
                body.velocity = Vector2.zero;
                animator.SetTrigger("Attack");
            }
        }

    }
}

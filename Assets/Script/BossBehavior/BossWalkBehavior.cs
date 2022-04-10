using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����BOSS������״̬�Ķ�������
/// </summary>
public class BossWalkBehavior : StateMachineBehaviour
{
    /// <summary>���ߵ����ʱ��</summary>
    public float minTime;
    /// <summary>���ߵ��ʱ��</summary>
    public float maxTime;
    /// <summary>�����ٶ�</summary>
    public float speed;
    /// <summary>���߼�����</summary>
    public float distance;
    /// <summary>����ʱ���ʱ��</summary>
    private float timer;
    /// <summary>���λ��</summary>
    private Transform player;
    /// <summary>2D����</summary>
    private Rigidbody2D body;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ��������ʱ����ȡһ�����ֵ
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

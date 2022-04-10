using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ƹ������������׷����Ϊ��״̬����
/// </summary>
public class ChaseBehavior : StateMachineBehaviour
{
    /// <summary>���λ��</summary>
    private Transform player;
    /// <summary>�����2D����</summary>
    private Rigidbody2D body;
    /// <summary>�������ǰ����</summary>
    private Vector2 forwardVector;
    /// <summary>����Ĺ�����Χ</summary>
    [SerializeField] private float distance;
    /// <summary>�����׷���ٶ�</summary>
    [SerializeField] private float speed;
    /// <summary>
    /// <para>�жϹ����Ƿ����ǰ���Ĳ���ֵ</para>
    /// <para>ͨ����ǰ�·��������߼�⣬ͨ�������Ƿ�Ӵ���Ŀ��㼶�����ò���ֵ</para>
    /// <para>������ֵΪ�棬����������ǰ�ƶ���������ֵΪ�٣������ֹͣ</para>
    /// </summary>
    [Header("���߼��")]
    private bool canForward;
    /// <summary>Ŀ��㼶</summary>
    public LayerMask identifyLayer;
    /// <summary>���߳���</summary>
    public float rayLength;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* ��ȡ���λ�ú͹����2D���� */
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        body = animator.gameObject.GetComponent<Rigidbody2D>();
        /* ���﷢����ң����������л���ս��ģʽ���� */
        AudioManager.instance.CrossFadeToBattleMode();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* ׷��״̬ʱ�����ﳯ�������λ�þ��� */
        if (player.position.x - animator.transform.position.x >= 0)
            animator.transform.localScale = new Vector3(1, 1, 1);
        else
            animator.transform.localScale = new Vector3(-1, 1, 1);

        forwardVector = animator.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        canForward = Physics2D.Raycast(animator.transform.position, Vector2.down + forwardVector, rayLength, identifyLayer) && !(Physics2D.Raycast(animator.transform.position, forwardVector, rayLength/2, identifyLayer));
        /* ������ڹ����Ҳ࣬����������ƶ� */
        if (player.position.x - animator.transform.position.x >= distance )
        {
            if (canForward)
                body.velocity = new Vector2(speed, 0);
            /* canForwardΪ��ʱ������ֹͣ׷�����л���Ѳ��״̬ */
            else
                animator.SetTrigger("Patrol");
        }
        /* ������ڹ�����࣬����������ƶ� */
        else if (animator.transform.position.x - player.position.x >= distance)
        {
            if (canForward)
                body.velocity = new Vector2(-speed, 0);
            else
                animator.SetTrigger("Patrol");
        }
        /* ������ڹ��﹥����Χ�ڣ����￪ʼ���� */
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

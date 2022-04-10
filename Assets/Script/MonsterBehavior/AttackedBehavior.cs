using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedBehavior : StateMachineBehaviour
{
    private Rigidbody2D body;// �����2D����
    [SerializeField] private float speed;// �����˵��ٶ�
    [SerializeField] private AudioClip attackedSFX;// �ܻ���Ч
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body = animator.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = animator.transform.localScale.x < 0 ? new Vector2(speed, 0) : new Vector2(-speed, 0);// �����﹥������
        AudioManager.instance.PlayRandomSFX(attackedSFX);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedBehavior : StateMachineBehaviour
{
    private Rigidbody2D body;// 怪物的2D刚体
    [SerializeField] private float speed;// 被击退的速度
    [SerializeField] private AudioClip attackedSFX;// 受击音效
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body = animator.gameObject.GetComponent<Rigidbody2D>();
        body.velocity = animator.transform.localScale.x < 0 ? new Vector2(speed, 0) : new Vector2(-speed, 0);// 被人物攻击击退
        AudioManager.instance.PlayRandomSFX(attackedSFX);
    }
}

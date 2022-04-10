using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 火球类(Projectile的继承类),由怪物Worm发射
/// </summary>
public class FireBall : Projectile
{
    /// <summary>火球的动画机</summary>
    [SerializeField]private Animator animator;
    /// <summary>发射者</summary>
    [SerializeField]private Transform shooter;
    /// <summary>
    /// 启动火球，火球开始飞行
    /// </summary>
    void OnEnable()
    {
        moveDirection = gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        StartCoroutine(moveDirectly());
        StartCoroutine(lifeTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Map") || collision.CompareTag("stair"))// 当火球触碰到地图场景时，火球爆炸
        {
            animator.SetTrigger("Explosion");
        }
        if (collision.gameObject.TryGetComponent<CharacterController>(out CharacterController character))// 当火球触碰到玩家时，火球爆炸，给玩家造成伤害
        {
            character.getDamage(damage);
            animator.SetTrigger("Explosion");
        }
    }
    /// <summary>
    /// 火球爆炸的动画帧事件
    /// </summary>
    void explosion()
    {
        gameObject.SetActive(false);
    }
}

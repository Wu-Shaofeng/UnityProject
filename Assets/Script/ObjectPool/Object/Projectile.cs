using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 飞行物基类（玩家普通远程攻击）
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>飞行速度</summary>
    [SerializeField]protected float moveSpeed;
    /// <summary>飞行物伤害</summary>
    [SerializeField]protected int damage;
    /// <summary>飞行方向</summary>
    protected Vector2 moveDirection;
    /// <summary>击中特效的预制体</summary>
    [SerializeField]protected GameObject hitPrefab;
    /// <summary>玩家位置</summary>
    [SerializeField]protected Transform player;
    /// <summary>
    /// 启用飞行物，通过玩家朝向决定射出方向
    /// </summary>
    void OnEnable()
    {
        moveDirection = player.transform.localScale.x > 0 ? Vector2.left : Vector2.right;
        /// 开始飞行
        StartCoroutine(moveDirectly());
        /// 存活时间
        StartCoroutine(lifeTime());
    }
    /// <summary>
    /// 飞行超时后销毁飞行物，存活时间为3s
    /// </summary>
    protected IEnumerator lifeTime()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 飞行物笔直飞行
    /// </summary>
    protected IEnumerator moveDirectly()
    {
        while(gameObject.activeSelf)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 若飞行物击中怪物，则对怪物造成伤害
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.getDamage(damage);
            enemy.fendOff(moveDirection);
            // 在击中位置播放击中特效
            PoolManager.Release(hitPrefab, transform.position);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.TryGetComponent<MeleeMonster>(out MeleeMonster melee))
        {
            melee.getDamage(damage);
            melee.fendOff(moveDirection);
            PoolManager.Release(hitPrefab, transform.position);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.TryGetComponent<BringOfDeath>(out BringOfDeath boss))
        {
            boss.getDamage(damage);
            PoolManager.Release(hitPrefab, transform.position);
            gameObject.SetActive(false);
        }
        // 若飞行物击中地图，则直接销毁
        if (collision.CompareTag("Map"))
        {
            PoolManager.Release(hitPrefab, transform.position);
            gameObject.SetActive(false);
        }
        
    }

}

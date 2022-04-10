using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 飞行物继承类（玩家爆气远程攻击，追踪子弹）
/// </summary>
public class OverLoadProjectile : Projectile
{
    /// <summary>子弹攻击目标</summary>
    [SerializeField]private GameObject target;
    /// <summary>子弹攻击目标所在的层级</summary>
    [SerializeField]private LayerMask targetLayer;
    /// <summary>攻击目标的搜索半径</summary>
    [SerializeField]private float searchRadius;
    /// <summary>子弹追踪飞行的最小旋转角度</summary>
    [SerializeField]private float minRotateAngle;
    /// <summary>子弹追踪飞行的最大旋转角度</summary>
    [SerializeField]private float maxRotateAngle;
    /// <summary>子弹追踪飞行的旋转角度</summary>
    float rotateAngle;
    /// <summary>子弹追踪飞行的当前朝向</summary>
    Vector3 targetDirection;

    /// <summary>
    /// 启用飞行物，通过玩家朝向决定射出方向
    /// </summary>
    void OnEnable()
    {
        moveDirection = player.localScale.x > 0 ? Vector2.left : Vector2.right;
        StartCoroutine(lifeTime());
        // 以玩家为圆心搜索半径内的目标
        findTarget(player);
        StartCoroutine(aimTowardsTarget());
    }
    /// <summary>
    /// 以传入目标的位置为圆心搜索半径内是否存在目标
    /// </summary>
    /// <param name="point">圆心位置</param>
    void findTarget(Transform point)
    {
        Vector2 center = new Vector2(point.position.x, point.position.y);
        // 若范围内存在目标层级，则将目标设置为任意一个该层级的对象，若不存在则将目标设置为玩家自己
        target = Physics2D.OverlapCircle(center,searchRadius,targetLayer)? Physics2D.OverlapCircle(center, searchRadius, targetLayer).GetComponent<Collider2D>().gameObject: player.gameObject;
    }
    /// <summary>
    /// 飞行物朝向目标旋转飞行，若目标为玩家本身，则追踪子弹围绕玩家飞行形成屏障
    /// </summary>
    /// <returns></returns>
    IEnumerator aimTowardsTarget()
    {
        // 随机旋转角度
        rotateAngle = Random.Range(minRotateAngle, maxRotateAngle);
        if (target != null)
        {
            while (gameObject.activeSelf)
            {
                if (target != null)
                {
                    // 当目标存在且飞行物存活，飞行物旋转追踪目标
                    targetDirection = target.transform.position - transform.position;
                    float angle = 360 - Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, 0, angle);
                    transform.rotation *= Quaternion.Euler(0, 0, rotateAngle);
                    transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
                }
                else
                {
                    // 当目标不存在，取消追踪，笔直飞行
                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                }
                yield return null;
            }
        }
    }
    // 追踪子弹具有无视地图场景的效果，能够穿透墙壁地面攻击目标
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 若飞行物击中怪物，则对怪物造成伤害
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
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

    }
}

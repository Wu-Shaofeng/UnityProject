using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������̳��ࣨ��ұ���Զ�̹�����׷���ӵ���
/// </summary>
public class OverLoadProjectile : Projectile
{
    /// <summary>�ӵ�����Ŀ��</summary>
    [SerializeField]private GameObject target;
    /// <summary>�ӵ�����Ŀ�����ڵĲ㼶</summary>
    [SerializeField]private LayerMask targetLayer;
    /// <summary>����Ŀ��������뾶</summary>
    [SerializeField]private float searchRadius;
    /// <summary>�ӵ�׷�ٷ��е���С��ת�Ƕ�</summary>
    [SerializeField]private float minRotateAngle;
    /// <summary>�ӵ�׷�ٷ��е������ת�Ƕ�</summary>
    [SerializeField]private float maxRotateAngle;
    /// <summary>�ӵ�׷�ٷ��е���ת�Ƕ�</summary>
    float rotateAngle;
    /// <summary>�ӵ�׷�ٷ��еĵ�ǰ����</summary>
    Vector3 targetDirection;

    /// <summary>
    /// ���÷����ͨ����ҳ�������������
    /// </summary>
    void OnEnable()
    {
        moveDirection = player.localScale.x > 0 ? Vector2.left : Vector2.right;
        StartCoroutine(lifeTime());
        // �����ΪԲ�������뾶�ڵ�Ŀ��
        findTarget(player);
        StartCoroutine(aimTowardsTarget());
    }
    /// <summary>
    /// �Դ���Ŀ���λ��ΪԲ�������뾶���Ƿ����Ŀ��
    /// </summary>
    /// <param name="point">Բ��λ��</param>
    void findTarget(Transform point)
    {
        Vector2 center = new Vector2(point.position.x, point.position.y);
        // ����Χ�ڴ���Ŀ��㼶����Ŀ������Ϊ����һ���ò㼶�Ķ�������������Ŀ������Ϊ����Լ�
        target = Physics2D.OverlapCircle(center,searchRadius,targetLayer)? Physics2D.OverlapCircle(center, searchRadius, targetLayer).GetComponent<Collider2D>().gameObject: player.gameObject;
    }
    /// <summary>
    /// �����ﳯ��Ŀ����ת���У���Ŀ��Ϊ��ұ�����׷���ӵ�Χ����ҷ����γ�����
    /// </summary>
    /// <returns></returns>
    IEnumerator aimTowardsTarget()
    {
        // �����ת�Ƕ�
        rotateAngle = Random.Range(minRotateAngle, maxRotateAngle);
        if (target != null)
        {
            while (gameObject.activeSelf)
            {
                if (target != null)
                {
                    // ��Ŀ������ҷ��������������ת׷��Ŀ��
                    targetDirection = target.transform.position - transform.position;
                    float angle = 360 - Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, 0, angle);
                    transform.rotation *= Quaternion.Euler(0, 0, rotateAngle);
                    transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
                }
                else
                {
                    // ��Ŀ�겻���ڣ�ȡ��׷�٣���ֱ����
                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                }
                yield return null;
            }
        }
    }
    // ׷���ӵ��������ӵ�ͼ������Ч�����ܹ���͸ǽ�ڵ��湥��Ŀ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����������й����Թ�������˺�
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.getDamage(damage);
            enemy.fendOff(moveDirection);
            // �ڻ���λ�ò��Ż�����Ч
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

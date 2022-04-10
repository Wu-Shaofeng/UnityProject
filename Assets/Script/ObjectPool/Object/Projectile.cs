using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��������ࣨ�����ͨԶ�̹�����
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>�����ٶ�</summary>
    [SerializeField]protected float moveSpeed;
    /// <summary>�������˺�</summary>
    [SerializeField]protected int damage;
    /// <summary>���з���</summary>
    protected Vector2 moveDirection;
    /// <summary>������Ч��Ԥ����</summary>
    [SerializeField]protected GameObject hitPrefab;
    /// <summary>���λ��</summary>
    [SerializeField]protected Transform player;
    /// <summary>
    /// ���÷����ͨ����ҳ�������������
    /// </summary>
    void OnEnable()
    {
        moveDirection = player.transform.localScale.x > 0 ? Vector2.left : Vector2.right;
        /// ��ʼ����
        StartCoroutine(moveDirectly());
        /// ���ʱ��
        StartCoroutine(lifeTime());
    }
    /// <summary>
    /// ���г�ʱ�����ٷ�������ʱ��Ϊ3s
    /// </summary>
    protected IEnumerator lifeTime()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);
    }
    /// <summary>
    /// �������ֱ����
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
        // ����������й����Թ�������˺�
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
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
        // ����������е�ͼ����ֱ������
        if (collision.CompareTag("Map"))
        {
            PoolManager.Release(hitPrefab, transform.position);
            gameObject.SetActive(false);
        }
        
    }

}

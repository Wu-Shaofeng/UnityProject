using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������(Projectile�ļ̳���),�ɹ���Worm����
/// </summary>
public class FireBall : Projectile
{
    /// <summary>����Ķ�����</summary>
    [SerializeField]private Animator animator;
    /// <summary>������</summary>
    [SerializeField]private Transform shooter;
    /// <summary>
    /// �������򣬻���ʼ����
    /// </summary>
    void OnEnable()
    {
        moveDirection = gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        StartCoroutine(moveDirectly());
        StartCoroutine(lifeTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Map") || collision.CompareTag("stair"))// ������������ͼ����ʱ������ը
        {
            animator.SetTrigger("Explosion");
        }
        if (collision.gameObject.TryGetComponent<CharacterController>(out CharacterController character))// �������������ʱ������ը�����������˺�
        {
            character.getDamage(damage);
            animator.SetTrigger("Explosion");
        }
    }
    /// <summary>
    /// ����ը�Ķ���֡�¼�
    /// </summary>
    void explosion()
    {
        gameObject.SetActive(false);
    }
}

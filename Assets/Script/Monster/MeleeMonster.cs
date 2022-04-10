using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ս������(����)
/// </summary>
public class MeleeMonster : MonoBehaviour
{
    /// <summary>��ǰ����ֵ</summary>
    int health;
    /// <summary>�������ֵ</summary>
    [SerializeField] protected int maxHealth;
    /// <summary>������</summary>
    [SerializeField] protected int attack;
    /// <summary>�ж��Ƿ������Ĳ���ֵ</summary>
    protected bool death;


    /// <summary>�����2D����</summary>
    protected Rigidbody2D body;
    /// <summary>����Ķ�����</summary>
    protected Animator animator;
    /// <summary>�����Ѫ����</summary>
    [SerializeField] protected StatusBar healthBar;


    /// <summary>
    /// ���ù���Ļ�������
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        health = maxHealth;
        onEnableHealthBar();
    }

    // Update is called once per frame

    void Update()
    {
        if (gameObject.transform.position.y < -50.0f)// ���������������,ֱ������
            Destroy(gameObject);
    }
    /// <summary>
    /// <para>�������ܻ�ʱ�����ݴ������ҷ���ת���Ӷ��������</para>
    /// <para>���ô��������л���������״̬</para>
    /// </summary>
    /// <param name="direction">��ҹ�������</param>
    public void fendOff(Vector2 direction)
    {
        if (!death)
        {
            transform.localScale = new Vector3(-direction.x, 1, 1);
            animator.SetTrigger("Attacked");
        }
    }

    /// <summary>
    /// <para>��������ʱ���ˣ�����ֵ���٣�Ѫ����֮�仯</para>
    /// <para>������ֵ�۳���0�󣬹�������</para>
    /// </summary>
    /// <param name="damage">�����˺�</param>
    public void getDamage(int damage)
    {
        if (!death)// �����������˺��޷�Ӧ
        {
            AudioManager.instance.CrossFadeToBattleMode();
            health -= damage;
            healthBar.UpdateStatus(health, maxHealth);
            if (health <= 0)
            {
                death = true;
                onDisableHealthBar();
                animator.SetTrigger("Die");
            }
        }
    }
    /// <summary>
    /// ����Ѫ����Ѫ�����ӻ�
    /// </summary>
    public void onEnableHealthBar()
    {
        healthBar.gameObject.SetActive(true);
        healthBar.Initialize(health, maxHealth);
    }
    /// <summary>
    /// ����Ѫ����Ѫ����ʧ
    /// </summary>
    public void onDisableHealthBar()
    {
        healthBar.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����������ʱ���������
        if (collision.gameObject.TryGetComponent<CharacterController>(out CharacterController player))
        {
            player.getDamage(attack);
        }
    }
}

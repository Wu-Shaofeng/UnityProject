using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// BOSS��
/// </summary>
public class BringOfDeath : MonoBehaviour
{
    /// <summary>�������ֵ</summary>
    [SerializeField] private int maxHealth;
    /// <summary>��ǰ����ֵ</summary>
    [SerializeField] private int health;
    /// <summary>��ս�˺�</summary>
    [SerializeField] private int AttackDamage;
    /// <summary>�ڶ���̬���˺��ӳ�</summary>
    [SerializeField] private int bonus;
    /// <summary>����ֵδ�룬�ڶ���̬</summary>
    [SerializeField] private bool halfHealth;
    /// <summary>�Ƿ�����</summary>
    [SerializeField] private bool death;
    /// <summary>������</summary>
    [SerializeField] private Animator animator;
    /// <summary>BossѪ��</summary>
    [SerializeField] protected PlayerBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        healthBar.Initialize(health, maxHealth);
    }

    void Update()
    {
        AudioManager.instance.CrossFadeToBattleMode();
    }
    /// <summary>
    /// ��Ѫ���Boss�����ڶ���̬
    /// </summary>
    void secondState()
    {
        halfHealth = true;
        AttackDamage *= bonus;
        animator.SetBool("SecondState", true);
    }

    public void getDamage(int damage)
    {
        if (!death)// �����������˺��޷�Ӧ
        {
            animator.SetTrigger("Attacked");
            AudioManager.instance.CrossFadeToBattleMode();
            health -= damage;
            if(maxHealth * 0.5 >= health && !halfHealth)
                secondState();
            if (health <= 0)
            {
                health = 0;
                death = true;
                animator.SetTrigger("Die");
            }
            healthBar.UpdateStatus(health, maxHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<CharacterController>(out CharacterController character))
        {
            character.getDamage(AttackDamage);
        }
    }
}

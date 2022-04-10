using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// BOSS类
/// </summary>
public class BringOfDeath : MonoBehaviour
{
    /// <summary>最大生命值</summary>
    [SerializeField] private int maxHealth;
    /// <summary>当前生命值</summary>
    [SerializeField] private int health;
    /// <summary>近战伤害</summary>
    [SerializeField] private int AttackDamage;
    /// <summary>第二形态的伤害加成</summary>
    [SerializeField] private int bonus;
    /// <summary>生命值未半，第二形态</summary>
    [SerializeField] private bool halfHealth;
    /// <summary>是否死亡</summary>
    [SerializeField] private bool death;
    /// <summary>动画机</summary>
    [SerializeField] private Animator animator;
    /// <summary>Boss血条</summary>
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
    /// 半血后的Boss会进入第二形态
    /// </summary>
    void secondState()
    {
        halfHealth = true;
        AttackDamage *= bonus;
        animator.SetBool("SecondState", true);
    }

    public void getDamage(int damage)
    {
        if (!death)// 死亡后遭受伤害无反应
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

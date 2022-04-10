using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 近战怪物类(基类)
/// </summary>
public class MeleeMonster : MonoBehaviour
{
    /// <summary>当前生命值</summary>
    int health;
    /// <summary>最大生命值</summary>
    [SerializeField] protected int maxHealth;
    /// <summary>攻击力</summary>
    [SerializeField] protected int attack;
    /// <summary>判断是否死亡的布尔值</summary>
    protected bool death;


    /// <summary>怪物的2D刚体</summary>
    protected Rigidbody2D body;
    /// <summary>怪物的动画机</summary>
    protected Animator animator;
    /// <summary>怪物的血条类</summary>
    [SerializeField] protected StatusBar healthBar;


    /// <summary>
    /// 设置怪物的基础属性
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
        if (gameObject.transform.position.y < -50.0f)// 当怪物掉出场景外,直接死亡
            Destroy(gameObject);
    }
    /// <summary>
    /// <para>当怪物受击时，根据传入的玩家方向转身，从而面向玩家</para>
    /// <para>设置触发器，切换动画机的状态</para>
    /// </summary>
    /// <param name="direction">玩家攻击方向</param>
    public void fendOff(Vector2 direction)
    {
        if (!death)
        {
            transform.localScale = new Vector3(-direction.x, 1, 1);
            animator.SetTrigger("Attacked");
        }
    }

    /// <summary>
    /// <para>当怪物存活时受伤，生命值减少，血条随之变化</para>
    /// <para>当生命值扣除至0后，怪物死亡</para>
    /// </summary>
    /// <param name="damage">所受伤害</param>
    public void getDamage(int damage)
    {
        if (!death)// 死亡后遭受伤害无反应
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
    /// 启用血条，血条可视化
    /// </summary>
    public void onEnableHealthBar()
    {
        healthBar.gameObject.SetActive(true);
        healthBar.Initialize(health, maxHealth);
    }
    /// <summary>
    /// 弃用血条，血条消失
    /// </summary>
    public void onDisableHealthBar()
    {
        healthBar.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 当攻击到玩家时，玩家受伤
        if (collision.gameObject.TryGetComponent<CharacterController>(out CharacterController player))
        {
            player.getDamage(attack);
        }
    }
}

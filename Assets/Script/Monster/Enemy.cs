using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// É³°üÀà
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("Hit")]
    public float hitSpeed;
    private Vector2 hitDirection;
    private bool isHit;
    private AnimatorStateInfo info;

    [Header("Componment")]
    public Animator animator;
    public Animator hitAnimator;
    public Rigidbody2D body;
    [SerializeField] StatusBar healthBar;
    public bool showOnHead = true;

    [Header("Basic Attribute")]
    private int maxHealth; 
    public int health;
    public int damage;
    public bool death;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hitAnimator = transform.GetChild(0).GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        maxHealth = health;
        onEnableHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit && !death) 
        {
            Attacked();
        }
    }


    void Attacked()
    {
        info = animator.GetCurrentAnimatorStateInfo(0);
        body.AddForce(hitDirection * hitSpeed);
        if (info.normalizedTime >= .9f)
        {
            isHit = false;
        }
    }


    public void fendOff(Vector2 direction)
    {
        if (!death)
        {
            transform.localScale = new Vector3(-direction.x, 1, 1);
            hitDirection = direction;
            isHit = true;
            animator.SetTrigger("isHit");
            hitAnimator.SetTrigger("Hit");
        }
    }

    public void getDamage(int damage)
    {
        if (!death)
        {
            health -= damage;
            healthBar.UpdateStatus(health, maxHealth);
            if (health <= 0)
            {
                death = true;
                onDisableHealthBar();
                body.velocity = Vector2.zero;
                animator.ResetTrigger("Hit");
                animator.SetBool("Die", true);
            }
        }
    }

    void die()
    {
        Destroy(gameObject);
    }

    public void onEnableHealthBar()
    {
        healthBar.gameObject.SetActive(true);
        healthBar.Initialize(health, maxHealth);
    }
    public void onDisableHealthBar()
    {
        healthBar.gameObject.SetActive(false);
    }
}

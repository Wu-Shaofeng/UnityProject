using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveForce = 2f;
    public Vector2 direction;

    [Header("Components")]
    public Rigidbody2D body;
    public Animator animator;
    public LayerMask groundLayer;
    public LayerMask stairLayer;
    public AnimatorStateInfo info;
    public PlayerBar healthBar;
    public PlayerBar energyBar;
    public Animator dodgeAnimator;
    public Animator overLoadEffect;
    public Animator effectAnimator;

    [Header("Vertical Movement")]
    public float jumpForce;
    public bool canJump;
    public float ignoreRadius;

    [Header("Physics")]
    public float maxSpeed;
    public float linearDrag;
    public float gravity;
    public float fallMultiplier;

    [Header("Collision")]
    public float rayVerticalLength;
    public float rayHorizontalLength;
    public bool onGround = false;
    public bool onWall = false;

    [Header("Attacking")]
    public int comboStep;
    public float interval;
    public float timer;
    public bool isAttack;
    public string attackType;

    [Header("Shooting")]
    public bool isShoot;
    public float calculator;
    public Transform muzzle;
    public GameObject arrowPrefab;
    public int arriwEnergyCost;


    /// <summary>����ʱ��ͷ�ζ�ʱ��</summary>
    [Header("HitEffect")]
    [SerializeField] private float shakeTime;
    /// <summary>�ṥ��ʱͣ��֡��</summary>
    [SerializeField] private int lightAttackPause;
    /// <summary>�ṥ��ʱ�ζ�ǿ��</summary>
    [SerializeField] private float lightAttackStrength;
    /// <summary>�ع���ʱͣ��֡��</summary>
    [SerializeField] private int heavyAttackPause;
    /// <summary>�ع���ʱ�ζ�ǿ��</summary>
    [SerializeField] private float heavyAttackStrength;


    /// <summary>�ṥ���˺�</summary>
    [Header("Damage")]
    [SerializeField] private int[] lightDamage;
    /// <summary>�ع����˺�</summary>
    [SerializeField] private int[] heavyDamage;


    /// <summary>��ǰ����ֵ</summary>
    [Header("Basic Attribute")]
    [SerializeField] private int health;
    /// <summary>�������ֵ</summary>
    [SerializeField] private int maxHealth;
    /// <summary>��ǰ����ֵ</summary>
    [SerializeField] private int energy;
    /// <summary>�����ֵ</summary>
    [SerializeField] private int maxEnergy;
    /// <summary>
    /// �ж�����Ƿ����ܻ�״̬�Ĳ���ֵ
    /// </summary>
    bool isHit;
    bool isDeath;

    /// <summary>�ж��Ƿ�������״̬�Ĳ���ֵ</summary>
    [Header("Dodge")]
    bool isDodge;
    /// <summary>һ�����ܿ۳��ķ���ֵ</summary>
    [SerializeField] private int dodgeEnergyCost;


    /// <summary>�ж��Ƿ��ڳ�����״̬�Ĳ���ֵ</summary>
    [Header("OverLoad")]
    bool isOverLoad;
    /// <summary>������״̬��ɫ���õ�HDR�������</summary>
    [SerializeField]private Material overLoadMaterial;
    /// <summary>��ͨ��ɫ���õ�HDR�������</summary>
    [SerializeField] private Material originMaterial;
    /// <summary>������״̬��ɫ���õ�׷��ħ����</summary>
    [SerializeField] private GameObject overLoadArrowPrefab;

    /// <summary>�ṥ������Ч</summary>
    [Header("AudioController")]
    [SerializeField] private AudioClip[] lightAttackSFX;
    /// <summary>�ع�������Ч</summary>
    [SerializeField] private AudioClip[] heavyAttackSFX;
    /// <summary>������</summary>
    [SerializeField] private AudioClip shootSFX;
    /// <summary>�Ų���</summary>
    [SerializeField] private AudioClip footStep;
    /// <summary>�����</summary>
    [SerializeField] private AudioClip fallOnGround;
    /// <summary>������</summary>
    [SerializeField] private AudioClip dodgeSFX;
    /// <summary>���ͷ���Ƿ�����ڱ�������߳���</summary>
    [SerializeField] private float maskLength;
    /// <summary>��������Ƿ���ڵ��˵����߳���</summary>
    [SerializeField] private float travelRadius;
    /// <summary>�������ڵĲ㼶</summary>
    [SerializeField] private LayerMask enemyLayer;
    /// <summary>�ڱ������ڵĲ㼶</summary>
    [SerializeField] private LayerMask maskLayer;


    /// <summary>
    /// ��ʼ��������Ѫ�������÷���ÿ��ָ�����
    /// </summary>
    void Start()
    {
        healthBar.Initialize(health, maxHealth);
        energyBar.Initialize(energy, maxEnergy);
        StartCoroutine(recoverEnergy());
    }

    // ͻ���¼��ļ�����
    void Update()
    {
        /* ��������� */
        Vector3 leftCollsion = transform.position - new Vector3(rayHorizontalLength, 0, 0);
        /* ���ҵ����� */
        Vector3 rightCollsion = transform.position + new Vector3(rayHorizontalLength, 0, 0);
        /* �жϽ�ɫ�Ƿ��ڵ��棬ˢ����Ծ�������������䶯�� */
        onGround = Physics2D.Raycast(transform.position, Vector2.down, rayVerticalLength, groundLayer) || Physics2D.Raycast(transform.position, Vector2.down, rayVerticalLength, stairLayer);
        
        /* �жϽ�ɫ�Ƿ�Ӵ���ǽ�壬Լ����ɫ�ڿ��е��˶� */
        onWall = Physics2D.Raycast(leftCollsion, Vector2.down, rayVerticalLength-0.1f, groundLayer) || Physics2D.Raycast(rightCollsion, Vector2.down, rayVerticalLength-0.1f, groundLayer);
        /* �����ɫ�ƶ����� */
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (!isDodge)// ����Ҵ�������״̬ʱ������ִ����������
        {
            if (onGround && animator.GetBool("Fall"))// ����ɫ���ʱ���������䶯�����ָ�վ��״̬
            {
                animator.Play("PlayerStandUp");
            }
            if (Input.GetKeyDown(KeyCode.W) && canJump)// ����W(��Ծ)����������Ҵ�����Ծ����ʱ�����ִ����Ծ�� ��������ϸ��ʵ��
            {
                Jump();
            }
            if(Input.GetKeyDown(KeyCode.I) && costEnergy(dodgeEnergyCost))// ���ķ���ֵ��������
            {
                body.drag = 0;
                StartCoroutine(dodgeCoroutine());
            }
            if(Input.GetKeyDown(KeyCode.O)&& energy==maxEnergy)// ���ķ���ֵ���볬����״̬
            {
                isOverLoad = true;// �л�״̬
                overLoadEffect.SetTrigger("OverLoad");// ���������ɶ���������
                gameObject.GetComponent<SpriteRenderer>().material = overLoadMaterial;// ������Ҳ���Ϊ������HDR�������
            }
            resetJump();// ��Ծˢ��
        }
        /* ��ɫ����Ծʱ���ʵ������Ϸ���ƽ̨����ײ�壬�ܹ���͸���� */
        ignoreStair();
        adaptiveMusicController();
    }
    /// <summary>
    /// ����ɫ�ص����棬��Ծ����ˢ��
    /// </summary>
    void resetJump()
    {
        if (onGround)
        {
            if (!animator.GetBool("StandUp"))// ���������Ч
                AudioManager.instance.PlayRandomSFX(fallOnGround);
            animator.SetBool("StandUp", true);
            canJump = true;
        }
        else
            animator.SetBool("StandUp", false);
    }
    // �����¼��ļ�����
    void FixedUpdate()
    {
        if (!isDodge)// ����Ҵ�������״̬ʱ������ִ����������
        {
            if (!isShoot && !isAttack)// �����û�й���ʱ�������ƶ�
            {
                Movement(direction.x);
            }
            startAttack();// ��⹥��
            startShoot();// ������
            modifyPhysics();// ����ģ��
        }
    }
    /// <summary>
    /// <para>��ǿ��ɫ���������ʵ��</para>
    /// <para>����ɫ�����ƶ�ʱ��ͨ����������ʵ�ּӼ���</para>
    /// <para>����ɫ��Ծʱ��ͨ����������������ģ���������䣬ʵ�ִ���(����)��С��(�̰�)</para>
    /// </summary>
    void modifyPhysics()
    {
        if (onGround)// ����ɫ�ڵ����ƶ�
        {
            if (direction.x != 0 && Mathf.Sign(direction.x) != -Mathf.Sign(transform.localScale.x))// ��ɫͻȻ�л��ƶ�����
            {
                transform.localScale = new Vector3(-1 * direction.x, 1, 1);// ��ɫת��
                body.drag = linearDrag;// ����������ʵ�ֻ���ת��(ԭ�������)
            }
            else if (direction.x == 0 && body.velocity.x != 0)// ���̲�������
            {
                body.drag = linearDrag;// ��������,��ɫ����ֱ��ֹͣ
            }
            else if(!isAttack)// ��ɫ�����ƶ�
            {
                body.drag = 0;// ȡ����������ɫ�𽥼���������ٶ�(�ƶ�ʹ�õ���addForce)
            }
            body.gravityScale = 0;
        }
        else// ����ɫ����Ծ
        {
            body.gravityScale = gravity;// ��������
            body.drag = linearDrag * 0.15f;// ģ���������
            if (direction.x != 0 && Mathf.Sign(direction.x) != -Mathf.Sign(transform.localScale.x))
            {
                transform.localScale = new Vector3(-1 * direction.x, 1, 1);// ʱ���л���ɫ����
            }
            if (body.velocity.y < 0)
            {
                body.gravityScale *= gravity * fallMultiplier;// ����ɫ����ʱ,����������ʹ�������
            }
            else if (body.velocity.y > 0 && !Input.GetKey(KeyCode.W))// ����ɫ����ʱ�����ɿ���Ծ������������
            {
                body.gravityScale = gravity * fallMultiplier / 2;// ͨ��������Ծ��ʱ��ĳ��̿�����Ծ�߶ȣ�ʵ�ִ�����С��
            }
        }
    }
    /// <summary>
    /// ͨ��������ƽ�ɫ�����ƶ�
    /// </summary>
    /// <param name="horizontal">��ɫ�ƶ�����: -1Ϊ��1Ϊ��</param>
    void Movement(float horizontal)
    {
        if (!isAttack)
        {
            if (!onWall || onGround)// ����Ҳ����ڿ��л��߲��Ӵ���ǽ��ʱ���������ƶ������������
            {
                body.AddForce(Vector2.right * horizontal * moveForce, ForceMode2D.Impulse);
                if (Mathf.Abs(body.velocity.x) > maxSpeed)// ���ٵ�����ٶȺ��ٶȲ�������
                    body.velocity = new Vector2(horizontal * maxSpeed, body.velocity.y);
            }
            animator.SetFloat("horizontal", Mathf.Abs(horizontal));// ���ݲ����������������������ݸ���ֵ�����ƶ��;�ֹ����
        }
    }
    /// <summary>
    /// ��⹥������ļ����ж�
    /// </summary>
    void startAttack()
    {
        if(Input.GetKey(KeyCode.J) && !isAttack)// ����ɫδ���ڹ���״̬�Ұ���J������ɫ�ṥ��
        {
            body.velocity = Vector2.zero;
            AudioManager.instance.PlayRandomSFX(lightAttackSFX, 20.0f);// �����ṥ����Ч
            isAttack = true;// ����״̬
            comboStep++;// ����������
            if (comboStep > 3)
                comboStep = 1;// �ṥ��ֻ����ʽ
            timer = interval;// ˢ�¼�ʱ��
            attackType = "LightAttack";// ���¹�������
            animator.SetTrigger("LightAttack");// ���������Ŷ�Ӧ�ṥ������
            animator.SetInteger("ComboStep", comboStep);
        }
        else if(Input.GetKey(KeyCode.K) && !isAttack)// ����ɫδ���ڹ���״̬�Ұ���K������ɫ�ع���
        {
            body.velocity = Vector2.zero;
            AudioManager.instance.PlayRandomSFX(heavyAttackSFX, 20.0f);// �����ع�����Ч
            isAttack = true;
            comboStep++;
            if (comboStep > 3)
                comboStep = 1;
            timer = interval;
            attackType = "HeavyAttack";
            animator.SetTrigger("HeavyAttack");
            animator.SetInteger("ComboStep", comboStep);
        }
        if(timer!=0)// ����ɫ�������쳣״̬���ʱ������״̬����δ��ȡ���������timer��ʱ��
        {
            timer -= Time.deltaTime;
            if(timer<0)// ����ʱ��Ϊ��ʱ�����ù���״̬
            {
                timer = 0;
                comboStep = 0;
                isAttack = false;
                attackType = "";
            }
        }
    }
    /// <summary>
    /// ʵ���������ļ����ж�
    /// </summary>
    public void startShoot()
    {
        if (Input.GetKey(KeyCode.U) && !isShoot && costEnergy(arriwEnergyCost))// ����ɫδ�������״̬�Ұ���U������ɫ���
        {
            AudioManager.instance.PlayRandomSFX(shootSFX);
            isShoot = true;
            calculator = interval;
            animator.SetTrigger("Shoot");
        }
        
        if (calculator != 0)// ������쳣״̬�ָ���ʱ��
        {
            calculator -= Time.deltaTime;
            if (calculator < 0)
            {
                calculator = 0;
                isShoot = false;
            }
        }
    }
    /// <summary>
    /// ������һ��̬���Ʊ������Ĵ�С����Ŀ
    /// </summary>
    public void adaptiveMusicController()
    {
        /* ����ⷶΧ��û�е���ʱ�������������л�������ģʽ���� */
        if (!Physics2D.OverlapCircle(transform.position, travelRadius, enemyLayer))
            AudioManager.instance.CrossFadeToTravelMode();
        /* ��ͷ�����ڱ���ʱ����������С */
        if (Physics2D.Raycast(transform.position, Vector2.up, maskLength, maskLayer))
            AudioManager.instance.TurnDownAmbientMusic();
        else
            AudioManager.instance.TurnUpAmbientMusic();

    }
    /// <summary>
    /// ִ�����ܹ��ܵ�Э��
    /// </summary>
    /// <returns></returns>
    IEnumerator dodgeCoroutine()
    {
        isDodge = true;// ��������״̬
        animator.SetTrigger("Dodge");// �������������ܶ���
        dodgeAnimator.SetTrigger("Dodge");// �Ӷ���������������Ч
        AudioManager.instance.PlayRandomSFX(dodgeSFX, 0.4f);
        Vector2 Direction = transform.localScale.x > 0 ? Vector2.left : Vector2.right;// ���ݽ�ɫ������������ƶ�����
        while(isDodge)
        {
            body.AddForce(Direction, ForceMode2D.Impulse);// �����б����ƶ�
            yield return new WaitForSeconds(0.05f);
        }
        canJump = true;// ���ܺ�ˢ����Ծ����
    }
    /// <summary>
    /// ִ�з���ֵ�ָ������ĵ�Э��
    /// </summary>
    /// <returns></returns>
    IEnumerator recoverEnergy()
    {
        while (true)
        {
            if (!isOverLoad)// ����ɫ����ÿ����ָ�һ�㷨��ֵ
            {
                if (energy < maxEnergy)
                    energy++;
                energyBar.UpdateStatus(energy, maxEnergy);
                yield return new WaitForSeconds(2.0f);
            }
            else// ����ɫ����������״̬ʱ��ÿ��۳�10�㷨��ֵ��������ֵΪ0�󳬸���״̬ȡ��
            {
                energy--;
                energyBar.UpdateStatus(energy, maxEnergy);
                if (energy == 0 && isOverLoad)
                {
                    gameObject.GetComponent<SpriteRenderer>().material = originMaterial;// �ָ�����״̬�Ľ�ɫ����
                    isOverLoad = false;// ���³�����״̬
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    /// <summary>
    /// ���ܽ���ʱ�Ķ���֡�¼�
    /// </summary>
    void overDodge()
    {
        isDodge = false;// ��������״̬
    }
    /// <summary>
    /// ��������ʱ�Ķ���֡�¼�
    /// </summary>
    void overAttack()
    {
        isAttack = false;// ���¹���״̬
    }
    /// <summary>
    /// �������ʱ�Ķ���֡�¼�
    /// </summary>
    void overShoot()
    {
        isShoot = false;// �������״̬
    }
    /// <summary>
    /// �ƶ�ʱ���ŽŲ����Ķ���֡�¼�
    /// </summary>
    void playFootStep()
    {
        AudioManager.instance.PlayRandomSFX(footStep, 0.3f);
    }
    /// <summary>
    /// �������ʱ�Ķ���֡�¼������ħ����
    /// </summary>
    void generateArrow()
    {
        if (!isOverLoad)// ����״̬ʱ����׼��λ������һ֧ħ����
        {
            PoolManager.Release(arrowPrefab, muzzle.position);
        }
        else// ������״̬ʱ����׼��λ��������ֻ׷��ħ����
        {
            for(int i=0;i<5;i++)
            {
                PoolManager.Release(overLoadArrowPrefab, muzzle.position);
            }
        }
    }
    /// <summary>
    /// <para>ʵ�ֽ�ɫ����Ծ����</para>
    /// <para>�ú���ʵ���˶�����</para>
    /// <para>���ڽ�ɫ��һ�ο�ʼ��Ծʱδ�뿪���棬����ֵonGroundΪtrue����canJump�ᱻˢ�£���ɫ�����ڿ�������һ��</para>
    /// </summary>
    void Jump()
    {
        canJump = false;// ������Ծ����
        body.velocity = new Vector2(body.velocity.x, 0);// ������ֱ������ٶ�
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);// ���ڽ�ɫ���ϵ���
        animator.SetTrigger("Jump");
    }
    /// <summary>
    /// �жϵ�ǰ����ֵ�Ƿ��ܹ��������ģ������򷵻��棬�����򷵻ؼ�
    /// </summary>
    /// <param name="cost">��������</param>
    /// <returns></returns>
    bool costEnergy(int cost)
    {
        if (isOverLoad)// ����ɫ���ڳ�����״̬�����ü���������ʹ��
            return true;
        if (cost > energy)
            return false;
        energy -= cost;
        return true;
    }
    /// <summary>
    /// �Խ�ɫ�����Ķ�������˺�
    /// </summary>
    /// <param name="collision">�����Ӧ����ײ��</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))// ����ײ��ı�ǩΪEnemy
        {
            if (attackType=="LightAttack")// ����������Ϊ�ṥ��
            {
                //effectAnimator.SetTrigger("LightAttack");
                AttackScene.Instance.hitPause(lightAttackPause);// �ṥ����֡
                AttackScene.Instance.hitShake(shakeTime, lightAttackStrength);// �ṥ����ͷ�ζ�
            }
            else if (attackType=="HeavyAttack")// ����������Ϊ�ع���
            {
                //effectAnimator.SetTrigger("HeavyAttack");
                AttackScene.Instance.hitPause(heavyAttackPause);// �ع�����֡
                AttackScene.Instance.hitShake(shakeTime, heavyAttackStrength);// �ع�����ͷ�ζ�
            }
            Vector2 Direction = transform.localScale.x > 0 ? Vector2.left : Vector2.right;// ��ȡ��ɫ����
            int currentDamage = 0;
            if (attackType == "LightAttack")// ��ȡ��Ӧ�������˺�
                currentDamage = lightDamage[comboStep - 1];
            else if (attackType == "HeavyAttack")
                currentDamage = heavyDamage[comboStep - 1];
            if(isOverLoad)
                Recovery(currentDamage, 0);
            if (collision.gameObject.TryGetComponent<BringOfDeath>(out BringOfDeath boss))
            {
                boss.getDamage(currentDamage);
            }
            if(collision.gameObject.TryGetComponent<MeleeMonster>(out MeleeMonster melee))// MeleeMonster�����й�����Ļ���
            {
                melee.getDamage(currentDamage);// ��������˺�
                melee.fendOff(Direction);// ���ɫ������˹���
            }
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))// enemy����Ϸ�̵̳�ɳ����
            {
                enemy.getDamage(currentDamage);// ��������˺�
                enemy.fendOff(Direction);// ���ɫ������˹���
            }
        }
    }
    /// <summary>
    /// ��ɫ�ܵ�����Ĺ������۳�����ֵ
    /// </summary>
    /// <param name="damage">�ܵ����˺�</param>
    public void getDamage(int damage)
    {
        if (!isDeath)
        {
            if (!isDodge)
            {
                health -= damage;
                if(health <= 0)
                {
                    health = 0;
                    isDeath = true;
                }
                healthBar.UpdateStatus(health, maxHealth);// ����Ѫ��UI
                if (!isHit)
                {
                    animator.SetTrigger("isHit");
                }
            }
            else// ����ɫ��������״̬ʱ�ܻ��������κ��˺����������ӵ�ʱ��
            {
                TimeController.instance.enableBulletTime();// �����ӵ�ʱ��
            }
        }
    }
    /// <summary>
    /// ��ɫ��ƽ̨��Ծʱ������ͷ����ƽ̨��ײ�壬ʵ�ִ�͸ƽ̨�Ĺ���
    /// </summary>
    public void ignoreStair()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, ignoreRadius, stairLayer);// ��ȡһ����Χ�ڵ�����ƽ̨��ײ��
        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].transform.position.y > transform.position.y - 1)// ���Ӹ��ڽ�ɫ��ƽ̨
                collider[i].isTrigger = true;
            else
                collider[i].isTrigger = false;
        }
    }
    /// <summary>
    /// �����ܻ�״̬���ܻ��Ķ���֡�¼�
    /// </summary>
    void endHit()
    {
        isHit = false;
    }

    public void Recovery(int healthValue, int energyValue)
    {
        health += healthValue;
        energy += energyValue;
        if (health > maxHealth)
            health = maxHealth;
        if (energy > maxEnergy)
            energy = maxEnergy;
        healthBar.UpdateStatus(health, maxHealth);
        energyBar.UpdateStatus(energy, maxEnergy);
    }

}

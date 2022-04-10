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


    /// <summary>攻击时镜头晃动时间</summary>
    [Header("HitEffect")]
    [SerializeField] private float shakeTime;
    /// <summary>轻攻击时停顿帧数</summary>
    [SerializeField] private int lightAttackPause;
    /// <summary>轻攻击时晃动强度</summary>
    [SerializeField] private float lightAttackStrength;
    /// <summary>重攻击时停顿帧数</summary>
    [SerializeField] private int heavyAttackPause;
    /// <summary>重攻击时晃动强度</summary>
    [SerializeField] private float heavyAttackStrength;


    /// <summary>轻攻击伤害</summary>
    [Header("Damage")]
    [SerializeField] private int[] lightDamage;
    /// <summary>重攻击伤害</summary>
    [SerializeField] private int[] heavyDamage;


    /// <summary>当前生命值</summary>
    [Header("Basic Attribute")]
    [SerializeField] private int health;
    /// <summary>最大生命值</summary>
    [SerializeField] private int maxHealth;
    /// <summary>当前法力值</summary>
    [SerializeField] private int energy;
    /// <summary>最大法力值</summary>
    [SerializeField] private int maxEnergy;
    /// <summary>
    /// 判断玩家是否处于受击状态的布尔值
    /// </summary>
    bool isHit;
    bool isDeath;

    /// <summary>判断是否处于闪避状态的布尔值</summary>
    [Header("Dodge")]
    bool isDodge;
    /// <summary>一次闪避扣除的法力值</summary>
    [SerializeField] private int dodgeEnergyCost;


    /// <summary>判断是否处于超负荷状态的布尔值</summary>
    [Header("OverLoad")]
    bool isOverLoad;
    /// <summary>超负荷状态角色所用的HDR发光材质</summary>
    [SerializeField]private Material overLoadMaterial;
    /// <summary>普通角色所用的HDR发光材质</summary>
    [SerializeField] private Material originMaterial;
    /// <summary>超负荷状态角色所用的追踪魔法箭</summary>
    [SerializeField] private GameObject overLoadArrowPrefab;

    /// <summary>轻攻击的音效</summary>
    [Header("AudioController")]
    [SerializeField] private AudioClip[] lightAttackSFX;
    /// <summary>重攻击的音效</summary>
    [SerializeField] private AudioClip[] heavyAttackSFX;
    /// <summary>拉弓声</summary>
    [SerializeField] private AudioClip shootSFX;
    /// <summary>脚步声</summary>
    [SerializeField] private AudioClip footStep;
    /// <summary>落地声</summary>
    [SerializeField] private AudioClip fallOnGround;
    /// <summary>闪避声</summary>
    [SerializeField] private AudioClip dodgeSFX;
    /// <summary>检测头顶是否存在遮蔽物的射线长度</summary>
    [SerializeField] private float maskLength;
    /// <summary>检测四周是否存在敌人的射线长度</summary>
    [SerializeField] private float travelRadius;
    /// <summary>敌人所在的层级</summary>
    [SerializeField] private LayerMask enemyLayer;
    /// <summary>遮蔽物所在的层级</summary>
    [SerializeField] private LayerMask maskLayer;


    /// <summary>
    /// 初始化蓝条和血条，启用法力每秒恢复功能
    /// </summary>
    void Start()
    {
        healthBar.Initialize(health, maxHealth);
        energyBar.Initialize(energy, maxEnergy);
        StartCoroutine(recoverEnergy());
    }

    // 突发事件的检测更新
    void Update()
    {
        /* 向左的射线 */
        Vector3 leftCollsion = transform.position - new Vector3(rayHorizontalLength, 0, 0);
        /* 向右的射线 */
        Vector3 rightCollsion = transform.position + new Vector3(rayHorizontalLength, 0, 0);
        /* 判断角色是否处于地面，刷新跳跃次数，结束下落动画 */
        onGround = Physics2D.Raycast(transform.position, Vector2.down, rayVerticalLength, groundLayer) || Physics2D.Raycast(transform.position, Vector2.down, rayVerticalLength, stairLayer);
        
        /* 判断角色是否接触到墙体，约束角色在空中的运动 */
        onWall = Physics2D.Raycast(leftCollsion, Vector2.down, rayVerticalLength-0.1f, groundLayer) || Physics2D.Raycast(rightCollsion, Vector2.down, rayVerticalLength-0.1f, groundLayer);
        /* 输入角色移动方向 */
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (!isDodge)// 当玩家处于闪避状态时，不能执行其他操作
        {
            if (onGround && animator.GetBool("Fall"))// 当角色落地时，结束下落动画，恢复站立状态
            {
                animator.Play("PlayerStandUp");
            }
            if (Input.GetKeyDown(KeyCode.W) && canJump)// 按下W(跳跃)键，并且玩家存在跳跃次数时，玩家执行跳跃， 二段跳的细节实现
            {
                Jump();
            }
            if(Input.GetKeyDown(KeyCode.I) && costEnergy(dodgeEnergyCost))// 消耗法力值进行闪避
            {
                body.drag = 0;
                StartCoroutine(dodgeCoroutine());
            }
            if(Input.GetKeyDown(KeyCode.O)&& energy==maxEnergy)// 消耗法力值进入超负荷状态
            {
                isOverLoad = true;// 切换状态
                overLoadEffect.SetTrigger("OverLoad");// 启动超负荷动画触发器
                gameObject.GetComponent<SpriteRenderer>().material = overLoadMaterial;// 更新玩家材质为超负荷HDR发光材质
            }
            resetJump();// 跳跃刷新
        }
        /* 角色在跳跃时，适当无视上方的平台的碰撞体，能够穿透阶梯 */
        ignoreStair();
        adaptiveMusicController();
    }
    /// <summary>
    /// 若角色回到地面，跳跃次数刷新
    /// </summary>
    void resetJump()
    {
        if (onGround)
        {
            if (!animator.GetBool("StandUp"))// 播放落地音效
                AudioManager.instance.PlayRandomSFX(fallOnGround);
            animator.SetBool("StandUp", true);
            canJump = true;
        }
        else
            animator.SetBool("StandUp", false);
    }
    // 常规事件的检测更新
    void FixedUpdate()
    {
        if (!isDodge)// 当玩家处于闪避状态时，不能执行其他操作
        {
            if (!isShoot && !isAttack)// 当玩家没有攻击时，正常移动
            {
                Movement(direction.x);
            }
            startAttack();// 检测攻击
            startShoot();// 检测射箭
            modifyPhysics();// 物理模拟
        }
    }
    /// <summary>
    /// <para>增强角色活动的物理真实性</para>
    /// <para>当角色左右移动时，通过更改阻力实现加减速</para>
    /// <para>当角色跳跃时，通过更改阻力和重力模拟自由下落，实现大跳(长按)和小跳(短按)</para>
    /// </summary>
    void modifyPhysics()
    {
        if (onGround)// 当角色在地面移动
        {
            if (direction.x != 0 && Mathf.Sign(direction.x) != -Mathf.Sign(transform.localScale.x))// 角色突然切换移动方向
            {
                transform.localScale = new Vector3(-1 * direction.x, 1, 1);// 角色转向
                body.drag = linearDrag;// 增加阻力，实现缓慢转身(原方向减速)
            }
            else if (direction.x == 0 && body.velocity.x != 0)// 键盘不再输入
            {
                body.drag = linearDrag;// 增加阻力,角色减速直至停止
            }
            else if(!isAttack)// 角色持续移动
            {
                body.drag = 0;// 取消阻力，角色逐渐加速至最大速度(移动使用的是addForce)
            }
            body.gravityScale = 0;
        }
        else// 当角色在跳跃
        {
            body.gravityScale = gravity;// 设置重力
            body.drag = linearDrag * 0.15f;// 模拟空气阻力
            if (direction.x != 0 && Mathf.Sign(direction.x) != -Mathf.Sign(transform.localScale.x))
            {
                transform.localScale = new Vector3(-1 * direction.x, 1, 1);// 时刻切换角色朝向
            }
            if (body.velocity.y < 0)
            {
                body.gravityScale *= gravity * fallMultiplier;// 当角色下落时,增大重力，使下落加速
            }
            else if (body.velocity.y > 0 && !Input.GetKey(KeyCode.W))// 当角色上升时，且松开跳跃键，增大重力
            {
                body.gravityScale = gravity * fallMultiplier / 2;// 通过按下跳跃键时间的长短控制跳跃高度，实现大跳和小跳
            }
        }
    }
    /// <summary>
    /// 通过输入控制角色左右移动
    /// </summary>
    /// <param name="horizontal">角色移动方向: -1为左，1为右</param>
    void Movement(float horizontal)
    {
        if (!isAttack)
        {
            if (!onWall || onGround)// 当玩家不处于空中或者不接触到墙体时，持续向移动方向添加受力
            {
                body.AddForce(Vector2.right * horizontal * moveForce, ForceMode2D.Impulse);
                if (Mathf.Abs(body.velocity.x) > maxSpeed)// 加速到最大速度后，速度不再增加
                    body.velocity = new Vector2(horizontal * maxSpeed, body.velocity.y);
            }
            animator.SetFloat("horizontal", Mathf.Abs(horizontal));// 传递参数给动画机，动画机根据浮点值播放移动和静止动画
        }
    }
    /// <summary>
    /// 检测攻击输入的检测和判定
    /// </summary>
    void startAttack()
    {
        if(Input.GetKey(KeyCode.J) && !isAttack)// 若角色未处于攻击状态且按下J键，角色轻攻击
        {
            body.velocity = Vector2.zero;
            AudioManager.instance.PlayRandomSFX(lightAttackSFX, 20.0f);// 播放轻攻击音效
            isAttack = true;// 更新状态
            comboStep++;// 增加连击数
            if (comboStep > 3)
                comboStep = 1;// 轻攻击只有三式
            timer = interval;// 刷新计时器
            attackType = "LightAttack";// 更新攻击类型
            animator.SetTrigger("LightAttack");// 动画机播放对应轻攻击动画
            animator.SetInteger("ComboStep", comboStep);
        }
        else if(Input.GetKey(KeyCode.K) && !isAttack)// 若角色未处于攻击状态且按下K键，角色重攻击
        {
            body.velocity = Vector2.zero;
            AudioManager.instance.PlayRandomSFX(heavyAttackSFX, 20.0f);// 播放重攻击音效
            isAttack = true;
            comboStep++;
            if (comboStep > 3)
                comboStep = 1;
            timer = interval;
            attackType = "HeavyAttack";
            animator.SetTrigger("HeavyAttack");
            animator.SetInteger("ComboStep", comboStep);
        }
        if(timer!=0)// 当角色攻击被异常状态打断时，攻击状态可能未被取消，故添加timer计时器
        {
            timer -= Time.deltaTime;
            if(timer<0)// 当计时器为负时，重置攻击状态
            {
                timer = 0;
                comboStep = 0;
                isAttack = false;
                attackType = "";
            }
        }
    }
    /// <summary>
    /// 实现射箭输入的检测和判定
    /// </summary>
    public void startShoot()
    {
        if (Input.GetKey(KeyCode.U) && !isShoot && costEnergy(arriwEnergyCost))// 若角色未处于射箭状态且按下U键，角色射箭
        {
            AudioManager.instance.PlayRandomSFX(shootSFX);
            isShoot = true;
            calculator = interval;
            animator.SetTrigger("Shoot");
        }
        
        if (calculator != 0)// 射箭的异常状态恢复计时器
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
    /// 根据玩家活动动态控制背景音的大小和曲目
    /// </summary>
    public void adaptiveMusicController()
    {
        /* 当检测范围内没有敌人时，将背景音乐切换至旅行模式音乐 */
        if (!Physics2D.OverlapCircle(transform.position, travelRadius, enemyLayer))
            AudioManager.instance.CrossFadeToTravelMode();
        /* 当头顶有遮蔽物时，将雨声调小 */
        if (Physics2D.Raycast(transform.position, Vector2.up, maskLength, maskLayer))
            AudioManager.instance.TurnDownAmbientMusic();
        else
            AudioManager.instance.TurnUpAmbientMusic();

    }
    /// <summary>
    /// 执行闪避功能的协程
    /// </summary>
    /// <returns></returns>
    IEnumerator dodgeCoroutine()
    {
        isDodge = true;// 更新闪避状态
        animator.SetTrigger("Dodge");// 动画机播放闪避动画
        dodgeAnimator.SetTrigger("Dodge");// 子动画机播放闪避特效
        AudioManager.instance.PlayRandomSFX(dodgeSFX, 0.4f);
        Vector2 Direction = transform.localScale.x > 0 ? Vector2.left : Vector2.right;// 根据角色朝向决定闪避移动方向
        while(isDodge)
        {
            body.AddForce(Direction, ForceMode2D.Impulse);// 闪避中保持移动
            yield return new WaitForSeconds(0.05f);
        }
        canJump = true;// 闪避后刷新跳跃次数
    }
    /// <summary>
    /// 执行法力值恢复和消耗的协程
    /// </summary>
    /// <returns></returns>
    IEnumerator recoverEnergy()
    {
        while (true)
        {
            if (!isOverLoad)// 当角色正常每两秒恢复一点法力值
            {
                if (energy < maxEnergy)
                    energy++;
                energyBar.UpdateStatus(energy, maxEnergy);
                yield return new WaitForSeconds(2.0f);
            }
            else// 当角色激发超负荷状态时，每秒扣除10点法力值，当法力值为0后超负荷状态取消
            {
                energy--;
                energyBar.UpdateStatus(energy, maxEnergy);
                if (energy == 0 && isOverLoad)
                {
                    gameObject.GetComponent<SpriteRenderer>().material = originMaterial;// 恢复正常状态的角色材质
                    isOverLoad = false;// 更新超负荷状态
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    /// <summary>
    /// 闪避结束时的动画帧事件
    /// </summary>
    void overDodge()
    {
        isDodge = false;// 更新闪避状态
    }
    /// <summary>
    /// 攻击结束时的动画帧事件
    /// </summary>
    void overAttack()
    {
        isAttack = false;// 更新攻击状态
    }
    /// <summary>
    /// 射箭结束时的动画帧事件
    /// </summary>
    void overShoot()
    {
        isShoot = false;// 更新射箭状态
    }
    /// <summary>
    /// 移动时播放脚步声的动画帧事件
    /// </summary>
    void playFootStep()
    {
        AudioManager.instance.PlayRandomSFX(footStep, 0.3f);
    }
    /// <summary>
    /// 射箭动画时的动画帧事件，射出魔法箭
    /// </summary>
    void generateArrow()
    {
        if (!isOverLoad)// 正常状态时，在准星位置生成一支魔法箭
        {
            PoolManager.Release(arrowPrefab, muzzle.position);
        }
        else// 超负荷状态时，在准星位置生成五只追踪魔法箭
        {
            for(int i=0;i<5;i++)
            {
                PoolManager.Release(overLoadArrowPrefab, muzzle.position);
            }
        }
    }
    /// <summary>
    /// <para>实现角色的跳跃功能</para>
    /// <para>该函数实现了二段跳</para>
    /// <para>由于角色第一次开始跳跃时未离开地面，布尔值onGround为true，故canJump会被刷新，角色可以在空中再跳一次</para>
    /// </summary>
    void Jump()
    {
        canJump = false;// 更新跳跃次数
        body.velocity = new Vector2(body.velocity.x, 0);// 重置竖直方向的速度
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);// 基于角色向上的力
        animator.SetTrigger("Jump");
    }
    /// <summary>
    /// 判断当前法力值是否能够满足消耗，若能则返回真，不能则返回假
    /// </summary>
    /// <param name="cost">本次消耗</param>
    /// <returns></returns>
    bool costEnergy(int cost)
    {
        if (isOverLoad)// 当角色处于超负荷状态，所用技能无限制使用
            return true;
        if (cost > energy)
            return false;
        energy -= cost;
        return true;
    }
    /// <summary>
    /// 对角色攻击的对象，造成伤害
    /// </summary>
    /// <param name="collision">怪物对应的碰撞体</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))// 若碰撞体的标签为Enemy
        {
            if (attackType=="LightAttack")// 若攻击类型为轻攻击
            {
                //effectAnimator.SetTrigger("LightAttack");
                AttackScene.Instance.hitPause(lightAttackPause);// 轻攻击顿帧
                AttackScene.Instance.hitShake(shakeTime, lightAttackStrength);// 轻攻击镜头晃动
            }
            else if (attackType=="HeavyAttack")// 若攻击类型为重攻击
            {
                //effectAnimator.SetTrigger("HeavyAttack");
                AttackScene.Instance.hitPause(heavyAttackPause);// 重攻击顿帧
                AttackScene.Instance.hitShake(shakeTime, heavyAttackStrength);// 重攻击镜头晃动
            }
            Vector2 Direction = transform.localScale.x > 0 ? Vector2.left : Vector2.right;// 获取角色方向
            int currentDamage = 0;
            if (attackType == "LightAttack")// 获取对应攻击的伤害
                currentDamage = lightDamage[comboStep - 1];
            else if (attackType == "HeavyAttack")
                currentDamage = heavyDamage[comboStep - 1];
            if(isOverLoad)
                Recovery(currentDamage, 0);
            if (collision.gameObject.TryGetComponent<BringOfDeath>(out BringOfDeath boss))
            {
                boss.getDamage(currentDamage);
            }
            if(collision.gameObject.TryGetComponent<MeleeMonster>(out MeleeMonster melee))// MeleeMonster是所有怪物类的基类
            {
                melee.getDamage(currentDamage);// 给予怪物伤害
                melee.fendOff(Direction);// 向角色朝向击退怪物
            }
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))// enemy是游戏教程的沙包类
            {
                enemy.getDamage(currentDamage);// 给予怪物伤害
                enemy.fendOff(Direction);// 向角色朝向击退怪物
            }
        }
    }
    /// <summary>
    /// 角色受到怪物的攻击，扣除生命值
    /// </summary>
    /// <param name="damage">受到的伤害</param>
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
                healthBar.UpdateStatus(health, maxHealth);// 更新血条UI
                if (!isHit)
                {
                    animator.SetTrigger("isHit");
                }
            }
            else// 当角色处于闪避状态时受击，不受任何伤害，并进入子弹时间
            {
                TimeController.instance.enableBulletTime();// 启动子弹时间
            }
        }
    }
    /// <summary>
    /// 角色在平台跳跃时，无视头顶的平台碰撞体，实现穿透平台的功能
    /// </summary>
    public void ignoreStair()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, ignoreRadius, stairLayer);// 获取一定范围内的所有平台碰撞体
        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].transform.position.y > transform.position.y - 1)// 无视高于角色的平台
                collider[i].isTrigger = true;
            else
                collider[i].isTrigger = false;
        }
    }
    /// <summary>
    /// 结束受击状态，受击的动画帧事件
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

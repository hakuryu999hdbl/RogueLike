using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{

    [Header("主动触发声音")]
    public FrameEvents frameEvents;

    private void Start()
    {
        RegisterHandle();//登录手柄控制

        UpdateAllBar();//更新UI


        // 随机从 Enum 中选择一个值
        //visionType = (PlayerType)Random.Range(0, System.Enum.GetValues(typeof(PlayerType)).Length);

        //visionType = PlayerType.LongRangePlayer;
        //visionType = PlayerType.ShortRangePlayer;

        AnimSetWeapon();//设置好武器模式


        anim.Play("Girl_Default_Idle");


        //随机皮肤
        SetRandomSkin();
    }


    private void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            anim.Play("Girl_Default_Die_2");
            //rbody.simulated = false;//当玩家挂的时候，如果踩着墙，会导致墙跳出来遮挡视线
            return;
        }//死亡完全切断所有输入
        if (!isDie && currentHealth > 0)
        {
            BaseMove();//站走跑攻

            if (isKeepWeapon)
            {
                WeaponDrawn();//持械切换
            }

        }
        else
        {
            rbody.velocity = Vector2.zero; // 停止所有移动
        }

        // 每帧更新剑物体的旋转
        Strike_Effect.transform.Rotate(0, 0, 100 * Time.deltaTime);

        //当这些动画在播放的时候玩家不能移动
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Girl_Attack_1") ||
            state.IsName("Girl_Attack_2") ||
            state.IsName("Girl_Attack_3") ||
            state.IsName("Girl_Attack_4") ||
            state.IsName("Girl_Shoot_1") ||

            state.IsName("Girl_Strike_Block") ||
            state.IsName("Girl_Shoot_Block")
            )
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    public bool isDie = false;

    /// <summary>
    /// 基础数值
    /// </summary>
    #region
    [Header("基础数值")]
    public Animator anim;//接入Spine动画机
    private float inputX, inputY;
    private float StopX, StopY;

    int moveSpeed = 0;//改动画器用的

    public Rigidbody2D rbody;//声明刚体
    float speed = 2; // 基础移动速度 （站0 走2 跑4）

    public GameObject Arrow;//小地图朝向

    private void BaseMove()
    {



        //这个是拉杆控制，最优先，如果手柄没有输入，再检测手柄键盘等
        inputX = Joystick.Horizontal;
        inputY = Joystick.Vertical;
        Vector2 input = (transform.right * inputX + transform.up * inputY).normalized;//旋转摄像头

        if (inputX == 0 && inputY == 0)
        {

            input = moveAction.action.ReadValue<Vector2>();
            //Debug.Log("移动方向: " + input);

            // 记录原始输入值（四向判断用）
            inputX = input.x;
            inputY = input.y;

        }


        if (inputX > 0.5f)
        {
            inputX = 1; inputY = 0;
            attack.transform.rotation = Quaternion.Euler(0, 0, -90); // 右
            Arrow.transform.rotation = Quaternion.Euler(0, 0, -90);  // 小地图朝向右
        }
        else if (inputX < -0.5f)
        {
            inputX = -1; inputY = 0;
            attack.transform.rotation = Quaternion.Euler(0, 0, 90); // 左
            Arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (inputY > 0.5f && inputX > -0.5f && inputX < 0.5f)
        {
            inputX = 0; inputY = 1;
            attack.transform.rotation = Quaternion.Euler(0, 0, 0); // 上
            Arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (inputY < -0.5f && inputX > -0.5f && inputX < 0.5f)
        {
            inputX = 0; inputY = -1;
            attack.transform.rotation = Quaternion.Euler(0, 0, 180); // 下
            Arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else { inputX = 0; inputY = 0; } // 静止时也归零（这个很重要，当手机手柄在各自方向小于0.5的内圈时不会出现错位）

        // 保存上一次方向（用于静止状态播放对应Idle动画）
        if (inputX != 0 || inputY != 0)
        {
            StopX = inputX;
            StopY = inputY;
            if (isRunning)
            {
                moveSpeed = 2; speed = 4;
                ChangeStrength(-2);


            }
            else
            {
                moveSpeed = 1; speed = 2;
                ChangeStrength(1);

            }


        }
        else
        {
            moveSpeed = 0;
            ChangeStrength(3);
        }

        if (inputY > -0.5f && inputY < 0.5f && inputX > -0.5f && inputX < 0.5f) { speed = 0; }//防止微微拉动拉杆也移动



        CheckAttack();//检测你按着攻击键或者没有
        CheckDodge();//检测你按着闪避键或者没有



        if (!canMove)
        {
            input = Vector2.zero;

        }//玩家只有在不攻击的时候才能移动，闪避的时候也无法叠加



        rbody.velocity = input * speed;

        // 传给 Spine 动画机
        anim.SetFloat("InputX", StopX);
        anim.SetFloat("InputY", StopY);

        anim.SetInteger("Speed", moveSpeed);


    }


    #endregion



    /// <summary>
    /// 持械状态/类型玩家
    /// </summary>
    #region
    [Header("类型玩家")]
    public PlayerType visionType;
    public enum PlayerType
    {
        ShortRangePlayer,//近战
        LongRangePlayer//远程
    }

    

    [Header("持械状态")]

    bool isKeepWeapon = false;
    float weaponIdleTimer = 0f;
    float sheathDelay = 1.5f;

    void WeaponDrawn()
    {

        if (moveSpeed == 0 && !isAttacking)
        {
            weaponIdleTimer += Time.deltaTime;

            // 如果2秒内完全没动/没攻击，则自动收刀
            if (weaponIdleTimer >= sheathDelay)
            {
                weaponIdleTimer = 0f;

                Sheathe();

                frameEvents._Attack_katana_in();

                isKeepWeapon = false;
            }
        }
        else
        {
            weaponIdleTimer = 0f;
        }
    }


    public void Sheathe()
    {
        //anim.SetInteger("Weapon", 0);

        anim.ResetTrigger("DrawWeapon");    // 重置状态，避免残留
        anim.SetTrigger("SheatheWeapon");
    }


    public void ToggleWeaponMode()
    {
        if (visionType == PlayerType.ShortRangePlayer)
        {
            visionType = PlayerType.LongRangePlayer;
        }
        else
        {
            visionType = PlayerType.ShortRangePlayer;
        }

        AnimSetWeapon();
    }
    public void AnimSetWeapon() 
    {

        if (visionType == PlayerType.ShortRangePlayer)
        {
            anim.SetInteger("Weapon", 1);
        }
        else
        {
            anim.SetInteger("Weapon", 2);

        }
    }//近远切换



    #endregion

    /// <summary>
    /// 皮肤
    /// </summary>
    #region
    [Header("皮肤")]
    public CharacterSkin characterSkin;

    public int YYY_headIndex;
    public int YYY_bodyIndex;
    public int YYY_legsIndex;
    public int YYY_hatIndex;

    public int Man_headIndex;
    public int Man_bodyIndex;
    public int Man_hatIndex;

    public int Girl_headIndex;
    public int Girl_bodyIndex;
    public int Girl_legsIndex;
    public int Girl_hatIndex;

    public int weaponIndex;

    public void SetRandomSkin()
    {
        //YYY_headIndex = Random.Range(1, 14);  // 1~13
        //YYY_bodyIndex = Random.Range(1, 14);
        //YYY_legsIndex = Random.Range(1, 14);
        //YYY_hatIndex = Random.Range(1, 14);
        //
        //Man_headIndex = Random.Range(1, 7);   // 1~6
        //Man_bodyIndex = Random.Range(1, 7);
        //Man_hatIndex = Random.Range(1, 7);
        //
        //Girl_headIndex = Random.Range(1, 14);  // 1~13
        //Girl_bodyIndex = Random.Range(1, 14);
        //Girl_legsIndex = Random.Range(1, 14);
        //Girl_hatIndex = Random.Range(1, 14);
        //
        //weaponIndex = Random.Range(1, 5);   // 1~4



        YYY_headIndex = Random.Range(1, 14);  // 1~13
        YYY_bodyIndex = 11;
        YYY_legsIndex = 11;
        YYY_hatIndex = 1;

        Man_headIndex = Random.Range(1, 6); 
        Man_bodyIndex = 2;
        Man_hatIndex = Random.Range(1,3);

        Girl_headIndex = Random.Range(1, 14);  // 1~13
        Girl_bodyIndex = Random.Range(1, 14);
        Girl_legsIndex = Random.Range(1, 14);
        Girl_hatIndex = Random.Range(1, 14);

        weaponIndex = 1;






        SetSkin();
    }


    public void SaveCurrentSkin
        (
           int _YYY_headIndex, int _YYY_bodyIndex, int _YYY_legsIndex, int _YYY_hatIndex,
           int _Man_headIndex, int _Man_bodyIndex, int _Man_hatIndex,
           int _Girl_headIndex, int _Girl_bodyIndex, int _Girl_legsIndex, int _Girl_hatIndex,
           int _weaponIndex

        )
    {
        // 保存 YYY 部位
        YYY_headIndex = _YYY_headIndex;
        YYY_bodyIndex = _YYY_bodyIndex;
        YYY_legsIndex = _YYY_legsIndex;
        YYY_hatIndex = _YYY_hatIndex;

        // 保存 Man 部位
        Man_headIndex = _Man_headIndex;
        Man_bodyIndex = _Man_bodyIndex;
        Man_hatIndex = _Man_hatIndex;

        // 保存 Girl 部位
        Girl_headIndex = _Girl_headIndex;
        Girl_bodyIndex = _Girl_bodyIndex;
        Girl_legsIndex = _Girl_legsIndex;
        Girl_hatIndex = _Girl_hatIndex;

        // 保存武器
        weaponIndex = _weaponIndex;

        SetSkin();
    }

    public void SetSkin()
    {


        characterSkin.ShowCurrentAll
            (
            YYY_headIndex, YYY_bodyIndex, YYY_legsIndex, YYY_hatIndex,
            Man_headIndex, Man_bodyIndex, Man_hatIndex,
            Girl_headIndex, Girl_bodyIndex, Girl_legsIndex, Girl_hatIndex,
            weaponIndex
            );



    }


    #endregion

    /// <summary>
    /// 近战系统
    /// </summary>
    #region
    [Header("蓄力攻击")]
    private float attackPressTime = 0f;      // 持续按下时长计时器
    private bool attackTriggered = false;    // 是否已经触发攻击动作（防止反复触发）

    public bool canMove = true;

    public GameObject attack;//伤害朝向
    public GameObject attack_Collider;//伤害碰撞体
    public GameObject attack_Range;//技能范围



    void Attack_Start()
    {
        if (!isDie)
        {
            isAttacking = true;
            attackPressTime = 0f;

            attackTriggered = false;
        }

    }

    void Attack_Cancel()
    {
        if (!isDie)
        {
            isAttacking = false;




            if (!attackTriggered)
            {
                if (attackPressTime < 0.2f)
                {
                    PlayNormalAttack(); // 普通攻击
                }
                else
                {
                    PlayChargeAttack(); // 蓄力攻击
                }

                attackPressTime = 0;

                attackTriggered = true;
            }

            //attack_Range.SetActive(false);//关闭技能范围
        }



    }

    void CheckAttack()
    {
        if (isAttacking && !attackTriggered)
        {
            attackPressTime += Time.deltaTime;

            if (attackPressTime >= 0.2f)
            {
                ChangeCritical(10);//按下暴击率快速上升
                //attack_Range.SetActive(true);//技能范围
            }


        }
        else
        {
            ChangeCritical(-10);//松开暴击率快速下降
        }
    }

    [Header("攻击")]
    public int currentCombo = 0;
    public bool isAttacking2 = false;
    public bool canCombo = false;
    public bool comboQueued = false;


    private void PlayNormalAttack()
    {

        if (!isDie)
        {
            TryCrit();

            //if (isDodge) { strike.isCritial = true; }//闪避中攻击冲刺（这个可以做冲刺攻击动画）

            attackTriggered = true;


            if (visionType == PlayerType.ShortRangePlayer)
            {
                if (!isAttacking2)
                {
                    StartCombo();
                }
                else if (canCombo)
                {
                    comboQueued = true;
                }
            }
            else
            {
                if (CanShoot)
                {
                    anim.Play("Girl_Shoot_1", 0, 0);

                    CanShoot = false;
                    Invoke("SetCanShoot", 0.3f);//似乎这是目前唯一
                }
              
            }



            isKeepWeapon = true;//进入武器状态
        }


    }//普通攻击



    void StartCombo()
    {
        currentCombo = 1;
        isAttacking2 = true;
        anim.Play("Girl_Attack_1", 0, 0);


    }

    public void ResetCombo()
    {
        if (currentHealth > 0) 
        {
            currentCombo = 0;
            comboQueued = false;
            canCombo = false;
            isAttacking2 = false;



            //这里不知道什么原因，必须分开
            if (visionType == PlayerType.ShortRangePlayer)
            {
                anim.Play("Girl_Strike_Idle");
            }
            else
            {
                anim.Play("Girl_Shoot_Idle");

            }

        }//生命值大于0才可以resetCombo

      
    }


    private void PlayChargeAttack()
    {
        TryCrit(); // 改用新方法触发暴击
        strike.chargeTime = attackPressTime; // 把蓄力时间传过去（蓄力那段时间也能成攻击力 能加上去）
        PlayNormalAttack();

    }//蓄力攻击


    public void AttackVoice()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                frameEvents._Attack_sword_chop1();
                break;
            case 1:
                frameEvents._Attack_sword_chop2();
                break;
            case 2:
                frameEvents._Attack_sword_chop3();
                break;
        }
    }//攻击声音




    #endregion


    /// <summary>
    /// 射击系统
    /// </summary>
    #region
    [Header("射击")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public List<GameObject> nearbyEnemies = new List<GameObject>();


    public void ShootBullet()
    {


        // 优先攻击最近的敌人
        if (nearbyEnemies.Count > 0)
        {
            GameObject closestEnemy = GetClosestEnemy();
            if (closestEnemy != null)
            {
                Vector3 dir = (closestEnemy.transform.position - bulletSpawnPoint.position).normalized;

                // 🟢 更新角色面向方向（动画参数）
                UpdateFacingDirection(dir);

                FireBullet(dir);
                return;
            }
        }

        // 没有敌人 → 朝方向射击
        Vector3 inputDir = new Vector3(StopX, StopY, 0).normalized;
        if (inputDir.magnitude > 0.1f)
        {
            FireBullet(inputDir);
        }

    }

    private GameObject GetClosestEnemy()
    {
        GameObject closest = null;
        float minDist = float.MaxValue;

        foreach (GameObject enemy in nearbyEnemies)
        {
            if (enemy == null) continue;
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }

        return closest;
    }

    private void UpdateFacingDirection(Vector3 dir)
    {
        // 判断主方向（上下左右）
        float absX = Mathf.Abs(dir.x);
        float absY = Mathf.Abs(dir.y);

        StopX = 0;
        StopY = 0;

        if (absX > absY)
        {
            StopX = dir.x > 0 ? 1 : -1;
        }
        else
        {
            StopY = dir.y > 0 ? 1 : -1;
        }

        // 传给 Spine 动画机
        anim.SetFloat("InputX", StopX);
        anim.SetFloat("InputY", StopY);
    }//射击近距离敌人的时候朝向

    private void FireBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // 计算当前暴击率
        float critRate = (float)currentCritical / (float)maxCritical;

        // 只有在暴击率大于等于 60% 时，才可能暴击
        if (critRate >= 0.6f)
        {
            bullet.GetComponent<Shooting>().isCritial = true;
        }

        bullet.GetComponent<Shooting>().chargeTime = attackPressTime; // 把蓄力时间传过去（蓄力那段时间也能成攻击力 能加上去）

        bullet.GetComponent<Shooting>().SetDirection(direction, Shooting.BulletOwnerType.Friend); // 玩家属于Friend阵营
    }//射击子弹




    [Header("射击冷却")]
    public bool CanShoot = true;

    void SetCanShoot()
    {
        CanShoot = true;
    }

    #endregion



    /// <summary>
    /// 闪避系统
    /// </summary>
    #region
    [Header("闪避键按下")]
    private float dodgePressTime = 0f;      // 持续按下时长计时器
    private bool dodgeTriggered = false;    // 是否已经触发攻击动作（防止反复触发）


    public SpriteRenderer GhostPhantom;//幻影
    public Sprite Phantom, None;

    void Dodge_Start()
    {
        if (!isDie)
        {
            isDodging = true;
            dodgePressTime = 0f;

            dodgeTriggered = false;
        }

    }
    void Dodge_Cancel()
    {

        if (!isDie)
        {
            isDodging = false;

            if (!dodgeTriggered)
            {
                if (dodgePressTime < 0.2f)
                {
                    PlayDodge(); // 闪避
                }
                else
                {
                    //魔族变身

                    //PlayChargeAttack(); // 蓄力攻击
                }

                dodgePressTime = 0;

                dodgeTriggered = true;
            }
        }



    }

    void CheckDodge()
    {
        if (isDodging && !dodgeTriggered)
        {
            dodgePressTime += Time.deltaTime;

        }
    }


    [Header("闪避触发")]


    //public float dodgeSpeed = 10f;
    //public float dodgeDistance = 0.5f;
    public LayerMask obstacleLayer;

    public bool isDodge = false;//闪避动画期间的Dodge


    void PlayDodge()
    {

        if (currentStrength > 50) // 确保不在连续闪避状态
        {
            if (isDodge) return;//防止连续闪避


            if (inputX == 0 && inputY == 0)
            {
                Vector2 dodgeDir = new Vector2(-StopX, -StopY).normalized;//站立的时候向后闪避
                if (dodgeDir == Vector2.zero) return;

                StartCoroutine(Dodge(dodgeDir, 15f, 2f));
            }
            else
            {
                Vector2 dodgeDir = new Vector2(StopX, StopY).normalized;//移动的时候向后冲刺
                if (dodgeDir == Vector2.zero) return;

                StartCoroutine(Dodge(dodgeDir, 15f, 2f));
            }


            //anim.SetTrigger("Dodge");
        }
        else
        {
            //显示体力不足
            if (!isOutOfStrength)
            {

                frameEvents._SE_Glass();

                isOutOfStrength = true;

                //显示体力不足
                HudText.SpecialText(0);

                Invoke("OutOfStrengthCollDown", 2f);
            }
        }
    }

    [Header("冷却提示")]
    bool isOutOfStrength = false;
    void OutOfStrengthCollDown()
    {
        isOutOfStrength = false;
    }


    IEnumerator Dodge(Vector2 direction, float dodgeSpeed, float dodgeDistance)
    {

        //闪避后连击取消
        if (currentHealth > 0)
        {
            Invoke("ResetCombo", 1f);//防止挂了又站起来
        }


        // 音效、体力扣除
        frameEvents._SE_Clothes();
        //ChangeStrength(-50);


        GhostPhantom.sprite = Phantom;


        isDodge = true;

        canMove = false; // 【在闪避的一段时间内无法上下左右移动】防止位移冲突

        float movedDistance = 0f;


        while (movedDistance < dodgeDistance)
        {
            float step = dodgeSpeed * Time.fixedDeltaTime;

            Vector3 newPos = rbody.position + direction * step;

            // 检测闪避方向是否有障碍物（使用 BoxCast 替代 Raycast）
            Vector2 boxSize = new Vector2(0.5f, 0.5f); // 角色体积大小，请根据实际角色尺寸设置
            if (Physics2D.BoxCast(rbody.position, boxSize, 0f, direction, 0.1f, obstacleLayer))
            {
                Debug.Log("障碍物检测到，终止闪避");
                break;
            }

            rbody.MovePosition(newPos);  // 物理安全移动
            movedDistance += step;

            yield return new WaitForFixedUpdate();
        }



        Invoke(nameof(DodgingOver), 0.6f);// 让子弹时间更容易触发

        canMove = true; // 【在闪避的一段时间内无法上下左右移动】防止位移冲突


        GhostPhantom.sprite = None;

       

    }

    void DodgingOver()
    {
        isDodge = false;
    }

    [Header("闪避触发成功暴击")]
    public Strike strike;//目前用于触发暴击效果

    public void DodgeEnemyAttack()
    {
        // 音效
        frameEvents._Attack_katana_draw();

        //显示闪避成功
        HudText.SpecialText(1);

        Time.timeScale = 0.3f;

        Invoke("DodgeEnemyAttackOver", 0.2f);


        ChangeCritical(maxCritical);//充满暴击率

    }


    void DodgeEnemyAttackOver()
    {
        Time.timeScale = 1f;//继续


    }



    #endregion



    /// <summary>
    /// 多端输入
    /// </summary>
    #region
    [Header("InputSystem")]
    [SerializeField] private InputActionReference moveAction;//方向键控制
    [SerializeField] private InputActionAsset inputActions;//跑攻闪

    private InputAction runAction;

    private InputAction AttackAction;

    private InputAction DodgeAction;

    private void RegisterHandle()
    {
        // 获取动作（根据你的Action Map结构可能需要调整路径）
        runAction = inputActions.FindAction("Run");
        AttackAction = inputActions.FindAction("Attack");
        DodgeAction = inputActions.FindAction("Dodge");

        // 订阅输入事件
        runAction.started += OnRunStarted;
        runAction.canceled += OnRunCanceled;

        // 订阅输入事件
        AttackAction.started += OnAttackStarted;
        AttackAction.canceled += OnAttackCanceled;

        // 订阅输入事件
        DodgeAction.started += OnDodgeStarted;
        DodgeAction.canceled += OnDodgeCanceled;


    }
    private void OnRunStarted(InputAction.CallbackContext context)
    {
        if (!isDie && currentHealth > 0)
        {
            isRunning = true;
        }
        
    }
    private void OnRunCanceled(InputAction.CallbackContext context)
    {

        if (!isDie && currentHealth > 0)
        {
            isRunning = false;
        }
       
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {

        if (!isDie && currentHealth > 0)
        {
            Attack_Start();
        }
        
    }
    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        if (!isDie && currentHealth > 0)
        {
            Attack_Cancel();
        }
      
    }

    private void OnDodgeStarted(InputAction.CallbackContext context)
    {

        if (!isDie && currentHealth > 0)
        {
            Dodge_Start();
        }
        
    }
    private void OnDodgeCanceled(InputAction.CallbackContext context)
    {

        if (!isDie && currentHealth > 0)
        {
            Dodge_Cancel();
        }
        
    }

    [Header("手机端触发")]
    public Joystick Joystick;

    //手机端触发
    public bool isRunning = false;//持续按下跑步键
    public void ButtonSetRun()
    {
        if (!isDie && currentHealth > 0)
        {
            isRunning = true;
        }

    }
    public void ButtonSetStop()
    {
        if (!isDie && currentHealth > 0)
        {
            isRunning = false;
        }

    }

    //手机端触发
    public bool isAttacking = false;//持续按下攻击键
    public void ButtonSetAttack()
    {
        if (!isDie && currentHealth > 0)
        {
            Attack_Start();
        }
    }
    public void ButtonSetAttackOver()
    {
        if (!isDie && currentHealth > 0)
        {
            Attack_Cancel();
        }

    }

    //手机端触发
    public bool isDodging = false;//持续按下闪避键
    public void ButtonSetDodge()
    {
        if (!isDie && currentHealth > 0)
        {
            Dodge_Start();
        }
    }
    public void ButtonSetDodgeOver()
    {

        if (!isDie && currentHealth > 0)
        {
            Dodge_Cancel();
        }
    }

    #endregion




    /// <summary>
    /// 生命值体力值等数值
    /// </summary>
    #region

    void UpdateAllBar()
    {
        //更新UI
        UIManager.instance.UpdateStrengthBar(currentStrength, maxStrength);
        UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);
    }
    [Header("特效")]
    public GameObject Strike_Effect;//剑光特效
    public GameObject BloodEffect;//受伤特效
    public GameObject SparkEffect;//火星特效


    public GameObject Floor_Blood_0, Floor_Blood_1, Floor_Blood_2, Floor_Blood_3;

    [Header("生命值")]
    public int currentHealth;
    public int maxHealth;


    [Header("伤害显示")]
    public GameObject RedScreen;
    public bool isScreaming;
    public HudText HudText;

    [Header("暴击")]
    public GameObject Critial;

    public void ChangeHealth(int amount, int TypeOfAttack)//【攻击方式】 0无  1剑击特效  2闪电特效  3冻结
    {
        if (!isScreaming && currentHealth > 0 && !isDie)//冷却不受击，死亡后不受击，倒地不受击，(所有攻击都无法canMove)攻击中不受击
        {



            if (isDodging)
            {

                DodgeEnemyAttack();
                return;

            }//闪避伤害
            if (amount < 0)
            {

                if (!isDie && canMove)//处于攻击状态下无法防御
                {
                    // 计算体力百分比
                    float strengthPercent = (float)currentStrength / maxStrength;
                
                    // 根据体力百分比决定防御几率（体力越高越容易防御）
                    // 比如体力满时为 100% 几率，体力最低时为 10%
                    float blockChance = Mathf.Lerp(0.1f, 1f, strengthPercent);
                
                    if (Random.value < blockChance)
                    {
                        //anim.SetTrigger("Block");

                        if (visionType == PlayerType.ShortRangePlayer)
                        {
                            anim.Play("Girl_Strike_Block");
                        }
                        else
                        {
                            anim.Play("Girl_Shoot_Block");
                        }

                        // 防御成功扣除体力
                        ChangeStrength(-50);
                
                
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                frameEvents._Attack_sword_clash2();
                                break;
                            case 1:
                                frameEvents._Attack_sword_clash3();
                                break;
                            case 2:
                                frameEvents._Attack_sword_clash4();
                                break;
                        }
                
                
                        //显示伤害
                        HudText.HUD(0);//0会显示Miss
                
                        //火花特效
                        Vector3 offset_2 = new Vector3(0, 0, 2); // 这里的1表示沿Z轴上升的距离，可以根据需要调整
                        Vector3 spawnPosition_2 = transform.position + offset_2;
                        GameObject effectPrefabs_2 = Instantiate(SparkEffect, spawnPosition_2, transform.rotation);
                        Destroy(effectPrefabs_2, 2f);
                
                        return;
                    }
                
                }


                //受伤时连击取消
                if (currentHealth > 0)
                {
                    Invoke("ResetCombo", 1f);//防止挂了又站起来
                }


            }//格挡

            //伤害类型
            switch (TypeOfAttack)
            {
                case 1:
                    Strike_Effect.SetActive(true);//剑伤害
                    break;
                case 2:
                    //Palsy_Effect.SetActive(true);//雷电伤害
                    break;
            }



            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);

            //显示伤害
            HudText.HUD(amount);

            //伤害冷却
            Invoke("HurtOver", 0.5f);
            RedScreen.SetActive(true);
            isScreaming = true;








            //血
            #region
            switch (Random.Range(0, 3))
            {
                case 0:
                    frameEvents._Attack_blood1();
                    break;
                case 1:
                    frameEvents._Attack_blood2();
                    break;
                case 2:
                    frameEvents._Attack_blood3();
                    break;
            }

            //血特效
            Vector3 offset = new Vector3(0, 0, 2); // 这里的1表示沿Z轴上升的距离，可以根据需要调整
            Vector3 spawnPosition = transform.position + offset;
            GameObject effectPrefabs = Instantiate(BloodEffect, spawnPosition, transform.rotation);
            Destroy(effectPrefabs, 2f);



            // 从预设中随机挑一个
            GameObject[] bloodPrefabs = { Floor_Blood_0, Floor_Blood_1, Floor_Blood_2, Floor_Blood_3 };
            int index = Random.Range(0, bloodPrefabs.Length);
            GameObject blood = Instantiate(
                bloodPrefabs[index],
                transform.position,
                Quaternion.Euler(0, 0, Random.Range(0, 360))
            );

            // 向 Z 方向下沉一点，避免和角色重合
            Vector3 pos = blood.transform.position;
            pos.z -= 0.1f;
            blood.transform.position = pos;

            // 可选：缩放或微调位置
            // blood.transform.localScale *= Random.Range(0.8f, 1.2f);

            // 自动销毁血迹
            Destroy(blood, Random.Range(4f, 5f));
            #endregion




            if (currentHealth <= 0)
            {
                isDie = true;

                anim.Play("Girl_Default_Die_2");

                Critical.SetActive(false);
                return;
            }

            //击倒再站起
            if (Random.Range(0, 2) == 0 && !isDie && currentHealth > 0)
            {
                isDie = true;

                anim.Play("Girl_Default_Die");

                //防止最后一下又击倒站起
                if (currentHealth > 0)
                {
                    Invoke("GetUp", 0.5f);//比起敌人，玩家可以更快站起来
                }

                Critial.SetActive(true);
            }

        }

    }


    void HurtOver()
    {
        isScreaming = false;
        RedScreen.SetActive(false);
    }//有1秒左右的伤害冷却



    void GetUp()
    {
        if (currentHealth > 0)
        {
            anim.SetTrigger("GetUp");

            Invoke("GetUpOver", 0.2f);//完全站起来才能攻击
        }
      
    }



    public void GetUpOver() 
    {

        isDie = false;
        canMove = true;//站起同时按攻击导致无法移动但是仍旧播放站起动画
    }



    [Header("体力值")]
    public int currentStrength;
    public int maxStrength;



    public void ChangeStrength(int amount)
    {

        currentStrength = Mathf.Clamp(currentStrength + amount, 0, maxStrength);
        UIManager.instance.UpdateStrengthBar(currentStrength, maxStrength);
    }


    [Header("UI条 暴击值")]
    public GameObject Critical;

    public int currentCritical;
    public int maxCritical;


    public void ChangeCritical(int amount)
    {

        //Debug.Log("充能");
        if (!isDie)
        {

            if (currentCritical <= 0)
            {
                Critical.SetActive(false);
            }
            else
            {
                Critical.SetActive(true);
            }


        }//如果是已经Die了，那么这个淫乱槽不需要出现

        currentCritical = Mathf.Clamp(currentCritical + amount, 0, maxCritical);
        UIManager.instance.UpdateCriticalBar(currentCritical, maxCritical);
    }

    private void TryCrit()
    {
        // 计算当前暴击率
        float critRate = (float)currentCritical / (float)maxCritical;

        // 只有在暴击率大于等于 60% 时，才可能暴击
        if (critRate >= 0.6f)
        {
            strike.isCritial = true;


        }
        else
        {
            strike.isCritial = false;
        }
    }
    #endregion

}

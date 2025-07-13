using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("主动触发声音")]
    public FrameEvents frameEvents;

    [Header("寻找玩家/RoomGenerator")]
    public GameObject _Player;//玩家
    public Player player;

    public RoomGenerator RoomGenerator;//寻找RoomGenerator

    public void Start()
    {
        //找玩家
        _Player = GameObject.FindGameObjectWithTag("Player");
        player = _Player.GetComponent<Player>();

        //寻找RoomGenerator
        RoomGenerator = GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<RoomGenerator>();


        UpdateAllBar();//更新UI

        //速度岔开
        RunSpeed = Random.Range(3, 5);
        WalkSpeed = Random.Range(1, 3);





        // 随机从 Enum 中选择一个值
        visionType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);

        //visionType = EnemyType.LongRangeEnemy;
        //visionType = EnemyType.ShortRangeEnemy;


        //不同敌人攻击范围不一样
        switch (visionType) 
        {
            case EnemyType.ShortRangeEnemy:
                attackCooldown = 1f;
                enemyVision.circleCollider2D.radius = 1.5f;
                break;

            case EnemyType.LongRangeEnemy:
                attackCooldown = 1f;
                enemyVision.circleCollider2D.radius = 4f;
                break;
        }




        //随机皮肤
        if (CanChangeSkin)
        {
            SetRandomSkin();
            // 随机从 Enum 中选择一个值
            Class = (EnemyClass)Random.Range(0, System.Enum.GetValues(typeof(EnemyClass)).Length);

            Class = EnemyClass.Succubus;
        }


        anim.Play(GetAnimPrefix() + "Default_Idle");


        GateEffect.SetActive(true);//传送门特效
    }

    void FixedUpdate()
    {
        if (!isDie)
        {
            BaseMove();//站走跑攻射


            if (isKeepWeapon)
            {
                WeaponDrawn();//持械切换
            }



            AntiOverlapping.SetActive(true);//站起后无法被穿过
            //rbody.simulated = true;
        }
        else
        {
            //倒下后不能移动
            moveSpeed = 0;
            aiPath.maxSpeed = 0f;

            //只要倒地就不显示
            attack_Collider.SetActive(false);
            attack_Range.SetActive(false);



            AntiOverlapping.SetActive(false);//跪下后被穿过防止堵着敌人
            //rbody.simulated = false ;
        }







        //始终跟随目标
        if (CurrentTarget != null)
        {
            _Target.transform.position = CurrentTarget.transform.position;

        }



        // 每帧更新剑物体的旋转
        Strike_Effect.transform.Rotate(0, 0, 100 * Time.deltaTime);


        //当这些动画在播放的时候玩家不能移动
        //AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        //
        //if (state.IsName("attack_1") ||
        //    state.IsName("attack_2") ||
        //    state.IsName("attack_3") ||
        //    state.IsName("attack_4") ||
        //    state.IsName("Girl_Strike_Block") ||
        //    state.IsName("Girl_Strike_Idle"))
        //{
        //    aiPath.canMove = false;
        //
        //}
        //else
        //{
        //    aiPath.canMove = true;
        //}

    }

    public bool isPatrol = false;
    public bool isAttack = false;//用来作为处于攻击状态的……标准
    public bool isDie = false;
    public bool isRape = false;

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

    public AIPath aiPath;// A* 路径控制器

    public GameObject Arrow;//小地图朝向

    [Header("速度岔开")]
    float RunSpeed = 4f;
    float WalkSpeed = 2f;

    [Header("队友索敌冷却")]
    bool MakeSureEnemy = false;
    float MakeSureEnemyTimer = 0;

    private void BaseMove()
    {

        if (aiPath == null || !aiPath.hasPath) return;

        Vector2 current = transform.position;
        Vector2 target = aiPath.steeringTarget;

        Vector2 dir = (target - current).normalized;



        float dist = Vector2.Distance(current, target);

        if (!isPatrol)
        {
            if (player.currentHealth <= 0)
            {
                if (tag != "Friend")
                {
                    //只要玩家生命值为0就聚过去
                    moveSpeed = 2;
                    aiPath.maxSpeed = RunSpeed;
                }
                else
                {
                    //玩家死亡时队友也全部死亡
                    ChangeHealth(-maxHealth,0);
                }
               
            }
            else
            {

                if (!isAttack)
                {
                    if (tag != "Friend")
                    {
                        //目前战斗下全员跑
                        moveSpeed = 2;
                        aiPath.maxSpeed = RunSpeed;


                    }
                    else if (!isPatrol)
                    {

                        if (dist > 1)
                        {
                            //队友跟随玩家的时候，玩家走，队友走，玩家跑，队友跑/队友目标为敌人的时候只会跑
                            if (player.isRunning == false && CurrentTarget == _Player)
                            {
                                moveSpeed = 1;
                                aiPath.maxSpeed = WalkSpeed;
                            }
                            else
                            {
                                moveSpeed = 2;
                                aiPath.maxSpeed = RunSpeed;
                            }
                        }
                        else
                        {
                            //玩家在队友旁边，队友站着不动
                            moveSpeed = 0;
                            aiPath.maxSpeed = 0.01f;
                        }


                    }



                    //重置计数
                    attackTimer = 0f;//间隔归零
                    isInAttackDelay = false;



                    attack_Range.SetActive(false);//关闭技能范围    
                                                  //isAttack = false;


                }
                else
                {
                    if (tag == "Friend" && CurrentTarget == _Player)
                    {
                        //队友在追随玩家的情况下必须在一定距离停下
                    }
                    else
                    {
                        BaseAttack();//攻击
                    }



                    moveSpeed = 0;
                    aiPath.maxSpeed = 0.01f;


                    attack_Range.SetActive(true);//显示技能范围
                                                 //isAttack = true;

                }
            }


            //一旦target没有了就自动玩家
            if (CurrentTarget == null && tag == "Enemy")
            {
                CurrentTarget = _Player;
            }

            AntiOverlapping.SetActive(true);//这个玩意会让敌人队友不重叠，但是巡逻的时候会贴在一起，巡逻的时候去掉


            if (tag == "Friend")
            {


                if (CurrentTarget == null || CurrentTarget.tag == "Friend" || !CurrentTarget.activeInHierarchy || CurrentTarget == _Player)
                {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                    if (enemies.Length > 0 && !MakeSureEnemy)
                    {

                        int index = Random.Range(0, enemies.Length);
                        CurrentTarget = enemies[index];

                        Debug.Log("队友索敌目标: " + CurrentTarget.name);

                        MakeSureEnemy = true;
                    }
                    else
                    {
                        CurrentTarget = _Player;
                    }
                }


                if (MakeSureEnemy)
                {
                    MakeSureEnemyTimer += Time.deltaTime;

                    if (MakeSureEnemyTimer >= 2f)
                    {
                        MakeSureEnemy = false;
                    }
                }
            }




        }
        else
        {


            //巡逻
            Patrol();


            CurrentTarget = Patrol_Target;//巡逻目标

            AntiOverlapping.SetActive(false);//这个玩意会让敌人队友不重叠，但是巡逻的时候会贴在一起，巡逻的时候去掉
        }



        // 八方向判断（上下左右为主）
        if (dir.x > 0.5f)
        {
            inputX = 1; inputY = 0;
            attack.transform.rotation = Quaternion.Euler(0, 0, -90); Arrow.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (dir.x < -0.5f)
        {
            inputX = -1; inputY = 0;
            attack.transform.rotation = Quaternion.Euler(0, 0, 90); Arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dir.y > 0.5f)
        {
            inputX = 0; inputY = 1;
            attack.transform.rotation = Quaternion.Euler(0, 0, 0); Arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (dir.y < -0.5f)
        {
            inputX = 0; inputY = -1;
            attack.transform.rotation = Quaternion.Euler(0, 0, 180); Arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        //else
        //{
        //    //inputX = 0; inputY = 0;
        //
        //    inputX = 0; inputY = -1;//朝正面
        //}

        // 储存方向用于 idle 状态
        if (inputX != 0 || inputY != 0)
        {
            StopX = inputX;
            StopY = inputY;
        }

        // 动画传入方向
        anim.SetFloat("InputX", StopX);
        anim.SetFloat("InputY", StopY);
        anim.SetInteger("Speed", moveSpeed);
    }




    #endregion

    /// <summary>
    /// 捕获与被捕获
    /// </summary>
    #region

    private void OnTriggerEnter2D(Collider2D collision)//检测到玩家显示
    {

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().currentHealth <= 0)
            {
                if (collision.gameObject.GetComponent<Player>().isRape == false)
                {
                    isRape = true;
                    anim.Play("RBQ_Punish_Rape");

                    gameObject.transform.position = collision.gameObject.transform.position;


                    collision.gameObject.GetComponent<Player>().characterSkin.HideSkeleton();


                    collision.gameObject.GetComponent<Player>().isRape = true;
                }
                else
                {
                    //isPatrol = true;
                    //isDie = true;
                }

            }

        }//敌人捕获玩家
    }


    #endregion

    /// <summary>
    /// 持械状态/类型敌人
    /// </summary>
    #region
    [Header("类型敌人")]
    public EnemyType visionType;
    public enum EnemyType
    {
        ShortRangeEnemy,//近战
        LongRangeEnemy,//远程
    }
    public void ChangeType(int t)
    {
        switch (t)
        {
            case 0:
                visionType = EnemyType.ShortRangeEnemy;
                break;
            case 1:
                visionType = EnemyType.LongRangeEnemy;
                break;
        }
    }

    public EnemyClass Class;
    public enum EnemyClass
    {
        Girl,
        Man,
        Succubus,
    }
    public void ChangeClass(int c)
    {
        switch (c)
        {
            case 0:
                Class = EnemyClass.Girl;
                break;
            case 1:
                Class = EnemyClass.Man;
                break;
            case 2:
                Class = EnemyClass.Succubus;
                break;
        }
    }



    private string GetAnimPrefix()
    {
        switch (Class)
        {
            case EnemyClass.Girl:
                return "Girl_";
            case EnemyClass.Man:
                return "Man_";
            case EnemyClass.Succubus:
                return "Succubus_";
            // 未来扩展：Tentacle, Demon 等
            default:
                return "";
        }
    }


    //public float AttackRange = 1;//敌人接近多少才会开始攻击


    [Header("持械状态")]
    bool isKeepWeapon = false;
    float weaponIdleTimer = 0f;
    float sheathDelay = 1.5f;

    void WeaponDrawn()
    {

        if (moveSpeed == 0 && !isAttack)
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



    public void Draw()
    {

        switch (visionType)
        {
            case EnemyType.ShortRangeEnemy:
                anim.SetInteger("Weapon", 1);
                break;

            case EnemyType.LongRangeEnemy:
                anim.SetInteger("Weapon", 2);
                break;
        }

        anim.SetTrigger("DrawWeapon");
    }
    public void Sheathe()
    {
        //anim.SetInteger("Weapon", 0);

        anim.ResetTrigger("DrawWeapon");    // 重置状态，避免残留
        anim.SetTrigger("SheatheWeapon");
    }

    void ReSetAttack()
    {
        if (Class == EnemyClass.Succubus){ anim.Play(GetAnimPrefix() + "Default_Idle");return; }//只有魔族需要更改

        switch (visionType)
        {

            case EnemyType.ShortRangeEnemy:
                anim.Play(GetAnimPrefix() + "Strike_Idle");
                break;

          
            case EnemyType.LongRangeEnemy:
                anim.Play(GetAnimPrefix() + "Shoot_Idle");
                break;

        }

    }

    #endregion

    /// <summary>
    /// 皮肤
    /// </summary>
    #region
    [Header("皮肤")]
    public CharacterSkin characterSkin;
    public bool CanChangeSkin = true;

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
        YYY_bodyIndex = Random.Range(11, 13);
        YYY_legsIndex = Random.Range(11, 13);
        YYY_hatIndex = Random.Range(1, 5);

        Man_headIndex = Random.Range(1, 6);
        Man_bodyIndex = 2;
        Man_hatIndex = Random.Range(1, 3);

        Girl_headIndex = Random.Range(1, 14);  // 1~13
        Girl_bodyIndex = Random.Range(1, 14);
        Girl_legsIndex = Random.Range(1, 14);
        Girl_hatIndex = Random.Range(1, 14);

        weaponIndex = Random.Range(1, 7);

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
    [Header("攻击")]


    public GameObject attack;//伤害朝向
    public GameObject attack_Collider;//伤害碰撞体
    public GameObject attack_Range;//技能范围

    public EnemyVision enemyVision;//视野范围

    private float attackTimer = 0f;
    private float attackCooldown = 1f; // 原本 Invoke 的 1f
    private bool isInAttackDelay = false;

    void BaseAttack()
    {

        //隔一会触发一下攻击
        if (!isInAttackDelay)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {

                switch (visionType)
                {

                    case EnemyType.ShortRangeEnemy:

                        Attack_Start(); // 攻击警告开始闪

                        break;

                    case EnemyType.LongRangeEnemy:

                        //队友使用玩家的攻击动画
                        if (tag == "Friend") { anim.Play(GetAnimPrefix() + "Shoot_1", 0, 0); }
                        else { anim.Play(GetAnimPrefix() + "shoot_1", 0, 0); }

                        break;
                }



                attackTimer = 0f;


                isInAttackDelay = true;
            }


            isKeepWeapon = true;//没有持械的话进入持械状态
        }

    }


    void FlashWarning()
    {
        if (AttackRangeImage.color == Color.white)
        {
            AttackRangeImage.color = Color.black;
        }
        else
        {
            AttackRangeImage.color = Color.white;
        }
    } //技能范围作为攻击警告黑白黑白一闪一闪





    void Attack_Start()
    {
        InvokeRepeating(nameof(FlashWarning), 0f, 0.1f);


        //队友使用玩家的攻击动画
        if (tag == "Friend")
        {

            switch (Random.Range(1, 5))
            {
                case 1:
                    anim.Play(GetAnimPrefix() + "Attack_1", 0, 0);
                    break;
                case 2:
                    anim.Play(GetAnimPrefix() + "Attack_2", 0, 0);
                    break;
                case 3:
                    anim.Play(GetAnimPrefix() + "Attack_3", 0, 0);
                    break;
                case 4:
                    anim.Play(GetAnimPrefix() + "Attack_4", 0, 0);
                    break;
            }


        }
        else
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    anim.Play(GetAnimPrefix() + "attack_1", 0, 0);
                    break;
                case 2:
                    anim.Play(GetAnimPrefix() + "attack_2", 0, 0);
                    break;
                case 3:
                    anim.Play(GetAnimPrefix() + "attack_3", 0, 0);
                    break;
                case 4:
                    anim.Play(GetAnimPrefix() + "attack_4", 0, 0);
                    break;
            }
        }




        //Invoke("Attack_Cancel", 1f);//一旦动画帧事件被跳过就会站着不动不攻击，所以这个还是Invoke触发
    }


    public void Attack_Cancel()
    {
        isInAttackDelay = false;

        CancelInvoke(nameof(FlashWarning));//关闭技能范围作为攻击警告关闭一闪一闪

        if (tag == "Friend")
        {
            AttackRangeImage.color = Color.green;


        }
        else
        {
            AttackRangeImage.color = Color.red;
        }




        ReSetAttack();
    }

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

    [Header("射击攻击")]
    public GameObject transparentBulletPrefab;
    public Transform bulletSpawnPoint;


    public void ShootBullet()
    {
        if (CurrentTarget == null) return;

        Vector3 dir = (CurrentTarget.transform.position - bulletSpawnPoint.position).normalized;

        // 🟢 更新角色面向方向（动画参数）
        UpdateFacingDirection(dir);

        GameObject bullet = Instantiate(transparentBulletPrefab, bulletSpawnPoint.position, Quaternion.identity);


        if (tag == "Friend")
        {
            bullet.GetComponent<Shooting>().SetDirection(dir, Shooting.BulletOwnerType.Friend);//队友发射子弹
        }
        else
        {
            bullet.GetComponent<Shooting>().SetDirection(dir, Shooting.BulletOwnerType.Enemy);//敌人发射子弹
        }



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





    #endregion



    /// <summary>
    /// 巡逻系统
    /// </summary>
    #region
    [Header("索敌系统")]
    public GameObject _Target;//持续寻路对象
    public GameObject Patrol_Target;//碰到就传送的巡逻目标

    public GameObject CurrentTarget;//当前的目标


    [Header("巡逻系统")]
    bool isWalking = true;
    float patrolTimer = 0f;
    float walkDuration = 2f;  // 每次走几秒
    float idleDuration = 1f;  // 每次停几秒

    public GameObject AntiOverlapping;//这个玩意会让敌人队友不重叠，但是巡逻的时候会贴在一起，巡逻的时候去掉
    void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if (isWalking)
        {
            if (patrolTimer >= walkDuration)
            {
                isWalking = false;
                patrolTimer = 0f;
            }

            moveSpeed = 1;
            aiPath.maxSpeed = WalkSpeed;
        }
        else
        {
            if (patrolTimer >= idleDuration)
            {
                isWalking = true;
                patrolTimer = 0f;
            }

            moveSpeed = 0;
            aiPath.maxSpeed = 0.01f;  // 停止几乎不动
        }
    }//走走停停
    #endregion






    /// <summary>
    /// 生命值体力值等数值
    /// </summary>
    #region

    void UpdateAllBar()
    {
        //更新UI
        UpdateHealthBar(currentHealth, maxHealth);
    }

    [Header("特效")]
    public GameObject Strike_Effect;//剑光特效
    public GameObject BloodEffect;//受伤特效
    public GameObject SparkEffect;//火星特效
    public GameObject GateEffect;//传送门特效

    public GameObject Floor_Blood_0, Floor_Blood_1, Floor_Blood_2, Floor_Blood_3;

    [Header("生命值体力值等数值")]
    public int currentHealth;
    public int maxHealth;

    //伤害显示
    public bool isScreaming;
    public HudText HudText;



    public void ChangeHealth(int amount, int TypeOfAttack)//【攻击方式  0无  1剑击特效  2闪电特效  3冻结
    {

        if (!isScreaming&&!isRape)//冷却中与捕获中不会被伤到
        {




            if (amount < 0)
            {
                if (isPatrol)
                {
                    Time.timeScale = 0;

                    //显示暗杀
                    Assassinate.SetActive(true);

                    amount = -currentHealth;

                }//暗杀


                isPatrol = false;//受伤后立刻进入战斗


                if (!isDie && currentHealth > 0 && amount != -currentHealth)
                {
                    //队友比敌人更加容易触发防御
                    if (tag == "Friend" && Random.Range(0, 2) == 0)
                    {
                        Block();
                        return;
                    }

                    if (tag == "Enemy" && Random.Range(0, 5) == 0)
                    {
                        Block();
                        return;
                    }
                }

            }

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
            UpdateHealthBar(currentHealth, maxHealth);

            //显示伤害
            HudText.HUD(amount);

            //伤害冷却
            Invoke("HurtOver", 0.2f);

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
                Die();

                return;
            }


            //击倒再站起(和暴击结合)
            if (Random.Range(0, 2) == 0 && !isDie && currentHealth > 0)
            {
                Knockdown();
            }
        }




    }

    void HurtOver()
    {
        isScreaming = false;
    }//有1秒左右的伤害冷却

    void GetUp()
    {
        if (currentHealth > 0)
        {
            isDie = false;
            anim.SetTrigger("GetUp");
        }  //防止最后一下又击倒站起

    }//起身


    void Block()
    {
        switch (visionType)
        {
          
            case EnemyType.ShortRangeEnemy:
                anim.Play(GetAnimPrefix() + "Strike_Block");
                break;
          
            case EnemyType.LongRangeEnemy:
                anim.Play(GetAnimPrefix() + "Shoot_Block");
                break;

        }






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
    }//防御


    [Header("暴击")]
    public GameObject Critial;
    public GameObject Assassinate;//暗杀
    public void CritialAttack()
    {

        Knockdown();



        Time.timeScale = 0;

        //显示暴击
        Critial.SetActive(true);

        //暴击清零
        player.ChangeCritical(-player.maxCritical);

    }//暴击

    public void Knockdown()
    {


        isDie = true;
        anim.Play(GetAnimPrefix() + "Default_Die");

        if (currentHealth >= 0)
        {
            Invoke("GetUp", 1f);
        }  //防止最后一下又击倒站起

        //每次击倒后再站起来重新计算
        isInAttackDelay = false;
        attackTimer = 0f;

    }//击倒

    public void Die()
    {
        isDie = true;
        anim.Play(GetAnimPrefix() + "Default_Die_2");//防止倒下又起来,搞了第二死亡

        Invoke("Disappear", 1f);
    }//死亡


    [Header("全部自身存在")]
    public GameObject AllOfThis;
    public WallMap wallmap;
    void Disappear()
    {
        if (!OneTimeRebirth)
        {
            Destroy(AllOfThis);



            Time.timeScale = 1;//防止 Critial消失之前次物体已经被毁坏，然后卡住不动了

            //RoomGenerator.SetEnemy();
            if (wallmap != null)
            {
                Debug.Log("调用 wallmap.CheckEnemyList()");
                wallmap.CheckEnemyList();
            }
            else
            {
                //Debug.LogWarning("wallmap 是 null，无法调用 CheckEnemyList()");
            }

            OneTimeRebirth = true;
        }

    }

    bool OneTimeRebirth = false;//只死一次


    [Header("生命值UI显示")]
    public Image HealthBar;
    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        HealthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }//Enemy，Friend，NPC替代UIManager的地方


    #endregion




    /// <summary>
    /// 阵营转换
    /// </summary>
    #region
    [Header("阵营转换")]
    public EnemyVision vision;
    public Strike strike;
    public Image HealthValueImage;
    public SpriteRenderer AttackColliderImage;
    public SpriteRenderer AttackRangeImage;
    //切换为队友
    public void ConvertToFriend()
    {
        //  修改标签
        this.tag = "Friend";

        //  视野脚本：变成队友
        vision.isFriend = true;


        //  攻击脚本：攻击敌人，不再攻击队友
        strike.DamageToPlayer = false;
        strike.DamageToEnemy = true;
        strike.DamageToFriend = false;

        //  改变血条颜色为绿色（友军色）
        HealthValueImage.color = Color.green;

        //  改变攻击实体面积显示颜色为绿色
        AttackColliderImage.color = Color.green;

        //  改变攻击范围显示颜色为绿色
        AttackRangeImage.color = Color.green;

        //  改变小地图显示颜色为绿色
        Arrow.GetComponent<SpriteRenderer>().color = Color.green;

        Debug.Log($"{gameObject.name} has switched to Friend.");

        //敌人攻击冷却
        attackCooldown = 1f;
    }

    // 切换为敌人
    public void ConvertToEnemy()
    {
        // 修改标签
        this.tag = "Enemy";

        // 视野脚本：不是队友
        vision.isFriend = false;

        // 攻击脚本：攻击玩家和友军，不攻击敌人
        strike.DamageToPlayer = true;
        strike.DamageToEnemy = false;
        strike.DamageToFriend = true;

        // 改变血条颜色为红色（敌人色）
        HealthValueImage.color = Color.red;

        // 改变攻击实体面积显示颜色为红色
        AttackColliderImage.color = Color.red;

        // 改变攻击范围显示颜色为红色
        AttackRangeImage.color = Color.red;

        //  改变小地图显示颜色为绿色
        Arrow.GetComponent<SpriteRenderer>().color = Color.red;



        Debug.Log($"{gameObject.name} has switched to Enemy.");

        //队友攻击冷却
        attackCooldown = 0.2f;
    }
    #endregion





}


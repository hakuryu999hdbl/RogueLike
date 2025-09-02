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
        StopDir = Random.Range(0.8f, 1.15f);




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



        if (currentSaveName == "")
        {

            //随机皮肤
            if (CanChangeSkin)
            {
                SetRandomSkin();
                // 随机从 Enum 中选择一个值
                Class = (EnemyClass)Random.Range(0, System.Enum.GetValues(typeof(EnemyClass)).Length);

                //Class = EnemyClass.Succubus;
                //Class = EnemyClass.Girl;
                //Class = EnemyClass.Man;
                //Class = EnemyClass.Monster;
                //Class = EnemyClass.Tentacle_Monster;
                Class = EnemyClass.Tentacle_Bug;


                if (Class == EnemyClass.Girl && visionType == EnemyType.LongRangeEnemy && Random.Range(0, 2) == 0)
                {
                    isMage = true;

                }//一部分远程女射手变成女法师


                if (Class == EnemyClass.Monster||Class == EnemyClass.Tentacle_Monster || Class == EnemyClass.Tentacle_Bug) 
                {

                    visionType = EnemyType.ShortRangeEnemy;
                    attackCooldown = 1f;
                    enemyVision.circleCollider2D.radius = 1.5f;

                }//这部分怪物只能近战


            }
        }//如果已经赋值了队友，那么不随机

        anim.Play(GetAnimPrefix() + "Default_Idle");


        GateEffect.SetActive(true);//传送门特效


    }


    /// <summary>
    /// 存读档
    /// </summary>
    #region

    [Header("当前操纵的存档名称")]
    public string currentSaveName; // 当前操作的存档名
    public Text Name;
    public void ApplySaveData(PlayerSaveData data)
    {
        // 应用皮肤信息
        this.YYY_headIndex = data.headIndex;
        this.YYY_eyesIndex = data.eyesIndex;
        this.YYY_bodyIndex = data.bodyIndex;
        this.YYY_legsIndex = data.legsIndex;
        this.YYY_hatIndex = data.hatIndex;

        // 应用武器
        this.weaponIndex = data.weaponIndex;
        this.CurrentProfession = data.professionIndex;

        // 根据这些数据设置皮肤
        SetSkin(); // 你已有的方法（或自己写个用这些 Index 设置皮肤的方法）


        //数值赋予
        this.maxHealth = data.maxHP;
        currentHealth = maxHealth;
        UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);

        //记录当前名称
        currentSaveName = data.characterName;
        Name.text = currentSaveName;

        //涉及升级储存，所以保持正数，只有在需要攻击伤害的时候变成复数
        MeleeDamage = data.meleeDamage;
        ShootDamage = data.shootDamage;
        SpellDamage = data.spellDamage;

        CurrentWeaponPower = data.weaponAtk;
        CurrentArmorDefence = data.armorDef;
        CurrentStockingDefence = data.stockingDef;


        //近战武器赋值
        if (CurrentProfession == 0)
        {
            strike.Damage = -data.meleeDamage - data.weaponAtk;
        }
        else
        {

            //法师近战攻击力急剧缩减
            strike.Damage = -data.meleeDamage / 5;
        }

        switch (weaponIndex)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 7:
            case 10:
                strike.TypeOfAttack = 1;//剑伤
                break;

            case 6:
                strike.TypeOfAttack = 3;//冻结
                break;

            case 8:
                strike.TypeOfAttack = 4;//火伤
                break;

            case 9:
                strike.TypeOfAttack = 2;//闪电
                break;
        }


    }//存档形式赋值皮肤数值

    #endregion

    void FixedUpdate()
    {




        if (!isDie)
        {

            if (MakeSureIsPatrol) { isPatrol = true; }//碰到已死玩家强制巡逻

            BaseMove();//站走跑攻射


            if (isKeepWeapon)
            {
                WeaponDrawn();//持械切换
            }



            AntiOverlapping.SetActive(true);//站起后无法被穿过
            //rbody.simulated = true;

            if (isBurning)
            {
                BurnTimer += Time.deltaTime;

                if (BurnTimer >= 0.2f)
                {
                    currentHealth = Mathf.Clamp(currentHealth - 10, 0, maxHealth);
                    UpdateHealthBar(currentHealth, maxHealth);

                    //显示伤害
                    HudText.HUD(-10);

                    BurnTimer = 0;

                    if (currentHealth <= 0)
                    {
                        Die();
                    }
                }
            }//持续灼烧伤害

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


        //只要是法师且处于攻击就使用魔法阵
        if (isAttack && isMage) { ShowMagicEffect(); } else { HideMagicEffect(); }



        //始终跟随目标
        if (CurrentTarget != null)
        {
            _Target.transform.position = CurrentTarget.transform.position;

        }



        // 每帧更新剑物体的旋转
        Strike_Effect.transform.Rotate(0, 0, 100 * Time.deltaTime);


        //当这些动画在播放的时候玩家不能移动
        // AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        // if (
        //     state.IsName(GetAnimPrefix() + "Attack_1") ||
        //     state.IsName(GetAnimPrefix() + "Attack_2") ||
        //     state.IsName(GetAnimPrefix() + "Attack_3") ||
        //     state.IsName(GetAnimPrefix() + "Attack_4") ||
        //     state.IsName(GetAnimPrefix() + "Shoot_1") ||
        //     state.IsName(GetAnimPrefix() + "Spell_1") ||
        //     state.IsName(GetAnimPrefix() + "Spell_2") ||
        //
        //     state.IsName(GetAnimPrefix() + "Strike_Block") ||
        //     state.IsName(GetAnimPrefix() + "Shoot_Block") ||
        //
        //     state.IsName(GetAnimPrefix() + "Default_Die") ||
        //     state.IsName(GetAnimPrefix() + "Default_Die_2") ||
        //     state.IsName(GetAnimPrefix() + "Default_GetUp") ||
        //     state.IsName(GetAnimPrefix() + "Default_Hurt")
        //     )
        // {
        //     aiPath.canMove = false;
        //
        // }
        // else
        // {
        //     aiPath.canMove = true;
        // }


        //不知道什么原因，敌人滑跪可能是这个强制可移动产生的原因

    }

    public bool MakeSureIsPatrol = false;//这个是强制巡逻(同时监视碰撞检测只执行一次)
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
    private int StopX, StopY;
    int moveSpeed = 0;//改动画器用的

    public Rigidbody2D rbody;//声明刚体

    public AIPath aiPath;// A* 路径控制器

    public GameObject Arrow;//小地图朝向

    [Header("速度岔开")]
    float RunSpeed = 4f;
    float WalkSpeed = 2f;
    float StopDir = 1f;//队友在玩家身边停止的位置岔开

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
                    ChangeHealth(-maxHealth, 0);
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

                        if (dist > StopDir)
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

        CheckJump();

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
            StopX = Mathf.RoundToInt(inputX);
            StopY = Mathf.RoundToInt(inputY);
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

    private void OnTriggerStay2D(Collider2D collision)//检测到玩家显示
    {

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().currentHealth <= 0 && !MakeSureIsPatrol)
            {
                if (collision.gameObject.GetComponent<Player>().isRape == false)
                {
                    isRape = true;
                    //anim.Play("RBQ_Punish_Rape");

                    switch (Random.Range(1, 9))
                    {
                        case 1:
                            anim.Play("CG/CG_OnanismFront_1");
                            break;
                        case 2:
                            anim.Play("CG/CG_OnanismSide_1");
                            break;


                        case 3:
                            anim.Play("CG/CG_GagSide_1");
                            break;
                        case 4:
                            anim.Play("CG/CG_FistingFront_1");
                            break;


                        case 5:
                            anim.Play("CG/CG_RapeFront_1");
                            break;
                        case 6:
                            anim.Play("CG/CG_RapeSide_1");
                            break;
                        case 7:
                            anim.Play("CG/CG_AssaultFront_1");
                            break;
                        case 8:
                            anim.Play("CG/CG_AssaultSide_1");
                            break;

                    }


                    gameObject.transform.position = collision.gameObject.transform.position;//敌人拉到玩家位置


                    collision.gameObject.GetComponent<Player>().characterSkin.HideSkeleton();//隐藏玩家


                    collision.gameObject.GetComponent<Player>().isRape = true;

                    rbody.simulated = false;//当捕获折磨玩家挂的时候，不能移动

                    #region
                    // 只获取 YYY 部位的皮肤
                    int yHead = player.YYY_headIndex;
                    int yEyes = player.YYY_eyesIndex;
                    int yBody = player.YYY_bodyIndex;
                    int yLegs = player.YYY_legsIndex;
                    int yHat = player.YYY_hatIndex;

                    // 读取敌人自己原本的其他部位
                    int mHead = Man_headIndex;
                    int mBody = Man_bodyIndex;
                    int mHat = Man_hatIndex;

                    int gHead = Girl_headIndex;
                    int gEyes = Girl_eyesIndex;
                    int gBody = Girl_bodyIndex;
                    int gLegs = Girl_legsIndex;
                    int gHat = Girl_hatIndex;

                    int weapon = weaponIndex;

                    // 调用保存方法
                    SaveCurrentSkin(
                        yHead, yEyes, yBody, yLegs, yHat,
                        mHead, mBody, mHat,
                        gHead, gEyes, gBody, gLegs, gHat,
                        weapon
                    );
                    #endregion
                }

                MakeSureIsPatrol = true;

            }

        }//敌人捕获玩家
    }

    public void ReadyToSayThankYou()
    {
        Invoke("SayThankYou", 0.2f);
    }

    void SayThankYou()
    {

        if (Random.Range(0, 2) == 0)
        {
            frameEvents._01_Word_ThankYou_1();
        }
        else
        {
            frameEvents._01_Word_ThankYou_2();
        }
    }//谢谢声（让产生的队友说）

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
                visionType = EnemyType.ShortRangeEnemy; isMage = false;//战士
                break;
            case 1:
                visionType = EnemyType.LongRangeEnemy; isMage = false;//射手
                break;
            case 2:
                visionType = EnemyType.LongRangeEnemy; isMage = true;//法师
                break;
        }
    }

    public EnemyClass Class;
    public enum EnemyClass
    {
        Girl,
        Man,
        Succubus,
        Monster,
        Tentacle_Monster,
        Tentacle_Bug,
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
            case 3:
                Class = EnemyClass.Monster;
                break;
            case 4:
                Class = EnemyClass.Tentacle_Monster;
                break;
            case 5:
                Class = EnemyClass.Tentacle_Bug;
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
            case EnemyClass.Monster:
                return "Monster_";
            case EnemyClass.Tentacle_Monster:
                return "Tentacle_Monster_";
            case EnemyClass.Tentacle_Bug:
                return "Tentacle_Bug_";

            // 未来扩展：Tentacle, Demon 等
            default:
                return "";
        }
    }


    [Header("远程射手/法师")]
    public bool isMage = false;//false 射手   true 法师

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
                if (isMage) { anim.SetInteger("Weapon", 3); }
                else { anim.SetInteger("Weapon", 2); }
                break;
        }

        anim.SetTrigger("DrawWeapon");


        CheckWeapon();//根据当前皮肤，代码层面武器确认
    }
    public void Sheathe()
    {
        //anim.SetInteger("Weapon", 0);

        anim.ResetTrigger("DrawWeapon");    // 重置状态，避免残留
        anim.SetTrigger("SheatheWeapon");
    }

    void ReSetAttack()
    {
        if (currentHealth > 0)
        {
            if (Class == EnemyClass.Succubus|| Class == EnemyClass.Monster || Class == EnemyClass.Tentacle_Monster || Class == EnemyClass.Tentacle_Bug) { anim.Play(GetAnimPrefix() + "Default_Idle"); return; }//只有魔族和变异体需要更改

            switch (visionType)
            {

                case EnemyType.ShortRangeEnemy:
                    anim.Play(GetAnimPrefix() + "Strike_Idle");
                    break;


                case EnemyType.LongRangeEnemy:
                    if (isMage) { anim.Play(GetAnimPrefix() + "Spell_Idle"); }
                    else { anim.Play(GetAnimPrefix() + "Shoot_Idle"); }
                    break;

            }
        }



    }

    #endregion


    public bool CanChangeSkin = false;


    /// <summary>
    /// 皮肤
    /// </summary>
    #region
    [Header("皮肤")]
    public CharacterSkin characterSkin;

    public int YYY_headIndex;
    public int YYY_eyesIndex;
    public int YYY_bodyIndex;
    public int YYY_legsIndex;
    public int YYY_hatIndex;

    public int Man_headIndex;
    public int Man_bodyIndex;
    public int Man_hatIndex;

    public int Girl_headIndex;
    public int Girl_eyesIndex;
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
        YYY_eyesIndex = Random.Range(1, 14);  // 1~13
        YYY_bodyIndex = Random.Range(10, 13);
        YYY_legsIndex = Random.Range(10, 13);

        int[] YYY_pool = { 1, 2, 3, 4, 10, 11, 12 };
        Girl_hatIndex = YYY_pool[UnityEngine.Random.Range(0, YYY_pool.Length)];





        Man_headIndex = Random.Range(1, 6);
        Man_bodyIndex = 2;
        Man_hatIndex = Random.Range(1, 3);

        Girl_headIndex = Random.Range(1, 14);  // 1~13
        Girl_eyesIndex = Random.Range(1, 14);  // 1~13
        Girl_bodyIndex = Random.Range(10, 13);
        Girl_legsIndex = Random.Range(10, 13);

        int[] Girl_pool = { 1, 2, 3, 4, 10, 11, 12 };
        Girl_hatIndex = Girl_pool[UnityEngine.Random.Range(0, Girl_pool.Length)];

        weaponIndex = Random.Range(1, 11);


        SetSkin();
    }


    public void SaveCurrentSkin
        (
           int _YYY_headIndex, int _YYY_eyesIndex, int _YYY_bodyIndex, int _YYY_legsIndex, int _YYY_hatIndex,
           int _Man_headIndex, int _Man_bodyIndex, int _Man_hatIndex,
           int _Girl_headIndex, int _Girl_eyesIndex, int _Girl_bodyIndex, int _Girl_legsIndex, int _Girl_hatIndex,
           int _weaponIndex

        )
    {
        // 保存 YYY 部位
        YYY_headIndex = _YYY_headIndex;
        YYY_eyesIndex = _YYY_eyesIndex;
        YYY_bodyIndex = _YYY_bodyIndex;
        YYY_legsIndex = _YYY_legsIndex;
        YYY_hatIndex = _YYY_hatIndex;

        // 保存 Man 部位
        Man_headIndex = _Man_headIndex;
        Man_bodyIndex = _Man_bodyIndex;
        Man_hatIndex = _Man_hatIndex;

        // 保存 Girl 部位
        Girl_headIndex = _Girl_headIndex;
        Girl_eyesIndex = _Girl_eyesIndex;
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
            YYY_headIndex, YYY_eyesIndex, YYY_bodyIndex, YYY_legsIndex, YYY_hatIndex,
            Man_headIndex, Man_bodyIndex, Man_hatIndex,
            Girl_headIndex, Girl_eyesIndex, Girl_bodyIndex, Girl_legsIndex, Girl_hatIndex,
            weaponIndex
            );

        CheckWeapon();
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

                        if (isMage)
                        {
                            //队友使用玩家的攻击动画
                            if (MageAttackType)
                            {
                                if (tag == "Friend") { anim.Play(GetAnimPrefix() + "Spell_1", 0, 0); }
                                else { anim.Play(GetAnimPrefix() + "spell_1", 0, 0); }
                            }
                            else
                            {
                                if (tag == "Friend") { anim.Play(GetAnimPrefix() + "Spell_2", 0, 0); }
                                else { anim.Play(GetAnimPrefix() + "spell_2", 0, 0); }
                            }
                            MageAttackType = !MageAttackType;

                        }
                        else
                        {
                            //队友使用玩家的攻击动画
                            if (tag == "Friend") { anim.Play(GetAnimPrefix() + "Shoot_1", 0, 0); }
                            else { anim.Play(GetAnimPrefix() + "shoot_1", 0, 0); }

                        }

                        break;
                }

                //20攻击帧相当于0.7
                Invoke("Attack_Cancel", 0.7f);//一旦动画帧事件被跳过就会站着不动不攻击，所以这个还是Invoke触发(触发检测生命值，防止倒地上后突然站起来攻击)

                attackTimer = 0f;


                isInAttackDelay = true;


            }


            isKeepWeapon = true;//没有持械的话进入持械状态


        }

    }

    bool MageAttackType = false;

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
            if (Class == EnemyClass.Monster || Class == EnemyClass.Tentacle_Monster || Class == EnemyClass.Tentacle_Bug)
            {
                anim.Play(GetAnimPrefix() + "attack_1", 0, 0);
            }
            else
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
            


        }
        else
        {

            if (Class == EnemyClass.Monster || Class == EnemyClass.Tentacle_Monster || Class == EnemyClass.Tentacle_Bug) 
            {
                anim.Play(GetAnimPrefix() + "attack_1", 0, 0);
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

           
        }




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

    public void BattleCryVoice()
    {
        switch (Class)
        {
            case EnemyClass.Girl:
            case EnemyClass.Succubus:
                switch (Random.Range(0, 4))
                {
                    case 0:
                        frameEvents._JK_attack1();
                        break;
                    case 1:
                        frameEvents._JK_attack2();
                        break;
                    case 2:
                        frameEvents._JK_attack3();
                        break;
                    case 3:
                        frameEvents._JK_attack4();
                        break;
                }//女性
                break;
            case EnemyClass.Man:
                frameEvents._Man_attack();//男性
                break;

            case EnemyClass.Monster:
            case EnemyClass.Tentacle_Monster:
                switch (Random.Range(0, 2))
                {
                    case 0:
                        frameEvents._Zombie_Summon_1();
                        break;
                    case 1:
                        frameEvents._Zombie_Summon_2();
                        break;
                }//感染者 变异体
                break;

            case EnemyClass.Tentacle_Bug:
                switch (Random.Range(0, 2))
                {
                    case 0:
                        frameEvents._Orangutan_Summon_1();
                        break;
                    case 1:
                        frameEvents._Orangutan_Attack_1();
                        break;
                }//肉翅蜂
                break;


             
                //case 5:
                //case 6:
                //case 7:
                //    switch (Random.Range(0, 3))
                //    {
                //        case 0:
                //            frameEvents._monster_Summon_01();
                //            break;
                //        case 1:
                //            frameEvents._monster_Summon_02();
                //            break;
                //        case 2:
                //            frameEvents._Shrike_Summon_Attack();
                //            break;
                //    }//肉袋 淫毒肉炮
                //    break;
        }
    }//近战攻击发出的叫声

    #endregion


    /// <summary>
    /// 射击系统
    /// </summary>
    #region

    [Header("射击攻击")]
    public GameObject transparentBulletPrefab;
    public Transform bulletSpawnPoint;

    int special;//暂时储存子弹类型
    public void ShootBullet()
    {
        if (CurrentTarget == null) return;

        Vector3 dir = (CurrentTarget.transform.position - bulletSpawnPoint.position).normalized;

        // 🟢 更新角色面向方向（动画参数）
        UpdateFacingDirection(dir);

        var go = Instantiate(transparentBulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        var s = go.GetComponent<Shooting>();



        switch (CurrentWeapon)
        {



            case 201:
            case 207:
                special = 5;//剧毒法球
                break;
            case 203:
            case 210:
                special = 3;//火焰法球
                break;
            case 204:
            case 206:
            case 208:
                special = 4;//冰冻法球
                break;
            case 205:
            case 209:
            case 202:
                special = 2;//雷电法球
                break;

            case 101:
            case 102:
            case 103:
                frameEvents._Bullet_Arrow();
                special = 1;//弩弓
                break;


            case 104:
                special = 0;//子弹
                frameEvents._Bullet_Pistol();
                break;
            case 105:
                special = 0;//子弹
                frameEvents._Bullet_Pistol_2();
                break;
            case 106:
                special = 0;//子弹
                frameEvents._Bullet_Pistol_3();
                break;
            case 107:
                special = 0;//子弹
                frameEvents._Bullet_AK();
                break;
            case 108:
            case 109:
            case 110:
                special = 0;//子弹
                frameEvents._Bullet_SD();
                break;


            default:
                special = 0;//子弹
                break;
        }

        if (tag == "Friend")
        {
            s.Init(-ShootDamage, -SpellDamage, false, 0, special, dir, Shooting.BulletOwnerType.Friend);//角色数值＋武器数值的基础伤害，暴击，蓄力时间，子弹类型，方位，子弹所有者
        }
        else
        {
            s.Init(-ShootDamage, -SpellDamage, false, 0, special, dir, Shooting.BulletOwnerType.Enemy);//角色数值＋武器数值的基础伤害，暴击，蓄力时间，子弹类型，方位，子弹所有者
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
    /// 武器系统
    /// </summary>
    #region
    [Header("武器系统")]
    public int CurrentWeapon;
    public int CurrentProfession;//0战士 1射手 2法师

    //0无武器
    //1铁剑  2阔剑  3长柄双刃斧  4长枪   5长柄斧   6冻结剑   7黑铁刺剑  8熔岩剑  9引雷剑  10古重剑
    //101轻弩   102重弩   103复合弩   104火绳复合枪  105火绳短枪   106火绳长枪   107火绳黄铜枪
    //201黄木短杖  202鹰身短杖   203红宝石短杖    204蓝宝石短杖   205黄玉短杖   206冰冻法杖  207紫水晶法杖  208翡翠法杖  209雷霆法杖  210古木法杖

    public void CheckWeapon()
    {

        if (visionType == EnemyType.ShortRangeEnemy) { CurrentWeapon = weaponIndex; }//实装战士武器
        if (visionType == EnemyType.LongRangeEnemy && !isMage) { CurrentWeapon = weaponIndex + 100; }//实装射手武器
        if (visionType == EnemyType.LongRangeEnemy && isMage) { CurrentWeapon = weaponIndex + 200; }//实装法师武器

        if (isMage)
        {
            switch (CurrentWeapon)
            {


                case 201:
                case 207:
                    ChangeMagicEffectColor(5);//剧毒法球
                    break;
                case 203:
                case 210:
                    ChangeMagicEffectColor(2);//火焰法球
                    break;
                case 204:
                case 206:
                case 208:
                    ChangeMagicEffectColor(4);//冰冻法球
                    break;
                case 205:
                case 209:
                case 202:
                    ChangeMagicEffectColor(3);//雷电法球
                    break;
            }
        }
    }
    public GameObject ExitEffect;//施法粒子特效（出现消失）
    public ParticleSystem exitEffect;//施法粒子特效(改变颜色)



    public Animator MagicFormationAnim;//魔法阵
    public SpriteRenderer MagicFormation;//魔法阵样式
    public Sprite Magic_Fire, Magic_Electricity, Magic_Ice, Magic_Poison;

    public void ChangeMagicEffectColor(int ColorNumber)
    {
        switch (ColorNumber)
        {
            case 2:
                MagicFormation.sprite = Magic_Fire;
                var main = exitEffect.main;
                main.startColor = new Color(1f, 0.5f, 0f); //橘黄色
                break;
            case 3:
                MagicFormation.sprite = Magic_Electricity;
                var main2 = exitEffect.main;
                main2.startColor = Color.yellow;
                break;
            case 4:
                MagicFormation.sprite = Magic_Ice;
                var main3 = exitEffect.main;
                main3.startColor = Color.cyan;
                break;
            case 5:
                MagicFormation.sprite = Magic_Poison;
                var main4 = exitEffect.main;
                main4.startColor = new Color(0.5f, 0f, 0.5f); //紫色
                break;
        }
    }


    public void ShowMagicEffect()
    {
        //exitEffect.Play();
        ExitEffect.SetActive(true);
        MagicFormationAnim.SetBool("Show", true);
    }
    public void HideMagicEffect()
    {
        //exitEffect.Stop();
        ExitEffect.SetActive(false);
        MagicFormationAnim.SetBool("Show", false);
    }

    [Header("基础与武器装备结合后数值")]
    int MeleeDamage = 100;
    int ShootDamage = 100;
    int SpellDamage = 100;

    int CurrentWeaponPower = 10;    // 武器攻击值
    int CurrentArmorDefence = 10;      // 衣服防御值
    int CurrentStockingDefence = 10;   // 丝袜防御值
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
    /// 击飞系统
    /// </summary>
    #region
    [Header("模拟跳跃")]
    // 模拟跳跃高度
    float zHeight = 0f;
    float zVelocity = 0f;
    float gravity = -20f; // 可以调成 -20f 更快落下
    float jumpForce = 7f;//原来是5f

    // 角色跳跃偏移对象（Spine动画对象）
    float groundZ = 0f; // 初始化地面位置
    bool wasInAir = false; // 前一帧是否在空中
    public void PlayJump()
    {
        if (IsGrounded())
        {
            Debug.Log("跳跃");
            zVelocity = jumpForce;
            frameEvents._SE_Clothes();
        }


    }

    void CheckJump()
    {
        // 应用重力
        zVelocity += gravity * Time.deltaTime;
        zHeight += zVelocity * Time.deltaTime;



        bool isGroundedNow = zHeight <= 0f;

        //落地
        if (isGroundedNow)
        {
            if (wasInAir) // 刚刚落地的那一帧
            {
                frameEvents._Effect_falldown();// 播放落地音效等逻辑
                Knockdown();//落地一帧触发
            }



            zHeight = 0f;
            zVelocity = 0f;
            groundZ = transform.position.z;



            // ✅ 落地时重置击飞速度
            knockbackX = 0f;
            knockbackY = 0f;
        }

        if (zHeight > 0f)
        {
            Vector3 pos = transform.position;
            pos.z = groundZ - zHeight;
            transform.position = pos;

        }


        // 更新前一帧状态
        wasInAir = !isGroundedNow;





        //被击飞
        if (!IsGrounded() && (knockbackX != 0f || knockbackY != 0f))
        {

            // 计算潜在新位置
            Vector3 nextPos = transform.position - new Vector3(knockbackX, knockbackY, 0f) * Time.deltaTime;

            // 发射射线检测墙壁（Room Layer）
            Vector2 direction = nextPos - transform.position;
            float distance = direction.magnitude;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, distance, LayerMask.GetMask("Room"));

            //当这些动画在播放的时候玩家不能移动
            //AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            //
            //if (state.IsName(GetAnimPrefix() + "Default_Die") ||
            //    state.IsName(GetAnimPrefix() + "Default_Die_2") ||
            //    state.IsName(GetAnimPrefix() + "Default_GetUp") ||
            //    state.IsName(GetAnimPrefix() + "Default_Hurt")
            //)
            //{
            //    // ❌ 撞到了墙壁，阻止移动
            //    knockbackX = 0f;
            //    knockbackY = 0f;
            //
            //    // 你也可以在这里播放一个“撞墙反弹”的音效或特效
            //}
            //else if (hit.collider == null)
            //{
            //
            //    // ✅ 没有撞墙，可以移动
            //    Vector3 move = new Vector3(knockbackX, knockbackY, 0f) * Time.deltaTime;
            //    transform.position -= move;
            //
            //}

            if (hit.collider == null)
            {
                // ✅ 没有撞墙，可以移动
                transform.position = nextPos;
            }
            else
            {
                // ❌ 撞到了墙壁，阻止移动
                knockbackX = 0f;
                knockbackY = 0f;

                // 你也可以在这里播放一个“撞墙反弹”的音效或特效
            }

        }



    }

    public bool IsGrounded()
    {
        return zHeight <= 0.01f; // 只要高度为 0 即为落地
    }




    [Header("被击飞")]
    float knockbackX = 0f; // X方向击飞
    float knockbackY = 0f; // Y方向击飞

    public void Knockback(float forceX, float forceY = 0f)
    {
        knockbackX = forceX;
        knockbackY = forceY;
        zVelocity = jumpForce; // 上弹
    }



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
    public GameObject Palsy_Effect;//闪电特效
    public GameObject ThunderEffect;//麻痹特效
    public GameObject Frozen_Effect;//冻结特效
    public GameObject IceEffect;//冰特效
    public GameObject Burning_Effect;//灼烧特效
    public GameObject FireEffect;//烧特效
    public GameObject ProtectiveCoverEffect;//防护罩特效

    public GameObject Floor_Blood_0, Floor_Blood_1, Floor_Blood_2, Floor_Blood_3;

    [Header("生命值体力值等数值")]
    public int currentHealth;
    public int maxHealth;

    //伤害显示
    public bool isScreaming;
    public HudText HudText;



    public void ChangeHealth(int amount, int TypeOfAttack)//【攻击方式  0无  1剑击特效  2闪电特效  3冻结  4灼烧  5毒物
    {

        if (!isScreaming && !isRape && IsGrounded())//冷却中与捕获中不会被伤到,在空中也不会被伤到
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

                //防护检测
                amount += CurrentArmorDefence;
                amount += CurrentStockingDefence;

                

            }
            //else 
            //{
            //    //回血在这里
            //    currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            //    UpdateHealthBar(currentHealth, maxHealth);
            //
            //    //显示伤害
            //    HudText.HUD(amount);
            //
            //    return;
            //
            //}

            //一直发生防御超过攻击回血情况
            if (amount >= 0)
            {
                amount = 0;
            }


            //伤害类型
            switch (TypeOfAttack)
            {
                case 1:
                    Strike_Effect.SetActive(true);//剑伤害
                    break;
                case 2:
                    if (Random.Range(0, 3) == 0)
                    {
                        Palsy(1);//麻痹伤害
                    }
                    else
                    {
                        Palsy_Effect.SetActive(true);//雷电伤害
                    }
                    break;
                case 3:
                    if (Random.Range(0, 3) == 0)
                    {
                        Freeze(1);//冻结伤害
                    }
                    else
                    {
                        Vector3 offset_2 = new Vector3(0, 0, 2); // 这里的1表示沿Z轴上升的距离，可以根据需要调整
                        Vector3 spawnPosition_2 = transform.position + offset_2;
                        GameObject EffectPrefabs = Instantiate(IceEffect, spawnPosition_2, transform.rotation);
                        Destroy(EffectPrefabs, 0.5f);
                    }
                    break;
                case 4:
                    if (Random.Range(0, 2) == 0)
                    {
                        Burning(Random.Range(1, 8));//灼烧伤害
                    }
                    else
                    {
                        Vector3 offset_2 = new Vector3(0, 0, 2); // 这里的1表示沿Z轴上升的距离，可以根据需要调整
                        Vector3 spawnPosition_2 = transform.position + offset_2;
                        GameObject EffectPrefabs = Instantiate(FireEffect, spawnPosition_2, transform.rotation);
                        Destroy(EffectPrefabs, 0.5f);
                    }

                    break;

                case 5:

                    //毒特效
                    if (amount >= 0)
                    {
                        amount = 0;
                    }
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

            if (!isDie && currentHealth > 0)
            {
                if (Random.Range(0, 2) == 0)
                {
                    Knockdown();//普通攻击随机击倒
                }
                else
                {

                    //击飞
                    if (StopX < 0)
                        Knockback(forceX: -3f);
                    else if (StopX > 0)
                        Knockback(forceX: 3f);
                    else if (StopY < 0)
                        Knockback(forceX: 0, forceY: -3f);
                    else if (StopY > 0)
                        Knockback(forceX: 0, forceY: 3f);




                    //PlayJump();

                    //受伤动画
                    anim.Play(GetAnimPrefix() + "Default_Hurt");
                    Invoke("ReSetAttack", 0.5f);//防止动画回不去
                }
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

        if (Class == EnemyClass.Succubus || isMage)
        {
            ProtectiveCoverEffect.SetActive(true);
            switch (Random.Range(0, 3))
            {
                case 0:
                    ProtectiveCoverEffect.GetComponent<Animator>().SetInteger("Color", 0);
                    break;
                case 1:
                    ProtectiveCoverEffect.GetComponent<Animator>().SetInteger("Color", 1);
                    break;
                case 2:
                    ProtectiveCoverEffect.GetComponent<Animator>().SetInteger("Color", 2);
                    break;
            }
        }//只有魔族和法师需要特效（遮挡无防御动画）

        if (visionType == EnemyType.ShortRangeEnemy)
        {
            anim.Play(GetAnimPrefix() + "Strike_Block");
        }
        else
        {
            if (isMage)
            {
                anim.Play(GetAnimPrefix() + "Spell_Block");
            }
            else
            {
                anim.Play(GetAnimPrefix() + "Shoot_Block");
            }


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
        if (IsGrounded()) { Knockdown(); }//敌人必须站在地上才能被暴击击倒




        Time.timeScale = 0;


        Critial.SetActive(true);//显示暴击


        player.ChangeCritical(-player.maxCritical);//暴击清零

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
        //敌人受伤玩家获取经验
        player.ChangeExperience(100);

        isDie = true;
        anim.Play(GetAnimPrefix() + "Default_Die_2");//防止倒下又起来,搞了第二死亡

        Invoke("Disappear", 1f);

    }//死亡


    [Header("全部自身存在与出生点WallMap")]
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
    /// 异常状态
    /// </summary>
    #region
    public void Recover()//死亡，自我恢复，麻痹恢复调用
    {

        aiPath.canMove = true;

        // 恢复物理移动
        rbody.constraints = RigidbodyConstraints2D.None;
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation; // 恢复默认状态（通常冻结旋转即可）

        Frozen_Effect.SetActive(false);//去除冻结特效

        anim.speed = 1f; // 恢复到原来的时间缩放值，解除冻结



        Burning_Effect.SetActive(false);//去除灼烧特效
        isBurning = false;

    }

    //————————————————————麻痹

    public void Palsy(int Timer)
    {
        ThunderEffect.SetActive(true);

        Invoke("ThunderDamager", Timer);
    }
    void ThunderDamager()
    {
        ChangeHealth(-Random.Range(100, 500), 2);
        ThunderEffect.SetActive(false);
    }
    //————————————————————冻结

    public void Freeze(int Timer)
    {

        anim.speed = 0f;// 将动画速度设置为0，冻结动画
        Frozen_Effect.SetActive(true);//在受到冰冻伤害的时候就已经非处冰冻特效了,这里再写一遍因为有些时候敌人挡住了伤害，这里的冰冻是无法被挡住的  




        aiPath.canMove = false;

        // 保留物理模拟，只冻结移动
        rbody.velocity = Vector2.zero;
        rbody.constraints = RigidbodyConstraints2D.FreezeAll;

        Invoke("Recover", Timer);
    }

    //————————————————————灼烧
    public bool isBurning = false;
    float BurnTimer;
    public void Burning(int Timer)
    {
        Burning_Effect.SetActive(true);

        isBurning = true;

        Invoke("Recover", Timer);
    }
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


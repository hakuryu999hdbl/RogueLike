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


        StartCoroutine(RandomWalkRun());//隔段时间随机走跑



        if (currentSaveName == "")
        {
            // 随机从 Enum 中选择一个值
            //visionType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);

            //visionType = EnemyType.LongRangeEnemy;
            //visionType = EnemyType.ShortRangeEnemy;

            CurrentProfession = Random.Range(0, 2);//把CurrentProfession绑进去(法师只有女性可当)




            //随机皮肤
            if (CanChangeSkin)
            {



                switch (BossNumber)
                {
                    default:
                    case 0:



                        SetRandomSkin();
                        // 随机从 Enum 中选择一个值
                        Class = (EnemyClass)Random.Range(0, System.Enum.GetValues(typeof(EnemyClass)).Length);

                        //小怪中暂时不出现Demon(不知道为什么会对多米纳斯召唤物产生影响)
                        //if (Class == EnemyClass.Demon){ Class = EnemyClass.Man; }


                        if (BecomeSoldier_Man)
                        {
                            Class = EnemyClass.Man;
                        }//召集男性士兵（守卫队长/皇太子/1~3关）
                        if (BecomeSoldier_Girl)
                        {
                            Class = EnemyClass.Girl;
                        }//召集女性士兵（首席战斗修女/5~7关）
                       
                        if (BecomeTentacleMonster)
                        {

                            switch (UnityEngine.Random.Range(0, 5))
                            {
                                case 0:
                                    Class = EnemyClass.Monster;
                                    break;
                                case 1:
                                    Class = EnemyClass.Tentacle_Monster;
                                    break;
                                case 2:
                                    Class = EnemyClass.Tentacle_Bug;
                                    break;
                                case 3:
                                    Class = EnemyClass.Tentacle_Bag;
                                    break;
                                case 4:
                                    Class = EnemyClass.Tentacle_HermitCrab;
                                    break;
                            }//多米纳斯Boss战

                            if(GameFlowData.nextScene == "Story_08")
                            {
                                Class = EnemyClass.RBQ;

                            }//在对战摩尔根的时候，只有肉货和寄生虫肉便器
                           

                        }//召集触手怪（宰相摩尔根和多米纳斯/9~11关）
                        if (BecomeFleshArmor)
                        { 
                            Class = EnemyClass.FleshArmor;
                        }//召集肉铠（典狱长）



                        //Class = EnemyClass.Succubus;
                        //Class = EnemyClass.Girl;
                        //Class = EnemyClass.Man;
                        //Class = EnemyClass.Monster;
                        //Class = EnemyClass.Tentacle_Monster;
                        //Class = EnemyClass.Tentacle_Bug;
                        //Class = EnemyClass.Tentacle_Bag;
                        //Class = EnemyClass.Tentacle_HermitCrab;
                        //Class = EnemyClass.HermitCrab;
                        //Class = EnemyClass.RBQ;
                        //Class = EnemyClass.FleshArmor;
                        //Class = EnemyClass.Demon;





                        if (Class == EnemyClass.Girl && visionType == EnemyType.LongRangeEnemy && Random.Range(0, 2) == 0)
                        {
                            CurrentProfession = 2;

                        }//一部分远程女射手变成女法师


                        if (Class == EnemyClass.Monster || Class == EnemyClass.Tentacle_Monster || Class == EnemyClass.Tentacle_Bug || Class == EnemyClass.HermitCrab || Class == EnemyClass.RBQ || Class == EnemyClass.FleshArmor || Class == EnemyClass.Demon)
                        {

                            CurrentProfession = 0;


                        }//这部分怪物只能近战

                        if (Class == EnemyClass.Tentacle_Bug)
                        {

                            strike.TypeOfAttack = 2;//闪电

                        }//肉翅虫具有麻痹效果

                        if (Class == EnemyClass.FleshArmor)
                        {
                            RunSpeed = Random.Range(1, 3);
                            WalkSpeed = Random.Range(1, 3);

                            //Man_headIndex = Random.Range(1, 5);//除去 皇子和皇帝
                            Man_bodyIndex = 2;//盔甲
                            Man_hatIndex = 6;//绷带

                            SetSkin();

                            //随机延后喘息声
                            Invoke("Delay_Breath_Voice", Random.Range(1, 2.5f));


                  

                        }//肉铠只能走  且固定Hat皮肤



                        if (Class == EnemyClass.Demon)
                        {
                            RunSpeed = Random.Range(1, 3);
                            WalkSpeed = Random.Range(1, 3);

                     

                            

                        }//恶魔只能走 



                        if (Class == EnemyClass.RBQ|| Class == EnemyClass.HermitCrab)
                        {
                            //随机延后喘息声
                            Invoke("Delay_Breath_Voice", Random.Range(1, 2.5f));

                            //isPatrol = true;

                        }//肉货和寄生肉便器也有呻吟,而且不攻击只巡逻随机产卵



                        ChangeType(CurrentProfession);//把CurrentProfession绑进去
                        SetAttackRange();

                        break;
                    case 1:
                        BecomeBoss_Captain();

                        if(GameFlowData.nextScene!= "Story_01")
                        {
                            // 每隔 5 秒执行一次 Boss技能 召集士兵
                            InvokeRepeating(nameof(BossSkill_CallSoldier), 3f, 5f);
                        
                        }//第一关的守卫队长不召集敌人



                        break;
                    case 2:
                        BecomeBoss_Selene();
                        break;
                    case 3:
                        BecomeBoss_Selene_2();
                        break;
                    case 4:
                        BecomeBoss_Morgan();

                        //随机延后喘息声
                        Invoke("Delay_Breath_Voice", Random.Range(1, 2.5f));

                        // 每隔 5 秒执行一次 Boss技能 召集触手怪物
                        InvokeRepeating(nameof(BossSkill_CallTentacleMonster), 3f, 5f);

                        break;
                    case 5:
                        BecomeBoss_Alexis();


                        // 每隔 5 秒执行一次 Boss技能 召集士兵
                        InvokeRepeating(nameof(BossSkill_CallSoldier), 5f, 10f);


                     

                        break;
                    case 6:
                        BecomeBoss_Dominus();

                        // 每隔 5 秒执行一次 Boss技能 召集触手怪物
                        InvokeRepeating(nameof(BossSkill_CallTentacleMonster), 3f, 5f);

                     

                        break;

                    case 7:
                        BecomeBoss_DarkMage();

                        //召唤暗影
                        InvokeRepeating(nameof(BossSkill_ToPlayerPlace), 5f, 10f);

                        break;

                    case 8:
                        BecomeBoss_Warden();

                        // 每隔 5 秒执行一次 Boss技能 召集肉铠
                        InvokeRepeating(nameof(BossSkill_CallFleshArmor), 5f, 10f);

                        //随机延后喘息声
                        Invoke("Delay_Breath_Voice", Random.Range(1, 2.5f));


                    


                        break;

                    case 9:
                        BecomeBoss_CombatNun();

                        if (GameFlowData.nextScene != "Story_07")
                        {
                            // 每隔 5 秒执行一次 Boss技能 召集惩戒修女
                            InvokeRepeating(nameof(BossSkill_CallSoldier_Girl), 5f, 10f);

                        }//第六关的首席战斗修女不召集敌人


       
                       

                        break;
                }



            }


            //ToDo:削减敌人攻击力加成
            //根据玩家当前的等级赋予敌人生命值，攻击力等

            //CurrentWeaponPower = player.Level * Random.Range(10, 20);
            //
            ////近战攻击修改
            //MeleeDamage = 100 + player.Level * 10;
            //strike.Damage = -CurrentWeaponPower - MeleeDamage;
            ////远程攻击修改
            //ShootDamage = CurrentWeaponPower + 100 + player.Level * 10;
            ////攻击修改
            //SpellDamage = CurrentWeaponPower + 100 + player.Level * 10;
            //
            //
            //maxHealth += player.Level * 200;
            //currentHealth = maxHealth;






            // ==== 难度系数 ==== 
            int difficulty = PlayerPrefs.GetInt("Difficulty", 0); // 0=简单,1=一般,2=困难

            float damageMul = 1f;
            float hpMul = 1f;

            switch (difficulty)
            {
                case 0: // 简单
                    damageMul = 0.7f;
                    hpMul = 0.8f;
                    break;

                case 1: // 一般
                    damageMul = 1f;
                    hpMul = 1f;
                    break;

                case 2: // 困难
                    damageMul = 1.3f;
                    hpMul = 1.3f;
                    break;
            }

            // ==== 原有公式 + 难度加成 ====
            // 注意：这里先算“基础值”，最后乘难度，再四舍五入成 int

            // 武器基础威力（给远程/法术用）
            int baseWeaponPower = player.Level * Random.Range(10, 20);
            CurrentWeaponPower = Mathf.RoundToInt(baseWeaponPower * damageMul);

            // 近战伤害
            int baseMelee = 100 + player.Level * 10;
            MeleeDamage = Mathf.RoundToInt(baseMelee * damageMul);
            strike.Damage = -(CurrentWeaponPower + MeleeDamage);  // 仍然是负数扣血

            // 远程攻击
            int baseShoot = baseWeaponPower + 100 + player.Level * 10;
            ShootDamage = Mathf.RoundToInt(baseShoot * damageMul);

            // 法术攻击
            int baseSpell = baseWeaponPower + 100 + player.Level * 10;
            SpellDamage = Mathf.RoundToInt(baseSpell * damageMul);

            // 生命值（先在基础上加等级再乘 hpMul）
            int baseHpGain = player.Level * 200;
            maxHealth += Mathf.RoundToInt(baseHpGain * hpMul);
            currentHealth = maxHealth;



        }//如果已经赋值了队友，那么不随机

        anim.Play(GetAnimPrefix() + "Default_Idle");


        GateEffect.SetActive(true);//传送门特效


        Invoke("DelayDialogue", Random.Range(0.5f, 2.5f));
    }

    void DelayDialogue()
    {

        if (tag != "Friend")
        {
            switch (BossNumber)
            {
                default:
                case 0:
                    //一次出现很多小怪太吵限制一些
                    if (Random.Range(0, 4) == 3)
                    {
                        switch (Class)
                        {
                            case EnemyClass.Man:
                                UIManager.instance.ShowDialogue("Man");
                                break;



                            case EnemyClass.Girl:
                            case EnemyClass.Succubus:
                                UIManager.instance.ShowDialogue("Girl");
                                break;
                        }
                    }
                   

                    break;

                case 1:
                    UIManager.instance.ShowDialogue("Boss_Captain");
                    break;
                case 2:
                case 3:
                    UIManager.instance.ShowDialogue("Boss_Selene");
                    break;
                case 4:
                    UIManager.instance.ShowDialogue("Boss_Morgan");
                    break;
                case 5:
                    UIManager.instance.ShowDialogue("Boss_Alexis"); 
                    break;


                case 7:
                    UIManager.instance.ShowDialogue("Boss_DarkMage");
                    break;
                case 8:
                    UIManager.instance.ShowDialogue("Boss_Warden");
                    break;
                case 9:
                    UIManager.instance.ShowDialogue("Boss_CombatNun");
                    break;
            }


        }


    }


    void SetAttackRange()
    {


        //不同敌人攻击范围不一样
        switch (visionType)
        {
            case EnemyType.ShortRangeEnemy:
                //attackCooldown = 1f;
                enemyVision.circleCollider2D.radius = 1.5f;
                break;

            case EnemyType.LongRangeEnemy:
                //attackCooldown = 1f;
                enemyVision.circleCollider2D.radius = 4f;
                break;
        }

    }//两条通路设立范围，一条是随机出来的敌人/队友，一条是读取存档的队友

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

        ChangeType(CurrentProfession);//首先根据职业

        // 根据这些数据设置皮肤
        SetSkin(); // 你已有的方法（或自己写个用这些 Index 设置皮肤的方法）

        SetAttackRange();

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


            SetStrikeTypeOfAttack();//根据近战武器赋予特殊近战伤害

        }
        else
        {

            //法师近战攻击力急剧缩减
            strike.Damage = -data.meleeDamage / 5;
        }



    }//存档形式赋值皮肤数值

    void SetStrikeTypeOfAttack()
    {
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
    }


    #endregion

    [Header("当队友踩入魔法阵")]
    public bool InMagicCircle = false;

    void FixedUpdate()
    {
        if (isRape)
        {
            return;
        }//Rape捕获锁
        else if (!isDie)
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
                        Die();//灼烧死亡
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


        //队友踩入回血法阵
        if (InMagicCircle && currentHealth < maxHealth)
        {
            BurnTimer += Time.deltaTime;

            if (BurnTimer >= 0.2f)
            {
                RestoreHealth(100);

                BurnTimer = 0;
            }
        }


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


    float WaitTimer;//玩家不动等待时间




    bool EnemyRunning = true;
    IEnumerator RandomWalkRun()
    {
        while (true)
        {

            if (Class == EnemyClass.RBQ && currentHealth > 0 &&Random.Range(0,2)==1) { Attack_Start(); aiPath.canMove = false;Invoke(nameof(RBQCanMove), 1f); }//RBQ隔一会就产卵

            EnemyRunning = !EnemyRunning;



            // 等待下次切换（可随机间隔）
            yield return new WaitForSeconds(Random.Range(1.5f, 3f));
        }
    }
    void RBQCanMove() 
    {
        aiPath.canMove = true;
        Attack_Cancel();
    }

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
                        //moveSpeed = 2;
                        //aiPath.maxSpeed = RunSpeed;

                        if (EnemyRunning)
                        {
                            moveSpeed = 2;
                            aiPath.maxSpeed = RunSpeed;
                        }
                        else
                        {
                            moveSpeed = 1;
                            aiPath.maxSpeed = WalkSpeed;
                          
                        }


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


                            WaitTimer += Time.deltaTime;
                            //玩家不处于战斗状态或者跑步状态下大家巡逻,一旦玩家跑起来大家再关掉巡逻
                            if (WaitTimer >= 1 && player.isRunning == false)
                            {
                                isPatrol = true;
                                WaitTimer = 0;
                            }


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

                        //Debug.Log("队友索敌目标: " + CurrentTarget.name);

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


            //队友在玩家开始跑后立刻聚集过来
            if (tag == "Friend" && player.isRunning)
            {
                isPatrol = false;
                CurrentTarget = _Player;
            }
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

        if (collision.gameObject.tag == "Player"&&tag=="Enemy")//防止死后刚好碰到队友
        {
            if (collision.gameObject.GetComponent<Player>().currentHealth <= 0 && !MakeSureIsPatrol)
            {
                if (collision.gameObject.GetComponent<Player>().isRape == false)
                {
                    isRape = true;

                    switch (Class)
                    {
                        case EnemyClass.Girl:
                        case EnemyClass.Succubus:
                            switch (Random.Range(1, 3))
                            {
                                case 1:
                                    anim.Play("CG/CG_InsultSide_1");
                                    if (PlayerPrefs.GetInt("CG_InsultSide_1") == 0) { PlayerPrefs.SetInt("CG_InsultSide_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                                    break;
                                case 2:
                                    anim.Play("CG/CG_FistingFront_1");
                                    if (PlayerPrefs.GetInt("CG_FistingFront_1") == 0) { PlayerPrefs.SetInt("CG_FistingFront_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                                    break;
                            }

                            break;

                        case EnemyClass.Man:
                        case EnemyClass.FleshArmor:
                        case EnemyClass.Demon:

                            switch (Random.Range(1, 6))
                            {
                                case 1:
                                    anim.Play("CG/CG_RapeSide_1");
                                    if (PlayerPrefs.GetInt("CG_RapeSide_1") == 0) { PlayerPrefs.SetInt("CG_RapeSide_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                                    break;
                                case 2:
                                    anim.Play("CG/CG_RapeFront_1");
                                    if (PlayerPrefs.GetInt("CG_RapeFront_1") == 0) { PlayerPrefs.SetInt("CG_RapeFront_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                                    break;
                                case 3:
                                    anim.Play("CG/CG_AssaultSide_1");
                                    if (PlayerPrefs.GetInt("CG_AssaultSide_1") == 0) { PlayerPrefs.SetInt("CG_AssaultSide_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                                    break;
                                case 4:
                                    anim.Play("CG/CG_AssaultFront_1");
                                    if (PlayerPrefs.GetInt("CG_AssaultFront_1") == 0) { PlayerPrefs.SetInt("CG_AssaultFront_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                                    break;
                                case 5:
                                    anim.Play("CG/CG_GagSide_1");
                                    if (PlayerPrefs.GetInt("CG_GagSide_1") == 0) { PlayerPrefs.SetInt("CG_GagSide_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                                    break;
                            }

                            break;


                        case EnemyClass.Monster:
                            anim.Play("CG/CG_MonsterSide_1");
                            if (PlayerPrefs.GetInt("CG_MonsterSide_1") == 0) { PlayerPrefs.SetInt("CG_MonsterSide_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                            break;

                        case EnemyClass.Tentacle_Monster:
                            anim.Play("CG/CG_TentacleMonsterFront_1");
                            if (PlayerPrefs.GetInt("CG_TentacleMonsterFront_1") == 0) { PlayerPrefs.SetInt("CG_TentacleMonsterFront_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                            break;

                        case EnemyClass.Tentacle_Bag:
                            anim.Play("CG/CG_TentacleBagFront_1");
                            if (PlayerPrefs.GetInt("CG_TentacleBagFront_1") == 0) { PlayerPrefs.SetInt("CG_TentacleBagFront_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                            break;

                        case EnemyClass.Tentacle_Bug:
                            anim.Play("CG/CG_TentacleBugFront_1");
                            if (PlayerPrefs.GetInt("CG_TentacleBugFront_1") == 0) { PlayerPrefs.SetInt("CG_TentacleBugFront_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                            break;

                        case EnemyClass.Tentacle_HermitCrab:
                            anim.Play("CG/CG_TentacleHermitCrabFront_1");
                            if (PlayerPrefs.GetInt("CG_TentacleHermitCrabFront_1") == 0) { PlayerPrefs.SetInt("CG_TentacleHermitCrabFront_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                            break;

                        case EnemyClass.HermitCrab:
                        case EnemyClass.RBQ:
                            anim.Play("CG/CG_TentacleHermitCrabSide_1");
                            if (PlayerPrefs.GetInt("CG/CG_TentacleHermitCrabSide_1") == 0) { PlayerPrefs.SetInt("CG/CG_TentacleHermitCrabSide_1", 1); RoomGenerator.ShowInformationOfStage(0); }
                            break;
                    }




                    gameObject.transform.position = collision.gameObject.transform.position;//敌人拉到玩家位置
                    collision.gameObject.GetComponent<Player>().characterSkin.HideSkeleton();//隐藏玩家
                    collision.gameObject.GetComponent<Player>().isRape = true;
                    collision.gameObject.GetComponent<Player>().isMage = false; collision.gameObject.GetComponent<Player>().HideMagicEffect();//隐藏魔法阵
                     rbody.simulated = false;//当捕获折磨玩家挂的时候，不能移动

                    //隐藏血条
                    HudText.gameObject.SetActive(false);

                    //变白(多米纳斯召唤物)
                    characterSkin.ResetColor();

                    //停止召唤物
                    StopPressureField();

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

                    //将自己YYY的皮肤转移到Girl上
                    int gHead = YYY_headIndex;
                    int gEyes = YYY_eyesIndex;
                    int gBody = YYY_bodyIndex;
                    int gLegs = YYY_legsIndex;
                    int gHat = YYY_hatIndex;

                    int weapon = weaponIndex;

                    // 调用保存方法
                    SaveCurrentSkin(
                        yHead, yEyes, yBody, yLegs, yHat,
                        mHead, mBody, mHat,
                        gHead, gEyes, gBody, gLegs, gHat,
                        weapon
                    );
                    #endregion


                    Invoke("DelayChangeAnim", 0.1f);
                }

                MakeSureIsPatrol = true;

            }

        }//敌人捕获玩家
    }

    void DelayChangeAnim()
    {
        //将动画器转移到玩家动画器上,防止其他指令干扰
        anim = player.anim;
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
        Tentacle_Bag,
        Tentacle_HermitCrab,
        HermitCrab,
        RBQ,
        FleshArmor,
        Demon,
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
            case 6:
                Class = EnemyClass.Tentacle_Bag;
                break;
            case 7:
                Class = EnemyClass.Tentacle_HermitCrab;
                break;
            case 8:
                Class = EnemyClass.HermitCrab;
                break;
            case 9:
                Class = EnemyClass.RBQ;
                break;
            case 10:
                Class = EnemyClass.FleshArmor;
                break;
            case 11:
                Class = EnemyClass.Demon;
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
            case EnemyClass.Tentacle_Bag:
                return "Tentacle_Bag_";
            case EnemyClass.Tentacle_HermitCrab:
                return "Tentacle_HermitCrab_";
            case EnemyClass.HermitCrab:
                return "HermitCrab_";
            case EnemyClass.RBQ:
                return "RBQ_";
            case EnemyClass.FleshArmor:
                return "FleshArmor_";
            case EnemyClass.Demon:
                return "Demon_";
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
            if (Class == EnemyClass.Succubus || Class == EnemyClass.Monster || Class == EnemyClass.Tentacle_Monster || Class == EnemyClass.Tentacle_Bug || Class == EnemyClass.Tentacle_Bag || Class == EnemyClass.Tentacle_HermitCrab || Class == EnemyClass.HermitCrab || Class == EnemyClass.RBQ || Class == EnemyClass.FleshArmor || Class == EnemyClass.Demon) { anim.Play(GetAnimPrefix() + "Default_Idle"); return; }//只有魔族和变异体需要更改

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



      

        switch (GameFlowData.nextScene)
        {
            case "Story_04":
            case "Story_06":
            case "Story_07":

                //惩戒修女关卡
                YYY_headIndex = Random.Range(1, 5);  //黑发主要
                YYY_eyesIndex = Random.Range(1, 14);  // 1~13
                YYY_bodyIndex = 7;//惩戒修女
                int[] YYY_pool2 = { 2, 4, 5, 6, 7, 11, 12 };
                YYY_legsIndex = YYY_pool2[UnityEngine.Random.Range(0, YYY_pool2.Length)];//和修女服搭配的丝袜
                YYY_hatIndex = 7;//惩戒修女头巾

                break;

            default:
                YYY_headIndex = Random.Range(1, 13);  // 除去皇女
                YYY_eyesIndex = Random.Range(1, 14);  // 1~13

                //目前已有的中挑选，除去王女和黑魔导士
                int[] validIndexes = { 2,  4,  6, 7, 10, 11, 12 };
                YYY_bodyIndex = validIndexes[Random.Range(0, validIndexes.Length)];
                YYY_legsIndex = validIndexes[Random.Range(0, validIndexes.Length)];

                int[] YYY_pool = { 1, 2, 3, 4, 10, 11, 12 };
                YYY_hatIndex = YYY_pool[UnityEngine.Random.Range(0, YYY_pool.Length)];//人类 精灵 高等精灵 北方兔族 南方兔族 魔族 大魔族
                break;


        }


        Man_headIndex = Random.Range(1, 5);//除去 皇子和皇帝
        Man_bodyIndex = Random.Range(1, 5);//除去 皇子和皇帝
        Man_hatIndex = Random.Range(1, 5);//除去 魔族角和绷带

        //Girl_headIndex = Random.Range(1, 13);  // 除去皇女
        //Girl_eyesIndex = Random.Range(1, 14);  // 1~13
        //Girl_bodyIndex = Random.Range(10, 13);//剑士射手法师
        //Girl_legsIndex = Random.Range(10, 13);//剑士射手法师
        //
        //int[] Girl_pool = { 1, 2, 3, 4, 10, 11, 12 };//人类 精灵 高等精灵 北方兔族 南方兔族 魔族 大魔族
        //Girl_hatIndex = Girl_pool[UnityEngine.Random.Range(0, Girl_pool.Length)];


        Girl_headIndex = Random.Range(1, 5);  //黑发主要
        Girl_eyesIndex = Random.Range(1, 14);  // 1~13
        Girl_bodyIndex = 7;//惩戒修女
        int[] Girl_pool = { 2, 4, 5, 6, 7, 11, 12 };
        Girl_legsIndex = Girl_pool[UnityEngine.Random.Range(0, Girl_pool.Length)];//和修女服搭配的丝袜
        Girl_hatIndex = 7;//惩戒修女头巾

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

            //if ((Class == EnemyClass.FleshArmor||BossNumber==1) && !isInAttackDelay) 
            //{
            //    //attackTimer = attackCooldown; 
            //    attackTimer += Time.deltaTime * 3f; // 冷却速度加快3倍
            //}//守卫队长和肉铠一靠近就攻击
            //if (BossNumber == 9 && CombatNunLife<=1) { attackTimer = attackCooldown; }//首席战斗修女 在射手/近战情况下 立刻攻击


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
        //if (IsInvoking(nameof(Attack_Cancel))) return;//防止多个 Attack_Cancel() 同时排队

        InvokeRepeating(nameof(FlashWarning), 0f, 0.1f);


        //队友使用玩家的攻击动画
        if (tag == "Friend")
        {
            if (Class == EnemyClass.Monster || Class == EnemyClass.Tentacle_Monster || Class == EnemyClass.Tentacle_Bug || Class == EnemyClass.Tentacle_Bag || Class == EnemyClass.Tentacle_HermitCrab || Class == EnemyClass.HermitCrab || Class == EnemyClass.RBQ || Class == EnemyClass.FleshArmor || Class == EnemyClass.Demon)
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

            if (Class == EnemyClass.Monster || Class == EnemyClass.Tentacle_Monster || Class == EnemyClass.Tentacle_Bug || Class == EnemyClass.Tentacle_Bag || Class == EnemyClass.Tentacle_HermitCrab || Class == EnemyClass.HermitCrab || Class == EnemyClass.RBQ || Class == EnemyClass.FleshArmor || Class == EnemyClass.Demon)
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

        // ✅【关键补充】——强制恢复移动状态
        //if (aiPath != null)
        //{
        //    aiPath.canMove = true;       // 重新允许寻路
        //    aiPath.canSearch = true;     // 重新启动AI搜索目标
        //}
        //
        //isPatrol = true; // 或者你项目中对应的移动标志
        //isAttack = false;
        //isDie = false; // 防止误判死亡状态锁住动画
        //
        //// ✅【防御状态修复】如果动画还卡在攻击动作，强制转Idle
        //var st = anim.GetCurrentAnimatorStateInfo(0);
        //if (st.IsName("attack_1") || st.IsName("Attack_1") ||
        //    st.IsName("Attack_2") || st.IsName("Attack_3") || st.IsName("Attack_4"))
        //{
        //    anim.Play(GetAnimPrefix() + "Idle", 0, 0);
        //}

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
            case EnemyClass.FleshArmor:
            case EnemyClass.Demon:
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

            case EnemyClass.Tentacle_Bag:
            case EnemyClass.Tentacle_HermitCrab:
            case EnemyClass.HermitCrab:
            case EnemyClass.RBQ:
                switch (Random.Range(0, 3))
                {
                    case 0:
                        frameEvents._monster_Summon_01();
                        break;
                    case 1:
                        frameEvents._monster_Summon_02();
                        break;
                    case 2:
                        frameEvents._Shrike_Summon_Attack();
                        break;
                }//肉袋 淫毒肉炮 子宫寄生虫  母体   
                break;
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
            case 210:
                special = -1;//暗黑法球
                break;

            case 202:
            case 208:
                special = 6;//飓风法球
                break;

            case 201:
            case 207:
                special = 5;//剧毒法球
                break;
            case 203:
                //case 210:
                special = 3;//火焰法球
                break;
            case 204:
            case 206:
                special = 4;//冰冻法球
                break;
            case 205:
            case 209:
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
            case 108:
                special = 0;//子弹
                frameEvents._Bullet_AK();
                break;
            case 109:
            case 110:
                special = 0;//子弹
                frameEvents._Bullet_SD();
                break;


            default:
                special = 0;//子弹
                break;
        }

        //根据种族不同特殊远程攻击
        if (Class == EnemyClass.Tentacle_Bag || Class == EnemyClass.Tentacle_HermitCrab)
        {
            special = 5;//剧毒法球
        }

        //魔族化后赛琳娜
        if (Class == EnemyClass.Succubus)
        {
            special = 5;//剧毒法球
        }

        if (tag == "Friend")
        {
            s.Init(-ShootDamage, -SpellDamage, false, 0.2f, special, dir, Shooting.BulletOwnerType.Friend);//角色数值＋武器数值的基础伤害，暴击，蓄力时间，子弹类型，方位，子弹所有者
        }
        else
        {
            s.Init(-ShootDamage, -SpellDamage, false, 0.2f, special, dir, Shooting.BulletOwnerType.Enemy);//角色数值＋武器数值的基础伤害，暴击，蓄力时间，子弹类型，方位，子弹所有者
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
    //1匕首 2阔剑  3长柄双刃斧  4长枪   5长柄斧   6冻结剑   7黑铁刺剑  8熔岩剑  9引雷剑  10古重剑
    //101轻弩   102重弩   103复合弩   104火绳复合枪  105火绳短枪   106火绳长枪   107燧发枪  108刺刀火枪  109火绳黄铜枪  109 110镶银火枪   
    //201黄木短杖  202鹰身短杖   203红宝石短杖    204蓝宝石短杖   205黄玉短杖   206冰冻法杖  207紫水晶法杖  208翡翠法杖  209雷霆法杖  210古木法杖

    public void CheckWeapon()
    {
        //ChangeType(CurrentProfession);//首先根据职业

        if (visionType == EnemyType.ShortRangeEnemy) { CurrentWeapon = weaponIndex; SetStrikeTypeOfAttack(); }//实装战士武器
        if (visionType == EnemyType.LongRangeEnemy && !isMage) { CurrentWeapon = weaponIndex + 100; }//实装射手武器
        if (visionType == EnemyType.LongRangeEnemy && isMage) { CurrentWeapon = weaponIndex + 200; }//实装法师武器

        if (isMage)
        {
            switch (CurrentWeapon)
            {
                case 210:
                    ChangeMagicEffectColor(1);//魔族魔法阵
                    break;


                case 202:
                case 208:
                    ChangeMagicEffectColor(6);//飓风魔法阵
                    break;

                case 201:
                case 207:
                    ChangeMagicEffectColor(5);//剧毒魔法阵
                    break;
                case 203:
                    //case 210:
                    ChangeMagicEffectColor(2);//火焰魔法阵
                    break;
                case 204:
                case 206:
                    ChangeMagicEffectColor(4);//冰冻魔法阵
                    break;
                case 205:
                case 209:
                    ChangeMagicEffectColor(3);//雷电魔法阵
                    break;
            }
        }
    }
    public GameObject ExitEffect;//施法粒子特效（出现消失）
    public ParticleSystem exitEffect;//施法粒子特效(改变颜色)



    public Animator MagicFormationAnim;//魔法阵
    public SpriteRenderer MagicFormation;//魔法阵样式
    public Sprite Magic_Demon, Magic_Fire, Magic_Electricity, Magic_Ice, Magic_Poison, Magic_Wind;

    public void ChangeMagicEffectColor(int ColorNumber)
    {
        switch (ColorNumber)
        {
            case 1:
                MagicFormation.sprite = Magic_Demon;
                var main1 = exitEffect.main;
                main1.startColor = Color.red;
                break;

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
            case 6:
                MagicFormation.sprite = Magic_Wind;
                var main5 = exitEffect.main;
                main5.startColor = new Color(0.2f, 0.9f, 0.2f); // 偏荧光的亮绿色
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



    public void ChangeHealth(int amount, int TypeOfAttack)//【攻击方式 -1暗黑  0无  1剑击特效  2闪电特效  3冻结  4灼烧  5毒物  6击飞
    {

        if (!isScreaming && !isRape && IsGrounded())//冷却中与捕获中不会被伤到,在空中也不会被伤到
        {




            if (amount < 0)
            {
                if (isIndestructible){ return; }//处于无敌状态不会被击伤



                if (isPrecisionShooting)
                {
                    Time.timeScale = 0;

                    //显示暗杀（精准处决）
                    Assassinate.SetActive(true);
                
                    amount = -currentHealth-100;

                    isPrecisionShooting = false;//给那种多命敌人
                }//精准处决


                isPatrol = false;//受伤后立刻进入战斗

               
                if (BossNumber == 2 || BossNumber == 3)
                {
                    if (currentHealth <= maxHealth / 2 && Class == EnemyClass.Girl)
                    {

                        Attack_Cancel();//重置攻击
                        BecomeBoss_Selene_2();

                        BossSkillCoolDown_Timer = 2;//瞬间加快频率

                        wallmap.SetEnemy(2);//赛琳娜魔族化时刻召唤触手怪

                    }//赛琳娜魔族化


                    if (!BossSkillCoolDown_Move)
                    {

                        GateEffect.SetActive(true);
                        Invoke("BossSkill_Move", 0.5f);
                        //显示伤害
                        HudText.HUD(0);




                        //子弹类攻击会触发特殊反弹
                        if (TypeOfAttack == 0 && Random.Range(0, 2) == 0)
                        {
                            ShootBullet();
                        }

                        BossSkillCoolDown_Move = true;

                        return;

                    }


                } //赛琳娜瞬移技能
                if (BossNumber == 7&&Random.Range(0,3)==2)
                {
                    OpenIndestructible();//黑魔导士受伤1/3几率免疫伤害传送房间中央，并获得0.5秒无敌

                    BossSkill_ToDarknessPlace(); 

                    return;
                }//黑魔导士传送暗影位置

                if (currentHealth <= maxHealth / 4 && Class == EnemyClass.FleshArmor)
                {
                    Class = EnemyClass.Man;
                    Attack_Cancel();//重置攻击

                    anim.Play("Man_Strike_Idle");//转换


                    CurrentProfession = 0;
                    ChangeType(CurrentProfession);//把CurrentProfession绑进去
                    SetAttackRange();

                    maxHealth = 2000;
                    currentHealth = maxHealth;

                    HudText.HUD(maxHealth);

                    RunSpeed = 5;//瞬间提速


                }//肉铠二状态

                if (currentHealth <= maxHealth / 4 && Class == EnemyClass.RBQ)
                {
                    BecomeBoss_Elicia();


                }//肉货二状态寄生虫钻出

                if (currentHealth <= maxHealth / 4 && Class == EnemyClass.Demon)
                {
                    Class = EnemyClass.Man;
                    Attack_Cancel();//重置攻击

                    anim.Play("Man_Strike_Idle");//转换


                    CurrentProfession = 0;
                    ChangeType(CurrentProfession);//把CurrentProfession绑进去
                    SetAttackRange();

                    
                    currentHealth = maxHealth;

                    HudText.HUD(maxHealth);

                    RunSpeed = 5;//瞬间提速


                }//恶魔二状态

                if (!isDie && currentHealth > 0 && amount != -currentHealth - 100)
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

                }//防御


             


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
                        Freeze(Random.Range(2,5));//冻结伤害
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
                        Burning(Random.Range(1, 8), false);//灼烧伤害
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
                    Burning(Random.Range(1, 8), true);//中毒伤害
                    break;
            }


            //玩家如果是魔族化状态则回血
            if (player.Class == Player.PlayerClass.Succubus && tag != "Friend")
            {
                player.RestoreHealth(-amount / 4);
                //Debug.Log("回血");
                //player.ChangeHealth(-amount / 4, 0);
            }


            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            UpdateHealthBar(currentHealth, maxHealth);

            //显示伤害
            HudText.HUD(amount);

            //伤害冷却
            Invoke("HurtOver", 0.2f);

            isScreaming = true;

            //受伤尖叫
            Scream();

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
                if (isDominusSummon)//如果是多米纳斯的召唤物，那么不会死去，且切换下一个形态
                {

                    //每次复生都回到中央
                    GateEffect.SetActive(true);
                    transform.position = wallmap.transform.position;


                    //if (currentSummonIndex >= dominusSummonBossList.Count)
                    //{
                    //    currentHealth = maxHealth;
                    //    HudText.HUD(maxHealth);
                    //    UpdateHealthBar(currentHealth, maxHealth);
                    //
                    //    // 没有可以再召唤的 Boss 了，准备让皇帝本体登场
                    //    BecomeBoss_Dominus();
                    //    InvokeRepeating(nameof(BossSkill_CallTentacleMonster), 3f, 5f);// 每隔 5 秒执行一次 Boss技能 召集触手怪物
                    //
                    //    characterSkin.ResetColor();
                    //    return;
                    //}

                    if (dominusSummonBossList.Count > 0)
                    {
                        // 直接取第一个（按顺序）
                        BossNumber = dominusSummonBossList[0];

                        // 用完就移除
                        dominusSummonBossList.RemoveAt(0);
                    }
                    else
                    {
                        // 列表空了 → 召唤皇帝本体
                        BossNumber = 6;
                    }

                    currentSummonIndex++;


                    // 在执行新的 Boss 初始化前，先取消所有旧的 Invoke
                    //CancelInvoke();
                    //CancelInvoke(nameof(BossSkill_CallSoldier));
                    //CancelInvoke(nameof(BossSkill_CallSoldier_Girl));
                    //CancelInvoke(nameof(BossSkill_CallFleshArmor));
                    //CancelInvoke(nameof(BossSkill_ToPlayerPlace));
                    //CancelInvoke(nameof(Delay_Breath_Voice));

                    switch (BossNumber) 
                    {
                        case 1:
                            BecomeBoss_Captain();
                            InvokeRepeating(nameof(BossSkill_CallSoldier), 5f, 10f);  // 每隔 10 秒执行一次 Boss技能 召集士兵                           
                            break;

                        case 6:
                            BecomeBoss_Dominus();
                            InvokeRepeating(nameof(BossSkill_CallTentacleMonster), 5f, 10f);// 每隔 10 秒执行一次 Boss技能 召集触手怪物
                            characterSkin.ResetColor();
                            isDominusSummon = false;
                            if (wallmap.Dominus != null){ Destroy(wallmap.Dominus);}

                            break;


                        case 7:
                            BecomeBoss_DarkMage();
                            InvokeRepeating(nameof(BossSkill_ToPlayerPlace), 5f, 10f);//召唤暗影
                            break;


                        case 8:
                            BecomeBoss_Warden();       
                            InvokeRepeating(nameof(BossSkill_CallFleshArmor), 5f, 10f);// 每隔 10 秒执行一次 Boss技能 召集肉铠
                            Invoke("Delay_Breath_Voice", Random.Range(1, 2.5f)); //随机延后喘息声
                            break;


                        case 9:                                            
                            BecomeBoss_CombatNun();
                            InvokeRepeating(nameof(BossSkill_CallSoldier_Girl), 5f, 10f);// 每隔 10 秒执行一次 Boss技能 召集惩戒修女
                            break;
                    }

                    if (BossNumber != 6)
                    {
                        BecomeShadow();//每次复生之后召唤
                    }
                    

                    Invoke("DelayDialogue", Random.Range(0.5f, 2.5f));//召唤物台词

                    isCallEnemy = false;//重刷
                    anim.Play(GetAnimPrefix() + "Default_Idle");//每次切换完立即播放
                    return;
                }



                if (BossNumber == 2)
                {
                    currentHealth = 100;//保留生命值，防止触发了别的currentHealth <= 0
                    return;
                }//防止玩家攻击力过高，一上来就把王女打死



                if (BossNumber==9&& CombatNunLife>0)
                {
                    currentHealth = maxHealth;//保留生命值，防止触发了别的currentHealth <= 0
                    CombatNunLife -= 1;
                    BecomeBoss_CombatNun();
                    return;
                }//首席战斗修女不会死，直到把3个状态打完

                if (Class == EnemyClass.FleshArmor|| Class == EnemyClass.RBQ || Class == EnemyClass.Demon) 
                {
                    currentHealth = 100;//保留生命值，防止触发了别的currentHealth <= 0
                    return;
                }//恶魔，肉货和肉铠状态下不死，只有转换成Man死

                Die();//一般死亡

                return;
            }


            //击倒再站起(和暴击结合)

            if (!isDie && currentHealth > 0 && TypeOfAttack !=0)//子弹无法击倒击伤敌人
            {

                int DamageType = Random.Range(0, 2);

                if (TypeOfAttack == 6) { DamageType = 1; }//必定刮飞

                if (Class == EnemyClass.FleshArmor || Class == EnemyClass.Demon)
                {
                    DamageType = 0;
                }//这两种不会被击飞，但是被击中会停止移动


                if (DamageType == 0)
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


                    //一定几率打掉衣服丝袜（暂时别）
                    //if (Random.Range(0, 3) == 0)
                    //{
                    //    CurrentArmorDefence = 0;
                    //    YYY_bodyIndex = 1; SetSkin();
                    //    //SaveCurrent();
                    //
                    //    frameEvents._Effect_tear1();
                    //}
                    //if (Random.Range(0, 3) == 0)
                    //{
                    //    CurrentStockingDefence = 0;
                    //    YYY_legsIndex = 1; SetSkin();
                    //    //SaveCurrent();
                    //
                    //    frameEvents._Effect_tear1();
                    //}
                }
            }

        }




    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHealthBar(currentHealth, maxHealth);

        //显示伤害
        HudText.HUD(amount);

    }//回血专用路径

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
           //if (isMage)
           //{
           //    anim.Play(GetAnimPrefix() + "Spell_Block");
           //}
           //else
           //{
           //    anim.Play(GetAnimPrefix() + "Shoot_Block");
           //}

            anim.Play(GetAnimPrefix() + "Shoot_Block");

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

    void Scream()
    {
        switch (BossNumber)
        {
            case 0:

                switch (Class)
                {
                    case EnemyClass.Girl:
                    case EnemyClass.Succubus:
                    case EnemyClass.HermitCrab:
                    case EnemyClass.RBQ:
                        switch (Random.Range(0, 2))
                        {
                            case 0:
                                frameEvents._yyy_jianjiao4();
                                break;
                            case 1:
                                frameEvents._yyy_jianjiao5();
                                break;
                        }//女性
                        break;
                    case EnemyClass.Man:
                    case EnemyClass.FleshArmor:
                    case EnemyClass.Demon:
                        switch (Random.Range(0, 4))
                        {
                            case 0:
                                frameEvents._Man_die1();
                                break;
                            case 1:
                                frameEvents._Man_die2();
                                break;
                            case 2:
                                frameEvents._Man_die3();
                                break;
                            case 3:
                                frameEvents._Man_die4();
                                break;
                        }//男性
                        break;

                    case EnemyClass.Monster:
                    case EnemyClass.Tentacle_Monster:
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                frameEvents._Zombie_Die_1();
                                break;
                            case 1:
                                frameEvents._Zombie_Die_2();
                                break;
                            case 2:
                                frameEvents._Zombie_Attack();
                                break;
                        }//感染者 变异体
                        break;

                    case EnemyClass.Tentacle_Bug:
                        frameEvents._Orangutan_Die_1();//肉翅蜂
                        break;

                    case EnemyClass.Tentacle_Bag:
                    case EnemyClass.Tentacle_HermitCrab:
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                frameEvents._monster_Attack_01();
                                break;
                            case 1:
                                frameEvents._monster_Die_01();
                                break;
                            case 2:
                                frameEvents._Shrike_Die();
                                break;
                        }//肉袋  淫毒肉炮  
                        break;
                }

                break;

            case 1:
                //守卫队长尖叫
                break;

            case 2:
            case 3:
                //王女尖叫
                break;

        }





    }//尖叫声


    [Header("暴击")]
    public GameObject Critial;
    public GameObject Assassinate;//暗杀
    public void CritialAttack()
    {
        if (isIndestructible) { return; }//处于无敌状态不会被击伤

        if (BossNumber != 0 && BossSkillCoolDown_Move!) { return; }//Boss战中，在瞬移冷却中才能被重击到

        if (IsGrounded()) { Knockdown(); }//敌人必须站在地上才能被暴击击倒




        Time.timeScale = 0;


        Critial.SetActive(true);//显示暴击


        player.ChangeCritical(-player.maxCritical);//暴击清零

    }//暴击

    bool isPrecisionShooting = false;
    public void CheckPrecisionShooting() 
    {
        if (currentHealth<=maxHealth/2 && Random.Range(0, 2) == 0)//敌人生命值在一半以下，50%几率一击必杀
        {
        
            isPrecisionShooting = true;
        
        }

    }//精准射击

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

        if (!DieBonue) 
        {
            //Boss死亡音效
            switch (BossNumber)
            {
                case 1:
                    UIManager.instance.ShowDialogue("Boss_Captain_Die");
                    break;
                case 2:
                case 3:
                    UIManager.instance.ShowDialogue("Boss_Selene_Die");
                    break;

                case 5:
                    UIManager.instance.ShowDialogue("Boss_Alexis_Die");
                    break;

                case 6:
                    UIManager.instance.ShowDialogue("Boss_Dominus_Die");
                    break;

                case 7:
                    UIManager.instance.ShowDialogue("Boss_DarkMage_Die");
                    break;

                case 8:
                    UIManager.instance.ShowDialogue("Boss_Warden_Die");
                    break;

                case 9:
                    UIManager.instance.ShowDialogue("Boss_CombatNun_Die");
                    break;
            }


           
            //击杀敌人获得金币
            if (tag != "Friend")
            {

                int difficulty = PlayerPrefs.GetInt("Difficulty", 0); // 0=Easy,1=Common,2=Difficult
                float rewardMultiplier = 1f;

                // 根据难度决定收益倍率
                switch (difficulty)
                {
                    case 0: rewardMultiplier = 1f; break; // 简单
                    case 1: rewardMultiplier = 1.5f; break; // 一般
                    case 2: rewardMultiplier = 2f; break; // 困难
                }

                // Boss死亡处理
                switch (BossNumber)
                {
                    case 0:
                        //一般敌人
                        player.ChangeExperience((int)(100 * rewardMultiplier));
                        UIManager.instance.ChangeMoney((int)(Random.Range(10, 30) * rewardMultiplier));
                        break;

                    case 1:
                    case 4:
                    case 5:
                    case 8:
                        //一般Boss
                        player.ChangeExperience((int)(500 * rewardMultiplier));
                        UIManager.instance.ChangeMoney((int)(Random.Range(50, 150) * rewardMultiplier));
                        break;

                    case 2:
                    case 3:
                    case 7:
                    case 9:
                        //特殊Boss
                        player.ChangeExperience((int)(1000 * rewardMultiplier));
                        UIManager.instance.ChangeMoney((int)(Random.Range(100, 300) * rewardMultiplier));
                        break;

                    case 6:
                        //最终Boss
                        player.ChangeExperience((int)(1500 * rewardMultiplier));
                        UIManager.instance.ChangeMoney((int)(Random.Range(150, 450) * rewardMultiplier));
                        break;
                }



                //人类高等精灵高等魔族具有【狩猎】
                switch (player.YYY_hatIndex) 
                {
                    case 1:
                    case 3:
                    case 11:
                        if (Random.Range(0, 2) == 0)
                        {
                            player.HuntingExperience();
                        }
                        break;
                }

            }

            DieBonue = true;
        }

       
    


    }//死亡

    bool DieBonue = false;//死亡触发金币只能一次

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

                if (BossNumber != 0)
                {
                    wallmap.isCanWinRoom = true;//Boss房间的敌人被消灭的时候触发
                }


                //只有艾莉西亚第八关比较特殊，因为场景里有消灭不完的触手Enemy，所以直接获胜
                if (BossNumber == 4) 
                {
                    //完成关卡，结算画面
                    UIManager.instance._RoomGenerator.ShowResult();
                }

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
    public void Burning(int Timer, bool isPoison)
    {
        if (!isPoison) { Burning_Effect.SetActive(true); }


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

        //受伤数字变红
        HudText.isFriend = true;

        Debug.Log($"{gameObject.name} has switched to Friend.");

        //敌人攻击冷却
        attackCooldown = 1f;

        //如果是魔族队友的话，自动魔族化，不是CG状态下

        if (GameFlowData.nextScene != "CG" &&
            GameFlowData.nextScene != "CG_AVG_01"&&
            GameFlowData.nextScene != "CG_AVG_02" &&
            GameFlowData.nextScene != "CG_AVG_03" &&
            GameFlowData.nextScene != "CG_AVG_04" &&
            GameFlowData.nextScene != "CG_AVG_05" &&
            GameFlowData.nextScene != "CG_AVG_06" &&
            GameFlowData.nextScene != "CG_AVG_07" &&
            GameFlowData.nextScene != "CG_AVG_08") 
        {
            Invoke(nameof(MakeSureSuccubusFrined), 1f);
        }

    }

    void MakeSureSuccubusFrined()
    {
        //如果是魔族队友的话，自动魔族化
        if (YYY_hatIndex == 11 || YYY_hatIndex == 12)
        {
           // Class = EnemyClass.Succubus;
            ChangeClass(2);
            anim.Play("Succubus_Default_Walk");
            Debug.Log($"{gameObject.name} 已经魔族化");
        }

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

        //受伤数字变白
        HudText.isFriend = false;

        Debug.Log($"{gameObject.name} has switched to Enemy.");


        attackCooldown = 0.2f;//队友 快速攻击
    }
    #endregion


    /// <summary>
    /// 变成Boss
    /// </summary>
    #region
    [Header("Boss技能")]
    public int BossNumber = 0;//1守卫队长  2皇女  3魔族化皇女  4宰相   5皇太子   6皇帝    7黑魔导士   8典狱长  9首席战斗修女

    public void BecomeBoss_Captain()
    {

        attackCooldown = 0.3f;//守卫队长 快速攻击



        YYY_headIndex = 6;
        YYY_eyesIndex = 6;
        YYY_bodyIndex = 10;
        YYY_legsIndex = 10;

        YYY_hatIndex = 1;//人类

        weaponIndex = 5;//长斧

        SetSkin();

        Class = EnemyClass.Girl;

        CurrentProfession = 0;
        ChangeType(CurrentProfession);//把CurrentProfession绑进去
        SetAttackRange();


        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                Name.text = "衛兵隊長";   // 日语
                break;
            case 1:
                Name.text = "卫兵队长";   // 简体中文
                break;
            case 2:
                Name.text = "衛兵隊長";   // 繁体中文
                break;
            case 3:
                Name.text = "Guard Captain";   // 英语
                break;
            case 4:
                Name.text = "경비대장";   // 韩语
                break;
        }

        maxHealth *= 3;
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);
    }//Boss 士兵队长  3

    public void BecomeBoss_Morgan()
    {

        YYY_headIndex = 12;
        YYY_eyesIndex = 11;
        YYY_bodyIndex = 1;
        YYY_legsIndex = 1;

        YYY_hatIndex = 2;//精灵

        weaponIndex = 9;//引雷剑(近战麻痹攻击)

        SetSkin();

        Class = EnemyClass.RBQ;

        CurrentProfession = 0;
        ChangeType(CurrentProfession);//把CurrentProfession绑进去
        SetAttackRange();

        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                Name.text = "エリシア";   // 日语
                break;
            case 1:
                Name.text = "艾莉西亚";   // 简体中文
                break;
            case 2:
                Name.text = "艾莉西亞";   // 繁体中文
                break;
            case 3:
                Name.text = "Elicia";   // 英语
                break;
            case 4:
                Name.text = "엘리시아";   // 韩语
                break;
        }

        maxHealth *= 2;
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);



        player.FollowDamage = 0;
        player.StartMakeChild();//开始在玩家脚下生触手

    }//Boss 莫尔根侯爵（艾莉西亚躯体）2

    public void BecomeBoss_Elicia()
    {
        Attack_Cancel();//重置攻击

        Class = EnemyClass.HermitCrab;
        anim.Play("HermitCrab_Default_Idle");

        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);
        HudText.HUD(maxHealth);
    }//Boss 艾莉西亚二状态，寄生虫钻出  1

  




    public void BecomeBoss_Selene()
    {


        YYY_headIndex = 13;  // 皇女
        YYY_eyesIndex = 11;
        YYY_bodyIndex = 5;
        YYY_legsIndex = 5;

        YYY_hatIndex = 1;//人类

        weaponIndex = 3;//红宝石短杖

        SetSkin();

        Class = EnemyClass.Girl;

        CurrentProfession = 2;
        ChangeType(CurrentProfession);//把CurrentProfession绑进去
        SetAttackRange();

        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                Name.text = "セリーネ＝ヴァルドリア";   // 日语
                break;
            case 1:
                Name.text = "王女赛琳娜";   // 简体中文
                break;
            case 2:
                Name.text = "王女賽琳娜";   // 繁体中文
                break;
            case 3:
                Name.text = "Princess Selene";   // 英语
                break;
            case 4:
                Name.text = "세리네 공주";   // 韩语
                break;
        }


        maxHealth *= 3;
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);

    }//Boss 赛琳娜  3

    public void BecomeBoss_Selene_2()
    {


        YYY_headIndex = 13;  // 皇女
        YYY_eyesIndex = 11;
        YYY_bodyIndex = 5;
        YYY_legsIndex = 5;

        YYY_hatIndex = 11;//大魔族

        weaponIndex = 8;//近战熔岩

        SetSkin();

        Class = EnemyClass.Succubus;

        CurrentProfession = 0;
        ChangeType(CurrentProfession);//把CurrentProfession绑进去
        SetAttackRange();

        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                Name.text = "セリーネ＝ヴァルドリア";   // 日语
                break;
            case 1:
                Name.text = "王女赛琳娜";   // 简体中文
                break;
            case 2:
                Name.text = "王女賽琳娜";   // 繁体中文
                break;
            case 3:
                Name.text = "Princess Selene";   // 英语
                break;
            case 4:
                Name.text = "세리네 공주";   // 韩语
                break;
        }

        maxHealth = 1000;
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);
        HudText.HUD(maxHealth);

    }//Boss 魔族化赛琳娜  1
    public void BecomeBoss_Alexis()
    {

        attackCooldown = 0.3f;//皇太子亚历克西斯 快速攻击

        Man_headIndex = 5;//皇子
        Man_bodyIndex = 5;//皇子
        Man_hatIndex = 5;//魔族角

        weaponIndex = 10;//引雷剑

        SetSkin();

        Class = EnemyClass.Man;

        CurrentProfession = 0;
        ChangeType(CurrentProfession);//把CurrentProfession绑进去
        SetAttackRange();

        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                Name.text = "アレクシス＝ヴァルドリン";   // 日语
                break;
            case 1:
                Name.text = "皇太子亚历克西斯";   // 简体中文
                break;
            case 2:
                Name.text = "皇太子亞歷克西斯";   // 繁体中文
                break;
            case 3:
                Name.text = "Crown Prince Alexis";   // 英语
                break;
            case 4:
                Name.text = "알렉시스 황태자";   // 韩语
                break;
        }

        maxHealth *= 4;
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);

    }//Boss 亚历克西斯  4

    public void BecomeBoss_Dominus()
    {
        attackCooldown = 0.3f;//多米纳斯 快速攻击

        Man_headIndex = 6;//皇帝
        Man_bodyIndex = 6;//皇帝
        Man_hatIndex = 5;//魔族角

        weaponIndex = 10;//引雷剑

        SetSkin();

        Class = EnemyClass.Demon;

        CurrentProfession = 0;
        ChangeType(CurrentProfession);//把CurrentProfession绑进去
        SetAttackRange();

        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                Name.text = "ドミナス＝ヴァルドリン";   // 日语
                break;
            case 1:
                Name.text = "皇帝多米纳斯";   // 简体中文
                break;
            case 2:
                Name.text = "皇帝多米納斯";   // 繁体中文
                break;
            case 3:
                Name.text = "Emperor Dominus";   // 英语
                break;
            case 4:
                Name.text = "도미누스 황제";   // 韩语
                break;
        }


        maxHealth *= 4;
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);

        StartPressureField();//开始在自己脚下生气场

    }//Boss 多米纳斯  4

    public void BecomeBoss_DarkMage()
    {
        attackCooldown = 1f;//暗影法师 慢速攻击

        YYY_headIndex = 5;  // 粉毛
        YYY_eyesIndex = 11;
        YYY_bodyIndex = 3;
        YYY_legsIndex = 2;

        YYY_hatIndex = 1;//人类

        weaponIndex = 10;//黑魔法

        SetSkin();

        Class = EnemyClass.Girl;

        CurrentProfession = 2;
        ChangeType(CurrentProfession);//把CurrentProfession绑进去
        SetAttackRange();

        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                Name.text = "黒魔導士";   // 日语
                break;
            case 1:
                Name.text = "黑魔导士";   // 简体中文
                break;
            case 2:
                Name.text = "黑魔導士";   // 繁体中文
                break;
            case 3:
                Name.text = "Dark Sorceress";   // 英语
                break;
            case 4:
                Name.text = "흑마도사";   // 韩语
                break;
        }

        maxHealth *= 2;
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);
        HudText.HUD(maxHealth);

        player.FollowDamage = 1;
        player.StartMakeChild();//开始在玩家脚下生触手

    }//Boss 黑魔法法师   2

    public void BecomeBoss_Warden() 
    {

        attackCooldown = 0.3f;//典狱长 快速攻击

        YYY_headIndex = Random.Range(1, 13);  // 除去皇女
        YYY_eyesIndex = Random.Range(1, 14);  // 1~13
        YYY_bodyIndex = Random.Range(10, 13);//剑士射手法师
        YYY_legsIndex = Random.Range(10, 13);//剑士射手法师

        Man_headIndex = 6;
        Man_bodyIndex = 2;//盔甲
        Man_hatIndex = 6;//绷带

        weaponIndex = Random.Range(1, 11);

        SetSkin();



        RunSpeed = Random.Range(1, 3);
        WalkSpeed = Random.Range(1, 3);



        Class = EnemyClass.FleshArmor;

        CurrentProfession = 0;
        ChangeType(CurrentProfession);//把CurrentProfession绑进去
        SetAttackRange();

        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                Name.text = "典獄長";   // 日语
                break;
            case 1:
                Name.text = "典狱长";   // 简体中文
                break;
            case 2:
                Name.text = "典獄長";   // 繁体中文
                break;
            case 3:
                Name.text = "Warden";   // 英语
                break;
            case 4:
                Name.text = "간수장";   // 韩语 (Warden)
                break;
        }

        maxHealth *= 4;
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);
    }//Boss典狱长  4

    int CombatNunLife = 2;//先法师 再射手 最后近战  血越打越厚
    public void BecomeBoss_CombatNun()
    {
        attackCooldown = 0.3f;//首席战斗修女 快速攻击


        YYY_headIndex = 4;
        YYY_eyesIndex = 6;
        YYY_bodyIndex = 7;
        YYY_legsIndex = 7;

        YYY_hatIndex = 6;//首席战斗修女冠

        weaponIndex = 3;//重弩 双刃斧 火焰法杖

        SetSkin();

        Class = EnemyClass.Girl;

        CurrentProfession = CombatNunLife;
        ChangeType(CurrentProfession);//把CurrentProfession绑进去
        SetAttackRange();


        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                Name.text = "首席戦闘シスター";   // 日语
                break;
            case 1:
                Name.text = "首席战斗修女";   // 简体中文
                break;
            case 2:
                Name.text = "首席戰鬥修女";   // 繁体中文
                break;
            case 3:
                Name.text = "Chief Battle Sister";   // 英语
                break;
            case 4:
                Name.text = "최고 전투 수녀";   // 韩语
                break;
        }

        maxHealth += 500;
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);
        HudText.HUD(maxHealth);

    }//Boss 首席战斗修女  ＋1/2  ＋1/2  ＋1/2


    #region 王女赛琳娜技能
    //Boss技能  瞬移近  瞬移远
    bool BossSkillCoolDown_Move = false;
    float BossSkillCoolDown_Timer = 3f;
    void BossSkill_Move()
    {
        if (currentHealth <= 0 || isRape || player.currentHealth <= 0) { return; }


        wallmap.ChangeTargetPlace(gameObject);

        GateEffect.SetActive(false);
        GateEffect.SetActive(true);
        Invoke("BossSkill_Move_CoolDown", BossSkillCoolDown_Timer);
        BossSkillCoolDown_Timer += 1;//Boss的技能启动时间逐渐增加

        ShootBullet();//闪避的同时攻击

        BossSkill_ChangeMagic(Random.Range(2, 6));


    }
    void BossSkill_Move_CoolDown()
    {
        if (currentHealth <= 0 || isRape || player.currentHealth <= 0) { return; }


        BossSkillCoolDown_Move = false;

        //魔族化后快速闪避快速近身
        if (Class == EnemyClass.Succubus)
        {

            UIManager.instance.ShowDialogue("Boss_Selene_Skill2");

            GateEffect.SetActive(true);




            RoomGenerator.ChangeTargetPlace(gameObject, -1);

            //瞬移到玩家身边直接攻击
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

            Invoke("Attack_Cancel", 0.5f);
            return;
        }


        UIManager.instance.ShowDialogue("Boss_Selene_Skill");
    }


    //Boss技能  法术变换
    void BossSkill_ChangeMagic(int MagicNumber)
    {

        weaponIndex = MagicNumber;//2鹰身短杖 3红宝石短杖 4蓝宝石短杖 5黄玉短杖

        SetSkin();
    }
    #endregion

    #region  守卫队长技能  典狱长技能   首席战斗修女技能   皇太子亚历克西斯技能   宰相摩尔根技能    皇帝多米纳斯技能 
    //Boss技能  召集
    public bool BecomeSoldier_Man = false;//男性士兵
    public bool BecomeTentacleMonster = false;//触手怪物
    public bool BecomeSoldier_Girl = false;//女性士兵
    public bool BecomeFleshArmor = false;//肉铠


    public bool isCallEnemy = false;//这类召集能力，只能召一次
    void BossSkill_CallSoldier()
    {

        if (currentHealth <= 0 || isRape || player.currentHealth <= 0) { return; }


        if (PlayerPrefs.GetInt("Difficulty") == 2) { isCallEnemy = false; }//困难模式下无限刷
        
        //如果场景内敌人少于4个，再召唤一群士兵

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length <= 4&&!isCallEnemy)
        {
            wallmap.SetEnemy(1);//守卫队长召集男性士兵
            RoomGenerator.ShowInformationOfStage(-1);//敌人增援

            isCallEnemy = true;
        }

        switch (BossNumber)
        {
            case 1:
                UIManager.instance.ShowDialogue("Boss_Captain_Skill");
                break;
            case 5:
                UIManager.instance.ShowDialogue("Boss_Alexis_Skill");
                break;
        }



    }//召集男性士兵

    void BossSkill_CallSoldier_Girl()
    {
        if (currentHealth <= 0 || isRape || player.currentHealth <= 0) { return; }


        if (PlayerPrefs.GetInt("Difficulty") == 2) { isCallEnemy = false; }//困难模式下无限刷

        //如果场景内敌人少于4个，再召唤一群士兵

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length <= 4 && !isCallEnemy)
        {
            wallmap.SetEnemy(3);//首席战斗修女召集惩戒修女
            RoomGenerator.ShowInformationOfStage(-1);//敌人增援

            isCallEnemy = true;
        }

        switch (BossNumber)
        {
            case 9:
                UIManager.instance.ShowDialogue("Boss_CombatNun_Skill");
                break;
        }



    }//召集惩戒修女

    void BossSkill_CallTentacleMonster()
    {
        if (currentHealth <= 0 || isRape || player.currentHealth <= 0) { return; }


        if (PlayerPrefs.GetInt("Difficulty") == 2) { isCallEnemy = false; }//困难模式下无限刷

        //如果场景内敌人少于10个，再召唤一群触手怪

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length <= 4 && !isCallEnemy)
        {
            wallmap.SetEnemy(2);//未知Boss召唤触手怪
            RoomGenerator.ShowInformationOfStage(-1);//敌人增援

            isCallEnemy = true;
        }

        switch (BossNumber)
        {
            case 4:
                UIManager.instance.ShowDialogue("Boss_Morgan_Skill");
                break;

            case 6:
                UIManager.instance.ShowDialogue("Boss_Dominus_Skill");
                break;
        }

    }


    void BossSkill_CallFleshArmor() 
    {
        if (currentHealth <= 0 || isRape || player.currentHealth <= 0) { return; }


        if (PlayerPrefs.GetInt("Difficulty") == 2) { isCallEnemy = false; }//困难模式下无限刷

        //如果场景内敌人少于4个，再召唤一群肉铠

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length <= 4 && !isCallEnemy)
        {
            wallmap.SetEnemy(4);//召集肉铠
            RoomGenerator.ShowInformationOfStage(-1);//敌人增援

            isCallEnemy = true;
        }

        switch (BossNumber)
        {
            case 8:
                UIManager.instance.ShowDialogue("Boss_Warden");
                break;
        }
    }

    #endregion

    #region  皇帝多米纳斯的召唤物  多米纳斯的压制气场
    public bool isDominusSummon = false;   // 是否是皇帝的影子召唤
    public void BecomeShadow() 
    {
        isDominusSummon = true;
        characterSkin.SetBlack();


      
        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                Name.text = "ドミナスの召喚体";   // 日语
                break;
            case 1:
                Name.text = "多米纳斯的召唤物";   // 简体中文
                break;
            case 2:
                Name.text = "多米納斯的召喚物";   // 繁體中文
                break;
            case 3:
                Name.text = "Dominus’s Summoned Entity";   // 英语
                break;
            case 4:
                Name.text = "도미누스의 소환체";   // 韓語
                break;
        }

        //保持低血量
        maxHealth = 2000;
        currentHealth = maxHealth;
        UpdateHealthBar(currentHealth, maxHealth);
        HudText.HUD(maxHealth);



     

    }
    // 召唤用 Boss 列表
    public List<int> dominusSummonBossList = new List<int> {1, 8, 9, 7 };
    private int currentSummonIndex = 0;           // 已经用了几个



    [Header("皇帝压制领域节点预制体")]
    public GameObject pressureNodePrefab;
    // 用于在皇帝脚下生成的黑红魔力震荡节点


    private Coroutine pressureFieldCoroutine;

    //启动皇帝的【压制领域】
    public void StartPressureField()
    {
        pressureFieldCoroutine = StartCoroutine(PressureFieldRoutine());
    }

    // 不规律的压制节点生成循环
    public IEnumerator PressureFieldRoutine()
    {
        while (true)
        {
            // 2～5 秒随机时间，模拟无法预测的魔力震荡
            float randomInterval = Random.Range(2f, 5f);
            yield return new WaitForSeconds(randomInterval);

            SpawnPressureNode(); // 生成一次魔力震荡节点
        }
    }

    // 在皇帝脚下生成压制节点
    public void SpawnPressureNode()
    {
        GameObject node = Instantiate(
        pressureNodePrefab,
        transform.position,
        Quaternion.identity
    );

        // 先指定父物体
        node.transform.SetParent(transform);

        // 再调整局部坐标（相对 Enemy 的偏移）
        node.transform.localPosition += new Vector3(0f, 0f, -0.3f);


        Plant_Attack plant_Attack = node.GetComponent<Plant_Attack>();
        plant_Attack.FollowDamage = 2;




    }
    // 停止压制领域（死亡、剧情切换、形态变化时）
    public void StopPressureField()
    {
        if (pressureFieldCoroutine != null)
        {
            StopCoroutine(pressureFieldCoroutine);
            pressureFieldCoroutine = null;

            Debug.Log("皇帝压制领域已停止");
        }
    }



    #endregion

    #region  黑魔导士技能
    //Boss技能  追逐暗影
    public GameObject Darkness_Enemy;
    public bool isIndestructible = false;//无敌
    public GameObject Invincible_Mark;//处于无敌中标志

    GameObject Darkness_Enemy_1;//短暂的记录自己生成的暗影
    public void BossSkill_CallDarkness() 
    {
        if (currentHealth <= 0 || isRape || player.currentHealth <= 0) { return; }




        //告诉自己生成的RBQ出生WallMap
        GameObject NewEnemy = Instantiate(Darkness_Enemy, transform.position, Quaternion.identity);
        NewEnemy.GetComponent<AIDestinationSetter>().target = player.transform;
        NewEnemy.GetComponentInChildren<Spell>().Init(-50*player.Level,-1, false, 0);
        Darkness_Enemy_1 = NewEnemy;
        Destroy(NewEnemy, 0.7f);





        switch (BossNumber)
        {
            case 7:
                UIManager.instance.ShowDialogue("Boss_DarkMage_Skill");
                break;
        }

        Invoke(nameof(BossSkill_ToDarknessPlace), 2f);


    }

    public void BossSkill_ToPlayerPlace() 
    {
        if (currentHealth <= 0 || isRape || player.currentHealth <= 0) { return; }

        //传送到玩家身边释放暗影
        GateEffect.SetActive(true);
        transform.position = player.transform.position;

        Invoke(nameof(BossSkill_CallDarkness), 0.7f);


        ShowMagicEffect();//显示魔法阵召唤

        OpenIndestructible();//黑魔导士每隔10秒，传送到玩家身边,0.5秒后召唤暗影近战攻击，2秒后传送回房间中央，这2.5秒期间无敌
    }

    //Boss技能  传送到暗影位置
    public void BossSkill_ToDarknessPlace() 
    {
        if (currentHealth <= 0 || isRape || player.currentHealth <= 0) { return; }

        GateEffect.SetActive(true);



        transform.position = wallmap.transform.position;


        Invoke(nameof(CloseIndestructible), 0.5f);
    }


    void OpenIndestructible() 
    {
        isIndestructible = true;
        Invincible_Mark.SetActive(true);
    }

    void CloseIndestructible()
    {
        isIndestructible = false;
        Invincible_Mark.SetActive(false);
    }

    #endregion

    #region  艾莉西亚技能
    public GameObject Egg;
    public void BossSkill_Childbirth()
    {

        GameObject effectPrefabs_2 = Instantiate(Egg, transform.position, transform.rotation);
        Egg.GetComponent<Plant_Tentacle>().isEgg = true;

        Destroy(effectPrefabs_2, 3f);

    }//艾莉西亚产卵
    #endregion
    void OnDestroy()
    {
        // Boss死亡时停止召唤

        CancelInvoke(nameof(BossSkill_CallSoldier));
        CancelInvoke(nameof(BossSkill_CallTentacleMonster));
        CancelInvoke(nameof(BossSkill_CallSoldier_Girl));
        CancelInvoke(nameof(BossSkill_CallFleshArmor));
        CancelInvoke(nameof(BossSkill_ToPlayerPlace));
        CancelInvoke(nameof(Delay_Breath_Voice));

        StopPressureField();
    }
    #endregion



    /// <summary>
    /// CG结局剧情控制
    /// </summary>
    #region
    [Header("CG结局剧情控制")]
    public GameObject HeathBar;//隐藏血条但是留下名称
                               //bool isCG_End_RBQ = false;

    public void CG_End_RBQ_Man_CarryUp()
    {

        switch (GameFlowData.nextScene)
        {
            case "CG_AVG_01":
                anim.SetBool("is_Man_CarryUp_Catch", true);//首枷輪姦
                break;
            case "CG_AVG_02":
                anim.SetBool("is_Man_CarryUp_ShameWagon", true);//陵辱車
                break;
            case "CG_AVG_03":
                anim.SetBool("is_Man_CarryUp_Cage", true);//性奴拍卖会狗笼肉货
                break;
            case "CG_AVG_04":

               // RunSpeed = Random.Range(1, 3);
               // WalkSpeed = Random.Range(1, 3);
               //
               // Man_headIndex = Random.Range(1, 5);
               // Man_bodyIndex = 2;//盔甲
               // Man_hatIndex = 6;//绷带
               //
               // SetSkin();
               //
               // Class = EnemyClass.FleshArmor;
               //
               // isPatrol = true;
               //
               // anim.Play("FleshArmor_Default_Walk");//肉铠结局
                break;

            case "CG_AVG_07":
                anim.SetBool("is_Girl_CarryUp_Side", true);//惩戒修女牵着
                break;
        }

       

        //隐藏血条但是留下名称
        HeathBar.SetActive(false);

        // 保存 Man 部位
        Man_headIndex = Random.Range(1, 5);//除去 皇子和皇帝
        Man_bodyIndex = Random.Range(1, 5);//除去 皇子和皇帝
        Man_hatIndex = Random.Range(1, 5);//除去 魔族角和绷带

        //保存 Girl 部位
        Girl_headIndex = Random.Range(1, 5);  //黑发主要
        Girl_eyesIndex = Random.Range(1, 14);  // 1~13
        Girl_bodyIndex = 7;//惩戒修女
        int[] Girl_pool = { 2, 4, 5, 6, 7, 11, 12 };
        Girl_legsIndex = Girl_pool[UnityEngine.Random.Range(0, Girl_pool.Length)];//和修女服搭配的丝袜
        Girl_hatIndex = 7;//惩戒修女头巾


        SetSkin();

        //长音频之前停声音
        frameEvents.audioS.Stop();
        //随机延后喘息声
        Invoke("Delay_Breath_Voice", Random.Range(1, 5.5f));

    }//侧面走扛

    public void CG_End_RBQ_Pillory(int SideOrFront)//0正面 1侧面强奸  2正面强奸
    {



        switch (GameFlowData.nextScene)
        {
            case "CG_AVG_01":
                anim.Play("RBQ_Punish_Pillory_2");//首枷輪姦
                break;
            case "CG_AVG_02":
                anim.Play("RBQ_Punish_ShameWagon_2");//陵辱車接客
                break;
            case "CG_AVG_03":
                anim.Play("RBQ_Punish_Cage_2");//狗笼肉货
                break;
            case "CG_AVG_04":
                anim.Play("RBQ_Torture_CutDown");//四肢切断挂饰   
                break;

            case "CG_AVG_07":
                anim.Play("RBQ_Punish_Crucifixion_2");//倒十字肉圣物  
                break;

        }

        switch (SideOrFront)
        {
            case 0:
                Invoke("DelayRBQToFront", Random.Range(0.2f, 0.7f));
                break;

            case 1:
                Invoke("DelayRBQToSide", Random.Range(0.2f, 0.7f));
                break;

            case 2:
                Invoke("DelayRBQToFront2", Random.Range(0.2f, 0.7f));
                break;
        }


        //迫使朝前
        CurrentTarget = _Player;


    }

    void DelayRBQToFront()
    {
        isDie = true;

        if (Random.Range(0, 2) == 0)
        {
            //迫使朝后（第二种变化的强奸）
            anim.SetFloat("InputX", 0);
            anim.SetFloat("InputY", 1);
        }
        else
        {
            //迫使朝前
            anim.SetFloat("InputX", 0);
            anim.SetFloat("InputY", -1);
        }

        //长音频之前停声音
        frameEvents.audioS.Stop();
        //随机延后喘息声
        Invoke("Delay_Breath_Voice", Random.Range(1, 5.5f));
    }//正面展示

    void DelayRBQToFront2()
    {
        isDie = true;

        if (Random.Range(0, 2) == 0)
        {
            //迫使朝后（第二种变化的强奸）
            anim.SetFloat("InputX", 0);
            anim.SetFloat("InputY", 1);
        }
        else
        {
            //迫使朝前
            anim.SetFloat("InputX", 0);
            anim.SetFloat("InputY", -1);
        }


    



        //长音频之前停声音
        frameEvents.audioS.Stop();
        switch (Random.Range(0, 4))
        {
            case 0:
                frameEvents._03_H_Gasping_0();
                break;
            case 1:
                frameEvents._03_H_Gasping_1();
                break;
            case 2:
                frameEvents._03_H_Gasping_Weak_0();
                break;
            case 3:
                frameEvents._03_H_Gasping_Weak_1();
                break;
        }


        switch (GameFlowData.nextScene)
        {
            case "CG_AVG_01":
                anim.Play("RBQ_Punish_Pillory");//首枷輪姦
                break;
            case "CG_AVG_02":
                anim.Play("RBQ_Punish_ShameWagon");//陵辱車接客
                break;
            case "CG_AVG_03":
                anim.Play("RBQ_Punish_Cage");//狗笼肉货
                break;


            case "CG_AVG_07":
                anim.Play("RBQ_Punish_Crucifixion");//倒十字肉圣物  


                //堵嘴
                frameEvents.audioS.Stop();

                switch (Random.Range(0, 3))
                {
                    case 0:
                        frameEvents._03_Resist_5();
                        break;
                    case 1:
                        frameEvents._03_Voice_Struggle_1();
                        break;
                    case 2:
                        frameEvents._03_Voice_Struggle_2();
                        break;

                }
                break;
        }

    }//正面强奸
    void DelayRBQToSide()
    {
        isDie = true;

        //长音频之前停声音
        frameEvents.audioS.Stop();


        if (Random.Range(0, 2) == 0)
        {
            //迫使朝右（第二种变化的强奸）
            anim.SetFloat("InputX", -1);
            anim.SetFloat("InputY", 0);

            //这种向右的是从CG这里直接扒过来，带着帧事件的声音，所以不发循环声
        }
        else
        {
            //迫使朝左
            anim.SetFloat("InputX", 1);
            anim.SetFloat("InputY", 0);


          
            switch (Random.Range(0, 11))
            {
                case 0:
                    frameEvents._03_H_ContinualClimax_0();
                    break;
                case 1:
                    frameEvents._03_H_ContinualClimax_1();
                    break;
                case 2:
                    frameEvents._03_H_ContinualClimax_2();
                    break;
                case 3:
                    frameEvents._03_H_ContinualClimax_3();
                    break;
                case 4:
                    frameEvents._03_H_Gasping_0();
                    break;
                case 5:
                    frameEvents._03_H_Gasping_1();
                    break;
                case 6:
                    frameEvents._03_H_Gasping_Weak_0();
                    break;
                case 7:
                    frameEvents._03_H_Gasping_Weak_1();
                    break;
                case 8:
                    frameEvents._03_H_Gasping_Quick_0();
                    break;
                case 9:
                    frameEvents._03_H_Gasping_Quick_1();
                    break;
                case 10:
                    frameEvents._03_H_Gasping_Quick_2();
                    break;
            }
        }


        switch (GameFlowData.nextScene)
        {
            case "CG_AVG_01":
                anim.Play("RBQ_Punish_Pillory");//首枷輪姦
                break;
            case "CG_AVG_02":
                anim.Play("RBQ_Punish_ShameWagon");//陵辱車接客
                break;
            case "CG_AVG_03":
                anim.Play("RBQ_Punish_Cage");//狗笼肉货
                break;

            case "CG_AVG_07":
                anim.Play("RBQ_Punish_Crucifixion");//倒十字肉圣物  

                //堵嘴
                frameEvents.audioS.Stop();

                switch (Random.Range(0, 3))
                {
                    case 0:
                        frameEvents._03_Resist_5();
                        break;
                    case 1:
                        frameEvents._03_Voice_Struggle_1();
                        break;
                    case 2:
                        frameEvents._03_Voice_Struggle_2();
                        break;
                  
                }

                break;
        }

        


        //不知道为啥会有魔法阵出来
        isMage = false;




    }//侧面强奸

    void Delay_Breath_Voice()
    {
       

        //随机喘息
        switch (Random.Range(0, 4))
        {
            case 0:
                frameEvents._03_Breath_0();
                break;
            case 1:
                frameEvents._03_Breath_1();
                break;
            case 2:
                frameEvents._03_Breath_2();
                break;
            case 3:
                frameEvents._03_Breath_3();
                break;
            case 4:
                frameEvents._03_Breath_4();
                break;
            case 5:
                frameEvents._03_Breath_5();
                break;
        }

    }




    #endregion
}


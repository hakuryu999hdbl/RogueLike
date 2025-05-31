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

    private void Start()
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


    }

    void FixedUpdate()
    {
        if (!isDie)
        {
            BaseMove();//站走跑攻


            if (isKeepWeapon)
            {
                WeaponDrawn();//持械切换
            }

        }
        else
        {
            //倒下后不能移动
            moveSpeed = 0;
            aiPath.maxSpeed = 0f;

            //只要倒地就不显示
            attack_Collider.SetActive(false);
            attack_Range.SetActive(false);

        }







        //始终跟随目标
        if (CurrentTarget != null)
        {
            _Target.transform.position = CurrentTarget.transform.position;

        }



        // 每帧更新剑物体的旋转
        Strike_Effect.transform.Rotate(0, 0, 100 * Time.deltaTime);
    }

    public bool isPatrol = false;
    public bool isAttack = false;
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

    public AIPath aiPath;// A* 路径控制器

    public GameObject Arrow;//小地图朝向

    [Header("速度岔开")]
    float RunSpeed = 4f;
    float WalkSpeed = 2f;





    private void BaseMove()
    {

        if (aiPath == null || !aiPath.hasPath) return;

        Vector2 current = transform.position;
        Vector2 target = aiPath.steeringTarget;

        Vector2 dir = (target - current).normalized;



        float dist = Vector2.Distance(current, target);

        if (!isPatrol)
        {
            if (!isAttack)
            {
                // 设置速度与动画状态
                if (dist > 1)
                {


                    if (tag != "Friend")
                    {
                        //目前战斗下全员跑
                        moveSpeed = 2;
                        aiPath.maxSpeed = RunSpeed;
                    }
                    else if (!isPatrol)
                    {
                        //非巡逻队友跟，随情况下会你走/我也走/你跑/我也跑

                        if (player.isRunning)
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







                }
                else
                {
                    moveSpeed = 0;
                    aiPath.maxSpeed = 0.01f;
                }

                attack_Range.SetActive(false);//关闭技能范围
            }
            else
            {
                BaseAttack();//攻击

                moveSpeed = 0;
                aiPath.maxSpeed = 0.01f;


                attack_Range.SetActive(true);//显示技能范围

            }



            //一旦target没有了就自动玩家
            if (CurrentTarget == null)
            {
                CurrentTarget = _Player;

            }

            AntiOverlapping.SetActive(true);//这个玩意会让敌人队友不重叠，但是巡逻的时候会贴在一起，巡逻的时候去掉
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
    /// 持械状态
    /// </summary>
    #region
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


                anim.ResetTrigger("DrawWeapon");    // 重置拔刀状态，避免残留
                anim.SetTrigger("SheatheWeapon");

                frameEvents._Attack_katana_in();

                isKeepWeapon = false;
            }
        }
        else
        {
            weaponIdleTimer = 0f;
        }
    }


    #endregion




    /// <summary>
    /// 攻击系统
    /// </summary>
    #region
    [Header("蓄力攻击")]


    public GameObject attack;//伤害朝向
    public GameObject attack_Collider;//伤害碰撞体
    public GameObject attack_Range;//技能范围



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
                Attack_Start(); // 攻击警告开始闪

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



    bool OneTimeAttak = false;

    void Attack_Start()
    {
        InvokeRepeating(nameof(FlashWarning), 0f, 0.1f);


        if (Random.Range(0, 3) == 2)
        {
            anim.SetTrigger("Attack");
        }
        else
        {
            anim.SetTrigger("Kick");
        }


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

        Invoke("Attack_Cancel", 1f);//一旦动画帧事件被跳过就会站着不动不攻击，所以这个还是Invoke触发
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

    [Header("生命值体力值等数值")]
    public int currentHealth;
    public int maxHealth;

    //伤害显示
    public bool isScreaming;
    public HudText HudText;



    public void ChangeHealth(int amount, int TypeOfAttack)//【攻击方式  0无  1剑击特效  2闪电特效  3冻结
    {

        if (!isScreaming)
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


                if (Random.Range(0, 3) == 0 && !isDie && currentHealth > 0 && amount != -currentHealth)
                {
                    anim.SetTrigger("Block");

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

            //有1秒左右的伤害冷却
            Invoke("HurtOver", 0.5f);

            isScreaming = true;

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
        }


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





    [Header("暴击")]
    public GameObject Critial;
    public GameObject Assassinate;//暗杀
    public void CritialAttack()
    {

        Knockdown();



        Time.timeScale = 0;

        //显示暴击
        Critial.SetActive(true);



    }//暴击

    public void Knockdown()
    {


        isDie = true;
        anim.SetTrigger("Die");

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
        anim.SetTrigger("Die_2");//防止倒下又起来,搞了第二死亡

        Invoke("Disappear", 1f);
    }//死亡


    [Header("全部自身存在")]
    public GameObject AllOfThis;
    void Disappear()
    {
        Destroy(AllOfThis);

        RoomGenerator.SetEnemy();

        Time.timeScale = 1;//防止 Critial消失之前次物体已经被毁坏，然后卡住不动了
    }




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
    public EnemyVision vision_2;
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

        //  视野脚本2：变成队友
        vision_2.isFriend = true;

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
    }
    #endregion





}


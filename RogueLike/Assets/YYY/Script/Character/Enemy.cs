using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("主动触发声音")]
    public FrameEvents frameEvents;

    [Header("寻找玩家")]
    public GameObject _Player;//玩家
    public Player player;

    private void Start()
    {
        //找玩家
        _Player = GameObject.FindGameObjectWithTag("Player");
        player = _Player.GetComponent<Player>();

        //初始都前往玩家附近
        //CurrentTarget = _Player;


        UpdateAllBar();//更新UI

        //ConvertToFriend();
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

      

        //始终跟随目标
        if (CurrentTarget != null)
        {
            _Target.transform.position = CurrentTarget.transform.position;

        }

        //一旦target没有了就自动玩家
        if (CurrentTarget == null)
        {
            CurrentTarget = _Player;

        }

        // 每帧更新剑物体的旋转
        Strike_Effect.transform.Rotate(0, 0, 100 * Time.deltaTime);
    }

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



    //public bool canMove = true;


    private void BaseMove()
    {

        if (aiPath == null || !aiPath.hasPath) return;

        Vector2 current = transform.position;
        Vector2 target = aiPath.steeringTarget;

        Vector2 dir = (target - current).normalized;



        float dist = Vector2.Distance(current, target);

        if (!isAttack)
        {
            // 设置速度与动画状态
            if (dist > 1)
            {

                if (player.isRunning)
                {
                    moveSpeed = 2;
                    aiPath.maxSpeed = 4f;
                }
                else
                {
                    moveSpeed = 1;
                    aiPath.maxSpeed = 2f;

                }

            }
            else
            {
                moveSpeed = 0;
                aiPath.maxSpeed = 0.2f;
            }

            //attack_Range.SetActive(false);//关闭技能范围
        }
        else
        {
            BaseAttack();//攻击

            moveSpeed = 0;
            aiPath.maxSpeed = 0f;
        }



        //if (!canMove)
        //{
        //    aiPath.canMove = false;
        //}
        //else
        //{
        //    aiPath.canMove = true;
        //}




        // 八方向判断（上下左右为主）
        if (dir.x > 0.5f)
        {
            inputX = 1; inputY = 0;
            attack.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (dir.x < -0.5f)
        {
            inputX = -1; inputY = 0;
            attack.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dir.y > 0.5f)
        {
            inputX = 0; inputY = 1;
            attack.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (dir.y < -0.5f)
        {
            inputX = 0; inputY = -1;
            attack.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            //inputX = 0; inputY = 0;

            inputX = 0; inputY = -1;//朝正面
        }

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

        if (moveSpeed == 0&&!isAttack)
        {
            weaponIdleTimer += Time.deltaTime;

            // 如果2秒内完全没动/没攻击，则自动收刀
            if (weaponIdleTimer >= sheathDelay)
            {
                weaponIdleTimer = 0f;

                anim.SetTrigger("DrawWeapon");

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

    void BaseAttack()
    {


        //attack_Range.SetActive(true);//技能范围

        //隔一会触发一下攻击
        if (!OneTimeAttak)
        {
            Invoke("Attack_Start", 1f);

            isKeepWeapon = true;

            OneTimeAttak = true;
        }
    }


    bool OneTimeAttak = false;

    void Attack_Start()
    {

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
        OneTimeAttak = false;
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
    /// 索敌系统
    /// </summary>
    #region
    [Header(" 索敌系统")]
    public GameObject _Target;//寻路目标
    public GameObject CurrentTarget;//当前的目标

    void TargetIs()
    {

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

    [Header("生命值体力值等数值")]
    public int currentHealth;
    public int maxHealth;

    //伤害显示
    public bool isScreaming;
    public HudText HudText;

    public GameObject BloodEffect;//受伤特效

    public void ChangeHealth(int amount, int TypeOfAttack)//【攻击方式】 0无  1剑击特效  2闪电特效  3冻结
    {

        if (!isScreaming)
        {

            if (amount < 0)
            {

                if (Random.Range(0, 3) == 0 && !isDie) 
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



            //击倒再站起
            if (Random.Range(0, 2) == 0 && !isDie)
            {
                isDie = true;
                anim.SetTrigger("Die");

                //防止最后一下又击倒站起
                if (currentHealth >= 0)
                {
                    Invoke("GetUp", 1f);
                }
               
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
            isDie = true;
            anim.SetTrigger("Die");

            Invoke("Disappear", 1f);
        }

    }

    void HurtOver()
    {
        isScreaming = false;
    }//有1秒左右的伤害冷却

    void GetUp() 
    {
        isDie = false;
        anim.SetTrigger("GetUp");
    }
    [Header("全部自身存在")]
    public GameObject AllOfThis;
    void Disappear() 
    {
        Destroy(AllOfThis);
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

        Debug.Log($"{gameObject.name} has switched to Friend.");
    }

    // 切换为敌人
    public void ConvertToEnemy()
    {
        // 1. 修改标签
        this.tag = "Enemy";

        // 2. 视野脚本：不是队友
        vision.isFriend = false;

        // 3. 攻击脚本：攻击玩家和友军，不攻击敌人
        strike.DamageToPlayer = true;
        strike.DamageToEnemy = false;
        strike.DamageToFriend = true;

        // 4. 改变血条颜色为红色（敌人色）
        HealthValueImage.color = Color.red;

        // 5. 改变攻击实体面积显示颜色为红色
        AttackColliderImage.color = Color.red;

        // 6. 改变攻击范围显示颜色为红色
        AttackRangeImage.color = Color.red;

        Debug.Log($"{gameObject.name} has switched to Enemy.");
    }
    #endregion



   

}


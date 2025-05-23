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
        CurrentTarget = _Player;


        UpdateAllBar();//更新UI

        anim.SetTrigger("None");
        anim.SetInteger("AttackMode", 1);
    }

    void FixedUpdate()
    {
        BaseMove();//站走跑攻


        //始终跟随目标
        if (CurrentTarget != null)
        {
            _Target.transform.position = CurrentTarget.transform.position;

        }

    }

    public bool isAttack = false;

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
    /// 攻击系统
    /// </summary>
    #region
    [Header("蓄力攻击")]


    public GameObject attack;//伤害朝向
    public GameObject attack_Collider;//伤害碰撞体
    public GameObject attack_Range;//技能范围

    void BaseAttack()
    {
        //anim.SetInteger("Speed", 3);

        //attack_Range.SetActive(true);//技能范围

        //隔一会触发一下攻击
        if (!OneTimeAttak)
        {
            Invoke("Attack_Start", 1f);

            OneTimeAttak = true;
        }
    }


    bool OneTimeAttak = false;

    void Attack_Start()
    {
        anim.SetTrigger("Attack");
        // if (Random.Range(0, 2) == 0)
        // {
        //     anim.SetTrigger("Attack");
        // }
        // else
        // {
        //     anim.SetTrigger("Kick");
        // }

        Invoke("StartAttack", 0.5f);
    }
    void StartAttack()
    {
        attack_Collider.SetActive(true);
        Invoke("Attack_Cancel", 0.5f);
    }

    void Attack_Cancel()
    {
        attack_Collider.SetActive(false);
        OneTimeAttak = false;
    }



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

                if (Random.Range(0,2)==0) 
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
    }

    void HurtOver()
    {
        isScreaming = false;
    }//有1秒左右的伤害冷却





    //生命值UI显示
    public Image HealthBar;
    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        HealthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }//Enemy，Friend，NPC替代UIManager的地方


    #endregion
}


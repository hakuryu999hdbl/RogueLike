using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{


    private void Start()
    {
        RegisterHandle();//登录手柄控制

        UpdateAllBar();//更新UI
    }


    private void FixedUpdate()
    {
        BaseMove();//站走跑攻
    }



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


        if (inputX > 0.5f) { inputX = 1; inputY = 0; attack.transform.rotation = Quaternion.Euler(0, 0, -90); }//右
        else if (inputX < -0.5f) { inputX = -1; inputY = 0; attack.transform.rotation = Quaternion.Euler(0, 0, 90); }//左
        else if (inputY > 0.5f && inputX > -0.5f && inputX < 0.5f) { inputX = 0; inputY = 1; attack.transform.rotation = Quaternion.Euler(0, 0, 0); }//上
        else if (inputY < -0.5f && inputX > -0.5f && inputX < 0.5f) { inputX = 0; inputY = -1; attack.transform.rotation = Quaternion.Euler(0, 0, 180); }//下
        else { inputX = 0; inputY = 0; } // 静止时也归零

        // 保存上一次方向（用于静止状态播放对应Idle动画）
        if (inputX != 0 || inputY != 0)
        {
            StopX = inputX;
            StopY = inputY;
            if (isRunning)
            {
                moveSpeed = 2; speed = 4;
                ChangeStrength(-3);
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
            ChangeStrength(2);
        }

        if (inputY > -0.5f && inputY < 0.5f && inputX > -0.5f && inputX < 0.5f) { speed = 0; }//防止微微拉动拉杆也移动



        CheckAttack();

        if (!canMove || attackPressTime >= 0.2f)
        {
            //rbody.velocity = Vector2.zero;
            input = Vector2.zero;
        }//玩家只有在不攻击不蓄力的时候才能移动

        rbody.velocity = input * speed;

        // 传给 Spine 动画机
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
    private float attackPressTime = 0f;      // 持续按下时长计时器
    private bool attackTriggered = false;    // 是否已经触发攻击动作（防止反复触发）

    public bool canMove = true;

    public GameObject attack;//伤害朝向
    public GameObject attack_Collider;//伤害碰撞体
    public GameObject attack_Range;//技能范围

    void Attack_Start()
    {
        isAttacking = true;
        attackPressTime = 0f;

        attackTriggered = false;
    }

    void Attack_Cancel()
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

        attack_Range.SetActive(false);//关闭技能范围
    }

    void CheckAttack()
    {
        if (isAttacking && !attackTriggered)
        {
            attackPressTime += Time.deltaTime;

            if (attackPressTime >= 0.2f)
            {
                // 进入蓄力待机动画
                moveSpeed = 3;
                attack_Range.SetActive(true);//技能范围
            }
        }
    }

    private void PlayNormalAttack()
    {
        attackTriggered = true;

        anim.SetTrigger("Attack");
    }//普通攻击

    private void PlayChargeAttack()
    {
        attackTriggered = true;
        anim.SetTrigger("Attack");
    }//蓄力攻击





    #endregion


    /// <summary>
    /// 多端输入
    /// </summary>
    #region
    [Header("InputSystem")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionAsset inputActions;

    private InputAction runAction;

    private InputAction AttackAction;


    private void RegisterHandle()
    {
        // 获取Run动作（根据你的Action Map结构可能需要调整路径）
        runAction = inputActions.FindAction("Run");
        AttackAction = inputActions.FindAction("Attack");

        // 订阅输入事件
        runAction.started += OnRunStarted;
        runAction.canceled += OnRunCanceled;

        // 订阅输入事件
        AttackAction.started += OnAttackStarted;
        AttackAction.canceled += OnAttackCanceled;


    }
    private void OnRunStarted(InputAction.CallbackContext context)
    {
        isRunning = true;
    }
    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        isRunning = false;
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        Attack_Start();
    }
    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        Attack_Cancel();
    }

    [Header("手机端触发")]
    public Joystick Joystick;

    public bool isRunning = false;

    //手机端触发
    public void ButtonSetRun()
    {
        isRunning = true;
    }
    public void ButtonSetStop()
    {
        isRunning = false;
    }

    public bool isAttacking = false;

    public void ButtonSetAttack()
    {
        Attack_Start();
    }
    public void ButtonSetAttackOver()
    {
        Attack_Cancel();
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



    [Header("生命值体力值等数值")]
    public int currentHealth;
    public int maxHealth;

    //伤害显示
    public GameObject RedScreen;
    public bool isScreaming;
    public HudText HudText;

    public int currentStrength;
    public int maxStrength;

    public void ChangeHealth(int amount, int TypeOfAttack)//【攻击方式】 0无  1剑击特效  2闪电特效  3冻结
    {
        if (!isScreaming) 
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);

            //显示伤害
            HudText.HUD(amount);

            //有1秒左右的伤害冷却
            Invoke("HurtOver", 0.5f);
            RedScreen.SetActive(true);
            isScreaming = true;

        }
        
    }

    void HurtOver()
    {
        isScreaming = false;
        RedScreen.SetActive(false);
    }//有1秒左右的伤害冷却
    public void ChangeStrength(int amount)
    {

        currentStrength = Mathf.Clamp(currentStrength + amount, 0, maxStrength);
        UIManager.instance.UpdateStrengthBar(currentStrength, maxStrength);
    }


    #endregion

}

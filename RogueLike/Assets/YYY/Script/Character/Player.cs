using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



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


        //随机皮肤(你的皮肤里面Man/Girl是随机的)
        //SetRandomSkin();

        //PlayerSaveData data = SaveManager.Load("CurrentPlayer");
        //if (data != null)
        //{
        //    ApplySaveData(data); // 套用保存的数据（比如皮肤）
        //}
        //else 
        //{
        //    //SaveCurrent();
        //}



        // 随机从 Enum 中选择一个值
        //Class = (PlayerClass)Random.Range(0, System.Enum.GetValues(typeof(PlayerClass)).Length);
        Class = PlayerClass.Girl;

        anim.Play(GetAnimPrefix() + "Default_Idle");

        isMage = true;


       
       
       
       
       
       
       
       
       
    }




    /// <summary>
    /// 存读档
    /// </summary>
    #region

    [Header("当前操纵的存档名称")]
    public string currentSaveName; // 当前操作的存档名

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

        // 根据这些数据设置皮肤
        SetSkin(); // 你已有的方法（或自己写个用这些 Index 设置皮肤的方法）


        //数值赋予
        this.maxHealth = data.maxHP;
        currentHealth = maxHealth;
        UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);

        this.maxStrength = maxHealth;
        currentStrength = maxStrength;
        UIManager.instance.UpdateStrengthBar(currentStrength, maxStrength);

        this.Level  = data.level;
        LevelText.text = Level.ToString();
        maxExperience = data.level * 1000;
        currentExperience = data.exp;
        UIManager.instance.UpdateExperienceBar(currentExperience, maxExperience);

        MeleeDamage = data.meleeDamage;
        ShootDamage = data.shootDamage;
        SpellDamage = data.spellDamage;

        CurrentWeaponPower = data.weaponAtk;
        CurrentArmorDefence = data.armorDef;
        CurrentStockingDefence = data.stockingDef;

        currentSaveName = data.characterName;//记录当前名称

    }//存档形式赋值皮肤数值


    public void _CreateNewSkin()
    {
        SetRandomSkin();
        SaveCurrent();
    }//新增皮肤临时随机

    public void SaveCurrent()
    {

        if (string.IsNullOrEmpty(currentSaveName))
        {
            // 如果还没有命名，则生成

            PlayerSaveData data = new PlayerSaveData();

            List<string> allSaves = SaveManager.GetAllSaveNames();  // 获取已有存档名
            string newName = NameGenerator.GenerateUniqueName(allSaves);
            data.characterName = newName; // ✅ 记得设置进去！

            data.headIndex = this.YYY_headIndex;
            data.eyesIndex = this.YYY_eyesIndex;
            data.bodyIndex = this.YYY_bodyIndex;
            data.legsIndex = this.YYY_legsIndex;
            data.hatIndex = this.YYY_hatIndex;
            data.weaponIndex = this.weaponIndex;


            data.level = 1;
            data.exp = Random.Range(0,1000);
            data.maxHP = 1000;
            data.meleeDamage = 100;
            data.shootDamage = 100;
            data.spellDamage = 100;

            data.weaponAtk = Random.Range(100,200);
            data.armorDef= Random.Range(10,50);
            data.stockingDef = Random.Range(5, 25);

            SaveManager.Save(data);

            currentSaveName = data.characterName;//记录当前名称

        }
        else
        {

            // 处于捏人界面中，已有命名，覆盖当前
            PlayerSaveData data = SaveManager.Load(currentSaveName);

            data.headIndex = this.YYY_headIndex;
            data.eyesIndex = this.YYY_eyesIndex;
            data.bodyIndex = this.YYY_bodyIndex;
            data.legsIndex = this.YYY_legsIndex;
            data.hatIndex = this.YYY_hatIndex;
            data.weaponIndex = this.weaponIndex;

            SaveManager.Save(data);
        }



    }//记录当前皮肤并新建随机名称存档


    public void OpenSaveURL()
    {
        Application.OpenURL(Application.persistentDataPath);
    }//打开存档位置文件夹


    public static class NameGenerator
    {
        static readonly System.Random rng = new System.Random();

        // 每种语言的 First / Last Name 列表
        static readonly string[] JP_First = { "結", "琴", "美", "雪", "凛", "小", "陽", "咲", "優", "愛" };
        static readonly string[] JP_Last = { "月", "音", "咲", "乃", "菜", "子", "花", "里", "奈", "美" };

        static readonly string[] CN_First = { "梦", "思", "晓", "灵", "婉", "语", "可", "诗", "柔", "雪" };
        static readonly string[] CN_Last = { "雪", "儿", "彤", "涵", "瑶", "嫣", "欣", "瑾", "菲", "音" };

        static readonly string[] TC_First = { "夢", "思", "曉", "靈", "婉", "語", "可", "詩", "柔", "雪" };
        static readonly string[] TC_Last = { "雪", "兒", "彤", "涵", "瑤", "嫣", "欣", "瑾", "菲", "音" };

        static readonly string[] EN_First = { "Lily", "Sophie", "Emily", "Chloe", "Emma", "Mia", "Ella", "Grace", "Lucy", "Olivia" };
        static readonly string[] EN_Last = { "Smith", "Brown", "Miller", "Wilson", "Taylor", "White", "Clark", "Hall", "Lewis", "Young" };

        static readonly string[] KR_First = { "지", "수", "하", "예", "소", "채", "은", "유", "민", "서" };
        static readonly string[] KR_Last = { "은", "아", "림", "빈", "진", "연", "희", "원", "지", "경" };

        public static string GenerateUniqueName(List<string> existingNames)
        {
            string baseName = GenerateNameByLanguage();
            string finalName = baseName;
            int index = 1;

            while (existingNames.Contains(finalName))
            {
                finalName = $"{baseName}_{index}";
                index++;
            }

            return finalName;
        }

        private static string GenerateNameByLanguage()
        {
            int lang = PlayerPrefs.GetInt("language", 0); // 0日语 1简中 2繁中 3英文 4韩文

            switch (lang)
            {
                case 0: // Japanese
                    return JP_First[rng.Next(JP_First.Length)] + JP_Last[rng.Next(JP_Last.Length)];
                case 1: // Simplified Chinese
                    return CN_First[rng.Next(CN_First.Length)] + CN_Last[rng.Next(CN_Last.Length)];
                case 2: // Traditional Chinese
                    return TC_First[rng.Next(TC_First.Length)] + TC_Last[rng.Next(TC_Last.Length)];
                case 3: // English: 用空格隔开
                    return EN_First[rng.Next(EN_First.Length)] + " " + EN_Last[rng.Next(EN_Last.Length)];
                case 4: // Korean
                    return KR_First[rng.Next(KR_First.Length)] + KR_Last[rng.Next(KR_Last.Length)];
                default:
                    return JP_First[rng.Next(JP_First.Length)] + JP_Last[rng.Next(JP_Last.Length)];
            }
        }

    }//女孩名称随机（重名的情况下会加编号）

    public void ClearSkin()
    {
        this.YYY_headIndex = 1;
        this.YYY_eyesIndex = 1;
        this.YYY_bodyIndex = 1;
        this.YYY_legsIndex = 1;
        this.YYY_hatIndex = 1;
        this.weaponIndex = 1;

        SetSkin();
    }//清除皮肤

    #endregion


    private void FixedUpdate()
    {



        if (!isDie && currentHealth > 0)//不能&&IsGrounded()
        {
            BaseMove();//站走跑攻

            if (isKeepWeapon)
            {
                WeaponDrawn();//持械切换
            }

        }
        else
        {
            //死亡完全切断所有输入
            rbody.velocity = Vector2.zero; // 停止所有移动

            //anim.Play("Girl_Default_Die_2");//这个地方干扰了跳跃落地

            //rbody.simulated = false;//当玩家挂的时候，如果踩着墙，会导致墙跳出来遮挡视线
            return;
        }

        // 每帧更新剑物体的旋转
        Strike_Effect.transform.Rotate(0, 0, 100 * Time.deltaTime);

        //当这些动画在播放的时候玩家不能移动
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        if (
            state.IsName(GetAnimPrefix() + "Attack_1") ||
            state.IsName(GetAnimPrefix() + "Attack_2") ||
            state.IsName(GetAnimPrefix() + "Attack_3") ||
            state.IsName(GetAnimPrefix() + "Attack_4") ||
            state.IsName(GetAnimPrefix() + "Shoot_1") ||
            state.IsName(GetAnimPrefix() + "Spell_1") ||
            state.IsName(GetAnimPrefix() + "Spell_2") ||

            state.IsName(GetAnimPrefix() + "Strike_Block") ||
            state.IsName(GetAnimPrefix() + "Shoot_Block") ||

            state.IsName(GetAnimPrefix() + "Default_Die") ||
            state.IsName(GetAnimPrefix() + "Default_Die_2") ||
            state.IsName(GetAnimPrefix() + "Default_GetUp") ||
            state.IsName(GetAnimPrefix() + "Default_Hurt") ||
            isFrozen

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
    public bool isRape = false;//防止被多个敌人重复捕获

    /// <summary>
    /// 基础数值
    /// </summary>
    #region
    [Header("基础数值")]
    public Animator anim;//接入Spine动画机
    private float inputX, inputY;
    private int StopX, StopY;

    public bool FirstMove = false;//玩家还没有按下任何按钮的时候，Update里强制方向

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
            StopX = Mathf.RoundToInt(inputX);
            StopY = Mathf.RoundToInt(inputY);
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

            FirstMove = true;//一旦按下方向，初始强制方向去掉
        }
        else
        {
            moveSpeed = 0;
            ChangeStrength(3);
        }

        if (inputY > -0.5f && inputY < 0.5f && inputX > -0.5f && inputX < 0.5f) { speed = 0; }//防止微微拉动拉杆也移动



        CheckAttack();//检测你按着攻击键或者没有
        CheckDodge();//检测你按着闪避键或者没有

        CheckJump();

        if (!canMove|| !IsGrounded())
        {
            input = Vector2.zero;
            moveSpeed = 0;//攻击期间永远不要出现【跑】动画

        }//玩家只有在不攻击的时候才能移动，闪避的时候也无法叠加,在空中时没有输入






        if (!FirstMove)
        {
            StopX = 0;
            StopY = -1;
        }//玩家还没有按下任何按钮的时候，Update里强制方向（正面）

        if (isInputBlocked)
        {
            StopX = 0;
            StopY = -1;

            moveSpeed = 0;

        }//只要处于切断输入中，永远切掉输入正面朝向
        else
        {
            rbody.velocity = input * speed;
        }



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
    public void ChangeType(int t)
    {
        switch (t)
        {
            case 0:
                visionType = PlayerType.ShortRangePlayer; isMage = false;//战士
                break;
            case 1:
                visionType = PlayerType.LongRangePlayer; isMage = false;//射手
                break;
            case 2:
                visionType = PlayerType.LongRangePlayer; isMage = true;//法师
                break;
        }

        //武器（尤其是远程）更新
        CheckWeapon();

    }
    public PlayerClass Class;
    public enum PlayerClass
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
                Class = PlayerClass.Girl;
                break;
            case 1:
                Class = PlayerClass.Man;
                break;
            case 2:
                Class = PlayerClass.Succubus;
                break;
        }

        anim.Play(GetAnimPrefix() + "Default_Idle");
    }
    public string GetAnimPrefix()
    {
        switch (Class)
        {
            case PlayerClass.Girl:
                return "Girl_";
            case PlayerClass.Man:
                return "Man_";
            case PlayerClass.Succubus:
                return "Succubus_";
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
    }//【武器切换】备用



    public void AnimSetWeapon()
    {

        if (visionType == PlayerType.ShortRangePlayer)
        {
            anim.SetInteger("Weapon", 1);
        }
        else
        {
            if (isMage) { anim.SetInteger("Weapon", 3); }
            else { anim.SetInteger("Weapon", 2); }
        }

        CheckWeapon();
    }//设置武器



    public void _ClothesToClass()
    {

        //目前暂时以上衣区别职业
        switch (YYY_bodyIndex)
        {
            case 10:
                ChangeType(0);
                break;
            case 11:
                ChangeType(1);
                break;
            case 12:
                ChangeType(2);
                break;
        }

    }//临时根据玩家的衣服来确定职业



    #endregion

    public void ForCGRandomEnemySkin() 
    {
        Man_headIndex = Random.Range(1, 6);
        Man_bodyIndex = 2;
        Man_hatIndex = Random.Range(1, 3);

        Girl_headIndex = Random.Range(1, 14);  // 1~13
        Girl_eyesIndex = Random.Range(1, 14);  // 1~13
        Girl_bodyIndex = Random.Range(10, 13);
        Girl_legsIndex = Random.Range(10, 13);
        Girl_hatIndex = Random.Range(1, 14);

        SetSkin();
    }//CG鉴赏的时候，敌人皮肤是不存到存档的，所以更新一下

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
        YYY_hatIndex = Random.Range(1, 5);

        Man_headIndex = Random.Range(1, 6);
        Man_bodyIndex = 2;
        Man_hatIndex = Random.Range(1, 3);

        Girl_headIndex = Random.Range(1, 14);  // 1~13
        Girl_eyesIndex = Random.Range(1, 14);  // 1~13
        Girl_bodyIndex = Random.Range(10, 13);
        Girl_legsIndex = Random.Range(10, 13);
        Girl_hatIndex = Random.Range(1, 14);

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

            attack_Range.SetActive(false);//关闭技能范围
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
                attack_Range.SetActive(true);//技能范围

                if (isMage) { ShowMagicEffect(); }//法师产生法阵
            }


        }
        else
        {
            ChangeCritical(-10);//松开暴击率快速下降

            if (isMage) { HideMagicEffect(); }//法师隐藏法阵
        }
    }

    public void _Attack_Cancel()
    {
        if (visionType == Player.PlayerType.ShortRangePlayer)//男性女性女魔族近战都用这个
        {
            canCombo = false;


            if (comboQueued && currentCombo < 4)
            {
                currentCombo++;
                anim.Play(GetAnimPrefix() + "Attack_" + currentCombo, 0, 0);
                comboQueued = false;
            }
            else
            {
                ResetCombo();
            }
        }
        else
        {
            //只有魔族需要更改
            if (Class == PlayerClass.Succubus)
            {
                anim.Play(GetAnimPrefix() + "Default_Idle");
            }
            else
            {

                if (isMage) { anim.Play(GetAnimPrefix() + "Spell_Idle"); }
                else { anim.Play(GetAnimPrefix() + "Shoot_Idle"); }

            }



            //player.CanShoot = true;//这里用Invoke替代了
        }




        // 攻击完毕扣除暴击值
        ChangeCritical(-100);
        //player.ChangeCritical(-player.maxCritical); // 或者换成一部分
    }


    [Header("攻击")]
    public int currentCombo = 0;
    public bool isAttacking2 = false;//是否处于单个连击近战攻击动画中
    public bool canCombo = false;
    public bool comboQueued = false;


    public void PlayNormalAttack()
    {
        

        if (!isDie)
        {
            TryCrit();

            //if (isDodge) { strike.isCritial = true; }//闪避中攻击冲刺（这个可以做冲刺攻击动画）

            attackTriggered = true;//这个要不要留着考虑一下


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
                    if (Class == PlayerClass.Succubus) { anim.Play(GetAnimPrefix() + "Shoot_1", 0, 0); }
                    else
                    {
                        if (isMage)
                        {
                            if (MageAttackType)
                            {
                                anim.Play(GetAnimPrefix() + "Spell_1", 0, 0);
                            }
                            else
                            {
                                anim.Play(GetAnimPrefix() + "Spell_2", 0, 0);
                            }
                            MageAttackType = !MageAttackType;

                        }
                        else { anim.Play(GetAnimPrefix() + "Shoot_1", 0, 0); }
                    }



                    CanShoot = false;
                    Invoke("SetCanShoot", 0.3f);//似乎这是目前唯一
                }

            }



            isKeepWeapon = true;//进入武器状态
        }


    }//普通攻击

    bool MageAttackType = false;

    void StartCombo()
    {
        currentCombo = 1;
        isAttacking2 = true;
        anim.Play(GetAnimPrefix() + "Attack_1", 0, 0);


    }

    public void ResetCombo()
    {
        if (currentHealth > 0)
        {
            currentCombo = 0;
            comboQueued = false;
            canCombo = false;
            isAttacking2 = false;


            //只有魔族需要更改
            if (Class == PlayerClass.Succubus)
            {
                anim.Play(GetAnimPrefix() + "Default_Idle");
            }
            else
            {
                //这里不知道什么原因，必须分开
                if (visionType == PlayerType.ShortRangePlayer)
                {
                    anim.Play(GetAnimPrefix() + "Strike_Idle");
                }
                else
                {
                    if (isMage) { anim.Play(GetAnimPrefix() + "Spell_Idle"); }
                    else { anim.Play(GetAnimPrefix() + "Shoot_Idle"); }


                }

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


    public void BattleCryVoice()
    {
        switch (Class)
        {
            case PlayerClass.Girl:
            case PlayerClass.Succubus:
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
            case PlayerClass.Man:
                frameEvents._Man_attack();//男性
                break;

                //case 2:
                //case 3:
                //    switch (Random.Range(0, 2))
                //    {
                //        case 0:
                //            frameEvents._Zombie_Summon_1();
                //            break;
                //        case 1:
                //            frameEvents._Zombie_Summon_2();
                //            break;
                //    }//感染者 变异体
                //    break;
                //case 4:
                //    switch (Random.Range(0, 2))
                //    {
                //        case 0:
                //            frameEvents._Orangutan_Summon_1();
                //            break;
                //        case 1:
                //            frameEvents._Orangutan_Attack_1();
                //            break;
                //    }//肉翅蜂
                //    break;
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



        switch (CurrentWeapon)
        {



            case 201:
            case 207:
                bullet.GetComponent<Shooting>().SetSpecialBullet(5);//剧毒法球
                break;
            case 203:
            case 210:
                bullet.GetComponent<Shooting>().SetSpecialBullet(3);//火焰法球
                break;
            case 204:
            case 206:
            case 208:
                bullet.GetComponent<Shooting>().SetSpecialBullet(4);//冰冻法球
                break;
            case 205:
            case 209:
            case 202:
                bullet.GetComponent<Shooting>().SetSpecialBullet(2);//雷电法球
                break;

            case 101:
            case 102:
            case 103:
                frameEvents._Bullet_Arrow();
                bullet.GetComponent<Shooting>().SetSpecialBullet(1);//弩弓
                break;


            case 104:
                bullet.GetComponent<Shooting>().SetSpecialBullet(0);//子弹
                frameEvents._Bullet_Pistol();
                break;
            case 105:
                bullet.GetComponent<Shooting>().SetSpecialBullet(0);//子弹
                frameEvents._Bullet_Pistol_2();
                break;
            case 106:
                bullet.GetComponent<Shooting>().SetSpecialBullet(0);//子弹
                frameEvents._Bullet_Pistol_3();
                break;
            case 107:
                bullet.GetComponent<Shooting>().SetSpecialBullet(0);//子弹
                frameEvents._Bullet_AK();
                break;
            case 108:
            case 109:
            case 110:
                bullet.GetComponent<Shooting>().SetSpecialBullet(0);//子弹
                frameEvents._Bullet_SD();
                break;


            default:
                bullet.GetComponent<Shooting>().SetSpecialBullet(0);//子弹
                break;
        }

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
    /// 武器系统
    /// </summary>
    #region
    [Header("武器系统")]
    public int CurrentWeapon;
    //0无武器
    //1铁剑  2阔剑  3长柄双刃斧  4长枪   5长柄斧   6冻结剑   7黑铁刺剑  8熔岩剑  9引雷剑  10古重剑
    //101轻弩   102重弩   103复合弩   104火绳复合枪  105火绳短枪   106火绳长枪   107火绳黄铜枪
    //201黄木短杖  202鹰身短杖   203红宝石短杖    204蓝宝石短杖   205黄玉短杖   206冰冻法杖  207紫水晶法杖  208翡翠法杖  209雷霆法杖  210古木法杖

    public void CheckWeapon()
    {

        if (visionType == PlayerType.ShortRangePlayer) { CurrentWeapon = weaponIndex; }//实装战士武器
        if (visionType == PlayerType.LongRangePlayer && !isMage) { CurrentWeapon = weaponIndex + 100; }//实装射手武器
        if (visionType == PlayerType.LongRangePlayer && isMage) { CurrentWeapon = weaponIndex + 200; }//实装法师武器

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
    public int MeleeDamage;
    public int ShootDamage;
    public int SpellDamage;

    public int CurrentWeaponPower;    // 武器攻击值
    public int CurrentArmorDefence;      // 衣服防御值
    public int CurrentStockingDefence;   // 丝袜防御值
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
                Knockdown();
               
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
    /// 闪避系统
    /// </summary>
    #region
    [Header("闪避键按下")]
    public float dodgePressTime = 0f;      // 持续按下时长计时器
    public bool dodgeTriggered = false;    // 是否已经触发攻击动作（防止反复触发）


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
                if (canMove)
                {
                    if (dodgePressTime < 0.2f)
                    {
                        PlayDodge(); // 闪避
                    }
                    else
                    {
                        //魔族变身

                        if (Class == PlayerClass.Succubus)
                        {
                            ChangeClass(0);
                        }
                        else
                        {
                            ChangeClass(2);
                        }
                        GateEffect.SetActive(true);//传送门特效

                        //PlayChargeAttack(); // 蓄力攻击
                    }
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
        //当这些动画在播放的时候玩家不可以闪避(动画与可移动重合)
        if (!canMove) 
        {
            return;//防止连续闪避
        }


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

    public bool isInputBlocked = true;//在捏人界面暂时切断玩家的输入

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

        if (!isDie && currentHealth > 0 && canMove && !isInputBlocked && IsGrounded())
        {
            isRunning = true;
        }

    }
    private void OnRunCanceled(InputAction.CallbackContext context)
    {

        if (!isDie && currentHealth > 0 && !isInputBlocked && IsGrounded())
        {
            isRunning = false;
        }

    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {

        if (!isDie && currentHealth > 0 && !isInputBlocked && IsGrounded())
        {
            Attack_Start();
        }

    }
    private void OnAttackCanceled(InputAction.CallbackContext context)
    {

        if (!isDie && currentHealth > 0 && !isInputBlocked && IsGrounded())
        {
            Attack_Cancel();
        }

    }

    private void OnDodgeStarted(InputAction.CallbackContext context)
    {

        if (!isDie && currentHealth > 0 && !isInputBlocked && IsGrounded())
        {
            Dodge_Start();
        }

    }
    private void OnDodgeCanceled(InputAction.CallbackContext context)
    {

        if (!isDie && currentHealth > 0 && !isInputBlocked && IsGrounded())
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

        if (!isDie && currentHealth > 0 && !isInputBlocked && IsGrounded())
        {
            isRunning = true;
        }

    }
    public void ButtonSetStop()
    {

        if (!isDie && currentHealth > 0 && !isInputBlocked && IsGrounded())
        {
            isRunning = false;
        }

    }

    //手机端触发
    public bool isAttacking = false;//持续按下攻击键
    public void ButtonSetAttack()
    {

        if (!isDie && currentHealth > 0 && !isInputBlocked && IsGrounded())
        {
            Attack_Start();
        }
    }
    public void ButtonSetAttackOver()
    {

        if (!isDie && currentHealth > 0 && !isInputBlocked && IsGrounded())
        {
            Attack_Cancel();
        }

    }

    //手机端触发
    public bool isDodging = false;//持续按下闪避键
    public void ButtonSetDodge()
    {

        if (!isDie && currentHealth > 0 && !isInputBlocked && IsGrounded())
        {
            Dodge_Start();
        }
    }
    public void ButtonSetDodgeOver()
    {

        if (!isDie && currentHealth > 0 && !isInputBlocked && IsGrounded())
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
    public GameObject GateEffect;//传送门特效
    public GameObject Palsy_Effect;//闪电特效
    public GameObject Frozen_Effect;//冻结特效
    public GameObject ProtectiveCoverEffect;//防护罩特效

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
           
                        if (Class == PlayerClass.Succubus || isMage)
                        {
                            ProtectiveCoverEffect.SetActive(true);
           
                        }//只有魔族和法师需要特效（遮挡无防御动画）
           
                        if (visionType == PlayerType.ShortRangePlayer)
                        {
                            anim.Play(GetAnimPrefix() + "Strike_Block");
                        }
                        else
                        {
           
                            if (isMage)
                            { 
                                //没有法师防御动画
                            }
                            else { anim.Play(GetAnimPrefix() + "Shoot_Block"); }
           
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
                    Palsy_Effect.SetActive(true);//雷电伤害
                    break;
                case 3:
                    Freeze(1);//冻结伤害
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

                anim.Play(GetAnimPrefix() + "Default_Die_2");

                Critical.SetActive(false);

                UIManager.instance.Ending_UI();

                return;
            }

            //击倒再站起

            if (!isDie && currentHealth > 0) 
            {
                if (Random.Range(0, 2) == 0)
                {
                    Knockdown();

                    Critial.SetActive(true);
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
                    //Invoke("ReSetAttack", 0.5f);//防止动画回不去(这个在被击倒/站起流程后)
                }
            }
           
        }

    }


    void HurtOver()
    {
        isScreaming = false;
        RedScreen.SetActive(false);
    }//有1秒左右的伤害冷却

    public void Knockdown()
    {


        isDie = true;

        anim.Play(GetAnimPrefix() + "Default_Die");

        //防止最后一下又击倒站起
        if (currentHealth > 0)
        {
            Invoke("GetUp", 0.5f);//比起敌人，玩家可以更快站起来

            Invoke("ReSetAttack", 0.5f);//防止动画回不去(这个在被击倒/站起流程后)
        }

    }//击倒

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

    [Header("经验值")]
    public int currentExperience;
    public int maxExperience;

    public int Level;
    public Text LevelText;

    public GameObject LevelUpEffect;
    public void ChangeExperience(int amount)
    {
        PlayerSaveData data = SaveManager.Load(currentSaveName);

        currentExperience = Mathf.Clamp(currentExperience + amount, 0, maxExperience);
        UIManager.instance.UpdateExperienceBar(currentExperience, maxExperience);


        if (currentExperience >= maxExperience)
        {
            Level += 1;
            maxExperience = Level * 1000;
            LevelText.text = Level.ToString();

            data.level = Level;


            currentExperience = 0;
            UIManager.instance.UpdateExperienceBar(currentExperience, maxExperience);      

            LevelUpEffect.SetActive(true);




            //随机升级一项数值并储存（但是当前武器的偏向会增大）

            switch (Random.Range(0,5)) 
            {
                case 0:
                    //升级奖励：增大最大体力值和生命值(回满状态)
                    data.maxHP = maxHealth + 100;

                    this.maxHealth = data.maxHP;
                    currentHealth = maxHealth;
                    UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);

                    this.maxStrength = maxHealth;
                    currentStrength = maxStrength;
                    UIManager.instance.UpdateStrengthBar(currentStrength, maxStrength);

                    break;
                case 1:
                    //升级奖励：增大近战伤害
                    data.meleeDamage = MeleeDamage + 10;
                    break;
                case 2:
                    //升级奖励：增大远程伤害
                    data.shootDamage = ShootDamage + 10;
                    break;
                case 3:
                    //升级奖励：增大法术伤害
                    data.spellDamage = SpellDamage + 10;
                    break;
            }


        }


     
        data.exp = currentExperience;
        SaveManager.Save(data);

        //我不太清除频繁刷新会不会不太好……
        UIManager.instance.RefreshSaveSlots();
    }

    #endregion

    /// <summary>
    /// 异常状态
    /// </summary>
    #region
    //————————————————————冻结
    public bool isFrozen = false;
    public void Freeze(int Timer)
    {

        anim.speed = 0f;// 将动画速度设置为0，冻结动画
        Frozen_Effect.SetActive(true);//在受到冰冻伤害的时候就已经非处冰冻特效了,这里再写一遍因为有些时候敌人挡住了伤害，这里的冰冻是无法被挡住的  




        isFrozen = true;

        Invoke("Recover", Timer);
    }
    public void Recover()//死亡，自我恢复，麻痹恢复调用
    {

        isFrozen = false;

        Frozen_Effect.SetActive(false);//去除冻结特效

        anim.speed = 1f; // 恢复到原来的时间缩放值，解除冻结


    }


    #endregion
}

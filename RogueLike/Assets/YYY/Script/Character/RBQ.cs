using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RBQ : MonoBehaviour
{
    [Header("主动触发声音")]
    public FrameEvents frameEvents;//这个和Spine上面区分开来，防止声音叠住

    [Header("寻找RoomGenerator")]
    RoomGenerator _RoomGenerator;//寻找RoomGenerator



    [Header("基础数值")]
    public Animator anim;//接入Spine动画机
    private string[] tortureAnimations = { "RBQ_Torture_Impale", "RBQ_Torture_Strangle", "RBQ_Torture_CutDown" };

    public int RBQState = 0;//0单人拘束 1双人拷问中  2尸体  3肉货
    bool isCreateEnemy = false;//是否产生过敌人

    public int CurrentRapeType = 0;//1吊缚抽打 2后入奸
    public GameObject Torture_Rack;//刑架

    private float inputX, inputY;


    void Start()
    {
        //寻找RoomGenerator
        _RoomGenerator = GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<RoomGenerator>();

        if (RBQState == 0) { RBQState = Random.Range(1, 3); }//如果一开始没有赋值，那么随机


        // 随机动画
        switch (RBQState)
        {
            case 1:
                //被拷问
                //string animName = punishAnims[Random.Range(0, punishAnims.Length)];
                //anim.Play(animName);

                CurrentRapeType = Random.Range(1, 3);

                switch (CurrentRapeType)
                {
                    case 1:
                        anim.Play("RBQ_Punish_Hang");
                        break;
                    case 2:
                        anim.Play("RBQ_Punish_Rape");
                        break;

                }

                //循环叫声
                InvokeRepeating("Gasping_Long", 1f, 58f);

                Destroy(Check.gameObject);
                break;
            case 2:
                //尸体
                int rand = Random.Range(0, tortureAnimations.Length);
                anim.Play(tortureAnimations[rand]);

                Destroy(Check.gameObject);

                break;
            case 3:
                //商店肉货
                anim.Play("RBQ_Display_Idle_Front");

                Check.SetActive(true);

                //商店随机武器
                GenerateRandomWeapons();

                break;
        }



        // 根据方向旋转（可选，或控制朝向动画片段）
        ApplyFacingRotation();


        //随机皮肤
        SetRandomSkin();


    }

    void Gasping_Long()
    {
        //循环叫声
        frameEvents._02_Connection_Gasping_Long_0();
    }//优先级高128→28

    void ApplyFacingRotation()
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                inputX = 1; inputY = 0;
                break;
            case 2:
                inputX = -1; inputY = 0;
                break;
            case 3:
                inputX = 0; inputY = 1;
                break;
            case 4:
                inputX = 0; inputY = -1;
                break;
        }

        // 动画传入方向
        anim.SetFloat("InputX", inputX);
        anim.SetFloat("InputY", inputY);
    }



    /// <summary>
    /// 触发点
    /// </summary>
    #region
    [Header("出生点WallMap")]
    public WallMap wallmap;

    [Header("出生点WallMap")]
    public GameObject Prompt_Save;
    public GameObject Prompt_Take;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (RBQState == 1 && !isCreateEnemy)
            {
                //出现敌人,停止拷问，冲向玩家
                GameObject NewEnemy = Instantiate(_RoomGenerator.Enemy, transform.position, Quaternion.identity);
                Enemy enemy = NewEnemy.transform.Find("Enemy").GetComponent<Enemy>();
                //enemy.wallmap = wallmap;//告诉自己生成的Enemy出生点WallMap
                enemy.CanChangeSkin = false;
                StartCoroutine(DelayedApplySkin(enemy));
                enemy.ChangeClass(1);



                //RBQState = 0;


                switch (CurrentRapeType)
                {
                    case 1:
                        anim.Play("RBQ_Punish_Hang_2");
                        break;
                    case 2:
                        anim.Play("RBQ_Punish_Rape_2");
                        break;

                }


                //停止播放
                frameEvents.audioS.Stop();
                CancelInvoke(nameof(Gasping_Long));


                // 监听敌人状态（把原来立刻 RBQState=0 的代码删掉）
                if (_waitEnemyRoutine != null) StopCoroutine(_waitEnemyRoutine);
                _waitEnemyRoutine = StartCoroutine(WaitEnemyGoneThenReset(enemy));

                isCreateEnemy = true;
            }

            if (RBQState == 0)
            {
                Prompt_Save.SetActive(true);
            }

            //if (RBQState == 3 && RBQState != 3)//商店状态下这里不要触发取下
            //{
            //    Prompt_Take.SetActive(true);
            //}


        }
    }
    private Coroutine _waitEnemyRoutine;
    private IEnumerator WaitEnemyGoneThenReset(Enemy enemy)
    {
        // 等待敌人“死亡/销毁/失活”
        yield return new WaitUntil(() =>
            enemy == null ||                      // 已销毁（Destroy）
            !enemy.gameObject.activeInHierarchy ||// 被 SetActive(false)（对象池回收）
            (enemy.currentHealth <= 0)    // 如果你有死亡标记接口/字段
        );

        RBQState = 0;

        // 这里需要的话可播放恢复动画/解锁等
        // anim.Play("RBQ_Idle");
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (RBQState == 0)
            {
                Prompt_Save.SetActive(false);
            }

            //if (RBQState == 3 && RBQState!=3)//商店状态下这里不要触发取下
            //{
            //    Prompt_Take.SetActive(false);
            //}
        }
    }


    bool InteractOneTime = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (RBQState == 0 && other.GetComponent<Player>().isInteracting)//点击交互键
            {
                if (!InteractOneTime)
                {
                    //奖励一个队友
                    GameObject NewEnemy = Instantiate(_RoomGenerator.Enemy, transform.position, Quaternion.identity);
                    Enemy enemy = NewEnemy.transform.Find("Enemy").GetComponent<Enemy>();
                    //enemy.wallmap = wallmap;//告诉自己生成的Enemy出生点WallMap
                    enemy.CanChangeSkin = false;
                    StartCoroutine(DelayedApplySkin(enemy));
                    enemy.ChangeClass(0);


                    enemy.ConvertToFriend();



                    enemy.ReadyToSayThankYou();//谢谢声（让产生的队友说）


                    //生成刑架
                    switch (CurrentRapeType)
                    {
                        case 1:
                            GameObject TortureDevice = Instantiate(Torture_Rack, transform.position, Quaternion.identity);
                            TortureDevice.GetComponent<Plant>().SetImage(0);
                            break;

                    }

                    WeaponChangeDevice.transform.SetParent(null);//保留架子

                    // 消失自己(如果销毁的太快就容易传不进去)
                    Destroy(gameObject, 0.2f);

                    InteractOneTime = true;//只触发一次
                }


            }


            if (RBQState == 3 && other.GetComponent<Player>().isInteracting)//点击交互键
            {
                if (!InteractOneTime)
                {
                    Player _Player = other.GetComponent<Player>();

                    int currentMoney = PlayerPrefs.GetInt("Money", 0);





                    switch (CurrentType)
                    {
                        case RBQItemTrigger.ItemType.Sword:
                            if (SwordIndex > 0)
                            {

                                int itemPrice = swordPower * 10;
                                if (currentMoney >= itemPrice)
                                {
                                    // 减钱并更新 UI  
                                    UIManager.instance.ChangeMoney(-itemPrice);

                                    // 给玩家加属性
                                    _Player.PickupWeapon(SwordIndex, 0); // 剑，剑士
                                    int WeaponAtk = _Player.CurrentWeaponPower;
                                    WeaponAtk += swordPower;
                                    _Player.CurrentWeaponPower = WeaponAtk;
                                    _Player.SaveCurrent();


                                    //隐藏
                                    SwordIndex = 0;
                                    Weapon_Sword.sprite = null;
                                    Weapon_Sword.gameObject.SetActive(false);

                                    Check_Sword.SetActive(false);//隐藏踏板

                                }
                                else
                                {
                                    // 播放提示音或显示提示文字
                                    Debug.Log("金币不足！");
                                    frameEvents._Attack_pai1();
                                }

                            }
                            break;
                        case RBQItemTrigger.ItemType.Pistol:
                            if (PistolIndex > 0)
                            {

                                int itemPrice = pistolPower * 10;
                                if (currentMoney >= itemPrice)
                                {
                                    // 减钱并更新 UI  
                                    UIManager.instance.ChangeMoney(-itemPrice);

                                    // 给玩家加属性
                                    _Player.PickupWeapon(PistolIndex, 1);//枪，射手
                                    int WeaponAtk = _Player.CurrentWeaponPower;
                                    WeaponAtk += pistolPower;
                                    _Player.CurrentWeaponPower = WeaponAtk;
                                    _Player.SaveCurrent();


                                    //隐藏
                                    PistolIndex = 0;
                                    Weapon_Pistol.sprite = null;
                                    Weapon_Pistol.gameObject.SetActive(false);

                                    Check_Pistol.SetActive(false);//隐藏踏板

                                }
                                else
                                {
                                    // 播放提示音或显示提示文字
                                    Debug.Log("金币不足！");
                                    frameEvents._Attack_pai1();
                                }


                            }
                            break;
                        case RBQItemTrigger.ItemType.Staff:
                            if (StaffIndex > 0)
                            {

                                int itemPrice = staffPower * 10;
                                if (currentMoney >= itemPrice)
                                {
                                    // 减钱并更新 UI  
                                    UIManager.instance.ChangeMoney(-itemPrice);

                                    // 给玩家加属性
                                    _Player.PickupWeapon(StaffIndex, 2);//杖，法师
                                    int WeaponAtk = _Player.CurrentWeaponPower;
                                    WeaponAtk += staffPower;
                                    _Player.CurrentWeaponPower = WeaponAtk;
                                    _Player.SaveCurrent();


                                    //隐藏
                                    StaffIndex = 0;
                                    Weapon_Staff.sprite = null;
                                    Weapon_Staff.gameObject.SetActive(false);

                                    Check_Staff.SetActive(false);//隐藏踏板

                                }
                                else
                                {
                                    // 播放提示音或显示提示文字
                                    Debug.Log("金币不足！");
                                    frameEvents._Attack_pai1();
                                }

                            }
                            break;

                        case RBQItemTrigger.ItemType.Clothes:
                            if (ClothesIndex > 0)
                            {

                                int itemPrice = clothesDef * 10;
                                if (currentMoney >= itemPrice)
                                {
                                    // 减钱并更新 UI  
                                    UIManager.instance.ChangeMoney(-itemPrice);

                                    // 给玩家加属性
                                    _Player.YYY_bodyIndex = this.YYY_bodyIndex; _Player.SetSkin();
                                    int ArmorDef = _Player.CurrentArmorDefence;
                                    ArmorDef += clothesDef;
                                    _Player.CurrentArmorDefence = ArmorDef;
                                    _Player.SaveCurrent();

                                    //RBQ上尸体显示裸体
                                    YYY_bodyIndex = 1;
                                    SetSkin();


                                    //隐藏
                                    Check_Clothes.SetActive(false);//隐藏踏板

                                    ClothesIndex = 0;

                                }
                                else
                                {
                                    // 播放提示音或显示提示文字
                                    Debug.Log("金币不足！");
                                    frameEvents._Attack_pai1();
                                }

                               

                              
                            }

                            break;
                        case RBQItemTrigger.ItemType.Stockings:
                            if (StockingIndex > 0)
                            {
                                int itemPrice = stockingsDef * 10;
                                if (currentMoney >= itemPrice)
                                {
                                    // 减钱并更新 UI  
                                    UIManager.instance.ChangeMoney(-itemPrice);

                                    // 给玩家加属性
                                    _Player.YYY_legsIndex = this.YYY_legsIndex; _Player.SetSkin();
                                    int StockingDef = _Player.CurrentStockingDefence;
                                    StockingDef += stockingsDef;
                                    _Player.CurrentStockingDefence = StockingDef;
                                    _Player.SaveCurrent();

                                    //RBQ上尸体显示裸体
                                    YYY_legsIndex = 1;
                                    SetSkin();


                                    //隐藏
                                    Check_Stocking.SetActive(false);//隐藏踏板

                                    StockingIndex = 0;

                                }
                                else
                                {
                                    // 播放提示音或显示提示文字
                                    Debug.Log("金币不足！");
                                    frameEvents._Attack_pai1();
                                }


                              

                               
                            }

                            break;
                        case RBQItemTrigger.ItemType.Slave:
                            if (currentMoney >= slavePrice)
                            {
                                // 减钱并更新 UI  
                                UIManager.instance.ChangeMoney(-slavePrice);

                                //奖励一个队友
                                GameObject NewEnemy = Instantiate(_RoomGenerator.Enemy, transform.position, Quaternion.identity);
                                Enemy enemy = NewEnemy.transform.Find("Enemy").GetComponent<Enemy>();
                                //enemy.wallmap = wallmap;//告诉自己生成的Enemy出生点WallMap
                                enemy.CanChangeSkin = false;
                                StartCoroutine(DelayedApplySkin(enemy));
                                enemy.ChangeClass(0);


                                enemy.ConvertToFriend();



                                enemy.ReadyToSayThankYou();//谢谢声（让产生的队友说）

                                // 消失自己(如果销毁的太快就容易传不进去)
                                Destroy(gameObject, 0.2f);


                                Check_Slave.SetActive(false);//隐藏踏板



                                //生成刑架
                                GameObject TortureDevice = Instantiate(Torture_Rack, transform.position, Quaternion.identity);
                                TortureDevice.GetComponent<Plant>().SetImage(8);

                                WeaponChangeDevice.transform.SetParent(null);//保留架子

                            }
                            else
                            {
                                // 播放提示音或显示提示文字
                                Debug.Log("金币不足！");
                                frameEvents._Attack_pai1();
                            }

                           

                            break;
                    }

                    HidePrompt();


                    _Player.ResetCombo();//买完东西后一定要重置动画




                    other.GetComponent<Player>().frameEvents._SE_Clothes();

                    InteractOneTime = true;
                    Invoke("DelayCanTake", 0.5f);

                    Prompt_Take.SetActive(false);





                }

            }



        }


    }

    void DelayCanTake()
    {
        InteractOneTime = false;
    }


    private IEnumerator DelayedApplySkin(Enemy enemy)
    {
        yield return new WaitForSeconds(0.1f); // 延迟 0.1 秒后赋值

        enemy.SaveCurrentSkin(
            YYY_headIndex, YYY_eyesIndex, YYY_bodyIndex, YYY_legsIndex, YYY_hatIndex,
            Man_headIndex, Man_bodyIndex, Man_hatIndex,
            Girl_headIndex, Girl_eyesIndex, Girl_bodyIndex, Girl_legsIndex, Girl_hatIndex,
            weaponIndex
        );
    }





    #endregion


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



        YYY_headIndex = Random.Range(1, 13);  // 除去皇女
        YYY_eyesIndex = Random.Range(1, 14);  // 1~13
        YYY_bodyIndex = Random.Range(10, 13); //CurrentProfession = YYY_bodyIndex - 10;//注意这个地方的敌人的职业也不能轻易更改！
        YYY_legsIndex = Random.Range(10, 13);//剑士射手法师

        int[] YYY_pool = { 1, 2, 3, 4, 10, 11, 12 };
        YYY_hatIndex = YYY_pool[UnityEngine.Random.Range(0, YYY_pool.Length)];//人类 精灵 高等精灵 北方兔族 南方兔族 魔族 大魔族





        Man_headIndex = Random.Range(1, 5);//除去 皇子和皇帝
        Man_bodyIndex = Random.Range(1, 5);//除去 皇子和皇帝
        Man_hatIndex = Random.Range(1, 5);//除去 魔族角和绷带

        Girl_headIndex = Random.Range(1, 13);  // 除去皇女
        Girl_eyesIndex = Random.Range(1, 14);  // 1~13
        Girl_bodyIndex = Random.Range(10, 13);//剑士射手法师
        Girl_legsIndex = Random.Range(10, 13);//剑士射手法师

        int[] Girl_pool = { 1, 2, 3, 4, 10, 11, 12 };//人类 精灵 高等精灵 北方兔族 南方兔族 魔族 大魔族
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



    }

    #endregion


    /// <summary>
    /// 商店属性
    /// </summary>
    #region

    [Header("商店属性")]

    public SpriteRenderer Weapon_Sword;
    public SpriteRenderer Weapon_Pistol;
    public SpriteRenderer Weapon_Staff;

    public int SwordIndex;
    public int PistolIndex;
    public int StaffIndex;

    public int ClothesIndex = 1;//防止买两次
    public int StockingIndex = 1;//防止买两次

    //价格与增加数值
    public int swordPower;
    public int pistolPower;
    public int staffPower;
    public int clothesDef;
    public int stockingsDef;
    public int slavePrice;

    public SkinPartsDatabase database;
    public void GenerateRandomWeapons()
    {
        // 以一定概率生成每种武器（例如70%概率生成）
        SwordIndex = Random.value < 0.7f ? Random.Range(1, database.SwordSprites.Length + 1) : 0;
        PistolIndex = Random.value < 0.7f ? Random.Range(1, database.PistolSprites.Length + 1) : 0;
        StaffIndex = Random.value < 0.7f ? Random.Range(1, database.StaffSprites.Length + 1) : 0;

        Weapon_Sword.sprite = SwordIndex > 0 ? database.SwordSprites[SwordIndex - 1] : null;
        Weapon_Sword.gameObject.SetActive(SwordIndex > 0); if (SwordIndex <= 0) { Check_Sword.SetActive(false); } else { swordPower = Random.Range(1, 10); }

        Weapon_Pistol.sprite = PistolIndex > 0 ? database.PistolSprites[PistolIndex - 1] : null;
        Weapon_Pistol.gameObject.SetActive(PistolIndex > 0); if (PistolIndex <= 0) { Check_Pistol.SetActive(false); } else { pistolPower = Random.Range(1, 10); }

        Weapon_Staff.sprite = StaffIndex > 0 ? database.StaffSprites[StaffIndex - 1] : null;
        Weapon_Staff.gameObject.SetActive(StaffIndex > 0); if (StaffIndex <= 0) { Check_Staff.SetActive(false); } else { staffPower = Random.Range(1, 10); }




        clothesDef = Random.Range(1, 10);
        stockingsDef = Random.Range(1, 10);

        slavePrice = Random.Range(60, 101);
    }


    [Header("商店显示")]
    public Text promptText;//显示当前选中商品
    public GameObject promptCanvas;//商品标签


    public Text priceText; //显示价格的
    public Text bonusText; //显示加成


    public RBQItemTrigger.ItemType CurrentType;//当前玩家踩在哪里

    public GameObject Check;//商品检测站位
    public GameObject Check_Sword, Check_Pistol, Check_Staff, Check_Clothes, Check_Stocking, Check_Slave;//对应踏板


    public GameObject WeaponChangeDevice;//在被摧毁之前移出来





    public void ShowItemPrompt(RBQItemTrigger.ItemType type)
    {
        promptCanvas.SetActive(true);

        CurrentType = type;

        int lang = PlayerPrefs.GetInt("language", 0); // 默认日文

        switch (type)
        {
            case RBQItemTrigger.ItemType.Sword:
                promptText.text = GetLocalizedText(lang, "剑", "剣", "劍", "Sword", "검");
                bonusText.text = $"+{swordPower} Atk";
                priceText.text = $"{swordPower * 10}";
                break;
            case RBQItemTrigger.ItemType.Pistol:
                promptText.text = GetLocalizedText(lang, "枪", "銃", "槍", "Gun", "총");
                bonusText.text = $"+{pistolPower} Atk";
                priceText.text = $"{pistolPower * 10}";
                break;
            case RBQItemTrigger.ItemType.Staff:
                promptText.text = GetLocalizedText(lang, "杖", "杖", "杖", "Staff", "지팡이");
                bonusText.text = $"+{staffPower} Atk";
                priceText.text = $"{staffPower * 10}";
                break;
            case RBQItemTrigger.ItemType.Clothes:
                promptText.text = GetLocalizedText(lang, "衣服", "服", "衣服", "Clothes", "옷");
                bonusText.text = $"+{clothesDef} Def";
                priceText.text = $"{clothesDef * 10}";
                break;
            case RBQItemTrigger.ItemType.Stockings:
                promptText.text = GetLocalizedText(lang, "丝袜", "ストッキング", "絲襪", "Stockings", "스타킹");
                bonusText.text = $"+{stockingsDef} Def";
                priceText.text = $"{stockingsDef * 10}";
                break;
            case RBQItemTrigger.ItemType.Slave:
                promptText.text = GetLocalizedText(lang, "奴隶", "奴隷", "奴隸", "Slave", "노예");
                bonusText.text = "";
                priceText.text = $"{slavePrice}";
                break;
        }
    }

    private string GetLocalizedText(int lang, string cn, string jp, string tw, string en, string kr)
    {
        switch (lang)
        {
            case 0: return jp;
            case 1: return cn;
            case 2: return tw;
            case 3: return en;
            case 4: return kr;
            default: return en;
        }
    }
    public void HidePrompt()
    {
        promptCanvas.SetActive(false);
    }


    #endregion
}

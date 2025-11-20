using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RBQ : MonoBehaviour
{
    [Header("主动触发声音")]
    public FrameEvents frameEvents;//这个和Spine上面区分开来，防止声音叠住
    public FrameEvents frameEvents_Spine;//这个是Spine动画器声音，用来锁住那一侧声音

    [Header("寻找RoomGenerator")]
    RoomGenerator _RoomGenerator;//寻找RoomGenerator



    [Header("基础数值")]
    public Animator anim;//接入Spine动画机
    private string[] tortureAnimations = { "RBQ_Torture_Impale", "RBQ_Torture_Strangle", "RBQ_Torture_CutDown","RBQ_Torture_EggBirth", "RBQ_Punish_Cage_Left_2" };

    public int RBQState = 0;//0单人拘束 1双人拷问中  2尸体  3肉货
    bool isCreateEnemy = false;//是否产生过敌人

    public int CurrentRapeType = 0;//1吊缚抽打 2后入奸
    public GameObject Torture_Rack;//刑架

    private float inputX, inputY;

    [Header("尸体隐藏标志/商店展示标记")]
    public GameObject UI_Player_Icon;
    public GameObject UI_Shop_Icon;

    void Start()
    {
        //寻找RoomGenerator
        _RoomGenerator = GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<RoomGenerator>();

        if (RBQState == 0) { RBQState = Random.Range(1, 3); }//如果一开始没有赋值，那么随机

        // 根据方向旋转（可选，或控制朝向动画片段）
        ApplyFacingRotation();


        //随机皮肤
        SetRandomSkin();

        // 随机动画
        switch (RBQState)
        {
            case 1:
                //被拷问
                //string animName = punishAnims[Random.Range(0, punishAnims.Length)];
                //anim.Play(animName);


                if (GameFlowData.nextScene == "Story_01" || GameFlowData.nextScene == "Story_02")
                { 
                    CurrentRapeType = Random.Range(1, 5); 

                }//暂时先这样
                else if (GameFlowData.nextScene == "Story_04" || GameFlowData.nextScene == "Story_06")
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        CurrentRapeType = 7;
                    }
                    else
                    {
                        CurrentRapeType = Random.Range(1, 5);
                    }
                    
                }
                else
                {
                    CurrentRapeType = Random.Range(1, 8);//7，9，11关
                }


                switch (CurrentRapeType)
                {
                    case 1:
                        anim.Play("RBQ_Punish_Hang");//吊缚鞭打
                        break;
                    case 2:
                        anim.Play("RBQ_Punish_Rape");//后入强奸
                        break;
                    case 3:
                        anim.Play("RBQ_Punish_Pillory");//头枷拘束
                        break;
                    case 4:
                        anim.Play("RBQ_Punish_ShameWagon");//泄欲车
                        break;
                    case 5:
                        anim.Play("RBQ_Punish_Tentacle");//触手拘束
                        break;
                    case 6:
                        anim.Play("RBQ_Punish_Monster_Rape_Side");//变异体强奸
                        break;
                    case 7:
                        anim.Play("RBQ_Punish_Crucifixion");//修女榨精
                        break;
                }


                #region 是否循环叫声

                bool CanGasping = true;

                if (CurrentRapeType == 3 || CurrentRapeType == 4 || CurrentRapeType == 1)
                {
                    if (inputX == 1 && inputY == 0)
                    {
                        //朝右的有帧事件发出声音不能循环叫声
                        CanGasping = false;
                    }
                }

                if ( CurrentRapeType == 1)
                {
                    if (inputX == 0 && inputY == 1)
                    {
                        //朝上的有帧事件发出声音不能循环叫声
                        CanGasping = false;
                    }
                }

                //循环叫声
                if (CanGasping) { InvokeRepeating("Gasping_Long", 1f, 58f); }

                #endregion



                break;
            case 2:
                //尸体
                int rand = Random.Range(0, tortureAnimations.Length);
                anim.Play(tortureAnimations[rand]);


                //if (GameFlowData.nextScene == "Story_01" || GameFlowData.nextScene == "Story_02")
                //{
                //    CurrentRapeType = 5;
                //
                //}//暂时先这样
                //else if (GameFlowData.nextScene == "Story_04" || GameFlowData.nextScene == "Story_06")
                //{
                //    CurrentRapeType = Random.Range(1,3);
                //
                //}
                //else
                //{
                //    CurrentRapeType = Random.Range(1, 6);//7，9，11关
                //}
                //
                //
                //switch (CurrentRapeType)
                //{
                //    case 1:
                //        anim.Play("RBQ_Torture_Impale");//扎穿
                //        break;
                //    case 2:
                //        anim.Play("RBQ_Torture_Strangle");//勒死
                //        break;
                //    case 3:
                //        anim.Play("RBQ_Torture_CutDown");//四肢切断
                //        break;
                //    case 4:
                //        anim.Play("RBQ_Punish_Cage_Left_2");//狗笼
                //        break;
                //    case 5:         
                //        anim.Play("RBQ_Torture_EggBirth");//产卵
                //        break;
                //}



                Destroy(UI_Player_Icon.gameObject);//尸体隐藏标志


                break;
            case 3:
                //商店肉货
                anim.Play("RBQ_Display_Idle_Front");

                GenerateShopItems();


                Destroy(UI_Player_Icon.gameObject);//商店更换标志
                UI_Shop_Icon.SetActive(true);
                break;
        }









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
                // 随机或固定左右偏移量
                Vector3 basePos = transform.position;


                //出现敌人,停止拷问，冲向玩家
                GameObject NewEnemy = Instantiate(_RoomGenerator.Enemy, basePos + new Vector3(-0.6f, 0f, 0f), Quaternion.identity);
                Enemy enemy = NewEnemy.transform.Find("Enemy").GetComponent<Enemy>();
                //enemy.wallmap = wallmap;//告诉自己生成的Enemy出生点WallMap
                enemy.CanChangeSkin = false;
                StartCoroutine(DelayedApplySkin(enemy));//RBQ产生拷问敌人
                enemy.ChangeClass(1);



                //RBQState = 0;


                switch (CurrentRapeType)
                {
                    case 1:
                        anim.Play("RBQ_Punish_Hang_2");//吊缚鞭打
                        break;
                    case 2:
                        anim.Play("RBQ_Punish_Rape_2");//后入强奸
                        break;
                    case 3:
                        anim.Play("RBQ_Punish_Pillory_2");//头枷拘束
                        break;
                    case 4:
                        anim.Play("RBQ_Punish_ShameWagon_2");//泄欲车
                        break;
                    case 5:

                        anim.Play("RBQ_Punish_Rape_2");//触手拘束

                        enemy.ChangeClass(7);//触手拘束
                        break;
                    case 6:
                        anim.Play("RBQ_Punish_Rape_2");//变异体强奸
                        enemy.ChangeClass(4);//产生变异体
                        break;
                    case 7:
                        anim.Play("RBQ_Punish_Crucifixion_2");//修女榨精
                        enemy.ChangeClass(0);//产生惩戒修女
                        break;
                }


                //停止播放
                frameEvents.audioS.Stop();
                frameEvents_Spine.audioS.Stop();
                CancelInvoke(nameof(Gasping_Long));


                // 监听敌人状态（把原来立刻 RBQState=0 的代码删掉）
                if (_waitEnemyRoutine != null) StopCoroutine(_waitEnemyRoutine);
                _waitEnemyRoutine = StartCoroutine(WaitEnemyGoneThenReset(enemy));

                isCreateEnemy = true;

                //立刻锁门
                wallmap.OnlyLockDoor();
            }


            if (RBQState == 3)
            {
                playerInRange = other.GetComponent<Player>();
                if (playerInRange != null)
                {
                    Prompt_Take.SetActive(true);
                    playerInRange.InteractingButton.SetActive(true);
                    playerInRange.isInteracting = false;
                    isPlayerInside = true;
                }



                //Prompt_Take.SetActive(true);
                //
                //other.GetComponent<Player>().InteractingButton.SetActive(true);
                //other.GetComponent<Player>().isInteracting = false;
                //InteractOneTime = false; // 每次进入区域都重置交互锁
            }

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

                other.GetComponent<Player>().InteractingButton.SetActive(false);
                other.GetComponent<Player>().isInteracting = false;
            }




            if (RBQState == 3)//商店状态下这里不要触发取下
            {

                if (playerInRange != null)
                {
                    Prompt_Take.SetActive(false);
                    playerInRange.InteractingButton.SetActive(false);
                    playerInRange.isInteracting = false;
                }
                playerInRange = null;
                isPlayerInside = false;


                //Prompt_Take.SetActive(false);
                //
                //other.GetComponent<Player>().InteractingButton.SetActive(false);
                //other.GetComponent<Player>().isInteracting = false;
                //InteractOneTime = false;
            }
        }
    }


    #region   商店
    public void ReenablePrompt(Player player)
    {
        if (RBQState == 3)
        {
            Prompt_Take.SetActive(true);
            player.InteractingButton.SetActive(true);
            player.isInteracting = false;
            //InteractOneTime = false;
        }
    } // 这个函数让 UIManager 在关闭商店时重新显示提示

    private Player playerInRange; // 记录进入碰撞体的玩家
    private bool isPlayerInside = false;
    void Update()
    {
        if (!isPlayerInside || playerInRange == null)
            return;

        // ✅ 改成每帧检测输入状态（E键 / 手柄 / 手机按钮都会触发 Player.isInteracting）
        if (playerInRange.isInteracting && RBQState == 3)
        {
            if (!GameFlowData.BulletCanThroughtWall)
            {
                Debug.Log("打开商店");

                UIManager.instance.OpenShopMenu(this);//把自己的商品信息传过去

                playerInRange.transform.position = transform.position;

                Prompt_Take.SetActive(false);
                playerInRange.InteractingButton.SetActive(false);
                playerInRange.isInteracting = false;
            }
        }
    }

    #endregion


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
                    StartCoroutine(DelayedApplySkin_2(enemy));//RBQ产生奴隶同伴
                    enemy.ChangeClass(0);


                    enemy.ConvertToFriend();



                    enemy.ReadyToSayThankYou();//谢谢声（让产生的队友说）


                    //生成刑架
                    switch (CurrentRapeType)
                    {
                        case 1:

                            //吊缚鞭打
                            //GameObject TortureDevice = Instantiate(Torture_Rack, transform.position, Quaternion.identity);
                            //TortureDevice.GetComponent<Plant>().SetImage(0);
                            break;
                        case 3:

                            //头枷拘束
                            GameObject TortureDevice2 = Instantiate(Torture_Rack, transform.position, Quaternion.identity);

                            if (inputX == 0)
                            {
                                TortureDevice2.GetComponent<Plant>().SetImage(5);//正面
                            }
                            else
                            {
                                TortureDevice2.GetComponent<Plant>().SetImage(9);//侧面
                            }

                            break;

                        case 4:

                            //泄欲车
                            GameObject TortureDevice3 = Instantiate(Torture_Rack, transform.position, Quaternion.identity);

                            if (inputX == 0)
                            {
                                TortureDevice3.GetComponent<Plant>().SetImage(2);//正面
                            }
                            else
                            {
                                TortureDevice3.GetComponent<Plant>().SetImage(1);//侧面
                            }

                            break;
                    }

                    WeaponChangeDevice.transform.SetParent(null);//保留架子

                    // 消失自己(如果销毁的太快就容易传不进去)
                    Destroy(gameObject, 0.2f);

                    InteractOneTime = true;//只触发一次


                    other.GetComponent<Player>().InteractingButton.SetActive(false);
                    other.GetComponent<Player>().isInteracting = false;
                }


            }


            if (RBQState == 0)
            {
                Prompt_Save.SetActive(true);

                other.GetComponent<Player>().InteractingButton.SetActive(true);
                other.GetComponent<Player>().isInteracting = false;
            }








        }


    }

    void DelayCanTake()
    {
        InteractOneTime = false;
    }


    public void SaveFriend()
    {
        // 随机或固定左右偏移量
        Vector3 basePos = transform.position;


        GameObject NewEnemy = Instantiate(_RoomGenerator.Enemy, basePos + new Vector3(-0.6f, 0f, 0f), Quaternion.identity);
        Enemy enemy = NewEnemy.transform.Find("Enemy").GetComponent<Enemy>();
        //enemy.wallmap = wallmap;//告诉自己生成的Enemy出生点WallMap
        enemy.CanChangeSkin = false;
        StartCoroutine(DelayedApplySkin_2(enemy));//商店购买两个奴隶
        enemy.ChangeClass(0);
        enemy.ConvertToFriend();


        GameObject NewEnemy2 = Instantiate(_RoomGenerator.Enemy, transform.position, Quaternion.identity);
        Enemy enemy2 = NewEnemy2.transform.Find("Enemy").GetComponent<Enemy>();
        //enemy.wallmap = wallmap;//告诉自己生成的Enemy出生点WallMap
        enemy2.CanChangeSkin = false;
        StartCoroutine(DelayedApplySkin(enemy2));//商店购买两个奴隶
        enemy2.ChangeClass(0);


        enemy2.ConvertToFriend();




        enemy.ReadyToSayThankYou();//谢谢声（让产生的队友说）

        // 消失自己(如果销毁的太快就容易传不进去)
        Destroy(gameObject, 0.2f);




        //生成刑架
        GameObject TortureDevice = Instantiate(Torture_Rack, basePos + new Vector3(0.6f, 0f, 0f), Quaternion.identity);
        TortureDevice.GetComponent<Plant>().SetImage(8);

        WeaponChangeDevice.transform.SetParent(null);//保留架子



        // 购买反馈
        Debug.Log("成功解放一个奴隶！");
    }//商店购买奴隶传入



    private IEnumerator DelayedApplySkin(Enemy enemy)
    {
        yield return new WaitForSeconds(0.1f); // 延迟 0.1 秒后赋值

        //把Girl的皮肤套入YYY里
        enemy.SaveCurrentSkin(
           Girl_headIndex, Girl_eyesIndex, Girl_bodyIndex, Girl_legsIndex, Girl_hatIndex,
           Man_headIndex, Man_bodyIndex, Man_hatIndex,
           Girl_headIndex, Girl_eyesIndex, Girl_bodyIndex, Girl_legsIndex, Girl_hatIndex,
           weaponIndex
       );
    }//这个是产生敌人

    private IEnumerator DelayedApplySkin_2(Enemy enemy)
    {
        yield return new WaitForSeconds(0.1f); // 延迟 0.1 秒后赋值

         enemy.SaveCurrentSkin(
             YYY_headIndex, YYY_eyesIndex, YYY_bodyIndex, YYY_legsIndex, YYY_hatIndex,
             Man_headIndex, Man_bodyIndex, Man_hatIndex,
             Girl_headIndex, Girl_eyesIndex, Girl_bodyIndex, Girl_legsIndex, Girl_hatIndex,
             weaponIndex
         );


    }//这个是产生奴隶同伴



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

                //目前已有的中挑选，
                int[] validIndexes = { 2, 3, 4, 5, 6, 7, 10, 11, 12 };
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
    public SkinPartsDatabase database;
    public GameObject WeaponChangeDevice;//在被摧毁之前移出来

    public List<ShopItemData> shopItems = new List<ShopItemData>();

    public void GenerateShopItems()
    {
        shopItems.Clear();


        int lang = PlayerPrefs.GetInt("language");



        // ⚔️ 武器类
        if (Random.value < 0.7f)
        {
            ShopItemData sword = new ShopItemData();
            sword.type = ShopItemData.ItemType.Sword;
            sword.index = Random.Range(1, 11);
            sword.value = Random.Range(1, 10);
            sword.price = sword.value * 10;
            Weapon_Sword.sprite = database.SwordSprites[sword.index - 1];
            sword.displayName = ItemLocalization.GetName(ShopItemData.ItemType.Sword, sword.index, lang);
            sword.description = ItemLocalization.GetDescription(ShopItemData.ItemType.Sword, sword.index, lang);
            shopItems.Add(sword);
        }

        if (Random.value < 0.7f)
        {
            ShopItemData gun = new ShopItemData();
            gun.type = ShopItemData.ItemType.Pistol;
            gun.index = Random.Range(1, 11);
            gun.value = Random.Range(1, 10);
            gun.price = gun.value * 10;
            Weapon_Pistol.sprite = database.PistolSprites[gun.index - 1];
            gun.displayName = ItemLocalization.GetName(ShopItemData.ItemType.Pistol, gun.index, lang);
            gun.description = ItemLocalization.GetDescription(ShopItemData.ItemType.Pistol, gun.index, lang);
            shopItems.Add(gun);
        }

        if (Random.value < 0.7f)
        {
            ShopItemData staff = new ShopItemData();
            staff.type = ShopItemData.ItemType.Staff;
            staff.index = Random.Range(1, 11);
            staff.value = Random.Range(1, 10);
            staff.price = staff.value * 10;
            Weapon_Staff.sprite = database.StaffSprites[staff.index - 1];
            staff.displayName = ItemLocalization.GetName(ShopItemData.ItemType.Staff, staff.index, lang);
            staff.description = ItemLocalization.GetDescription(ShopItemData.ItemType.Staff, staff.index, lang);
            shopItems.Add(staff);
        }



        //衣服必定有
        ShopItemData clothes = new ShopItemData();
        clothes.type = ShopItemData.ItemType.Clothes;
        clothes.index = YYY_bodyIndex;
        clothes.value = Random.Range(1, 10);
        clothes.price = clothes.value * 10;
        //clothes.icon = database.ClothesSprites[clothes.index - 1];

        clothes.displayName = ItemLocalization.GetName(ShopItemData.ItemType.Clothes, clothes.index, lang);
        clothes.description = ItemLocalization.GetDescription(ShopItemData.ItemType.Clothes,clothes.index, lang);
        shopItems.Add(clothes);




        //丝袜必定有
        ShopItemData stockings = new ShopItemData();
        stockings.type = ShopItemData.ItemType.Stockings;
        stockings.index = YYY_legsIndex;
        stockings.value = Random.Range(1, 10);
        stockings.price = stockings.value * 10;
        //stockings.icon = database.ClothesSprites[stockings.index - 1];

        stockings.displayName = ItemLocalization.GetName(ShopItemData.ItemType.Stockings, stockings.index, lang);
        stockings.description = ItemLocalization.GetDescription(ShopItemData.ItemType.Stockings, stockings.index, lang);
        shopItems.Add(stockings);



        //性奴隶必定有
        ShopItemData slave = new ShopItemData();
        slave.type = ShopItemData.ItemType.Slave;
        slave.displayName = ItemLocalization.GetName(ShopItemData.ItemType.Slave, slave.index, lang);
        slave.description = ItemLocalization.GetDescription(ShopItemData.ItemType.Slave, slave.index, lang);
        slave.price = 300;
        shopItems.Add(slave);


    }

    public void RemoveItemFromShelf(ShopItemData.ItemType type)
    {
        // 从数据列表中移除该商品
        shopItems.RemoveAll(x => x.type == type);

        // 同时隐藏现场展示用的 Sprite
        switch (type)
        {
            case ShopItemData.ItemType.Sword:
                if (Weapon_Sword) Weapon_Sword.gameObject.SetActive(false);
                break;

            case ShopItemData.ItemType.Pistol:
                if (Weapon_Pistol) Weapon_Pistol.gameObject.SetActive(false);
                break;

            case ShopItemData.ItemType.Staff:
                if (Weapon_Staff) Weapon_Staff.gameObject.SetActive(false);
                break;

            case ShopItemData.ItemType.Clothes:
                YYY_bodyIndex = 1;
                SetSkin();
                break;

            case ShopItemData.ItemType.Stockings:
                YYY_legsIndex = 1;
                SetSkin();
                break;

            case ShopItemData.ItemType.Slave:

                break;
        }

        Debug.Log("RBQ 货架移除了物品：" + type);
    }
    #endregion





}

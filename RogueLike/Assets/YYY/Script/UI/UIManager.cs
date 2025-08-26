using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

using System;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    void Awake()
    {
        instance = this;

        //Debug.Log("目前存档里的语言" + PlayerPrefs.GetInt("language"));//0 日语 1中文 2繁中 3英语 4韩语

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_OnanismFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_OnanismSide_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_InsultSide_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_GagSide_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_FistingFront_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_RapeFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_RapeSide_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_AssaultFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_AssaultSide_1"));//0未解锁  1解锁

        //PlayerPrefs.SetInt("CG_InsultSide_1", 1);
        //PlayerPrefs.SetInt("CG_RapeFront_1", 1);
        //PlayerPrefs.SetInt("CG_RapeSide_1", 1);

        //PlayerPrefs.SetInt("CG_TentacleBagFront_1", 1);
        //PlayerPrefs.SetInt("CG_TentacleBugSide_1", 1);
        //PlayerPrefs.SetInt("CG_TentacleWallFront_1", 1);
        //PlayerPrefs.SetInt("CG_TentacleFront_1", 1);
    }
    /// <summary>
    /// 主菜单
    /// </summary>
    #region
    [Header("主菜单")]
    public GameObject Common_All;//移动血条等
    public GameObject NextButton;//播放结局动画
    public GameObject Loading;
    public void Ending_UI()
    {
        Common_All.SetActive(false);
        NextButton.SetActive(true);


    }//生命值归0后触发

    public void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Loading.SetActive(true);
    }//重刷场景

    [Header("主菜单界面层级")]
    public int CurrentChooseList = 0;// -1确认是否删除存档  0主菜单界面   1捏人界面   2存档界面   3设置界面  4语言选择界面   5CG界面   6CG鉴赏中
    public int CurrentMode = 0;//0 进入CG界面  1捏人/进入游戏
    public int HomePagecurrentIndex = 0;//0 开始游戏  1 CG鉴赏  2 设置  3 退出
    public int CreatNewcurrentIndex = 0;//0 名称 1 眼睛  2 头  3 种族  4 职业  5 确定
    public int SettingPagecurrentIndex = 0;//0 BGM  1 SE  2 语言  3 删除存档
    public int LanguagePagecurrentIndex = 0;//0 日语 1中文 2繁中 3英语 4韩语

    [SerializeField] private GameObject[] HomePage_highlightObjs; // 主页高亮显示
    [SerializeField] private GameObject[] highlightObjs; // 捏人界面高亮显示
    [SerializeField] private GameObject[] SettingPage_highlightObjs; // 设置高亮显示
    [SerializeField] private GameObject[] LanguagePage_highlightObjs; // 设置高亮显示

    private void UpdateHighlight()
    {
        for (int i = 0; i < highlightObjs.Length; i++)
        {
            highlightObjs[i].SetActive(i == CreatNewcurrentIndex);
        }
    }

    private void UpdateHomePage_Highlight()
    {
        for (int i = 0; i < HomePage_highlightObjs.Length; i++)
        {
            HomePage_highlightObjs[i].SetActive(i == HomePagecurrentIndex);
        }
    }
    private void UpdateSettingPage_Highlight()
    {
        for (int i = 0; i < SettingPage_highlightObjs.Length; i++)
        {
            SettingPage_highlightObjs[i].SetActive(i == SettingPagecurrentIndex);
        }
    }
    private void UpdateLanguagePage_Highlight()
    {
        for (int i = 0; i < LanguagePage_highlightObjs.Length; i++)
        {
            LanguagePage_highlightObjs[i].SetActive(i == LanguagePagecurrentIndex);
        }
    }

    [SerializeField] private Button HairLeft, HairRight;
    [SerializeField] private Button EyesLeft, EyesRight;
    [SerializeField] private Button RaceLeft, RaceRight;
    [SerializeField] private Button ClassLeft, ClassRight;

    public Button okButton;

    public void ToSavePageButton(int currentMode)
    {
        Invoke("ToSavePage", 0.1f);//开始游戏进入存档界面(CG)
        CurrentMode = currentMode;

        if (CurrentMode == 0)
        {
            //CG鉴赏
            player.anim.Play("Girl_Broken_Idle");
        }
        else
        {
            //主界面
            player.anim.Play("Girl_Default_Idle");
        }

    }


    public void ToSavePage()
    {

        HomePageCavans.SetActive(false);
        CGCavans.SetActive(false);
        CurrentChooseList = 2;

    }




    public void ToSettingPage()
    {
        SettingCavans.SetActive(true);
        LanguageCavans.SetActive(false);
        CurrentChooseList = 3;
    }

    public void ToLanguagePage()
    {
        LanguageCavans.SetActive(true);
        CurrentChooseList = 4;
    }
    public void ToHomePage()
    {
        HomePageCavans.SetActive(true);
        SettingCavans.SetActive(false);
        CurrentChooseList = 0;
    }

    public void ToCGPage()
    {
        CGCavans.SetActive(true);
        CurrentChooseList = 5;



        MainCamera.SetInteger("View", 0);
        ShowSaveCavansAnim.SetBool("Track", true);
        ShowSaveCavansAnim.gameObject.SetActive(true);


        player.frameEvents.audioS.Stop();
        player.anim.Play("Girl_Broken_Idle");
    }

    #endregion

    /// <summary>
    /// 捏人菜单
    /// </summary>
    #region
    [Header("各类菜单")]
    public bool isPause = true;//一开始就Menu界面
    public Animator MainCamera;//控制摄像机拉近远离
    public Animator ShowSaveCavansAnim;//黑幕显示背景

    public GameObject HomePageCavans, SaveCavans, CreateCavans, SettingCavans, LanguageCavans, CGCavans;//主菜单界面，存档界面,捏人界面,设置界面,CG界面

    [Header("捏人界面UI")]
    public InputField nameInputField; // 绑定在 Inspector 里

    public Text hairLabel;
    public Text eyesLabel;
    public Text raceLabel;
    public Text classLabel;

    #region
    // 6个可选种族（UI上循环的那一项）
    public enum RaceOption { Human = 0, Elf1 = 1, Elf2 = 2, Rabbit = 3, Demon1 = 4, Demon2 = 5 }
    // 当前 UI 的种族游标（0~5）
    [SerializeField] private int raceOptionIndex = 0;
    private int GetRabbitHatFromHead(int headIndex)
    {
        // 你的规则：
        // head 1~4,10,13 -> hat 4
        // head 5 -> hat 5
        // head 6 -> hat 6
        // head 7 -> hat 7
        // head 8 -> hat 8
        // head 9 -> hat 9
        // head 11,12 -> hat 10

        if ((headIndex >= 1 && headIndex <= 4) || headIndex == 10 || headIndex == 13) return 4;
        if (headIndex == 5) return 5;
        if (headIndex == 6) return 6;
        if (headIndex == 7) return 7;
        if (headIndex == 8) return 8;
        if (headIndex == 9) return 9;
        if (headIndex == 11 || headIndex == 12) return 10;

        // 兜底：如果出现未定义的 head，先归到最常用的 4
        return 4;
    }//兔族耳朵设置
    private void ApplyRaceSelection()
    {
        switch ((RaceOption)raceOptionIndex)
        {
            case RaceOption.Human: player.YYY_hatIndex = 1; break;
            case RaceOption.Elf1: player.YYY_hatIndex = 2; break;
            case RaceOption.Elf2: player.YYY_hatIndex = 3; break;
            case RaceOption.Rabbit: player.YYY_hatIndex = GetRabbitHatFromHead(player.YYY_headIndex); break;
            case RaceOption.Demon1: player.YYY_hatIndex = 11; break;
            case RaceOption.Demon2: player.YYY_hatIndex = 12; break;
        }
    }//种族选项控制Hat
    #endregion

    public void OnHairLeft() { ChangeSkin(ref player.YYY_headIndex, 1, 13, -1, isHead: true); CreatNewcurrentIndex = 1; UpdateHighlight(); }
    public void OnHairRight() { ChangeSkin(ref player.YYY_headIndex, 1, 13, +1, isHead: true); CreatNewcurrentIndex = 1; UpdateHighlight(); }

    public void OnEyesLeft() { ChangeSkin(ref player.YYY_eyesIndex, 1, 13, -1); CreatNewcurrentIndex = 2; UpdateHighlight(); }
    public void OnEyesRight() { ChangeSkin(ref player.YYY_eyesIndex, 1, 13, +1); CreatNewcurrentIndex = 2; UpdateHighlight(); }

    public void OnRaceLeft()
    {
        //ChangeSkin(ref player.YYY_hatIndex, 1, 4, -1);

        raceOptionIndex++; if (raceOptionIndex > 5){ raceOptionIndex = 0; }if (raceOptionIndex < 0) { raceOptionIndex = 5; }

        ApplyRaceSelection();
        AfterAnySelectionChanged();

        CreatNewcurrentIndex = 3; UpdateHighlight();
    }
    public void OnRaceRight()
    {
        //ChangeSkin(ref player.YYY_hatIndex, 1, 4, +1); 

        raceOptionIndex--; if (raceOptionIndex > 5) { raceOptionIndex = 5; }if (raceOptionIndex < 0) { raceOptionIndex = 0; }

        ApplyRaceSelection();
        AfterAnySelectionChanged();

        CreatNewcurrentIndex = 3; UpdateHighlight(); 
    }

    public void OnClassLeft() { ChangeSkin(ref player.YYY_bodyIndex, 10, 12, -1); player.PlayNormalAttack(); CreatNewcurrentIndex = 4; UpdateHighlight(); }
    public void OnClassRight() { ChangeSkin(ref player.YYY_bodyIndex, 10, 12, +1); player.PlayNormalAttack(); CreatNewcurrentIndex = 4; UpdateHighlight(); }
    void ChangeSkin(ref int index, int min, int max, int delta, bool isHead = false)
    {
        index += delta;
        if (index < min) index = max;
        if (index > max) index = min;


        switch (player.YYY_bodyIndex)
        {
            case 1:
            case 10:
                player.CurrentProfession = 0;
                break;
            case 11:
                player.CurrentProfession = 1;
                break;
            case 12:
                player.CurrentProfession = 2;
                break;
        }

        player._ClothesToClass();//临时让衣服改变职业


        // 如果这次改的是“头发”且当前种族=兔族，则根据最新 headIndex 重算 hatIndex
        if (isHead && (RaceOption)raceOptionIndex == RaceOption.Rabbit)
        {
            player.YYY_hatIndex = GetRabbitHatFromHead(player.YYY_headIndex);
        }

        AfterAnySelectionChanged();

    }//捏人界面玩家点击单个皮肤选项左右之后

 
    private void AfterAnySelectionChanged()
    {
        player.SetSkin();     // 更新外观
        UpdateUI();           // 刷UI文字
        player.SaveCurrent(); // 存当前
        RefreshSaveSlots();   // 刷存档列表
    }   // 一个小的合流函数，避免重复：统一做刷新/保存

    private void DetectRaceOptionFromHat()
    {
        int h = player.YYY_hatIndex;
        if (h == 0) raceOptionIndex = (int)RaceOption.Human;
        else if (h == 1) raceOptionIndex = (int)RaceOption.Elf1;
        else if (h == 2) raceOptionIndex = (int)RaceOption.Elf2;
        else if (h >= 4 && h <= 10) raceOptionIndex = (int)RaceOption.Rabbit;
        else if (h == 11) raceOptionIndex = (int)RaceOption.Demon1;
        else if (h == 12) raceOptionIndex = (int)RaceOption.Demon2;
        else raceOptionIndex = (int)RaceOption.Human; // 兜底
    }//初始化进页面时，请根据 player.YYY_hatIndex 反推一次 raceOptionIndex，避免显示错位：


    #region
    void UpdateUI()
    {
        nameInputField.text = player.currentSaveName;

        //hairLabel.text = $"Hair_{player.YYY_headIndex}";
        //eyesLabel.text = $"Eyes_{player.YYY_eyesIndex}";
        //raceLabel.text = $"Race_{player.YYY_hatIndex}";
        //classLabel.text = $"Class_{player.YYY_bodyIndex}";

        hairLabel.text = $"{LHair()}_{player.YYY_headIndex}";
        eyesLabel.text = $"{LEyes()}_{player.YYY_eyesIndex}";


        // 只在这里把 hatIndex → race 文案
        int ro = RaceOptionFromHat(player.YYY_hatIndex);
        raceOptionIndex = ro; // 保持 UI 内部一致（可要可不要）
        raceLabel.text = $"{LRace()}_{LRaceName(ro)}";
        //raceLabel.text = $"{LRace()}_{LRaceName(raceOptionIndex)}";



        classLabel.text = $"{LClass()}_{LClassName(player.CurrentProfession)}";

    }//捏人界面UI显示

    // 语言：0 日语 1 简中 2 繁中 3 英语 4 韩语
    private int Lang => PlayerPrefs.GetInt("language", 1);

    private static readonly string[,] LABELS = new string[,]
    {
    //        Hair     Eyes     Race      Class  
    /*JP*/ { "髪型",   "瞳",     "種族",    "職業"},
    /*CN*/ { "头发",   "眼睛",   "种族",    "职业" },
    /*TC*/ { "頭髮",   "眼睛",   "種族",    "職業" },
    /*EN*/ { "Hair",   "Eyes",   "Race",    "Class" },
    /*KR*/ { "머리",   "눈",     "종족",    "직업" }
    };

    private static readonly string[,] RACE_NAMES = new string[,]
    {
    //           Human      Elf1        Elf2        Rabbit      Demon1      Demon2
    /*JP*/ { "人間",      "エルフA",  "エルフB",  "ラビット", "魔族A",    "魔族B" },
    /*CN*/ { "人类",      "精灵1",    "精灵2",    "兔族",      "魔族1",    "魔族2" },
    /*TC*/ { "人類",      "精靈1",    "精靈2",    "兔族",      "魔族1",    "魔族2" },
    /*EN*/ { "Human",     "Elf I",     "Elf II",    "Rabbit",    "Demon I",   "Demon II" },
    /*KR*/ { "인간",      "엘프1",     "엘프2",     "토끼족",     "마족1",     "마족2" }
    };

    private static readonly string[,] CLASS_NAMES = new string[,]
    {
    //            Warrior    Archer     Mage
    /*JP*/ { "戦士",      "弓手",     "魔法使い" },
    /*CN*/ { "战士",      "射手",     "法师" },
    /*TC*/ { "戰士",      "射手",     "法師" },
    /*EN*/ { "Warrior",   "Archer",   "Mage" },
    /*KR*/ { "전사",      "궁수",     "마법사" }
    };

    private string LHair() => LABELS[Lang, 0];
    private string LEyes() => LABELS[Lang, 1];
    private string LRace() => LABELS[Lang, 2];
    private string LClass() => LABELS[Lang, 3];

    private string LRaceName(int idx) => RACE_NAMES[Lang, idx];
    private string LClassName(int idx) => CLASS_NAMES[Lang, idx];

    // 0..5 ：Human, Elf1, Elf2, Rabbit, Demon1, Demon2
    private int RaceOptionFromHat(int hat)
    {
        // —— 1 基版本（你当前描述）——
        if (hat == 1) return 0;                 // Human
        if (hat == 2) return 1;                 // Elf1
        if (hat == 3) return 2;                 // Elf2
        if (hat >= 4 && hat <= 10) return 3;    // Rabbit
        if (hat == 11) return 4;                // Demon1
        if (hat == 12) return 5;                // Demon2

        // 兜底：回到 Human
        return 0;

        /* —— 如果你的资源是 0=人类（0 基），请改用下面这段 —— 
        if (hat == 0) return 0;                 // Human
        if (hat == 1) return 1;                 // Elf1
        if (hat == 2) return 2;                 // Elf2
        if (hat >= 4 && hat <= 10) return 3;    // Rabbit
        if (hat == 11) return 4;                // Demon1
        if (hat == 12) return 5;                // Demon2
        return 0;
        */
    }


    #endregion


    public void OnConfirmNameInput()
    {
        string newName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(newName))
            return; // 不处理空输入

        string oldName = player.currentSaveName;

        // 避免重复操作
        if (oldName == newName) return;

        // 如果该名称已存在（可选：检查冲突）
        if (SaveManager.HasSave(newName))
        {
            Debug.LogWarning("已存在此命名的存档！");
            return;
        }

        // 创建新存档数据（复制当前捏人数据）
        PlayerSaveData data = new PlayerSaveData
        {
            characterName = newName,
            headIndex = player.YYY_headIndex,
            eyesIndex = player.YYY_eyesIndex,
            bodyIndex = player.YYY_bodyIndex,
            legsIndex = player.YYY_legsIndex,
            hatIndex = player.YYY_hatIndex,
            weaponIndex = player.weaponIndex,

            professionIndex = player.CurrentProfession,

            level = 1,
            exp = 0,
            maxHP = 1000,
            meleeDamage = UnityEngine.Random.Range(50, 100),
            shootDamage = UnityEngine.Random.Range(50, 100),
            spellDamage = UnityEngine.Random.Range(50, 100),


            weaponAtk = 0,
            armorDef = 0,
            stockingDef = 0,




        };

        //武器还是得分开，法术武器伤害最高，其次近战武器，其次远程武器
        switch (data.professionIndex)
        {
            case 0:
                data.weaponAtk = 100;
                data.armorDef = 30;
                data.stockingDef = 20;
                break;
            case 1:
                data.weaponAtk = 50;
                data.armorDef = 10;
                data.stockingDef = 10;
                break;
            case 2:
                data.weaponAtk = 200;
                data.armorDef = 15;
                data.stockingDef = 10;
                break;
        }

        SaveManager.Save(data); // ✅ 保存新名字存档
        SaveManager.DeleteSave(oldName); // 🗑️ 删除旧存档

        player.currentSaveName = newName; // 更新记录
        Debug.Log($"名称更换成功：{oldName} → {newName}");

        RefreshSaveSlots();//每次单独更新名称也需要刷新存档界面

    }// 玩家输入新名字，调用此函数（将当前玩家身上的值重新带入更改名字的存档）

    public void OnConfirm()
    {

        //抹去当前名称，下次捏人再度选中名称
        player.currentSaveName = null;

        //显示存档界面，隐藏捏人界面
        CreateCavans.SetActive(false);
        SaveCavans.SetActive(true);

        UpdateCurrentSelection(currentIndex);//完成捏人后再一次回到当前选中

        CurrentChooseList = 2;//返回存档界面

        //再度把捏人的检索回到名字
        CreatNewcurrentIndex = 0;
        UpdateHighlight();

        //重新恢复上下可移动
        isInputing = false;
    }//玩家点击Ok


    public void OpenCloseMenu()
    {
        if (CurrentMode == 0)
        {
            //ToCGPage();//打开CG界面
            Invoke("ToCGPage", 0.1f);
        }

        if (CurrentMode == 1)
        {
            if (!isPause)
            {
                MainCamera.SetInteger("View", 0);
                Common_All.SetActive(false);
                ShowSaveCavansAnim.gameObject.SetActive(true);
                ShowSaveCavansAnim.SetBool("Track", true);


                player.isInputBlocked = true;//切断玩家的方向攻击等输入

                RefreshSaveSlots();//只有在打开存档菜单时更新
            }
            else
            {
                MainCamera.SetInteger("View", 2);
                Common_All.SetActive(true);
                ShowSaveCavansAnim.gameObject.SetActive(false);
                ShowSaveCavansAnim.SetBool("Track", false);

                player.isInputBlocked = false;//恢复玩家的方向攻击等输入

                player.currentSaveName = currentSelectedSlot.Data.characterName;//开始游戏时，将这个存档名称带入Player
            }

            isPause = !isPause;
        }



    }

    public void To_CGScence()
    {

        CurrentChooseList = 6;

        ShowSaveCavansAnim.gameObject.SetActive(false);
        MainCamera.SetInteger("View", 1);
    }


    #endregion

    /// <summary>
    /// 存档显示
    /// </summary>
    #region
    [Header("寻找玩家")]
    public GameObject _Player;//玩家
    public Player player;

    [Header("存档显示")]
    public GameObject saveSlotPrefab;
    public Transform saveSlotParent;

    void Start()
    {
        CGUnclockStart();//检测CG解锁



        string folder = Application.persistentDataPath + "/Saves/";
        if (!Directory.Exists(folder)) return;

        foreach (string file in Directory.GetFiles(folder, "save_*.json"))
        {
            string json = File.ReadAllText(file);
            PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);

            GameObject slot = Instantiate(saveSlotPrefab, saveSlotParent);
            slot.GetComponent<SaveSlotUI>().SetInfo(data, skinParts);  // ✅ 新增头像预览

            //SetCurrentSlot(slot.GetComponent<SaveSlotUI>());// 自动选中

            saveSlots.Add(slot.GetComponent<SaveSlotUI>());//把这个存档加入列表
        }

        UpdateCurrentSelection(0);  // 初始化列表内选中第一个

        UpdateScrollLimits();//更新上下翻页范围

    }//读取，显示存档

    public void CreateNewSave()
    {
        player._CreateNewSkin();



        #region[兔族耳朵]
        DetectRaceOptionFromHat();//新建角色的时候根据耳朵反推

        // 如果是兔族，确保 hatIndex 与 headIndex 一致（修正旧档）
        if ((RaceOption)raceOptionIndex == RaceOption.Rabbit)
            player.YYY_hatIndex = GetRabbitHatFromHead(player.YYY_headIndex);

        raceOptionIndex = RaceOptionFromHat(player.YYY_hatIndex); // 只同步“显示所用”的索引
        UpdateUI();

        #endregion


        RefreshSaveSlots();

        //显示捏人界面，隐藏存档界面
        SaveCavans.SetActive(false);
        CreateCavans.SetActive(true);

        UpdateUI();//更新捏人界面UI

        CurrentChooseList = 1;//进入捏人界面
    }//点击【＋】就会随机存档

    public void RefreshSaveSlots()//新增存档，新增存档时更换名字，新增存档时更换皮肤，删除存档
    {
        saveSlots.Clear();  // 清空之前列表

        // 清除已有的
        foreach (Transform child in saveSlotParent)
        {
            Destroy(child.gameObject);
        }

        string folder = Application.persistentDataPath + "/Saves/";
        if (!Directory.Exists(folder)) return;

        foreach (string file in Directory.GetFiles(folder, "save_*.json"))
        {
            string json = File.ReadAllText(file);
            PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);

            GameObject slot = Instantiate(saveSlotPrefab, saveSlotParent);
            slot.GetComponent<SaveSlotUI>().SetInfo(data, skinParts);  // ✅ 新增头像预览



            //SetCurrentSlot(slot.GetComponent<SaveSlotUI>());// 这个是让最后一个高亮显示

            saveSlots.Add(slot.GetComponent<SaveSlotUI>());//把这个存档加入列表
        }


        //UpdateCurrentSelection(0);  // 初始化列表内选中第一个

        Invoke("UpdateScrollLimits", 0.1f);//更新上下翻页范围【一定要延迟一下，等物体销毁光重置光】



    }//刷新当前存档UI


    //////////////////////列表显示存档，方向键切换当前选中按钮//////////////////////////////////
    public List<SaveSlotUI> saveSlots = new List<SaveSlotUI>();
    public int currentIndex = 0;

    public void UpdateCurrentSelection(int newIndex)
    {
        if (saveSlots.Count == 0) return;

        newIndex = Mathf.Clamp(newIndex, 0, saveSlots.Count - 1);

        if (currentSelectedSlot != null)
            currentSelectedSlot.SetHighlight(false);

        currentIndex = newIndex;
        currentSelectedSlot = saveSlots[currentIndex];
        currentSelectedSlot.SetHighlight(true);

        currentSelectedSlot.DelayChoose();//高亮显示与展示当前角色绑定，一旦高亮选中，马上导入这个角色信息

    }//切换选中当前角色


    [Header("删除存档")]
    public GameObject MakeSureDeleteCurrentSave;

    public void DeleteCurrentSelection()
    {
        if (saveSlots.Count == 0) return;

        currentSelectedSlot.DeleteCurrentSave();

        Invoke("CancelDelete", 0.1f);//目前暂时这么做，以防确定按太快直接跳到捏人界面

    }//删除这个角色

    public void CancelDelete()
    {
        MakeSureDeleteCurrentSave.SetActive(false);
        CurrentChooseList = 2;//返回存档界面
    }

    public void TryDelete()
    {
        MakeSureDeleteCurrentSave.SetActive(true);
        CurrentChooseList = -1;//弹出确认删除存档框
    }

    //////////////////////高亮显示//////////////////////////////////

    [HideInInspector]
    public SaveSlotUI currentSelectedSlot = null;

    public void SetCurrentSlot(SaveSlotUI newSlot)
    {
        if (currentSelectedSlot != null)
        {
            currentSelectedSlot.SetHighlight(false);
        }

        currentSelectedSlot = newSlot;
        currentSelectedSlot.SetHighlight(true);
    }

    //////////////////////头像贴图显示//////////////////////////////////

    public SkinPartsDatabase skinParts;


    //////////////////////上下翻页显示//////////////////////////////////
    public RectTransform saveContainer; // Save_All


    private int itemsPerRow = 5;//一行5个
    private int rowsVisible = 2; //可见2行
    private int totalItems => saveContainer.childCount;//目前的存档数

    public int totalRows => Mathf.CeilToInt((float)totalItems / itemsPerRow);//存档需要几行显示
    public int currentRow = 0;//目前你在第几行(0最上面 1下面一行 以此类推)

    public GameObject ScrollUp_Button, ScrollDown_Button;//上下翻转按钮

    public float rowHeight = 350f; //每行的高度

    void UpdateScrollLimits()
    {
        Debug.Log("目前的存档数" + totalItems);
        Debug.Log("目前需要几行显示所有存档" + totalRows);

        // 回到最上面
        currentRow = 0;
        saveContainer.anchoredPosition = new Vector2(saveContainer.anchoredPosition.x, 0);

        // 更新按钮显示
        UpdateScrollButtons();

    }//更新存档数确认翻页

    void UpdateScrollButtons()
    {
        // 顶部不能再上翻
        if (currentRow == 0) { ScrollUp_Button.SetActive(false); }
        else { ScrollUp_Button.SetActive(true); }

        // 底部不能再下翻
        if (currentRow < totalRows - rowsVisible) { ScrollDown_Button.SetActive(true); }
        else { ScrollDown_Button.SetActive(false); }

        if (saveSlotParent.childCount <= 10) { ScrollDown_Button.SetActive(false); Debug.Log("目前需要几行显示所有存档[!!!!]" + totalRows); }//存档数小于等于10也不能下翻

    }//更新按钮

    public void ScrollUp()
    {
        currentRow--;

        saveContainer.anchoredPosition = new Vector2(saveContainer.anchoredPosition.x, saveContainer.anchoredPosition.y - 345);
        UpdateScrollButtons();

    }

    public void ScrollDown()
    {
        currentRow++;

        saveContainer.anchoredPosition = new Vector2(saveContainer.anchoredPosition.x, saveContainer.anchoredPosition.y + 345);
        UpdateScrollButtons();

    }


    #endregion


    /// <summary>
    /// 语言设置，声音设置
    /// </summary>
    #region


    public void ReStart_DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("删除存档");


        //删除C盘所有角色存档
        SaveManager.DeleteAllSaves();



        ReLoadScene();

    }//删除存档

    public void SetLanguage(int Number)
    {
        LanguagePagecurrentIndex = Number;

        SetLanguage_2();
    }
    void SetLanguage_2()
    {
        PlayerPrefs.SetInt("language", LanguagePagecurrentIndex);

        ReLoadScene();
    }

    public AudioMixer audioMixer;
    public AudioMixer BGM_Mixer;



    //--------音量
    public void SetVolune(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
        SE_Bar.fillAmount = Mathf.InverseLerp(-80f, 0f, SEVolume);
    }
    public void SetBGMVolune(float value)
    {
        BGM_Mixer.SetFloat("BGMVolume", value);
        BGM_Bar.fillAmount = Mathf.InverseLerp(-80f, 0f, BGMVolume);
    }

    public Image BGM_Bar;
    public Image SE_Bar;

    public float BGMVolume = 0f;
    public float SEVolume = 0f;



    #endregion

    /// <summary>
    /// CG界面选中
    /// </summary>
    #region
    public List<CGOptionUI> cgButtons = new List<CGOptionUI>();
    int CGcurrentIndex = 0;



    void CGUnclockStart()
    {
        foreach (var btn in cgButtons)
        {
            btn.SetUnlockedFromPrefs();
        }

        // 查找第一个已解锁的
        for (int i = 0; i < cgButtons.Count; i++)
        {
            if (cgButtons[i].unlocked)
            {
                currentIndex = i;
                break;
            }
        }
        UpdateHighlight();
    }//开始检测CG解锁数
    void MoveSelection(int direction)
    {
        // 取消旧高亮
        cgButtons[CGcurrentIndex].SetHighlight(false);

        // 循环查找下一个已解锁的项
        int max = cgButtons.Count;
        for (int i = 1; i < max; i++)
        {
            int newIndex = (CGcurrentIndex + direction * i + max) % max;
            if (cgButtons[newIndex].unlocked)
            {
                CGcurrentIndex = newIndex;
                break;
            }
        }

        // 更新高亮
        UpdateHighlight_CG();
    }//切换当前选中

    void UpdateHighlight_CG()
    {
        for (int i = 0; i < cgButtons.Count; i++)
        {
            cgButtons[i].SetHighlight(i == CGcurrentIndex);
        }
    }


    public void PlayPlayerCG(string CGName)
    {
        player.ForCGRandomEnemySkin();
        player.frameEvents.audioS.Stop();


        if (!string.IsNullOrEmpty(CGName))
        {
            player.anim.Play("CG/" + CGName);
        }

        //switch (CGName)
        //{
        //    case "CG_OnanismFront_1":
        //        player.anim.Play("CG/CG_OnanismFront_1");
        //        break;
        //    case "CG_OnanismSide_1":
        //        player.anim.Play("CG/CG_OnanismSide_1");
        //        break;
        //
        //
        //    case "CG_InsultSide_1":
        //        player.anim.Play("CG/CG_InsultSide_1");
        //        break;
        //    case "CG_GagSide_1":
        //        player.anim.Play("CG/CG_GagSide_1");
        //        break;
        //    case "CG_FistingFront_1":
        //        player.anim.Play("CG/CG_FistingFront_1");
        //        break;
        //
        //    case "CG_RapeFront_1":
        //        player.anim.Play("CG/CG_RapeFront_1");
        //        break;
        //    case "CG_RapeSide_1":
        //        player.anim.Play("CG/CG_RapeSide_1");
        //        break;
        //    case "CG_AssaultFront_1":
        //        player.anim.Play("CG/CG_AssaultFront_1");
        //        break;
        //    case "CG_AssaultSide_1":
        //        player.anim.Play("CG/CG_AssaultSide_1");
        //        break;
        //}
    }

    #endregion

    /// <summary>
    /// 菜单层面多端输入
    /// </summary>
    #region
    [SerializeField] private InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction confirmAction;
    private InputAction cancelAction;
    private InputAction deleteAction;
    private InputAction pauseAction;

    private void OnEnable()
    {
        moveAction = inputActions.FindAction("Move");
        confirmAction = inputActions.FindAction("Attack");  // 或者用名为 "Submit"
        cancelAction = inputActions.FindAction("Dodge");    // 或者用名为 "Cancel"
        deleteAction = inputActions.FindAction("Run");    // 或者用名为 "Delete"
        pauseAction = inputActions.FindAction("Pause");

        moveAction.performed += OnMove;
        confirmAction.started += OnConfirm;
        cancelAction.started += OnCancel;
        deleteAction.started += OnDelete;
        pauseAction.started += OnPause;

        moveAction.Enable();
        confirmAction.Enable();
        cancelAction.Enable();
        deleteAction.Enable();
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        confirmAction.started -= OnConfirm;
        cancelAction.started -= OnCancel;
        deleteAction.started -= OnDelete;
        pauseAction.started -= OnPause;

        moveAction.Disable();
        confirmAction.Disable();
        cancelAction.Disable();
        deleteAction.Disable();
        pauseAction.Disable();
    }

    //冷却时间
    private float inputCooldown2 = 0.2f;
    private float lastInputTime2 = -999f;

    //处于打字的时候不能上下移动
    public bool isInputing = false;

    private void OnMove(InputAction.CallbackContext ctx)
    {

        #region 冷却时间
        if (Time.time - lastInputTime2 < inputCooldown2)
            return;

        lastInputTime2 = Time.time;
        #endregion

        if (player.isInputBlocked && !isInputing)
        {
            Vector2 dir = ctx.ReadValue<Vector2>();

            //主菜单界面
            if (CurrentChooseList == 0)
            {
                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    HomePagecurrentIndex = Mathf.Clamp(HomePagecurrentIndex - 1, 0, 4);
                    UpdateHomePage_Highlight();


                }
                else if (dir.y < -0.5f)
                {
                    HomePagecurrentIndex = Mathf.Clamp(HomePagecurrentIndex + 1, 0, 4);
                    UpdateHomePage_Highlight();


                }
            }

            //存档界面
            if (CurrentChooseList == 2)
            {
                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    UpdateCurrentSelection(currentIndex + 1);
                }
                else if (dir.x < -0.5f)
                {
                    UpdateCurrentSelection(currentIndex - 1);
                }


                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {

                    if (currentIndex - 5 < 0)
                    {
                        UpdateCurrentSelection(0);
                    }
                    else
                    {
                        UpdateCurrentSelection(currentIndex - 5);
                    }

                    if (ScrollUp_Button.activeSelf) { ScrollUp(); }

                }
                else if (dir.y < -0.5f)
                {

                    if (currentIndex + 5 > saveSlots.Count)
                    {
                        UpdateCurrentSelection(saveSlots.Count);
                    }
                    else
                    {
                        UpdateCurrentSelection(currentIndex + 5);
                    }
                    if (currentIndex >= 6 && ScrollDown_Button.activeSelf) { ScrollDown(); }

                }

            }

            //捏人界面
            if (CurrentChooseList == 1)
            {
                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    switch (CreatNewcurrentIndex)
                    {
                        case 1: HairRight.onClick.Invoke(); break;
                        case 2: EyesRight.onClick.Invoke(); break;
                        case 3: RaceRight.onClick.Invoke(); break;
                        case 4: ClassRight.onClick.Invoke(); break;
                    }
                }
                else if (dir.x < -0.5f)
                {
                    switch (CreatNewcurrentIndex)
                    {
                        case 1: HairLeft.onClick.Invoke(); break;
                        case 2: EyesLeft.onClick.Invoke(); break;
                        case 3: RaceLeft.onClick.Invoke(); break;
                        case 4: ClassLeft.onClick.Invoke(); break;
                    }
                }



                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    CreatNewcurrentIndex = Mathf.Clamp(CreatNewcurrentIndex - 1, 0, 5);
                    UpdateHighlight();


                }
                else if (dir.y < -0.5f)
                {
                    CreatNewcurrentIndex = Mathf.Clamp(CreatNewcurrentIndex + 1, 0, 5);
                    UpdateHighlight();


                }

            }

            //设置界面
            if (CurrentChooseList == 3)
            {

                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    switch (SettingPagecurrentIndex)
                    {
                        case 0:

                            float NewBGMVolume = BGMVolume + 10f;
                            SetBGMVolune(NewBGMVolume);
                            BGMVolume = NewBGMVolume;
                            Debug.Log("拉高BGM");
                            break;
                        case 1:
                            float NewSEVolume = SEVolume + 10f;
                            SetVolune(NewSEVolume);
                            SEVolume = NewSEVolume;
                            Debug.Log("拉高SE");
                            break;

                    }

                }
                else if (dir.x < -0.5f)
                {

                    switch (SettingPagecurrentIndex)
                    {


                        case 0:

                            float NewBGMVolume = BGMVolume - 10f;
                            SetBGMVolune(NewBGMVolume);
                            BGMVolume = NewBGMVolume;
                            Debug.Log("降低BGM");
                            break;
                        case 1:
                            float NewSEVolume = SEVolume - 10f;
                            SetVolune(NewSEVolume);
                            SEVolume = NewSEVolume;
                            Debug.Log("降低SE");
                            break;

                    }
                }


                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    SettingPagecurrentIndex = Mathf.Clamp(SettingPagecurrentIndex - 1, 0, 4);
                    UpdateSettingPage_Highlight();


                }
                else if (dir.y < -0.5f)
                {
                    SettingPagecurrentIndex = Mathf.Clamp(SettingPagecurrentIndex + 1, 0, 4);
                    UpdateSettingPage_Highlight();


                }
            }

            //语言界面
            if (CurrentChooseList == 4)
            {
                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    LanguagePagecurrentIndex = Mathf.Clamp(LanguagePagecurrentIndex - 1, 0, 4);
                    UpdateLanguagePage_Highlight();


                }
                else if (dir.y < -0.5f)
                {
                    LanguagePagecurrentIndex = Mathf.Clamp(LanguagePagecurrentIndex + 1, 0, 4);
                    UpdateLanguagePage_Highlight();


                }
            }

            //CG界面
            if (CurrentChooseList == 5)
            {
                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    MoveSelection(-1);

                }
                else if (dir.y < -0.5f)
                {

                    MoveSelection(1);
                }
            }

            AudioManager.instance.AudioPlay(AudioManager.instance.Attack_pai1);
        }




    }

    public void OnChangeName()
    {
        isInputing = true;
        CreatNewcurrentIndex = 0;
        UpdateHighlight();
    }//打字的时候锁住上下移动
    public void OnChangeNameOver()
    {
        isInputing = false;
    }//打字的时候锁住上下移动

    private void OnConfirm(InputAction.CallbackContext ctx)
    {
        if (player.isInputBlocked)
        {
            // 可选：进入下一级菜单、确认开始游戏等

            //确认删除界面
            if (CurrentChooseList == -1)
            {
                DeleteCurrentSelection();//删除角色
            }

            //主菜单界面
            if (CurrentChooseList == 0)
            {
                switch (HomePagecurrentIndex)
                {
                    case 0:
                        ToSavePageButton(1);//开始游戏进入存档界面
                        break;
                    case 1:
                        ToSavePageButton(0);//开始游戏进入存档界面(CG)
                        break;
                    case 2:
                        //ToSettingPage();
                        Invoke("ToSettingPage", 0.1f);//进入设置界面
                        break;
                    case 3:
                        ExitGame();
                        break;
                }

                AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
            }

            //存档界面
            if (CurrentChooseList == 2)
            {
                if (CurrentMode == 0)
                {
                    //ToCGPage();//打开CG界面
                    Invoke("ToCGPage", 0.1f);
                }

                if (CurrentMode == 1)
                {
                    CreateNewSave();//新建角色
                }


                AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
            }

            //捏人界面
            if (CurrentChooseList == 1)
            {

                if (CreatNewcurrentIndex == 0)
                {
                    //编辑名称
                    nameInputField.ActivateInputField(); // ✅ 激活输入框并进入编辑

                }
                if (CreatNewcurrentIndex == 5)
                {
                    //点 OK
                    okButton.onClick.Invoke();
                    AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
                    isInputing = false;
                }
            }

            //设置界面
            if (CurrentChooseList == 3)
            {
                switch (SettingPagecurrentIndex)
                {

                    case 2:
                        //ToSettingPage();
                        Invoke("ToLanguagePage", 0.1f);//进入设置界面
                        break;
                    case 3:
                        ReStart_DeleteAll();//删除存档重刷场景
                        break;
                }

                AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
            }

            //语言界面
            if (CurrentChooseList == 4)
            {
                SetLanguage_2();
            }

            //CG界面
            if (CurrentChooseList == 5)
            {
                cgButtons[CGcurrentIndex].PlayCG();
            }
        }


    }//键盘J    xbox手柄B      ps手柄O

    private void OnCancel(InputAction.CallbackContext ctx)
    {
        if (player.isInputBlocked)
        {
            // 可选：退出菜单、返回上一级等

            //确认删除界面
            if (CurrentChooseList == -1)
            {
                //CancelDelete();//取消删除
                Invoke("CancelDelete", 0.1f);
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            //存档界面//设置界面
            if (CurrentChooseList == 2 || CurrentChooseList == 3)
            {
                ToHomePage();
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            //语言设置界面
            if (CurrentChooseList == 4)
            {
                ToSettingPage();
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }
            //CG界面
            if (CurrentChooseList == 5)
            {
                Invoke("ToSavePage", 0.1f);//开始游戏进入存档界面(CG)
                CurrentMode = 0;
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }
            //CG鉴赏中
            if (CurrentChooseList == 6)
            {
                Invoke("ToCGPage", 0.1f);//开始游戏进入存档界面(CG)
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }
        }

    }//键盘K      xbox手柄A       ps手柄X

    private void OnDelete(InputAction.CallbackContext ctx)
    {
        if (player.isInputBlocked)
        {
            // 可选：删除存档
            if (CurrentChooseList == 2)
            {
                currentSelectedSlot.Delete();//跳出是否删除存档界面
                //DeleteCurrentSelection();
            }

        }

    }//键盘Space    xbox手柄左肩键       ps手柄左肩键
    private void OnPause(InputAction.CallbackContext ctx)
    {


        // 可选：暂停继续游戏
        if (CurrentChooseList == 2)
        {
            OpenCloseMenu();
        }


        AudioManager.instance.AudioPlay(AudioManager.instance.Bullet_AK);
    }//键盘ESC      xbox手柄——        ps手柄opt


    #endregion

    /// <summary>
    /// 跳转网页/退出游戏
    /// </summary>
    #region
    public void OpenURL_Patreon()
    {
        Application.OpenURL("https://www.patreon.com/c/NEKOUJI/posts");
    }

    public void OpenURL_Discord()
    {
        Application.OpenURL("https://discord.com/channels/1342112706274267249/1342112706274267252");
    }

    public void OpenURL_Steam()
    {
        Application.OpenURL("https://store.steampowered.com/");
    }


    public void OpenURL_YYY()
    {
        Application.OpenURL("https://x.com/Detective_ye");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");

        Application.Quit();
    }

    #endregion



    /// <summary>
    /// 血条等各种值
    /// </summary>
    #region
    [Header("生命值")]
    public Image HealthBar;
    public Text HealthText;


    [Header("体力值")]
    public Image StrengthBar;
    public Text StrengthText;

    [Header("暴击值")]
    public Image CriticalBar;

    [Header("经验值等级")]
    public Image ExperienceBar;

    // 用于闪烁控制
    private float flashTimer = 0f;


    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        HealthBar.fillAmount = (float)curAmount / (float)maxAmount;

        HealthText.text = curAmount + "/" + maxAmount;

        //if (curAmount <= maxAmount / 3)
        //{ HealthBar.color = new Color(1.0f, 1.0f, 0.0f, 1.0f); }// 纯黄色
        //else if (curAmount > maxAmount / 3 && curAmount <= maxAmount / 2)
        //{ HealthBar.color = new Color(1.0f, 0.5f, 0.0f, 1.0f); } // 橙色
        //else
        //{ HealthBar.color = Color.red; }
    }
    public void UpdateStrengthBar(int curAmount, int maxAmount)
    {
        if (StrengthBar == null || StrengthText == null)
        {
            Debug.LogError("Strength UI 未绑定！");
            return;
        }
        StrengthBar.fillAmount = (float)curAmount / (float)maxAmount;

        StrengthText.text = curAmount + "/" + maxAmount.ToString();



        //if (curAmount <= maxAmount / 3)
        //{ StrengthBar.color = new Color(0.8f, 0.6f, 1.0f, 1.0f); }  // 淡紫色
        //else if (curAmount > maxAmount / 3 && curAmount <= maxAmount / 2)
        //{ StrengthBar.color = new Color(0.0f, 1.0f, 0.0f, 1.0f); } // 纯绿色
        //else
        //{ StrengthBar.color = new Color(0.0f, 0.68f, 0.93f, 1.0f); }//浅蓝色
    }
    public void UpdateCriticalBar(int curAmount, int maxAmount)
    {
        float fillPercent = (float)curAmount / (float)maxAmount;
        CriticalBar.fillAmount = fillPercent;

        // 多段颜色插值：蓝 → 绿 → 黄 → 红
        Color baseColor;

        if (fillPercent < 0.33f) // 0%~33%：蓝到绿
        {
            baseColor = Color.Lerp(new Color(0f, 0.5f, 1f), Color.green, fillPercent / 0.33f);
        }
        else if (fillPercent < 0.66f) // 33%~66%：绿到黄
        {
            baseColor = Color.Lerp(Color.green, Color.yellow, (fillPercent - 0.33f) / 0.33f);
        }
        else // 66%~100%：黄到红
        {
            baseColor = Color.Lerp(Color.yellow, Color.red, (fillPercent - 0.66f) / 0.34f);
        }

        // 高暴击值闪烁（红黄闪）
        if (fillPercent > 0.9f)
        {
            flashTimer += Time.deltaTime * 4f; // 闪烁速度
            float alpha = Mathf.Abs(Mathf.Sin(flashTimer));
            Color flashColor = Color.Lerp(baseColor, Color.yellow, alpha); // 红黄闪烁
            CriticalBar.color = flashColor;
        }
        else
        {
            CriticalBar.color = baseColor;
        }
    }

    public void UpdateExperienceBar(int curAmount, int maxAmount)
    {
        ExperienceBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

    #endregion



}

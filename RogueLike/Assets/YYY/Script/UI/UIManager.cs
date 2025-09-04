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





        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleBagFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleBugSide_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleFront_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleHermitCrabFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleHermitCrabSide_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleLeechSide_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleMonsterFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_MonsterSide_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleWallFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleWallSide_1"));//0未解锁  1解锁





        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_Hogtie_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_HangSide_4"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_HangSide_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_HangFront_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_HangDown_4"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_HangDown_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_FeraSide_1"));//0未解锁  1解锁

        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_01"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_02"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_03"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_04"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_05"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_06"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_07"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_08"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_09"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_10"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_11"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_12"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_13"));//0未解锁  1解锁


        PlayerPrefs.SetInt("CG_OnanismFront_1", 1);//目前保持第一个CG永远在
        //PlayerPrefs.SetInt("CG_OnanismSide_1", 1);
        //
        //PlayerPrefs.SetInt("CG_InsultSide_1", 1);
        //PlayerPrefs.SetInt("CG_GagSide_1", 1);
        //PlayerPrefs.SetInt("CG_FistingFront_1", 1);
        //
        //PlayerPrefs.SetInt("CG_RapeFront_1", 1);
        //PlayerPrefs.SetInt("CG_RapeSide_1", 1);
        //PlayerPrefs.SetInt("CG_AssaultFront_1", 1);
        //PlayerPrefs.SetInt("CG_AssaultSide_1", 1);
        //
        //PlayerPrefs.SetInt("CG_TentacleBagFront_1", 1);
        //PlayerPrefs.SetInt("CG_TentacleBugSide_1", 1);
        //
        //PlayerPrefs.SetInt("CG_TentacleFront_1", 1);
        //
        //PlayerPrefs.SetInt("CG_TentacleHermitCrabFront_1", 1);
        //PlayerPrefs.SetInt("CG_TentacleHermitCrabSide_1", 1);
        //
        //PlayerPrefs.SetInt("CG_TentacleLeechSide_1", 1);
        //
        //PlayerPrefs.SetInt("CG_TentacleMonsterFront_1", 1);
        //PlayerPrefs.SetInt("CG_MonsterSide_1", 1);
        //
        //PlayerPrefs.SetInt("CG_TentacleWallFront_1", 1);
        //PlayerPrefs.SetInt("CG_TentacleWallSide_1", 1);
        //
        //
        //
        //PlayerPrefs.SetInt("CG_Hogtie_1", 1);
        //
        //PlayerPrefs.SetInt("CG_HangSide_4", 1);
        //PlayerPrefs.SetInt("CG_HangSide_1", 1);
        //PlayerPrefs.SetInt("CG_HangFront_1", 1);
        //
        //PlayerPrefs.SetInt("CG_HangDown_4", 1);
        //PlayerPrefs.SetInt("CG_HangDown_1", 1);
        //
        //PlayerPrefs.SetInt("CG_FeraSide_1", 1);


        PlayerPrefs.SetInt("Chapter_01", 1);//目前保持第一章永远在
        //PlayerPrefs.SetInt("Chapter_02", 1);
        //PlayerPrefs.SetInt("Chapter_03", 1);
        //PlayerPrefs.SetInt("Chapter_04", 1);
        //PlayerPrefs.SetInt("Chapter_05", 1);
        //PlayerPrefs.SetInt("Chapter_06", 1);
        //PlayerPrefs.SetInt("Chapter_07", 1);
        //PlayerPrefs.SetInt("Chapter_08", 1);
        //PlayerPrefs.SetInt("Chapter_09", 1);
        //PlayerPrefs.SetInt("Chapter_10", 1);
        //PlayerPrefs.SetInt("Chapter_11", 1);
        //PlayerPrefs.SetInt("Chapter_12", 1);
        //PlayerPrefs.SetInt("Chapter_13", 1);

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
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Loading.SetActive(true);
    }//重刷场景

    [Header("主菜单界面层级")]
    public int CurrentChooseList = 0;//-2确认是否删除所有存档  -1确认是否删除存档  0主菜单界面   1捏人界面   2存档界面   3设置界面  4语言选择界面   5CG界面   6CG鉴赏中   7游戏模式选择   8剧情章节选择  9剧情AVG界面
    public int CurrentMode = 0;//0 进入CG界面  1捏人/进入游戏
    public int HomePagecurrentIndex = 0;//0 开始游戏  1 CG鉴赏  2 设置  3 退出
    public int CreatNewcurrentIndex = 0;//0 名称 1 眼睛  2 头  3 种族  4 职业  5 确定
    public int SettingPagecurrentIndex = 0;//0 BGM  1 SE  2 语言  3 删除存档
    public int LanguagePagecurrentIndex = 0;//0 日语 1中文 2繁中 3英语 4韩语
    public int ModePagecurrentIndex = 0;//0 故事模式 1角斗场模式 2地下城模式

    [SerializeField] private GameObject[] HomePage_highlightObjs; // 主页高亮显示
    [SerializeField] private GameObject[] highlightObjs; // 捏人界面高亮显示
    [SerializeField] private GameObject[] SettingPage_highlightObjs; // 设置高亮显示
    [SerializeField] private GameObject[] LanguagePage_highlightObjs; // 语言设置高亮显示
    [SerializeField] private GameObject[] ModePage_highlightObjs; // 游戏模式设置高亮显示


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

    private void UpdateModePage_Highlight()
    {
        for (int i = 0; i < ModePage_highlightObjs.Length; i++)
        {
            ModePage_highlightObjs[i].SetActive(i == ModePagecurrentIndex);
        }

        switch (ModePagecurrentIndex) 
        {
            case 0:
                Text_Story.SetActive(true);
                Text_OneToOne.SetActive(false);
                Text_Dungeon.SetActive(false);
                break;
            case 1:
                Text_Story.SetActive(false);
                Text_OneToOne.SetActive(true);
                Text_Dungeon.SetActive(false);
                break;
            case 2:
                Text_Story.SetActive(false);
                Text_OneToOne.SetActive(false);
                Text_Dungeon.SetActive(true);
                break;
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

            //钮按下后绿色选中也会过去
            HomePagecurrentIndex = 1;
            UpdateHomePage_Highlight();
        }
        else
        {
            //主界面
            player.anim.Play("Girl_Default_Idle");

            //钮按下后绿色选中也会过去
            HomePagecurrentIndex = 0;
            UpdateHomePage_Highlight();
        }

    }


    public void ToSavePage()
    {
        ModeCavans.SetActive(false);
        HomePageCavans.SetActive(false);
        CGCavans.SetActive(false);
        CurrentChooseList = 2;

    }

    public GameObject Text_Story, Text_OneToOne, Text_Dungeon;

    public void ToStoryStage() 
    {
        Invoke("ToChapterPage", 0.1f);

        //钮按下后绿色选中也会过去
        ModePagecurrentIndex = 0;
        UpdateModePage_Highlight();
    }
    public void ToOneToOneStage() 
    {
        ToSavePageButton(1);//开始游戏进入存档界面
        //钮按下后绿色选中也会过去
        ModePagecurrentIndex = 1;
        UpdateModePage_Highlight();
    }
    public void ToDungeonStage()
    {
        ToSavePageButton(1);//开始游戏进入存档界面
        //钮按下后绿色选中也会过去
        ModePagecurrentIndex = 2;
        UpdateModePage_Highlight();
    }

    public void ToChapterPage() 
    {
        ChapterCavans.SetActive(true);
        CurrentChooseList = 8;
    }

    public void ToSettingPage()
    {
        SettingCavans.SetActive(true);
        LanguageCavans.SetActive(false);
        CurrentChooseList = 3;

        HomePagecurrentIndex = 2;
        UpdateHomePage_Highlight();
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
        ModeCavans.SetActive(false);
        ChapterCavans.SetActive(false);
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

    public void ToModePage() 
    {
        ModeCavans.SetActive(true);
        ChapterCavans.SetActive(false);
        CurrentChooseList = 7;
    }

    public void To_AVGScene()
    {
        CurrentChooseList = 9;
        AVGCavans.SetActive(true);

        HomePageCavans.SetActive(false);
        ModeCavans.SetActive(false);
        ChapterCavans.SetActive(false);
    }
    void Delay_AVG_ShowText() 
    {
        dialogSystem.ShowText();
    }

    #endregion

    /// <summary>
    /// 捏人菜单
    /// </summary>
    #region
    [Header("各类菜单")]
    public bool isPause = true;//一开始就Menu界面
    public GameObject PauseMenu;

    public void PauseGame() 
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);

        player.isInputBlocked = true;//切断玩家的方向攻击等输入
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);

        player.isInputBlocked = false;//恢复玩家的方向攻击等输入
    }

    public Animator MainCamera;//控制摄像机拉近远离
    public Animator ShowSaveCavansAnim;//黑幕显示背景

    public GameObject HomePageCavans, SaveCavans, CreateCavans, SettingCavans, LanguageCavans, CGCavans,ModeCavans, ChapterCavans,AVGCavans;//主菜单界面，存档界面,捏人界面,设置界面,CG界面,游戏模式选择界面
    public DialogSystem dialogSystem;
    [Header("捏人界面UI")]
    public InputField nameInputField; // 绑定在 Inspector 里

    public Text hairLabel;
    public Text eyesLabel;
    public Text raceLabel;
    public Text classLabel;

    public Text IntroduceOfRace;//介绍文本

    #region 耳朵与种族绑定
    public enum RaceOption
    {
        Human = 0,      // 人类
        Elf = 1,        // 精灵
        HighElf = 2,    // 高等精灵
        RabbitBlack = 3,// 北方兔族（黑）
        RabbitWhite = 4,// 南方兔族（白）
        Demon = 6,      // 魔族
        HighDemon = 5   // 高等魔族
    }

    [SerializeField] private int raceOptionIndex = 0; // 0..6

    private void ApplyRaceSelectionSimple()
    {
        switch ((RaceOption)raceOptionIndex)
        {
            case RaceOption.Human: player.YYY_hatIndex = 1; IntroduceOfRace.text = RACE_DESC[Lang, 0]; break;
            case RaceOption.Elf: player.YYY_hatIndex = 2; IntroduceOfRace.text = RACE_DESC[Lang, 1]; break;
            case RaceOption.HighElf: player.YYY_hatIndex = 3; IntroduceOfRace.text = RACE_DESC[Lang, 2]; break;
            case RaceOption.RabbitBlack: player.YYY_hatIndex = 4; IntroduceOfRace.text = RACE_DESC[Lang, 3]; break; // 黑色兔耳
            case RaceOption.RabbitWhite: player.YYY_hatIndex = 10; IntroduceOfRace.text = RACE_DESC[Lang, 4]; break; // 白色兔耳
            case RaceOption.Demon: player.YYY_hatIndex = 11; IntroduceOfRace.text = RACE_DESC[Lang, 5]; break;
            case RaceOption.HighDemon: player.YYY_hatIndex = 12; IntroduceOfRace.text = RACE_DESC[Lang, 6]; break;
        }
    }
    #endregion












    public void OnHairLeft() 
    {
        if (IsLuna(player.currentSaveName) == false) { ChangeSkin(ref player.YYY_headIndex, 1, 13, -1); CreatNewcurrentIndex = 1; UpdateHighlight(); } 
    }
    public void OnHairRight() 
    { 
        if (IsLuna(player.currentSaveName) == false) { ChangeSkin(ref player.YYY_headIndex, 1, 13, +1); CreatNewcurrentIndex = 1; UpdateHighlight(); }
    }

    public void OnEyesLeft()
    {
        if (IsLuna(player.currentSaveName) == false) { ChangeSkin(ref player.YYY_eyesIndex, 1, 13, -1); CreatNewcurrentIndex = 2; UpdateHighlight(); }
    }
    public void OnEyesRight()
    {
        if (IsLuna(player.currentSaveName) == false) { ChangeSkin(ref player.YYY_eyesIndex, 1, 13, +1); CreatNewcurrentIndex = 2; UpdateHighlight(); }
    }

    public void OnRaceLeft()
    {
        //ChangeSkin(ref player.YYY_hatIndex, 1, 4, -1);
        if (IsLuna(player.currentSaveName) == false)
        {
            raceOptionIndex++; if (raceOptionIndex > 6) { raceOptionIndex = 0; }
            if (raceOptionIndex < 0) { raceOptionIndex = 6; }

            ApplyRaceSelectionSimple();
            AfterAnySelectionChanged();

            CreatNewcurrentIndex = 3; UpdateHighlight();
        }
    }
    public void OnRaceRight()
    {
        //ChangeSkin(ref player.YYY_hatIndex, 1, 4, +1); 
        if (IsLuna(player.currentSaveName) == false)
        {
            raceOptionIndex--; if (raceOptionIndex > 6) { raceOptionIndex = 0; }
            if (raceOptionIndex < 0) { raceOptionIndex = 6; }

            ApplyRaceSelectionSimple();
            AfterAnySelectionChanged();

            CreatNewcurrentIndex = 3; UpdateHighlight();
        }
    }

    public void OnClassLeft()
    {
        if (IsLuna(player.currentSaveName) == false) { ChangeSkin(ref player.YYY_bodyIndex, 10, 12, -1); player.PlayNormalAttack(); CreatNewcurrentIndex = 4; UpdateHighlight(); }
    }
    public void OnClassRight()
    {
        if (IsLuna(player.currentSaveName)==false) { ChangeSkin(ref player.YYY_bodyIndex, 10, 12, +1); player.PlayNormalAttack(); CreatNewcurrentIndex = 4; UpdateHighlight(); }
    }
    void ChangeSkin(ref int index, int min, int max, int delta)
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

        AfterAnySelectionChanged();

    }//捏人界面玩家点击单个皮肤选项左右之后

    public void AfterAnySelectionChanged()
    {
        player.SetSkin();     // 更新外观
        UpdateUI();           // 刷UI文字
        player.SaveCurrent(); // 存当前
        RefreshSaveSlots();   // 刷存档列表
    }

    #region 捏人界面启动多语言

    private int RaceOptionFromHat_Simple(int hat)
    {
        switch (hat)
        {
            case 1: return (int)RaceOption.Human;
            case 2: return (int)RaceOption.Elf;
            case 3: return (int)RaceOption.HighElf;
            case 4: return (int)RaceOption.RabbitBlack;
            case 10: return (int)RaceOption.RabbitWhite;
            case 11: return (int)RaceOption.Demon;
            case 12: return (int)RaceOption.HighDemon;
            // 兼容舊檔：5..9 以前的兔族耳，統一歸為黑兔
            default:
                if (hat >= 5 && hat <= 9) return (int)RaceOption.RabbitBlack;
                return (int)RaceOption.Human;
        }
    }

    void UpdateUI()
    {
        nameInputField.text = player.currentSaveName;

        //hairLabel.text = $"Hair_{player.YYY_headIndex}";
        //eyesLabel.text = $"Eyes_{player.YYY_eyesIndex}";
        //raceLabel.text = $"Race_{player.YYY_hatIndex}";
        //classLabel.text = $"Class_{player.YYY_bodyIndex}";

        hairLabel.text = $"{LHair()}_{player.YYY_headIndex}";
        eyesLabel.text = $"{LEyes()}_{player.YYY_eyesIndex}";

        int ro = RaceOptionFromHat_Simple(player.YYY_hatIndex);
        raceOptionIndex = ro; // 保持一致
        raceLabel.text = $"{LRaceName(ro)}";
        //raceLabel.text = $"{LRace()}_{LRaceName(raceOptionIndex)}";

        classLabel.text = $"{LClassName(player.CurrentProfession)}";

    }//捏人界面UI显示

    // 语言：0 日语 1 简中 2 繁中 3 英语 4 韩语
    private int Lang => PlayerPrefs.GetInt("language");

    private static readonly string[,] LABELS = new string[,]
    {
    //        Hair     Eyes  
    /*JP*/ { "髪型",   "瞳"},
    /*CN*/ { "头发",   "眼睛"},
    /*TC*/ { "頭髮",   "眼睛" },
    /*EN*/ { "Hair",   "Eyes"},
    /*KR*/ { "머리",   "눈" }
    };

    // 语言：0 日 1 简中 2 繁中 3 英 4 韩
    private static readonly string[,] RACE_NAMES = new string[,]
    {
    // Human,     Elf,      HighElf,            RabbitBlack,              RabbitWhite,               Demon,     HighDemon
    { "人間",    "エルフ",  "ハイエルフ",        "北方ラビット",     "南方ラビット",      "魔族",     "上位魔族" }, // JP
    { "人类",    "精灵",    "高等精灵",          "北方兔族",         "南方兔族",          "魔族",     "高等魔族" }, // CN
    { "人類",    "精靈",    "高等精靈",          "北方兔族",         "南方兔族",          "魔族",     "高等魔族" }, // TC
    { "Human",   "Elf",     "High Elf",         "Northern Rabbit","Southern Rabbit", "Demon",   "High Demon" }, // EN
    { "인간",    "엘프",    "하이 엘프",         "북부 토끼족",        "남부 토끼족",         "마족",     "상위 마족" }, // KR
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


    #region  种族介绍提示
    // 7个种族顺序：Human, Elf, HighElf, RabbitBlack, RabbitWhite, Demon, HighDemon
    private static readonly string[,] RACE_DESC = new string[,]
    {
        { // JP
            "大陸で最も好戦的な種族で、各地を征服し他種族を隷属させてきた。体力と近接攻撃に優れる。",
            "射撃と魔法に長ける種族。森の城邦が滅ぼされて以降、人間に奴隷として監禁され、市場価格は中庸。",
            "エルフの中でも稀少で、奴隷市場でも上物。強力な魔法適性を持ち、エルフ固有の術を行使できる。",
            "兎族は市場に数が多いが価格は並。身軽だが体力は低く、回避と突進攻撃を得意とする。",
            "人間が草原を征服して以降、兎族は大きく繁殖。温和な気質で身軽、体力は低いが回避と射撃に長ける。",
            "深淵をルーツにもつ混血。闇と炎に親和し、近接と魔法に優れる。魔族化で生命吸収の能力を得る。",
            "魔族の純血上位。強力な儀式と魔界召喚を操る。魔族化で生命吸収の能力を得る。"
        },
        { // CN(简体)
            "整片大陆上最好战的种族，四处征服和奴役其他种族。她们在生命值与近战攻击上有优势。",
            "擅长射击与法术的种族，在森林中的城邦被摧毁后她们被人类奴役监禁，性奴市场上的价格适中。",
            "精灵中的珍稀品种，也是性奴市场上的上等货。具有强大的法术天赋，她们会释放精灵族独有法术。",
            "兔族在性奴市场上数量巨大，但是价格一般。她们身手敏捷但生命值低下，擅长回避伤害和冲刺攻击。",
            "在人类征服草原后兔族大量繁衍，她们天性温顺，身手敏捷，生命值低下但是擅长回避伤害和射击。",
            "源于深渊的混血，亲和黑暗与火焰。她们对于近战攻击与法术伤害有优势，魔族化后可以拥有生命汲取能力。",
            "魔族中的纯血上位者，掌握强力仪式与魔界生物的召唤。魔族化后可以拥有生命汲取能力。"
        },
        { // TC(繁體)
            "整片大陸上最尚武的種族，四處征服並奴役其他種族。她們在生命值與近戰攻擊上有優勢。",
            "擅長射擊與法術的種族，森林城邦被摧毀後被人類奴役監禁，性奴市場上的價格中等。",
            "精靈中的稀有品種，也是性奴市場上的上乘貨。擁有強大的法術天賦，能施放精靈族特有法術。",
            "兔族在性奴市場數量龐大，但價格普通。身手敏捷但生命值偏低，擅長回避傷害與衝刺攻擊。",
            "在人類征服草原後繁衍甚多。天性溫順、身手敏捷，生命值偏低但擅長回避傷害與射擊。",
            "源於深淵的混血，親和黑暗與火焰。近戰與法術皆有優勢，魔族化後可獲得生命汲取能力。",
            "魔族中的純血上位者，精通強力儀式與魔界召喚。魔族化後可獲得生命汲取能力。"
        },
        { // EN
            "The most warlike people on the continent, conquering and enslaving others. Strong HP and melee power.",
            "Skilled with ranged weapons and magic. After their forest city-states fell, they were enslaved by humans; mid-tier price on the slave market.",
            "A rare strain among elves and a premium on the slave market. Exceptional arcane talent; can cast elf-exclusive spells.",
            "Numerous on the market at an average price. Agile but low HP; excels at evasion and dash attacks.",
            "Multiplied after humans conquered the plains. Gentle by nature, agile, low HP yet good at evasion and shooting.",
            "Hybrids born of the abyss, attuned to darkness and fire. Strong in melee and spells; demonic form grants life-steal.",
            "Pure-blooded elites of demonkind, wielding potent rituals and infernal summons. Demonic form grants life-steal."
        },
        { // KR
            "대륙에서 가장 호전적인 종족. 곳곳을 정복하고 타 종족을 노예화했다. 체력과 근접 공격에 강하다.",
            "사격과 마법에 능한 종족. 숲의 도시국가가 멸망한 뒤 인간에게 노예로 감금되었고, 노예 시장 가격은 중간대.",
            "엘프 중 희귀한 품종으로, 노예 시장의 상급 품. 강력한 마법 재능을 지녀 엘프 고유의 주문을 쓸 수 있다.",
            "토끼족은 시장에 수가 많지만 가격은 보통. 민첩하나 체력이 낮고, 회피와 돌진 공격에 능하다.",
            "인간이 초원을 정복한 뒤 크게 번성. 온순한 기질에 민첩하고, 체력은 낮지만 회피와 사격에 능하다.",
            "심연에서 비롯된 혼혈. 어둠과 불에 친화적이며 근접과 마법이 모두 강하다. 마족화 시 생명 흡수 능력을 얻는다.",
            "마족의 순혈 상위층. 강력한 의식과 마계 소환을 다룬다. 마족화 시 생명 흡수 능력을 얻는다."
        }
    };
    private static readonly string[] LUNA_LOCK = new string[]
  {
        "このキャラクターは外見・衣装・種族などを変更できません。",
        "此角色无法更换外貌、服装、种族等。",
        "此角色無法更換外貌、服裝、種族等。",
        "This character’s appearance, outfit, and race cannot be changed.",
        "이 캐릭터는 외형·의상·종족 등을 변경할 수 없습니다。"
  };
    // 由 hatIndex 反推 7个种族的 UI 索引（你当前的简化规则）
    #endregion



    private string LHair() => LABELS[Lang, 0];
    private string LEyes() => LABELS[Lang, 1];


    private string LRaceName(int idx) => RACE_NAMES[Lang, idx];
    private string LClassName(int idx) => CLASS_NAMES[Lang, idx];

    #endregion


    public void OnConfirmNameInput()
    {
        if (player.currentSaveName== "ルナ"|| player.currentSaveName == "露娜"||player.currentSaveName == "Luna" || player.currentSaveName == "루나") { return; }


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

    [Header("告知RoomGenerator产生队友")]
    public RoomGenerator _RoomGenerator;

    public void OpenCloseMenu()
    {
        if (CurrentMode == 0)
        {
            //ToCGPage();//打开CG界面
            Invoke("ToCGPage", 0.1f);
        }

        if (CurrentMode == 1)
        {
            if (SaveManager.CountSaves() > 0) // 没有任何存档无法开始
            {

                if (!isPause)
                {
                    // MainCamera.SetInteger("View", 0);
                    // Common_All.SetActive(false);
                    // ShowSaveCavansAnim.gameObject.SetActive(true);
                    // ShowSaveCavansAnim.SetBool("Track", true);
                    //
                    //
                    //player.isInputBlocked = true;//切断玩家的方向攻击等输入
                    //
                    // RefreshSaveSlots();//只有在打开存档菜单时更新

                    PauseGame();
                }
                else
                {



                    MainCamera.SetInteger("View", 2);
                    Common_All.SetActive(true);
                    ShowSaveCavansAnim.gameObject.SetActive(false);
                    ShowSaveCavansAnim.SetBool("Track", false);

                    player.isInputBlocked = false;//恢复玩家的方向攻击等输入

                    player.currentSaveName = currentSelectedSlot.Data.characterName;//开始游戏时，将这个存档名称带入Player


                    _RoomGenerator.SetAllFriends();
                }

                //isPause = !isPause;

                isPause = false;
            }
            else
            {
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Reba);
            }


        }



    }//从存档界面进入游戏界面(如果没有存档无法这么做)，再从游戏界面进入存档界面

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
        ChapterUnclockStart();//检测Chapter解锁


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

    #region 当前选中的是不是露娜
    private static readonly string[] LunaNames = { "ルナ", "露娜", "Luna", "루나" };
    private bool IsLuna(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        name = name.Trim();
        foreach (var n in LunaNames)
            if (string.Equals(name, n, StringComparison.OrdinalIgnoreCase))
                return true;
        return false;
    }
    #endregion

    public void CreateNewSave()
    {
        player._CreateNewSkin();

        #region 种族选项需要根据耳朵来设置
        raceOptionIndex = RaceOptionFromHat_Simple(player.YYY_hatIndex);
        if (IsLuna(player.currentSaveName)) { IntroduceOfRace.text = LUNA_LOCK[Lang]; } else { ApplyRaceSelectionSimple(); }//预先设置提示词
        UpdateUI();
        #endregion

        RefreshSaveSlots();

        //显示捏人界面，隐藏存档界面
        SaveCavans.SetActive(false);
        CreateCavans.SetActive(true);

        UpdateUI();//更新捏人界面UI

        CurrentChooseList = 1;//进入捏人界面


        nameInputField.ActivateInputField(); // ✅ 激活输入框并进入编辑

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
    [Header("删除全存档")]
    public GameObject MakeSureDeleteCurrentSave_All;

    public void TryDelete_All()
    {
        MakeSureDeleteCurrentSave_All.SetActive(true);
        CurrentChooseList = -2;//弹出确认删除存档框
    }

    public void CancelDelete_All() 
    {
        MakeSureDeleteCurrentSave_All.SetActive(false);
        CurrentChooseList = 3;//返回设置界面
    }

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
        // 先把当前选中改成这次点的按钮
        int idx = cgButtons.FindIndex(b => b.unlocked && b.cgKey == CGName);
        if (idx >= 0)
        {
            CGcurrentIndex = idx;
            UpdateHighlight_CG();
        }

        player.ForCGRandomEnemySkin();
        player.frameEvents.audioS.Stop();


        if (!string.IsNullOrEmpty(CGName))
        {
            player.anim.Play("CG/" + CGName);
        }
    }

    #endregion

    /// <summary>
    /// Chapter界面选中
    /// </summary>
    #region
    public List<CGOptionUI> chapterButtons = new List<CGOptionUI>();
    int ChapterCurrentIndex = 0;

    void ChapterUnclockStart()
    {
        foreach (var btn in chapterButtons)
        {
            btn.SetUnlockedFromPrefs();
        }

        // 查找第一个已解锁的
        for (int i = 0; i < chapterButtons.Count; i++)
        {
            if (chapterButtons[i].unlocked)
            {
                currentIndex = i;
                break;
            }
        }
        UpdateHighlight();
    }//开始检测CG解锁数
    void MoveSelection_2(int direction)
    {
        // 取消旧高亮
        chapterButtons[ChapterCurrentIndex].SetHighlight(false);

        // 循环查找下一个已解锁的项
        int max = chapterButtons.Count;
        for (int i = 1; i < max; i++)
        {
            int newIndex = (ChapterCurrentIndex + direction * i + max) % max;
            if (chapterButtons[newIndex].unlocked)
            {
                ChapterCurrentIndex = newIndex;
                break;
            }
        }

        // 更新高亮
        UpdateHighlight_Chapter();
    }//切换当前选中

    void UpdateHighlight_Chapter()
    {
        for (int i = 0; i < chapterButtons.Count; i++)
        {
            chapterButtons[i].SetHighlight(i == ChapterCurrentIndex);
        }
    }

    public void PlayAVG(string ChapterName)
    {
        // 先把当前选中改成这次点的按钮
        int idx = chapterButtons.FindIndex(b => b.unlocked && b.cgKey == ChapterName);
        if (idx >= 0)
        {
            ChapterCurrentIndex = idx;
            UpdateHighlight_Chapter();
        }


        switch (ChapterName) 
        {
            case "Chapter_01":

                break;
            case "Chapter_02":

                break;
            case "Chapter_03":

                break;
            case "Chapter_04":

                break;
            case "Chapter_05":

                break;
            case "Chapter_06":

                break;
            case "Chapter_07":

                break;
            case "Chapter_08":

                break;
            case "Chapter_09":

                break;
            case "Chapter_10":

                break;
            case "Chapter_11":

                break;
            case "Chapter_12":

                break;
            case "Chapter_13":

                break;
        }


        
        
        
        
    }
    #endregion



    /// <summary>
    /// 菜单层面多端输入
    /// </summary>
    #region
    [SerializeField] private InputActionAsset inputActions;
    private InputAction moveAction;//十字键
    private InputAction confirmAction;//J键
    private InputAction cancelAction;//K键
    private InputAction createAction;//Space键
    private InputAction deleteAction;//E键
    private InputAction pauseAction;//Esc键

    private void OnEnable()
    {
        moveAction = inputActions.FindAction("Move");
        confirmAction = inputActions.FindAction("Attack"); 
        cancelAction = inputActions.FindAction("Dodge");
        createAction = inputActions.FindAction("Run");
        deleteAction = inputActions.FindAction("Interact");  
        pauseAction = inputActions.FindAction("Pause");

        moveAction.performed += OnMove;
        confirmAction.started += OnConfirm;
        cancelAction.started += OnCancel;
        createAction.started += OnCreate;
        deleteAction.started += OnDelete;
        pauseAction.started += OnPause;

        moveAction.Enable();
        confirmAction.Enable();
        cancelAction.Enable();
        createAction.Enable();
        deleteAction.Enable();
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        confirmAction.started -= OnConfirm;
        cancelAction.started -= OnCancel;
        createAction.started -= OnCreate;
        deleteAction.started -= OnDelete;
        pauseAction.started -= OnPause;

        moveAction.Disable();
        confirmAction.Disable();
        cancelAction.Disable();
        createAction.Disable();
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
                    HomePagecurrentIndex = Mathf.Clamp(HomePagecurrentIndex - 1, 0, 9);
                    UpdateHomePage_Highlight();


                }
                else if (dir.y < -0.5f)
                {
                    HomePagecurrentIndex = Mathf.Clamp(HomePagecurrentIndex + 1, 0, 9);
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

                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    MoveSelection(9);


                }
                else if (dir.x < -0.5f)
                {
                    MoveSelection(-9);

                }
            }

            //游戏模式界面
            if (CurrentChooseList == 7)
            {
                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    ModePagecurrentIndex = Mathf.Clamp(ModePagecurrentIndex +1, 0, 2);
                    UpdateModePage_Highlight();


                }
                else if (dir.x < -0.5f)
                {
                    ModePagecurrentIndex = Mathf.Clamp(ModePagecurrentIndex - 1, 0, 2);
                    UpdateModePage_Highlight();


                }
            }

            //Chapter界面
            if (CurrentChooseList == 8)
            {
                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    MoveSelection_2(-1);

                }
                else if (dir.y < -0.5f)
                {

                    MoveSelection_2(1);
                }

                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    MoveSelection_2(6);


                }
                else if (dir.x < -0.5f)
                {
                    MoveSelection_2(-6);

                }
            }

            AudioManager.instance.AudioPlay(AudioManager.instance.Attack_pai1);
        }




    }

    public GameObject HideGameObjectWhenChangeName;
    public GameObject Prompt_Enter;
    public void OnChangeName()
    {
        isInputing = true;
        CreatNewcurrentIndex = 0;
        UpdateHighlight();

        HideGameObjectWhenChangeName.SetActive(false);
        Prompt_Enter.SetActive(true);
    }//打字的时候锁住上下移动
    public void OnChangeNameOver()
    {
        isInputing = false;

        HideGameObjectWhenChangeName.SetActive(true);
        Prompt_Enter.SetActive(false);

    }//打字的时候锁住上下移动

    private void OnConfirm(InputAction.CallbackContext ctx)
    {
        if (player.isInputBlocked)
        {
            // 可选：进入下一级菜单、确认开始游戏等

            //只要暂停菜单显示，攻击键按下就是触发这里
            if (PauseMenu.activeInHierarchy) 
            {
                ReLoadScene();

                return;
            }


            //确认删除全部存档界面
            if (CurrentChooseList == -2)
            {
                ReStart_DeleteAll();//删除存档重刷场景
            }

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
                        Invoke("ToModePage", 0.1f);//进入游戏模式界面
                        break;
                    case 1:
                        ToSavePageButton(0);//开始游戏进入存档界面(CG)
                        break;
                    case 2:
                        Invoke("ToSettingPage", 0.1f);//进入设置界面
                        break;
                    case 3:
                        ExitGame();
                        break;
                    case 4:
                        OpenURL_Ci_en();
                        break;
                    case 5:
                        OpenURL_Patreon();
                        break;
                    case 6:
                        OpenURL_Steam();
                        break;
                    case 7:
                        OpenURL_Discord();
                        break;
                    case 8:
                        OpenURL_YYY();
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
                    //CreateNewSave();//新建角色

                    OpenCloseMenu();
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
                    OnChangeName();//这个时候就需要遮住了
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
                        Invoke("ToLanguagePage", 0.1f);//进入设置界面
                        break;
                    case 3:
                        Invoke("TryDelete_All", 0.1f);//进入确认删除全部存档界面
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

            //游戏模式选择界面
            if (CurrentChooseList == 7)
            {
                switch (ModePagecurrentIndex)
                {
                    case 0:
                        ToStoryStage();       
                        break;
                    case 1:
                        ToOneToOneStage();
                        break;
                    case 2:
                        ToDungeonStage();
                        break;
                }

                AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
            }

            //Chapter界面
            if (CurrentChooseList == 8)
            {
                chapterButtons[ChapterCurrentIndex].PlayAVG();
            }

            //AVG章节界面
            if (CurrentChooseList == 9)
            {
                Invoke("Delay_AVG_ShowText",0.1f);
            }
        }


    }//键盘J    xbox手柄B      ps手柄O

    private void OnCancel(InputAction.CallbackContext ctx)
    {
        if (player.isInputBlocked)
        {
            // 可选：退出菜单、返回上一级等

            //只要暂停菜单显示，闪避键按下就是触发这里
            if (PauseMenu.activeInHierarchy)
            {
                ContinueGame();

                return;
            }



            //确认删除全部存档界面
            if (CurrentChooseList == -2)
            {
                Invoke("CancelDelete_All", 0.1f);
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            //确认删除界面
            if (CurrentChooseList == -1)
            {
                Invoke("CancelDelete", 0.1f);
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            //存档界面//设置界面//Mode游戏模式界面
            if (CurrentChooseList == 2 || CurrentChooseList == 3 || CurrentChooseList == 7)
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

            //Chapter章节界面
            if (CurrentChooseList == 8)
            {
                ToModePage();
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            //AVG章节界面
            if (CurrentChooseList == 9)
            {
                dialogSystem.ChangeStory();    
            }

        }

    }//键盘K      xbox手柄A       ps手柄X

    private void OnCreate(InputAction.CallbackContext ctx)
    {
        if (player.isInputBlocked)
        {
            // 可选：删除存档
            if (CurrentChooseList == 2)
            {
                CreateNewSave();//新建角色
            }
            AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
        }

    }//键盘Space    xbox手柄左肩键       ps手柄左肩键

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

    }//键盘E    xbox手柄X       ps手柄□
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
    public void ExitGame()
    {
        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 3;
        UpdateHomePage_Highlight();

        Debug.Log("Exiting game...");

        Application.Quit();
    }


    public void OpenURL_Ci_en()
    {
        Application.OpenURL("https://ci-en.dlsite.com/creator/16247");


        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 4;
        UpdateHomePage_Highlight();
    }

    public void OpenURL_Patreon()
    {
        Application.OpenURL("https://www.patreon.com/c/FTGirl");

        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 5;
        UpdateHomePage_Highlight();
    }
    public void OpenURL_Steam()
    {
        Application.OpenURL("https://store.steampowered.com/search/?developer=FT%20Girl%20Studio");

        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 6;
        UpdateHomePage_Highlight();
    }

    public void OpenURL_Discord()
    {
        Application.OpenURL("https://discord.gg/xZ3uafRm");

        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 7;
        UpdateHomePage_Highlight();
    }

  

    public void OpenURL_YYY()
    {
        Application.OpenURL("https://x.com/Detective_ye");

        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 8;
        UpdateHomePage_Highlight();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    void Awake()
    {
        instance = this;
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

        
    }

    public void ReLoadScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Loading.SetActive(true);
    }

    #endregion

    /// <summary>
    /// 捏人菜单
    /// </summary>
    #region
    [Header("各类菜单")]
    public bool isPause;
    public Animator MainCamera;//控制摄像机拉近远离
    public Animator ShowSaveCavansAnim;//黑幕显示背景

    public GameObject SaveCavans,CreateCavans;//存档界面,捏人界面

    [Header("捏人界面UI")]
    public Text nameLabel;
    public Text hairLabel;
    public Text eyesLabel;
    public Text raceLabel;
    public Text classLabel;



    public void OnHairLeft() { ChangeSkin(ref player.YYY_headIndex, 1, 13, -1); }
    public void OnHairRight() { ChangeSkin(ref player.YYY_headIndex, 1, 13, +1); }

    public void OnEyesLeft() { ChangeSkin(ref player.YYY_eyesIndex, 1, 13, -1); }
    public void OnEyesRight() { ChangeSkin(ref player.YYY_eyesIndex, 1, 13, +1); }

    public void OnRaceLeft() { ChangeSkin(ref player.YYY_hatIndex, 1, 4, -1); }
    public void OnRaceRight() { ChangeSkin(ref player.YYY_hatIndex, 1, 4, +1); }

    public void OnClassLeft() { ChangeSkin(ref player.YYY_bodyIndex, 10, 12, -1); }
    public void OnClassRight() { ChangeSkin(ref player.YYY_bodyIndex, 10, 12, +1); }
    void ChangeSkin(ref int index, int min, int max, int delta)
    {
        index += delta;
        if (index < min) index = max;
        if (index > max) index = min;

        player.SetSkin();         // 更新角色外观
        UpdateUI();               // 更新文字
        player.SaveCurrent();     // 存一份当前皮肤到缓存/存档

        RefreshSaveSlots();//刷新存档界面
    }

    void UpdateUI()
    {
        nameLabel.text = player.currentSaveName;

        hairLabel.text = $"Hair_{player.YYY_headIndex}";
        eyesLabel.text = $"Eyes_{player.YYY_eyesIndex}";
        raceLabel.text = $"Race_{player.YYY_hatIndex}";
        classLabel.text = $"Class_{player.YYY_bodyIndex}";
    }//捏人界面UI显示

    public void OnConfirm()
    {
        // 可能额外做点逻辑（比如保存到最终存档槽）
        Debug.Log("确认创建角色: " + player.YYY_headIndex);

        //抹去当前名称，下次捏人再度选中名称
        player.currentSaveName = null;

        //显示存档界面，隐藏捏人界面
        CreateCavans.SetActive(false);
        SaveCavans.SetActive(true);
    }


    public void OpenCloseMenu() 
    {
        if (!isPause)
        {
            MainCamera.SetBool("Track", true);
            Common_All.SetActive(false);
            ShowSaveCavansAnim.gameObject.SetActive(true);
            ShowSaveCavansAnim.SetBool("Track", true);
        }
        else
        {
            MainCamera.SetBool("Track", false);
            Common_All.SetActive(true);
            ShowSaveCavansAnim.gameObject.SetActive(false);
            ShowSaveCavansAnim.SetBool("Track",false);
        }

        isPause = !isPause;
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
        //找玩家
        _Player = GameObject.FindGameObjectWithTag("Player");
        player = _Player.GetComponent<Player>();



        string folder = Application.persistentDataPath + "/Saves/";
        if (!Directory.Exists(folder)) return;

        foreach (string file in Directory.GetFiles(folder, "save_*.json"))
        {
            string json = File.ReadAllText(file);
            PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);

            GameObject slot = Instantiate(saveSlotPrefab, saveSlotParent);
            slot.GetComponent<SaveSlotUI>().SetInfo(data, skinParts);  // ✅ 新增头像预览

        }
    }//读取，显示存档

    public void CreateNewSave() 
    {
        player._CreateNewSkin();
        RefreshSaveSlots();

        //显示捏人界面，隐藏存档界面
        SaveCavans.SetActive(false);
        CreateCavans.SetActive(true);

        UpdateUI();//更新捏人界面UI

    }//点击【＋】就会随机存档

    public void RefreshSaveSlots()
    {
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



            SetCurrentSlot(slot.GetComponent<SaveSlotUI>());// 自动选中
        }
    }//刷新当前存档UI

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
    #endregion
}

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
    [Header("捏人菜单")]
    public bool isPause;
    public Animator MainCamera;
    public GameObject ShowSaveCavans;
    public Animator ShowSaveCavansAnim;
    public void OpenCloseMenu() 
    {
        if (!isPause)
        {
            MainCamera.SetBool("Track", true);
            Common_All.SetActive(false);
            ShowSaveCavans.SetActive(true);
            ShowSaveCavansAnim.SetBool("Track", true);
        }
        else
        {
            MainCamera.SetBool("Track", false);
            Common_All.SetActive(true);
            //ShowSaveCavans.SetActive(false);
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

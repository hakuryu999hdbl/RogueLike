using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    void Awake()
    {
        instance = this;
    }

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
    private bool flashOn = false;

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

        // 基础颜色渐变（黄色 → 红色）
        // 黄色(1,1,0) 到 红色(1,0,0)
        float red = 1.0f;
        float green = Mathf.Lerp(1.0f, 0.0f, fillPercent); // 越高越绿越少
        float blue = 0.0f;

        Color baseColor = new Color(red, green, blue, 1.0f);

        // 闪烁逻辑：暴击值超过 90% 时开始闪烁
        if (fillPercent > 0.9f)
        {
            flashTimer += Time.deltaTime * 4f; // 控制闪烁速度
            float alpha = Mathf.Abs(Mathf.Sin(flashTimer)); // 0~1 间震荡

            // 混合颜色：红色和白色之间闪烁
            Color flashColor = Color.Lerp(baseColor, Color.white, alpha * 0.5f); // 可调
            CriticalBar.color = flashColor;
        }
        else
        {
            CriticalBar.color = baseColor;
        }
    }
    #endregion
}

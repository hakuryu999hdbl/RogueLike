using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制伤害效果的生成，附在Canvas上
/// </summary>
public class HudText : MonoBehaviour
{
    /// <summary>
    /// 文字预制体
    /// </summary>
    public GameObject hudText;
    public bool isFriend = false;//玩家和队友受到伤害为{红-1}，敌人受到伤害为{白1}
    /// <summary>
    /// 生成伤害文字
    /// </summary>
    public void HUD(int damage)
    {
        GameObject hud = Instantiate(hudText, transform) as GameObject;
        hud.GetComponent<Text>().text = damage.ToString();
        if (damage <= 0)
        {
            if (!isFriend)
            {

                hud.GetComponent<HUD>().color = 0;//敌人受伤为白
            }
            else
            {
                hud.GetComponent<HUD>().color = 1;//玩家和队友受伤为红
            }
        }
        else
        {
            hud.GetComponent<HUD>().color = 2;//双方回血都是绿
        }

    }

}
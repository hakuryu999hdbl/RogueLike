using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowData : MonoBehaviour
{
    public static string nextScene = null;

    //【Menu】进入菜单场景
    //【CG】进入CG处刑场景
    //【CG_AVG_01】进入CG结局剧情1 头枷轮奸
    //【CG_AVG_02】进入CG结局剧情2 泄欲车
    //【CG_AVG_03】进入CG结局剧情3 性奴拍卖会
    //【CG_AVG_04】进入CG结局剧情4 肉铠
    //【CG_AVG_05】进入CG结局剧情5 王女性玩具
    //【CG_AVG_06】进入CG结局剧情6 魔界试验体
    //【CG_AVG_07】进入CG结局剧情7 肉圣物
    //【CG_AVG_08】进入CG结局剧情8 榨精鞭刑



    //【Story_01】进入剧情关卡1
    //【Story_02】进入剧情关卡2
    //【Story_03】进入剧情关卡3
    //【Story_04】进入剧情关卡4
    //【Story_05】进入剧情关卡5
    //【Story_06】进入剧情关卡6
    //【Story_07】进入剧情关卡7
    //【Story_08】进入剧情关卡8
    //【Story_09】进入剧情关卡9
    //【Story_10】进入剧情关卡10
    //【Story_11】进入剧情关卡11
    //【Story_12】进入剧情关卡12


    //【AVG_02】进入剧情2
    //【AVG_03】进入剧情3
    //【AVG_04】进入剧情4
    //【AVG_05】进入剧情5
    //【AVG_06】进入剧情6
    //【AVG_07】进入剧情7
    //【AVG_08】进入剧情8
    //【AVG_09】进入剧情9
    //【AVG_10】进入剧情10
    //【AVG_11】进入剧情11
    //【AVG_12】进入剧情12




    //【Arena】进入角斗场模式  
    //【Dungeon】进入地下城模式  

    public static bool ForceKeyboardMode = false;//切断了多端输入中的奇怪输入，然后强制键盘输入

    public static bool BulletCanThroughtWall = false;//子弹和法术是否可以穿过墙壁

    public static　int RoomLevel;//房间刷敌数和难度随着越后越来越难

    public static int Sword_Buff;
    public static int Pistol_Buff;
    public static int Staff_Buff;


    public static bool hasShownCoverThisRun = false;//开始封面是否已经显示过了


}
public static class SlavePricing
{
    // 最低/最高保护，可按需改
    const int MIN_PRICE = 100;
    const int MAX_PRICE = 99999;

    public static int CalcPrice(PlayerSaveData d)
    {
        if (d == null) return 0;

        // 温和权重：等级、血量、三攻、武器、衣服、丝袜
        float score = 0f;
        score += d.level * 120f;                                      // 等级
        score += d.maxHP * 0.05f;                                     // 生命
        score += (d.meleeDamage + d.shootDamage + d.spellDamage) * 1.2f; // 综合攻击
        score += d.weaponAtk * 1.8f;                                  // 武器
        score += d.armorDef * 0.9f;                                   // 衣服
        score += d.stockingDef * 0.6f;                                // 丝袜

        int gold = Mathf.RoundToInt(score);
        return Mathf.Clamp(gold, MIN_PRICE, MAX_PRICE);
    }
}

public static class SellTexts
{
    public static string Build(int lang, string name, int price)
    {
        switch (lang)
        {
            case 0: return $"この性奴「{name}」を売却しますか？ 獲得：{price} 金貨";
            case 1: return $"是否贩卖这个性奴「{name}」？你会获得 {price} 金钱";
            case 2: return $"是否販賣這個性奴「{name}」？你會獲得 {price} 金錢";
            case 3: return $"Sell this slave \"{name}\"? You will get {price} gold.";
            case 4: return $"이 노예 \"{name}\"을(를) 판매하시겠습니까? {price} 골드를 얻습니다.";
            default: return $"是否贩卖这个性奴「{name}」？你会获得 {price} 金钱";
        }
    }
}


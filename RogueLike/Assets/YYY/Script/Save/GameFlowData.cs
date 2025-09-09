using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowData : MonoBehaviour
{
    public static string nextScene = null;     //【CG】进入CG处刑场景   【Menu】进入菜单场景    【AVG】进入AVG场景
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
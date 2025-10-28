using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ShopItemData
{
    public enum ItemType
    {
        Sword,
        Pistol,
        Staff,
        Clothes,
        Stockings,
        Slave,
        Potion,     // 以后扩展
    }

    public ItemType type;
    public int index;       // 物品编号（1~10 / 101~110 / 201~210）
    public int value;       // 数值加成（攻击力 / 防御力 / 回复量）
    public int price;       // 价格
    public Sprite icon;     // 图标
    public string displayName; // 名称
    public string description; // 说明文字
}

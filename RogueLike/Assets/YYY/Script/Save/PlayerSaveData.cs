using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public string characterName;

    // 外观
    public int eyesIndex;
    public int headIndex;
    public int bodyIndex;
    public int legsIndex;
    public int hatIndex;
    public int weaponIndex;



    // 数值
    public int level;
    public int exp;
    public int maxHP;

    public int meleeDamage;
    public int shootDamage;
    public int spellDamage;

    public int professionIndex;  // 0=战士，1=射手，2=法师

    public int weaponAtk;
    public int armorDef;
    public int stockingDef;

    public string lastSavedTime; // 存档时间记录
}
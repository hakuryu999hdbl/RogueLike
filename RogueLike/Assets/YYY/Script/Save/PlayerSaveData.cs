using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public string characterName;


    public int eyesIndex;
    public int headIndex;
    public int bodyIndex;
    public int legsIndex;
    public int hatIndex;
    public int weaponIndex;

    public string lastSavedTime; // 存档时间记录
}
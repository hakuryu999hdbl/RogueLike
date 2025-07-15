using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Linq;

public static class SaveManager
{
    private static string saveFolder = Application.persistentDataPath + "/Saves/";

    public static void Save(PlayerSaveData data)
    {
        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);

        data.lastSavedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string json = JsonUtility.ToJson(data, true);
        string path = saveFolder + "save_" + data.characterName + ".json";
        File.WriteAllText(path, json);
    }//用于储存当前这身皮肤名称数值存档(玩家被SaveSlotUI赋予皮肤数值后储存)

    public static PlayerSaveData Load(string characterName)
    {
        string path = saveFolder + "save_" + characterName + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<PlayerSaveData>(json);
        }
        else
        {
            Debug.LogWarning("存档不存在：" + path);
            return null;
        }
    }//读取指定名称的存档(好像没有被用上)

    public static void DeleteSave(string characterName)
    {
        string path = Application.persistentDataPath + "/Saves/save_" + characterName + ".json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("已删除存档：" + path);
        }
    }//删除指定名称的存档(各个存档SaveSlotUI来决定)

    public static List<string> GetAllSaveNames()
    {
        string dir = Application.persistentDataPath;
        string[] files = Directory.GetFiles(dir, "*.json");
        return files.Select(Path.GetFileNameWithoutExtension).ToList();
    }// 取名时获取已有存档名
}

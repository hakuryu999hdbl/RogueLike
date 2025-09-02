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
    }//读取指定名称的存档(玩家切换单个皮肤时)

    public static void DeleteSave(string characterName)
    {
        string path = Application.persistentDataPath + "/Saves/save_" + characterName + ".json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("已删除存档：" + path);
        }
    }//删除指定名称的存档(各个存档SaveSlotUI来决定)

    public static void DeleteAllSaves()
    {
        string saveFolder = Application.persistentDataPath + "/Saves/";

        if (Directory.Exists(saveFolder))
        {
            Directory.Delete(saveFolder, true); // true 表示递归删除整个文件夹
            Debug.Log("所有存档已删除！");
        }
        else
        {
            Debug.Log("未找到存档文件夹：" + saveFolder);
        }
    }//删除所有存档




    public static bool HasSave(string characterName)
    {
        string path = Application.persistentDataPath + "/Saves/save_" + characterName + ".json";
        return File.Exists(path);
    }//确认有没有这个存档(捏人界面改名字时)

    public static int CountSaves()
    {
        if (!Directory.Exists(saveFolder)) return 0;
        return Directory.GetFiles(saveFolder, "save_*.json").Length;
    }//存档数量为



    /// <summary>
    /// 正确获取所有存档的【角色名】（去掉前缀 "save_" 和扩展名）
    /// </summary>
    public static List<string> GetAllSaveNames()
    {

        var result = new List<string>();
        if (!Directory.Exists(saveFolder)) return result;

        var files = Directory.GetFiles(saveFolder, "save_*.json");
        foreach (var p in files)
        {
            string nameNoExt = Path.GetFileNameWithoutExtension(p); // e.g. "save_Luna"
            if (nameNoExt.StartsWith("save_"))
                result.Add(nameNoExt.Substring(5)); // -> "Luna"
        }
        return result;

    }

    /// 返回不与现有存档冲突的名字：baseName, baseName_2, baseName_3...
    public static string GetNextAvailableName(string baseName)
    {
        var existing = new HashSet<string>(GetAllSaveNames(), StringComparer.OrdinalIgnoreCase);
        if (!existing.Contains(baseName)) return baseName;

        int i = 2;
        while (true)
        {
            string candidate = $"{baseName}_{i}";
            if (!existing.Contains(candidate)) return candidate;
            i++;
        }
    }



    /// <summary>
    /// 读取除当前角色名以外的所有存档数据（坏档会自动跳过）
    /// </summary>
    public static List<PlayerSaveData> LoadAllSavesExcept(string currentCharacterName)
    {
        var result = new List<PlayerSaveData>();
        if (!Directory.Exists(saveFolder)) return result;

        string[] files = Directory.GetFiles(saveFolder, "save_*.json");
        foreach (var path in files)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(path); // save_xxx
                string name = fileName.StartsWith("save_") ? fileName.Substring(5) : fileName;

                if (!string.Equals(name, currentCharacterName, StringComparison.OrdinalIgnoreCase))
                {
                    string json = File.ReadAllText(path);
                    var data = JsonUtility.FromJson<PlayerSaveData>(json);
                    if (data != null) result.Add(data);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"跳过坏档：{path}，原因：{e.Message}");
            }
        }
        return result;
    }
}

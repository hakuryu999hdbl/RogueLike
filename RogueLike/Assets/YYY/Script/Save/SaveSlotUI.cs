using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    [Header("存档数值的显示")]
    public Text nameText;
    public Text timeText;

    public Image Hat;
    public Image Hair;
    public Image Eyes;
    public Image Mouse;
    public Image Head;
    public Image Clothes;
    public Image Longhair;
    public Image Ponytail;

    [Header("存档数值本身")]
    PlayerSaveData Data;

    [Header("高亮用UI")]
    public GameObject highlightFrame;

    [Header("寻找玩家")]
    public GameObject _Player;//玩家
    public Player player;

    public void Start()
    {
        //找玩家
        _Player = GameObject.FindGameObjectWithTag("Player");
        player = _Player.GetComponent<Player>();

    }//找到玩家，可以套皮肤

    public void SetInfo(PlayerSaveData data, SkinPartsDatabase database)
    {
        Data = data;

        nameText.text = Data.characterName;
        timeText.text = Data.lastSavedTime;



        Hat.sprite = database.HatSprites[data.hatIndex-1];
        Hair.sprite = database.HairSprites[data.headIndex - 1];
        Eyes.sprite = database.EyesSprites[data.eyesIndex - 1];
        Mouse.sprite = database.MouseSprites[data.headIndex - 1];
        Head.sprite = database.HeadSprites[data.headIndex - 1];
        Clothes.sprite = database.ClothesSprites[data.bodyIndex - 1]; // Body决定Clothes
        Longhair.sprite = database.LonghairSprites[data.headIndex - 1];
        Ponytail.sprite = database.PonytailSprites[data.headIndex - 1];










        highlightFrame.SetActive(false); // 初始隐藏
    }//导入皮肤






    public void SetHighlight(bool on)
    {
        highlightFrame.SetActive(on);
    }//高亮显示

    public void Choose() 
    {
        player.ApplySaveData(Data);
        UIManager.instance.SetCurrentSlot(this); // 通知UIManager进行高亮更新

    }//选择这个档的皮肤

    public void Delete()
    {
        SaveManager.DeleteSave(Data.characterName);
        player.ClearSkin(); // ✅ 清除当前皮肤

        Destroy(this.gameObject);
        UIManager.instance.RefreshSaveSlots(); // ✅ 删除后刷新
    }//删除这个档的皮肤


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

using System;
using System.IO;



//using Steamworks;/////////////////////////////////////////////////【电脑控制/Steam】/////////////////////////////////////////////////

public class UIManager : MonoBehaviour
{
    [Header("关卡信息")]
    public GameObject StageInformation;
    public Text _Stage_Information;

    public static UIManager instance { get; private set; }
    void Awake()
    {
        instance = this;

        Debug.Log("目前的NextScene" + GameFlowData.nextScene);

        //Debug.Log("目前存档里的语言" + PlayerPrefs.GetInt("language"));//0 日语 1中文 2繁中 3英语 4韩语


        Debug.Log("目前存档里的BGM音量" + PlayerPrefs.GetFloat("BGMVolume"));
        Debug.Log("目前存档里的SE音量" + PlayerPrefs.GetFloat("SEVolume"));

        Debug.Log("目前存档里的屏幕设置" + PlayerPrefs.GetFloat("ScreenMode"));//0全屏  1窗口  2带边窗口

        //Debug.Log("目前存档里的钱币" + PlayerPrefs.GetInt("Money"));




        ChangeMoney(0, false);//更新钱

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_OnanismFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_OnanismSide_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_InsultSide_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_GagSide_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_FistingFront_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_RapeFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_RapeSide_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_AssaultFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_AssaultSide_1"));//0未解锁  1解锁





        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleBagFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleBugSide_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleFront_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleHermitCrabFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleHermitCrabSide_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_FleshArmor_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleMonsterFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_MonsterSide_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleWallFront_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_TentacleWallSide_1"));//0未解锁  1解锁





        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_Hogtie_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_HangSide_4"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_HangSide_1"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_HangFront_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_HangDown_4"));//0未解锁  1解锁
        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_HangDown_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_FeraSide_1"));//0未解锁  1解锁

        //Debug.Log("目前的CG解锁状态【CG】" + PlayerPrefs.GetInt("CG_Pillory_Side_1"));//0未解锁  1解锁


        //Debug.Log("目前的CG结局解锁状态【CG_End】" + PlayerPrefs.GetInt("CG_AVG_01"));//0未解锁  1解锁
        //Debug.Log("目前的CG结局解锁状态【CG_End】" + PlayerPrefs.GetInt("CG_AVG_02"));//0未解锁  1解锁
        //Debug.Log("目前的CG结局解锁状态【CG_End】" + PlayerPrefs.GetInt("CG_AVG_03"));//0未解锁  1解锁


        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_01"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_02"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_03"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_04"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_05"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_06"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_07"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_08"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_09"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_10"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_11"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_12"));//0未解锁  1解锁
        //Debug.Log("目前的章节解锁状态【Chapter】" + PlayerPrefs.GetInt("Chapter_13"));//0未解锁  1解锁

        //Debug.Log("目前的游戏模式解锁状态【Arena】" + PlayerPrefs.GetInt("Chapter_Arena"));//0未解锁  1解锁
        //Debug.Log("目前的游戏模式解锁状态【Dungeon】" + PlayerPrefs.GetInt("Chapter_Dungeon"));//0未解锁  1解锁


        //Debug.Log("目前角斗场最高波次" + PlayerPrefs.GetInt("Arena_Wave"));
        //Debug.Log("目前地下城连胜次数" + PlayerPrefs.GetInt("Dungeon_Streak"));
        //Debug.Log("目前地下城最高连胜记录" + PlayerPrefs.GetInt("Dungeon_BestStreak"));

        //Debug.Log("目前高等精灵是否解锁" + PlayerPrefs.GetInt("HighElf"));//0未解锁  1解锁
        //Debug.Log("目前高等魔族是否解锁" + PlayerPrefs.GetInt("HighDemon"));//0未解锁  1解锁

        //地下城和角斗场模式解锁
        if (PlayerPrefs.GetInt("Chapter_Arena") == 0) { LockOfArena.SetActive(true); }
        if (PlayerPrefs.GetInt("Chapter_Dungeon") == 0) { LockOfDungeon.SetActive(true); }

        PlayerPrefs.SetInt("CG_OnanismFront_1", 1);//目前保持第一个CG永远在
        //PlayerPrefs.SetInt("CG_OnanismSide_1", 1);
        //
        //PlayerPrefs.SetInt("CG_InsultSide_1", 1);
        //PlayerPrefs.SetInt("CG_GagSide_1", 1);
        //PlayerPrefs.SetInt("CG_FistingFront_1", 1);
        //
        //PlayerPrefs.SetInt("CG_RapeFront_1", 1);
        //PlayerPrefs.SetInt("CG_RapeSide_1", 1);
        //PlayerPrefs.SetInt("CG_AssaultFront_1", 1);
        //PlayerPrefs.SetInt("CG_AssaultSide_1", 1);
        //
        //PlayerPrefs.SetInt("CG_TentacleBagFront_1", 1);
        //PlayerPrefs.SetInt("CG_TentacleBugSide_1", 1);
        //
        //PlayerPrefs.SetInt("CG_TentacleFront_1", 1);
        //
        //PlayerPrefs.SetInt("CG_TentacleHermitCrabFront_1", 1);
        //PlayerPrefs.SetInt("CG_TentacleHermitCrabSide_1", 1);
        //
        //PlayerPrefs.SetInt("CG_FleshArmor_1", 1);
        //
        //PlayerPrefs.SetInt("CG_TentacleMonsterFront_1", 1);
        //PlayerPrefs.SetInt("CG_MonsterSide_1", 1);
        //
        //PlayerPrefs.SetInt("CG_TentacleWallFront_1", 1);
        //PlayerPrefs.SetInt("CG_TentacleWallSide_1", 1);
        //
        //
        //
        //PlayerPrefs.SetInt("CG_Hogtie_1", 1);
        //
        //PlayerPrefs.SetInt("CG_HangSide_4", 1);
        //PlayerPrefs.SetInt("CG_HangSide_1", 1);
        //PlayerPrefs.SetInt("CG_HangFront_1", 1);
        //
        //PlayerPrefs.SetInt("CG_HangDown_4", 1);
        //PlayerPrefs.SetInt("CG_HangDown_1", 1);
        //
        //PlayerPrefs.SetInt("CG_FeraSide_1", 1);
        //PlayerPrefs.SetInt("CG_Pillory_Side_1", 1);


        PlayerPrefs.SetInt("CG", 1);//日常调教界面时常可进
        //PlayerPrefs.SetInt("CG_AVG_01", 1);//cg解锁
        //PlayerPrefs.SetInt("CG_AVG_02", 1);//cg解锁
        //PlayerPrefs.SetInt("CG_AVG_03", 1);//cg解锁



        PlayerPrefs.SetInt("Chapter_01", 1);//目前保持第一章永远在
        //PlayerPrefs.SetInt("Chapter_02", 1);
        //PlayerPrefs.SetInt("Chapter_03", 1);
        //PlayerPrefs.SetInt("Chapter_04", 1);
        //PlayerPrefs.SetInt("Chapter_05", 1);
        //PlayerPrefs.SetInt("Chapter_06", 1);
        //PlayerPrefs.SetInt("Chapter_07", 1);
        //PlayerPrefs.SetInt("Chapter_08", 1);
        //PlayerPrefs.SetInt("Chapter_09", 1);
        //PlayerPrefs.SetInt("Chapter_10", 1);
        //PlayerPrefs.SetInt("Chapter_11", 1);
        //PlayerPrefs.SetInt("Chapter_12", 1);
        //PlayerPrefs.SetInt("Chapter_13", 1);

        //GameFlowData.nextScene = "Arena";
        //GameFlowData.nextScene = "Story_08";//测试Boss使用
        //GameFlowData.nextScene = "CG_AVG_04";//测试CG使用

        switch (GameFlowData.nextScene)
        {
            case "AVG_02":
                PlayAVG("Chapter_02");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;
            case "AVG_03":
                PlayAVG("Chapter_03");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;
            case "AVG_04":
                PlayAVG("Chapter_04");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;
            case "AVG_05":
                PlayAVG("Chapter_05");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;
            case "AVG_06":
                PlayAVG("Chapter_06");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;
            case "AVG_07":
                PlayAVG("Chapter_07");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;
            case "AVG_08":
                PlayAVG("Chapter_08");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;
            case "AVG_09":
                PlayAVG("Chapter_09");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;
            case "AVG_10":
                PlayAVG("Chapter_10");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;
            case "AVG_11":
                PlayAVG("Chapter_11");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;
            case "AVG_12":
                PlayAVG("Chapter_12");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;
            case "AVG_13":
                PlayAVG("Chapter_13");
                To_AVGScene();
                Invoke("PlayBackgroundMusic", 1f);
                break;

            case "Story_01":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 4;
                _RoomGenerator.RoomType = 0;//Wall
                _RoomGenerator.SkyBoxNumber = 0;//夜晚

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第1章 王都潜入";
                        break;
                    case 1:
                        _Stage_Information.text = "第一章 潜入王都";
                        break;
                    case 2:
                        _Stage_Information.text = "第一章 潛入王都";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 1 – Infiltration";
                        break;
                    case 4:
                        _Stage_Information.text = "제1장 왕도 잠입";
                        break;
                }
                Invoke("PlayDungeonBGM", 1f);
                break;

            case "Story_02":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 5;
                _RoomGenerator.RoomType = 0;//Wall
                _RoomGenerator.SkyBoxNumber = 0;//夜晚

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第2章 皇太子暗殺";
                        break;
                    case 1:
                        _Stage_Information.text = "第二章 刺杀皇太子";
                        break;
                    case 2:
                        _Stage_Information.text = "第二章 刺殺皇太子";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 2 – Assassinate";
                        break;
                    case 4:
                        _Stage_Information.text = "제2장 황태자 암살";
                        break;
                }
                Invoke("PlayDungeonBGM", 1f);
                break;


            case "Story_03":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 8;//卫兵队长Boss战
                //_RoomGenerator.SkyBoxNumber = 1;//早上
                _RoomGenerator.SkyBoxNumber = 3;//红雾

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第3章 罠からの脱出";
                        break;
                    case 1:
                        _Stage_Information.text = "第三章 逃离陷阱";
                        break;
                    case 2:
                        _Stage_Information.text = "第三章 逃離陷阱";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 3 – Escape";
                        break;
                    case 4:
                        _Stage_Information.text = "제3장 함정 탈출";
                        break;
                }
                Invoke("PlayDungeonBGM", 1f);
                break;


            case "Story_04":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 5;
                _RoomGenerator.RoomType = 1;//Dungeon
                _RoomGenerator.SkyBoxNumber = 2;//白雾


                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第4章 宰相暗殺";
                        break;
                    case 1:
                        _Stage_Information.text = "第四章 刺杀宰相";
                        break;
                    case 2:
                        _Stage_Information.text = "第四章 刺殺宰相";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 4 – Assassinate";
                        break;
                    case 4:
                        _Stage_Information.text = "제4장 재상 암살";
                        break;
                }
                Invoke("PlayRuinsBGM", 1f);
                break;

            case "Story_05":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 9;//王女Boss战
                _RoomGenerator.SkyBoxNumber = 3;//红雾

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第5章 思わぬ遭遇";
                        break;
                    case 1:
                        _Stage_Information.text = "第五章 意外遭遇";
                        break;
                    case 2:
                        _Stage_Information.text = "第五章 意外遭遇";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 5 – Encounter";
                        break;
                    case 4:
                        _Stage_Information.text = "제5장 뜻밖의 조우";
                        break;
                }
                Invoke("PlayRuinsBGM", 1f);
                break;


            case "Story_06":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 5;
                _RoomGenerator.RoomType = 1;//Dungeon
                _RoomGenerator.SkyBoxNumber = 0;//夜晚


                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第6章 敗走";
                        break;
                    case 1:
                        _Stage_Information.text = "第六章 败退";
                        break;
                    case 2:
                        _Stage_Information.text = "第六章 敗退";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 6 – Defeat";
                        break;
                    case 4:
                        _Stage_Information.text = "제6장 패퇴";
                        break;
                }
                Invoke("PlayRuinsBGM", 1f);
                break;

            case "Story_07":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 6;
                _RoomGenerator.RoomType = 1;//Dungeon
                _RoomGenerator.SkyBoxNumber = 0;//夜晚

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第7章 立て直し";
                        break;
                    case 1:
                        _Stage_Information.text = "第七章 重整旗鼓";
                        break;
                    case 2:
                        _Stage_Information.text = "第七章 重整旗鼓";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 7 – Regroup";
                        break;
                    case 4:
                        _Stage_Information.text = "제7장 재정비";
                        break;
                }
                Invoke("PlayRuinsBGM", 1f);
                break;

            case "Story_08":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 10;//宰相Boss战
                _RoomGenerator.SkyBoxNumber = 3;//红雾

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第8章 遅れた復讐";
                        break;
                    case 1:
                        _Stage_Information.text = "第八章 迟到的复仇";
                        break;
                    case 2:
                        _Stage_Information.text = "第八章 遲來的復仇";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 8 – Revenge";
                        break;
                    case 4:
                        _Stage_Information.text = "제8장 늦은 복수";
                        break;
                }
                Invoke("PlayRuinsBGM", 1f);
                break;

            case "Story_09":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 7;
                _RoomGenerator.RoomType = 1;//Dungeon
                _RoomGenerator.SkyBoxNumber = 3;//红雾

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第9章 兄妹を探して";
                        break;
                    case 1:
                        _Stage_Information.text = "第九章 寻找兄妹";
                        break;
                    case 2:
                        _Stage_Information.text = "第九章 尋找兄妹";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 9 – Searching";
                        break;
                    case 4:
                        _Stage_Information.text = "제9장 남매를 찾아서";
                        break;
                }
                Invoke("PlayRuinsBGM", 1f);
                break;

            case "Story_10":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 11;//王女与皇太子Boss战
                _RoomGenerator.SkyBoxNumber = 3;//红雾

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第10章 地下決戦";
                        break;
                    case 1:
                        _Stage_Information.text = "第十章 地下决战";
                        break;
                    case 2:
                        _Stage_Information.text = "第十章 地下決戰";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 10 – Showdown";
                        break;
                    case 4:
                        _Stage_Information.text = "제10장 지하 결전";
                        break;
                }
                Invoke("PlayRuinsBGM", 1f);
                break;


            case "Story_11":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 7;
                _RoomGenerator.RoomType = 1;//Dungeon
                _RoomGenerator.SkyBoxNumber = 3;//红雾

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第11章 再びの敗北";
                        break;
                    case 1:
                        _Stage_Information.text = "第十一章 再度战败";
                        break;
                    case 2:
                        _Stage_Information.text = "第十一章 再度戰敗";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 11 – Defeat 2";
                        break;
                    case 4:
                        _Stage_Information.text = "제11장 다시 한번 패배";
                        break;
                }
                Invoke("PlayRuinsBGM", 1f);
                break;

            case "Story_12":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 12;//皇帝Boss战
                _RoomGenerator.SkyBoxNumber = 3;//红雾

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "第12章 皇帝";
                        break;
                    case 1:
                        _Stage_Information.text = "第十二章 皇帝";
                        break;
                    case 2:
                        _Stage_Information.text = "第十二章 皇帝";
                        break;
                    case 3:
                        _Stage_Information.text = "Chapter 12 – Emperor";
                        break;
                    case 4:
                        _Stage_Information.text = "제12장 황제";
                        break;
                }
                Invoke("PlayRuinsBGM", 1f);
                break;

            case "Arena":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 13;
                //_RoomGenerator.SkyBoxNumber = UnityEngine.Random.Range(0, 4);//随机
                //_RoomGenerator.SkyBoxNumber = 1;//早上
                _RoomGenerator.SkyBoxNumber = 2;//白雾
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "白濁アリーナ";
                        break;
                    case 1:
                        _Stage_Information.text = "白浊角斗场";
                        break;
                    case 2:
                        _Stage_Information.text = "白濁角鬥場";
                        break;
                    case 3:
                        _Stage_Information.text = "White Haze Colosseum";
                        break;
                    case 4:
                        _Stage_Information.text = "백탁 투기장";
                        break;
                }
                Invoke("PlayRuinsBGM", 1f);
                break;

            case "Dungeon":
                ToSavePageButton(1);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 4;
                _RoomGenerator.SkyBoxNumber = UnityEngine.Random.Range(0, 4);//随机
                _RoomGenerator.RoomType = 0;//Wall

                //if (UnityEngine.Random.Range(0, 2) == 0)
                //{
                //    _RoomGenerator.RoomType = 0;//Wall
                //}
                //else
                //{
                //    _RoomGenerator.RoomType = 1;//Dungeon
                //}

                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0:
                        _Stage_Information.text = "帝国調教所";
                        break;
                    case 1:
                        _Stage_Information.text = "帝国调教所";
                        break;
                    case 2:
                        _Stage_Information.text = "帝國調教所";
                        break;
                    case 3:
                        _Stage_Information.text = "Imperial Training Hall";
                        break;
                    case 4:
                        _Stage_Information.text = "제국 조교소";
                        break;
                }
                Invoke("PlayDungeonBGM", 1f);
                break;

            case "CG":
                ToSavePageButton(0);
                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 0;//调教室
                _RoomGenerator.SkyBoxNumber = 3;//红雾

                SavePageQuitButton.SetActive(true);//在存档界面退出按钮，只有CG界面可以显示
                Invoke("PlayDungeonBGM", 1f);
                break;


            case "CG_AVG_01":

                //拉出AVG
                PlayAVG("CG_01");
                To_AVGScene();

                //隐藏存档界面，拉摄像机,隐藏玩家
                Invoke("DelayHideSaveCavans", 0.3f);


                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 1;//游街和处刑广场
                _RoomGenerator.SkyBoxNumber = 2;//白雾


                Invoke("PlayDungeonBGM", 1f);
                break;


            case "CG_AVG_02":

                //拉出AVG
                PlayAVG("CG_02");
                To_AVGScene();

                //隐藏存档界面，拉摄像机,隐藏玩家
                Invoke("DelayHideSaveCavans", 0.3f);


                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 1;//游街和处刑广场
                _RoomGenerator.SkyBoxNumber = 2;//白雾

                Invoke("PlayDungeonBGM", 1f);
                break;

            case "CG_AVG_03":

                //拉出AVG
                PlayAVG("CG_03");
                To_AVGScene();

                //隐藏存档界面，拉摄像机,隐藏玩家
                Invoke("DelayHideSaveCavans", 0.3f);


                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = 1;//游街和处刑广场
                _RoomGenerator.SkyBoxNumber = 2;//白雾


                Invoke("PlayDungeonBGM", 1f);
                break;

            case "CG_AVG_04":
                //拉出AVG
                PlayAVG("CG_04");
                To_AVGScene();

                //隐藏存档界面，拉摄像机,隐藏玩家
                Invoke("DelayHideSaveCavans", 0.3f);


                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = -1;//惩戒回廊


                _RoomGenerator.SkyBoxNumber = 3;//红雾

                Invoke("PlayDungeonBGM", 1f);
                break;


            case "CG_AVG_05":
                //拉出AVG
                PlayAVG("CG_05");
                To_AVGScene();

                //隐藏存档界面，拉摄像机,隐藏玩家
                Invoke("DelayHideSaveCavans", 0.3f);


                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = -4;//赛琳娜王座


                _RoomGenerator.SkyBoxNumber = 3;//红雾

                Invoke("PlayDungeonBGM", 1f);
                break;


            case "CG_AVG_06":
                //拉出AVG
                PlayAVG("CG_06");
                To_AVGScene();

                //隐藏存档界面，拉摄像机,隐藏玩家
                Invoke("DelayHideSaveCavans", 0.3f);


                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = -3;//腐蝕胎巢


                _RoomGenerator.SkyBoxNumber = 3;//红雾

                Invoke("PlayDungeonBGM", 1f);
                break;


            case "CG_AVG_07":
                //拉出AVG
                PlayAVG("CG_07");
                To_AVGScene();

                //隐藏存档界面，拉摄像机,隐藏玩家
                Invoke("DelayHideSaveCavans", 0.3f);


                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = -2;//惩戒修会圣堂


                _RoomGenerator.SkyBoxNumber = 2;//白雾

                Invoke("PlayDungeonBGM", 1f);
                break;

            case "CG_AVG_08":
                //拉出AVG
                PlayAVG("CG_08");
                To_AVGScene();

                //隐藏存档界面，拉摄像机,隐藏玩家
                Invoke("DelayHideSaveCavans", 0.3f);


                Invoke("DelayShowRoomGenerator", 0.1f);
                _RoomGenerator.roomNumber = -2;//惩戒修会圣堂


                _RoomGenerator.SkyBoxNumber = 3;//红雾

                Invoke("PlayDungeonBGM", 1f);
                break;


            default:

                Invoke("PlayBackgroundMusic", 1f);

                break;
        }

        //GameFlowData.nextScene = "";//清理

        GameFlowData.RoomLevel = 0;//清理


        //ToDo :强制锁定地下城和角斗场 还有 除去1，2关之外的关卡, 还有除去 自慰1/被刺伤强奸CG  其他CG锁死
        //LockStage();



        UpdateCreateCostText();//更新创建奴隶价格
    }
    public GameObject HalfBlack;//这个完全就是我没敢去测试把AVG拉出ShowSaveCavans里多添加的
    void DelayHideSaveCavans()
    {
        HalfBlack.SetActive(false);
        SaveCavans.SetActive(false);
        MainCamera.SetInteger("View", 1);

        player.characterSkin.HideSkeleton();//隐藏玩家保持相机不动

    }//隐藏存档界面，拉摄像机,隐藏玩家


    void DelayShowRoomGenerator()
    {
        _RoomGenerator.gameObject.SetActive(true);
    }//防止一开始执行东西太多


    /// <summary>
    /// 作弊按钮
    /// </summary>
    #region

    //ToDo :强制锁定地下城和角斗场 还有 除去1，2关之外的关卡, 还有除去 自慰1/被刺伤强奸CG  其他CG锁死
    public void LockStage()
    {

        //PlayerPrefs.SetInt("CG_OnanismFront_1", 1);//目前保持第一个CG永远在
        PlayerPrefs.SetInt("CG_OnanismSide_1", 0);

        //PlayerPrefs.SetInt("CG_InsultSide_1", 0);
        //PlayerPrefs.SetInt("CG_GagSide_1", 0);
        //PlayerPrefs.SetInt("CG_FistingFront_1", 0);

        //PlayerPrefs.SetInt("CG_RapeFront_1", 0);
        //PlayerPrefs.SetInt("CG_RapeSide_1", 0);
        //PlayerPrefs.SetInt("CG_AssaultFront_1", 0);
        //PlayerPrefs.SetInt("CG_AssaultSide_1", 0);

        PlayerPrefs.SetInt("CG_TentacleBagFront_1", 0);
        PlayerPrefs.SetInt("CG_TentacleBugSide_1", 0);

        PlayerPrefs.SetInt("CG_TentacleFront_1", 0);

        PlayerPrefs.SetInt("CG_TentacleHermitCrabFront_1", 0);
        PlayerPrefs.SetInt("CG_TentacleHermitCrabSide_1", 0);

        PlayerPrefs.SetInt("CG_FleshArmor_1", 0);

        PlayerPrefs.SetInt("CG_TentacleMonsterFront_1", 0);
        PlayerPrefs.SetInt("CG_MonsterSide_1", 0);

        PlayerPrefs.SetInt("CG_TentacleWallFront_1", 0);
        PlayerPrefs.SetInt("CG_TentacleWallSide_1", 0);



        PlayerPrefs.SetInt("CG_Hogtie_1", 0);

        PlayerPrefs.SetInt("CG_HangSide_4", 0);
        PlayerPrefs.SetInt("CG_HangSide_1", 0);
        PlayerPrefs.SetInt("CG_HangFront_1", 0);

        PlayerPrefs.SetInt("CG_HangDown_4", 0);
        PlayerPrefs.SetInt("CG_HangDown_1", 0);

        PlayerPrefs.SetInt("CG_FeraSide_1", 0);
        PlayerPrefs.SetInt("CG_Pillory_Side_1", 0);


        //PlayerPrefs.SetInt("CG", 1);//日常调教界面时常可进
        //PlayerPrefs.SetInt("CG_AVG_01", 1);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_02", 0);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_03", 0);//cg解锁
        //PlayerPrefs.SetInt("CG_AVG_04", 1);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_05", 0);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_06", 0);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_07", 0);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_08", 0);//cg解锁


        //PlayerPrefs.SetInt("Chapter_01", 1);//目前保持第一章永远在
        //PlayerPrefs.SetInt("Chapter_02", 1);
        //PlayerPrefs.SetInt("Chapter_03", 1);
        //PlayerPrefs.SetInt("Chapter_04", 1);//第四章的AVG变成感谢试玩
        PlayerPrefs.SetInt("Chapter_05", 0);
        PlayerPrefs.SetInt("Chapter_06", 0);
        PlayerPrefs.SetInt("Chapter_07", 0);
        PlayerPrefs.SetInt("Chapter_08", 0);
        PlayerPrefs.SetInt("Chapter_09", 0);
        PlayerPrefs.SetInt("Chapter_10", 0);
        PlayerPrefs.SetInt("Chapter_11", 0);
        PlayerPrefs.SetInt("Chapter_12", 0);
        PlayerPrefs.SetInt("Chapter_13", 0);

        PlayerPrefs.SetInt("Chapter_Arena", 0);
        PlayerPrefs.SetInt("Chapter_Dungeon", 0);


    }

    public void SetCheatButton(int ReLoad)//0刷新场景  1不刷（通关后打开）
    {
        PlayerPrefs.SetInt("CG_OnanismFront_1", 1);//目前保持第一个CG永远在
        PlayerPrefs.SetInt("CG_OnanismSide_1", 1);

        PlayerPrefs.SetInt("CG_InsultSide_1", 1);
        PlayerPrefs.SetInt("CG_GagSide_1", 1);
        PlayerPrefs.SetInt("CG_FistingFront_1", 1);

        PlayerPrefs.SetInt("CG_RapeFront_1", 1);
        PlayerPrefs.SetInt("CG_RapeSide_1", 1);
        PlayerPrefs.SetInt("CG_AssaultFront_1", 1);
        PlayerPrefs.SetInt("CG_AssaultSide_1", 1);

        PlayerPrefs.SetInt("CG_TentacleBagFront_1", 1);
        PlayerPrefs.SetInt("CG_TentacleBugSide_1", 1);

        PlayerPrefs.SetInt("CG_TentacleFront_1", 1);

        PlayerPrefs.SetInt("CG_TentacleHermitCrabFront_1", 1);
        PlayerPrefs.SetInt("CG_TentacleHermitCrabSide_1", 1);

        PlayerPrefs.SetInt("CG_FleshArmor_1", 1);

        PlayerPrefs.SetInt("CG_TentacleMonsterFront_1", 1);
        PlayerPrefs.SetInt("CG_MonsterSide_1", 1);

        PlayerPrefs.SetInt("CG_TentacleWallFront_1", 1);
        PlayerPrefs.SetInt("CG_TentacleWallSide_1", 1);



        PlayerPrefs.SetInt("CG_Hogtie_1", 1);

        PlayerPrefs.SetInt("CG_HangSide_4", 1);
        PlayerPrefs.SetInt("CG_HangSide_1", 1);
        PlayerPrefs.SetInt("CG_HangFront_1", 1);

        PlayerPrefs.SetInt("CG_HangDown_4", 1);
        PlayerPrefs.SetInt("CG_HangDown_1", 1);

        PlayerPrefs.SetInt("CG_FeraSide_1", 1);
        PlayerPrefs.SetInt("CG_Pillory_Side_1", 1);


        PlayerPrefs.SetInt("CG", 1);//日常调教界面时常可进
        PlayerPrefs.SetInt("CG_AVG_01", 1);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_02", 1);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_03", 1);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_04", 1);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_05", 1);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_06", 1);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_07", 1);//cg解锁
        PlayerPrefs.SetInt("CG_AVG_08", 1);//cg解锁

        PlayerPrefs.SetInt("Chapter_01", 1);//目前保持第一章永远在
        PlayerPrefs.SetInt("Chapter_02", 1);
        PlayerPrefs.SetInt("Chapter_03", 1);
        PlayerPrefs.SetInt("Chapter_04", 1);
        PlayerPrefs.SetInt("Chapter_05", 1);
        PlayerPrefs.SetInt("Chapter_06", 1);
        PlayerPrefs.SetInt("Chapter_07", 1);
        PlayerPrefs.SetInt("Chapter_08", 1);
        PlayerPrefs.SetInt("Chapter_09", 1);
        PlayerPrefs.SetInt("Chapter_10", 1);
        PlayerPrefs.SetInt("Chapter_11", 1);
        PlayerPrefs.SetInt("Chapter_12", 1);
        PlayerPrefs.SetInt("Chapter_13", 1);

        PlayerPrefs.SetInt("Chapter_Arena", 1);
        PlayerPrefs.SetInt("Chapter_Dungeon", 1);

        PlayerPrefs.SetInt("HighElf", 1);
        PlayerPrefs.SetInt("HighDemon", 1);

        if (ReLoad == 0)
        {
            ReLoadScene();
        }

    }

    #endregion



    /// <summary>
    /// 主菜单
    /// </summary>
    #region
    [Header("封面图片")]
    public GameObject coverPanel;   // 封面图片所在的 UI 根节点

    void CheckcoverPanel()
    {
        if (GameFlowData.hasShownCoverThisRun)
        {
            // 本次已经显示过了，直接关闭封面
            coverPanel.SetActive(false);
        }
        else
        {
            // 第一次运行：显示封面，并标记
            GameFlowData.hasShownCoverThisRun = true;
            coverPanel.SetActive(true);
        }
    }

    public void OnCoverClicked()
    {
        coverPanel.SetActive(false);

        AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);

    } // 按钮点击用这个



    [Header("主菜单")]
    public GameObject Common_All;//移动血条等
    public GameObject NextButton;//播放结局动画
    public GameObject Loading;
    public GameObject SavePageQuitButton;//在存档界面退出按钮，只有CG界面可以显示
    public void Ending_UI()
    {
        Common_All.SetActive(false);
        NextButton.SetActive(true);

        player.isInputBlocked = true;//切断玩家的方向攻击等输入

        CurrentChooseList = -6;

        //拉近相机
        MainCamera.SetInteger("View", 1);

        //延后允许按下继续
        Invoke("EndUI_CanPush", 1);

    }//生命值归0后触发

    bool CanPushEndUI = false;//防止玩家快速按下
    void EndUI_CanPush()
    {
        CanPushEndUI = true;
    }


    public void ToEnd_Surrender_Cavans()
    {
        CurrentChooseList = -7;
        End_Surrender_Cavans.SetActive(true);

        Time.timeScale = 0f;

    }//进入投降战败界面

    public void ChooseSurrender()
    {

        switch (GameFlowData.nextScene)
        {
            default:
            case "Story_01":
            case "Story_02":
                //头枷轮奸结局
                PlayerPrefs.SetInt("CG_AVG_01", 1);//cg解锁
                GameFlowData.nextScene = "CG_AVG_01";
                break;
            case "Story_03":
            case "Story_12":
                //肉铠结局
                PlayerPrefs.SetInt("CG_AVG_04", 1);//cg解锁
                GameFlowData.nextScene = "CG_AVG_04";
                break;

            case "Story_04":
                //泄欲车结局
                PlayerPrefs.SetInt("CG_AVG_02", 1);//cg解锁
                GameFlowData.nextScene = "CG_AVG_02";
                break;


            case "Story_06":
                //肉圣物结局
                PlayerPrefs.SetInt("CG_AVG_07", 1);//cg解锁
                GameFlowData.nextScene = "CG_AVG_07";
                break;


            case "Story_07":
                //集体鞭打结局
                PlayerPrefs.SetInt("CG_AVG_08", 1);//cg解锁
                GameFlowData.nextScene = "CG_AVG_08";
                break;


            case "Story_08":
                //魔界生物试验体结局
                PlayerPrefs.SetInt("CG_AVG_06", 1);//cg解锁
                GameFlowData.nextScene = "CG_AVG_06";
                break;



            case "Story_09":
            case "Story_11":
                //拍卖会结局
                PlayerPrefs.SetInt("CG_AVG_03", 1);//cg解锁
                GameFlowData.nextScene = "CG_AVG_03";
                break;

            case "Story_10":
            case "Story_05":
                //王女性玩具结局
                PlayerPrefs.SetInt("CG_AVG_05", 1);//cg解锁
                GameFlowData.nextScene = "CG_AVG_05";
                break;
        }




        ReLoadScene();

    }//这个接口专门处理投降后根据当前关卡处理结局CG






    public void ReLoadScene()
    {
        Time.timeScale = 1;

        Invoke("DelayReLoadScene", 1f);
        Loading.SetActive(true);

        GameFlowData.BulletCanThroughtWall = false;//每次场景刷新的时候这个清掉

    }//重刷场景

    void DelayReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }




    [Header("主菜单界面层级")]
    public int CurrentChooseList = 0;//-8局内商店界面  -7战败投降界面  -6战败被抓住凌辱  -5三选一界面   -4游戏界面  -3暂停菜单    -2确认是否删除所有存档  -1确认是否删除存档  0主菜单界面   1捏人界面   2存档界面   3设置界面  4语言选择界面   5CG界面   6CG鉴赏中   7游戏模式选择   8剧情章节选择  9剧情AVG界面   10结算界面   11调教所选择界面  12感谢名单界面
    public int CurrentMode = 0;//0 进入CG界面  1捏人/进入游戏
    public int HomePagecurrentIndex = 0;//0 开始游戏  1 CG鉴赏  2 设置  3 退出
    public int CreatNewcurrentIndex = 0;//0 名称 1 眼睛  2 头  3 种族  4 职业  5 确定
    public int SettingPagecurrentIndex = 0;//0 BGM  1 SE  2 语言  3 删除存档
    public int LanguagePagecurrentIndex = 0;//0 日语 1中文 2繁中 3英语 4韩语
    public int ModePagecurrentIndex = 0;//0 故事模式 1角斗场模式 2地下城模式

    public int BonusPagecurrentIndex = 0;//0左边奖励  1中间奖励  2右边奖励

    [SerializeField] private GameObject[] HomePage_highlightObjs; // 主页高亮显示
    [SerializeField] private GameObject[] highlightObjs; // 捏人界面高亮显示
    [SerializeField] private GameObject[] SettingPage_highlightObjs; // 设置高亮显示
    [SerializeField] private GameObject[] LanguagePage_highlightObjs; // 语言设置高亮显示
    [SerializeField] private GameObject[] ModePage_highlightObjs; // 游戏模式设置高亮显示


    private void UpdateHighlight()
    {
        for (int i = 0; i < highlightObjs.Length; i++)
        {
            highlightObjs[i].SetActive(i == CreatNewcurrentIndex);
        }
    }

    private void UpdateHomePage_Highlight()
    {
        for (int i = 0; i < HomePage_highlightObjs.Length; i++)
        {
            HomePage_highlightObjs[i].SetActive(i == HomePagecurrentIndex);
        }
    }
    private void UpdateSettingPage_Highlight()
    {
        for (int i = 0; i < SettingPage_highlightObjs.Length; i++)
        {
            SettingPage_highlightObjs[i].SetActive(i == SettingPagecurrentIndex);
        }
    }
    private void UpdateLanguagePage_Highlight()
    {
        for (int i = 0; i < LanguagePage_highlightObjs.Length; i++)
        {
            LanguagePage_highlightObjs[i].SetActive(i == LanguagePagecurrentIndex);
        }
    }

    private void UpdateModePage_Highlight()
    {
        for (int i = 0; i < ModePage_highlightObjs.Length; i++)
        {
            ModePage_highlightObjs[i].SetActive(i == ModePagecurrentIndex);
        }

        switch (ModePagecurrentIndex)
        {
            case 0:
                Text_Story.SetActive(true);
                Text_OneToOne.SetActive(false);
                Text_Dungeon.SetActive(false);
                break;
            case 1:
                Text_Story.SetActive(false);
                Text_OneToOne.SetActive(true);
                Text_Dungeon.SetActive(false);
                break;
            case 2:
                Text_Story.SetActive(false);
                Text_OneToOne.SetActive(false);
                Text_Dungeon.SetActive(true);
                break;
        }

    }


    //Mode菜单(和三选一界面一样)增加了切换当前选中点击Play触发
    public void ChangeModeSelect(int ModeNumber)
    {
        ModePagecurrentIndex = ModeNumber;
        UpdateModePage_Highlight();
    }

    public void ToSelectMode()
    {
        switch (ModePagecurrentIndex)
        {
            case 0:
                ToChapterPage();
                break;
            case 1:
                ToOneToOneStage();
                break;
            case 2:
                ToDungeonStage();
                break;
        }
    }


    [SerializeField] private Button HairLeft, HairRight;
    [SerializeField] private Button EyesLeft, EyesRight;
    [SerializeField] private Button RaceLeft, RaceRight;
    [SerializeField] private Button ClassLeft, ClassRight;

    public Button okButton;

    public void ToSavePageButton(int currentMode)
    {
        Invoke("ToSavePage", 0.1f);//开始游戏进入存档界面(CG)
        CurrentMode = currentMode;

        if (CurrentMode == 0)
        {
            //CG鉴赏
            Invoke("DelayPlayerPose", 0.2f);

            //钮按下后绿色选中也会过去
            HomePagecurrentIndex = 1;
            UpdateHomePage_Highlight();
        }
        else
        {
            //主界面
            player.anim.Play("Girl_Default_Idle");

            //钮按下后绿色选中也会过去
            HomePagecurrentIndex = 0;
            UpdateHomePage_Highlight();
        }

    }


    public void ToSavePage()
    {
        ModeCavans.SetActive(false);
        HomePageCavans.SetActive(false);
        CGCavans.SetActive(false);
        CurrentChooseList = 2;

    }

    public GameObject Text_Story, Text_OneToOne, Text_Dungeon;

    public void ToStoryStage()
    {
        Invoke("ToChapterPage", 0.1f);

        //钮按下后绿色选中也会过去
        ModePagecurrentIndex = 0;
        UpdateModePage_Highlight();
    }
    public void ToOneToOneStage()
    {
        if (PlayerPrefs.GetInt("Chapter_Arena") == 1)
        {

            GameFlowData.nextScene = "Arena";
            ReLoadScene();//前往竞技场
        }
        else
        {
            //提示此模式尚未解锁
            AudioManager.instance.AudioPlay(AudioManager.instance.SE_Reba);

            _RoomGenerator.ShowInformationOfStage(4);
        }



        //  ToSavePageButton(1);//开始游戏进入存档界面
        //  //钮按下后绿色选中也会过去
        //  ModePagecurrentIndex = 1;
        //  UpdateModePage_Highlight();
    }

    [Header("角斗场最高纪录")]
    public Text BestWave;
    void Show_bestWave()
    {

        int bestWave = PlayerPrefs.GetInt("Arena_Wave", 0);
        string colorTag = "#FFD700"; // 金黄色，可改为红色 "#FF4444"、蓝色 "#00BFFF" 等
        switch (PlayerPrefs.GetInt("language"))
        {
            case 0: // 日语
                BestWave.text = $"最高記録：第<color={colorTag}>{bestWave}</color>波";
                break;
            case 1: // 简体中文
                BestWave.text = $"最高纪录：第<color={colorTag}>{bestWave}</color>波";
                break;
            case 2: // 繁体中文
                BestWave.text = $"最高紀錄：第<color={colorTag}>{bestWave}</color>波";
                break;
            case 3: // 英语
                BestWave.text = $"Best Record: Wave <color={colorTag}>{bestWave}</color>";
                break;
            case 4: // 韩语
                BestWave.text = $"최고 기록: <color={colorTag}>{bestWave}</color>번째 웨이브";
                break;
        }

    }//显示最高记录



    public void ToDungeonStage()
    {

        if (PlayerPrefs.GetInt("Chapter_Dungeon") == 1)
        {

            GameFlowData.nextScene = "Dungeon";
            ReLoadScene();//前往地下城
        }
        else
        {
            //提示此模式尚未解锁
            AudioManager.instance.AudioPlay(AudioManager.instance.SE_Reba);

            _RoomGenerator.ShowInformationOfStage(4);
        }



        //  ToSavePageButton(1);//开始游戏进入存档界面
        //  //钮按下后绿色选中也会过去
        //  ModePagecurrentIndex = 2;
        //  UpdateModePage_Highlight();
    }

    public void SavePageToHomePage()
    {
        //ToHomePage();

        GameFlowData.nextScene = "";//清理

        ReLoadScene();//存档页面返回主菜单

        //PauseGame();



    }//这个是存档界面在按下退出的时候产生路径[不确定怎么弄专门设置的]
    public void HomePageToCGPage()
    {
        GameFlowData.nextScene = "CG_AVG_01";
        //GameFlowData.nextScene = "CG";
        ReLoadScene();//前往CG页面




        //ToSavePageButton(0);

    }//这个是主界面在去CG界面的时候产生路径[不确定怎么弄专门设置的]
    void DelayPlayerPose() { player.anim.Play("RBQ_Punish_Rape_2"); }//UIManager里的Awake太早来不及触发
    public void ToChapterPage()
    {
        ChapterCavans.SetActive(true);
        CurrentChooseList = 8;
    }

    public void ToSettingPage()
    {
        SettingCavans.SetActive(true);
        LanguageCavans.SetActive(false);
        CurrentChooseList = 3;

        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 2;
        UpdateHomePage_Highlight();
    }

    public void ToLanguagePage()
    {
        LanguageCavans.SetActive(true);
        CurrentChooseList = 4;

        SettingPagecurrentIndex = 2;
        UpdateSettingPage_Highlight();
    }
    public void ToHomePage()
    {
        HomePageCavans.SetActive(true);
        SettingCavans.SetActive(false);
        ModeCavans.SetActive(false);
        ChapterCavans.SetActive(false);
        CG_End_Cavans.SetActive(false);
        ThanksCavans.SetActive(false);
        CurrentChooseList = 0;

        //这个主要是针对CG结局界面做的
        GameFlowData.nextScene = "";
    }

    public void ToCGPage()
    {
        if (SaveManager.CountSaves() > 0) // 没有任何存档无法开始
        {

            CGCavans.SetActive(true);
            CurrentChooseList = 5;



            MainCamera.SetInteger("View", 0);

            ShowSaveCavans.SetActive(true);


            player.frameEvents.audioS.Stop();
            player.anim.Play("RBQ_Punish_Rape_2");

            CG_BackButton.SetActive(false);//隐藏CG观赏后退按钮

        }
        else
        {

            //提示需要创建角色
            AudioManager.instance.AudioPlay(AudioManager.instance.SE_Reba);

            _RoomGenerator.ShowInformationOfStage(3);
        }

    }

    public void ToCG_EndPage()
    {
        CG_End_Cavans.SetActive(true);
        CurrentChooseList = 11;


        //打开菜单的时候就预先把CG这个概念放进去
        UpdateHighlight_CG_End();

        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 1;
        UpdateHomePage_Highlight();
    }


    public void ToModePage()
    {
        ModeCavans.SetActive(true);
        ChapterCavans.SetActive(false);
        CurrentChooseList = 7;


        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 0;
        UpdateHomePage_Highlight();
    }

    public void To_AVGScene()
    {
        CurrentChooseList = 9;
        AVGCavans.SetActive(true);

        HomePageCavans.SetActive(false);
        ModeCavans.SetActive(false);
        ChapterCavans.SetActive(false);
    }
    void Delay_AVG_ShowText()
    {
        dialogSystem.ShowText();
    }



    public void ToThanksPage()
    {
        CurrentChooseList = 12;
        ThanksCavans.SetActive(true);

        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 4;
        UpdateHomePage_Highlight();
    }


    #endregion



    /// <summary>
    /// 捏人菜单
    /// </summary>
    #region
    [Header("各类菜单")]
    public bool isPause = true;//一开始就Menu界面
    public GameObject PauseMenu;

    public GameObject LockOfArena, LockOfDungeon;

    public void StageClean()
    {
        CurrentChooseList = 10;//进入结算页面

        if (GameFlowData.nextScene == "Story_01") { PlayerPrefs.SetInt("Chapter_02", 1); }
        if (GameFlowData.nextScene == "Story_02") { PlayerPrefs.SetInt("Chapter_03", 1); }
        if (GameFlowData.nextScene == "Story_03") { PlayerPrefs.SetInt("Chapter_04", 1); }
        if (GameFlowData.nextScene == "Story_04") { PlayerPrefs.SetInt("Chapter_05", 1); }
        if (GameFlowData.nextScene == "Story_05") { PlayerPrefs.SetInt("Chapter_06", 1); }
        if (GameFlowData.nextScene == "Story_06") { PlayerPrefs.SetInt("Chapter_07", 1); }
        if (GameFlowData.nextScene == "Story_07") { PlayerPrefs.SetInt("Chapter_08", 1); }
        if (GameFlowData.nextScene == "Story_08") { PlayerPrefs.SetInt("Chapter_09", 1); }
        if (GameFlowData.nextScene == "Story_09") { PlayerPrefs.SetInt("Chapter_10", 1); }
        if (GameFlowData.nextScene == "Story_10") { PlayerPrefs.SetInt("Chapter_11", 1); }
        if (GameFlowData.nextScene == "Story_11") { PlayerPrefs.SetInt("Chapter_12", 1); }
        if (GameFlowData.nextScene == "Story_12") { PlayerPrefs.SetInt("Chapter_13", 1); }

        // 特殊解锁逻辑


        if (GameFlowData.nextScene == "Story_03" && PlayerPrefs.GetInt("HighElf") == 0)
        {
            PlayerPrefs.SetInt("HighElf", 1);
            _RoomGenerator.ShowInformationOfStage(13);
            //Debug.Log("高等精灵可选");

        }


        if (GameFlowData.nextScene == "Story_05" && PlayerPrefs.GetInt("Chapter_Arena") == 0)
        {
            PlayerPrefs.SetInt("Chapter_Arena", 1);
            //Debug.Log("解锁竞技场模式");

            _RoomGenerator.ShowInformationOfStage(7);
        }

        if (GameFlowData.nextScene == "Story_07" && PlayerPrefs.GetInt("HighDemon") == 0)
        {
            PlayerPrefs.SetInt("HighDemon", 1);
            _RoomGenerator.ShowInformationOfStage(13);
            //Debug.Log("高等魔族可选");
        }


        if (GameFlowData.nextScene == "Story_12" && PlayerPrefs.GetInt("Chapter_Dungeon") == 0)
        {
            PlayerPrefs.SetInt("Chapter_Dungeon", 1);
            //Debug.Log("解锁地下城模式");

            _RoomGenerator.ShowInformationOfStage(7);
        }

        if (GameFlowData.nextScene == "Story_12")
        {
            //击败皇帝解锁全部CG/模式/种族
            SetCheatButton(1);
        }



        if (GameFlowData.nextScene == "Dungeon")
        {
            _RoomGenerator.ShowInformationOfStage(11);
            //Debug.Log("当前地下城模式连胜增加");
        }


    }


    public void NextStage()
    {
        if (GameFlowData.nextScene == "Story_01") { GameFlowData.nextScene = "AVG_02"; }
        if (GameFlowData.nextScene == "Story_02") { GameFlowData.nextScene = "AVG_03"; }
        if (GameFlowData.nextScene == "Story_03") { GameFlowData.nextScene = "AVG_04"; }
        if (GameFlowData.nextScene == "Story_04") { GameFlowData.nextScene = "AVG_05"; }
        if (GameFlowData.nextScene == "Story_05") { GameFlowData.nextScene = "AVG_06"; }
        if (GameFlowData.nextScene == "Story_06") { GameFlowData.nextScene = "AVG_07"; }
        if (GameFlowData.nextScene == "Story_07") { GameFlowData.nextScene = "AVG_08"; }
        if (GameFlowData.nextScene == "Story_08") { GameFlowData.nextScene = "AVG_09"; }
        if (GameFlowData.nextScene == "Story_09") { GameFlowData.nextScene = "AVG_10"; }
        if (GameFlowData.nextScene == "Story_10") { GameFlowData.nextScene = "AVG_11"; }
        if (GameFlowData.nextScene == "Story_11") { GameFlowData.nextScene = "AVG_12"; }
        if (GameFlowData.nextScene == "Story_12") { GameFlowData.nextScene = "AVG_13"; }

        //地下城和角斗场都是重来

        ReLoadScene();



    }//通关后前往下一关卡



    public void PauseGame()
    {
        CurrentChooseList = -3;


        Time.timeScale = 0;
        PauseMenu.SetActive(true);



        player.isInputBlocked = true;//切断玩家的方向攻击等输入


    }
    public void ContinueGame()
    {
        CurrentChooseList = -4;


        Time.timeScale = 1;
        PauseMenu.SetActive(false);


        Invoke("isInputBlockedFalse", 0.5f);

    }


    void isInputBlockedFalse()
    {

        player.isInputBlocked = false;//恢复玩家的方向攻击等输入

    }//防止按键连续触发

    public Animator MainCamera;//控制摄像机拉近远离
    public GameObject ShowSaveCavans;//通用UI层

    public GameObject HomePageCavans, SaveCavans, CreateCavans, SettingCavans, LanguageCavans, CGCavans, ModeCavans, ChapterCavans, AVGCavans, CG_End_Cavans, End_Surrender_Cavans, ThanksCavans;//主菜单界面，存档界面,捏人界面,设置界面,CG界面,游戏模式选择界面,CG结局界面,战败投降界面
    public DialogSystem dialogSystem;
    [Header("捏人界面UI")]
    public InputField nameInputField; // 绑定在 Inspector 里

    public Text hairLabel;
    public Text eyesLabel;
    public Text raceLabel;
    public Text classLabel;

    public Text IntroduceOfRace;//介绍文本

    #region 种族概览
    public enum RaceOption
    {
        Human = 0,      // 人类
        Elf = 1,        // 精灵
        HighElf = 2,    // 高等精灵
        RabbitBlack = 3,// 北方兔族（黑）
        RabbitWhite = 4,// 南方兔族（白）
        Demon = 5,      // 魔族
        HighDemon = 6,   // 高等魔族
        Oni = 7,        // 鬼族
        Deer = 8        // 鹿族
    }

    [SerializeField] private int raceOptionIndex = 0; // 0..6

    private void ApplyRaceSelectionSimple()
    {
        switch ((RaceOption)raceOptionIndex)
        {
            case RaceOption.Human: player.YYY_hatIndex = 1; IntroduceOfRace.text = RACE_DESC[Lang, 0]; break;
            case RaceOption.Elf: player.YYY_hatIndex = 2; IntroduceOfRace.text = RACE_DESC[Lang, 1]; break;
            case RaceOption.HighElf: player.YYY_hatIndex = 3; IntroduceOfRace.text = RACE_DESC[Lang, 2]; break;
            case RaceOption.RabbitBlack: player.YYY_hatIndex = 4; IntroduceOfRace.text = RACE_DESC[Lang, 3]; break; // 黑色兔耳
            case RaceOption.RabbitWhite: player.YYY_hatIndex = 10; IntroduceOfRace.text = RACE_DESC[Lang, 4]; break; // 白色兔耳
            case RaceOption.Demon: player.YYY_hatIndex = 12; IntroduceOfRace.text = RACE_DESC[Lang, 5]; break;
            case RaceOption.HighDemon: player.YYY_hatIndex = 11; IntroduceOfRace.text = RACE_DESC[Lang, 6]; break;
            case RaceOption.Oni: player.YYY_hatIndex = 8; IntroduceOfRace.text = RACE_DESC[Lang, 7]; break; // 鬼族
            case RaceOption.Deer: player.YYY_hatIndex = 5; IntroduceOfRace.text = RACE_DESC[Lang, 8]; break; // 鹿族
        }
    }

    private const int RaceCount = 9;//种族总数
    private const int RaceMaxIndex = RaceCount - 1;

    private int GetNextRaceIndex(int currentIndex, int delta)
    {
        // delta = +1 表示向右（下一个），delta = -1 表示向左（上一个）
        int idx = currentIndex;

        for (int i = 0; i < RaceCount; i++)
        {
            idx += delta;

            if (idx > RaceMaxIndex) idx = 0;
            if (idx < 0) idx = RaceMaxIndex;

            RaceOption ro = (RaceOption)idx;

            // 只锁高等精灵 / 高等魔族，其他包括鬼族 / 鹿族默认可选
            if (ro == RaceOption.HighElf && PlayerPrefs.GetInt("HighElf", 0) == 0) continue;
            if (ro == RaceOption.HighDemon && PlayerPrefs.GetInt("HighDemon", 0) == 0) continue;

            return idx;
        }

        // 理论上不会走到这里，兜底返回原值
        return currentIndex;
    }
    #endregion












    public void OnHairLeft()
    {
        if (IsLuna(player.currentSaveName) == false) { ChangeSkin(ref player.YYY_headIndex, 1, 12, -1); CreatNewcurrentIndex = 1; UpdateHighlight(); }//王女的发型无法选择
    }
    public void OnHairRight()
    {
        if (IsLuna(player.currentSaveName) == false) { ChangeSkin(ref player.YYY_headIndex, 1, 12, +1); CreatNewcurrentIndex = 1; UpdateHighlight(); }//王女的发型无法选择
    }

    public void OnEyesLeft()
    {
        if (IsLuna(player.currentSaveName) == false) { ChangeSkin(ref player.YYY_eyesIndex, 1, 14, -1); CreatNewcurrentIndex = 2; UpdateHighlight(); }
    }
    public void OnEyesRight()
    {
        if (IsLuna(player.currentSaveName) == false) { ChangeSkin(ref player.YYY_eyesIndex, 1, 14, +1); CreatNewcurrentIndex = 2; UpdateHighlight(); }
    }

    public void OnRaceLeft()
    {

        if (IsLuna(player.currentSaveName) == false)
        {
            raceOptionIndex = GetNextRaceIndex(raceOptionIndex, +1);

            ApplyRaceSelectionSimple();
            AfterAnySelectionChanged();

            CreatNewcurrentIndex = 3; UpdateHighlight();
        }
    }
    public void OnRaceRight()
    {

        if (IsLuna(player.currentSaveName) == false)
        {
            raceOptionIndex = GetNextRaceIndex(raceOptionIndex, -1);

            ApplyRaceSelectionSimple();
            AfterAnySelectionChanged();

            CreatNewcurrentIndex = 3; UpdateHighlight();
        }
    }

    public void OnClassLeft()
    {
        if (IsLuna(player.currentSaveName) == false)
        {
            ChangeSkin(ref player.YYY_bodyIndex, 10, 12, -1);
            if (CurrentMode == 1) { player.PlayNormalAttack(); }//只有在非CG界面捏人才能更换职业时不增加攻击动作，防止动画变不回来           
            CreatNewcurrentIndex = 4;
            UpdateHighlight();
        }
    }
    public void OnClassRight()
    {
        if (IsLuna(player.currentSaveName) == false)
        {
            ChangeSkin(ref player.YYY_bodyIndex, 10, 12, +1);
            if (CurrentMode == 1) { player.PlayNormalAttack(); }//只有在非CG界面捏人才能更换职业时不增加攻击动作，防止动画变不回来       
            CreatNewcurrentIndex = 4;
            UpdateHighlight();
        }
    }
    void ChangeSkin(ref int index, int min, int max, int delta)
    {
        index += delta;
        if (index < min) index = max;
        if (index > max) index = min;


        switch (player.YYY_bodyIndex)
        {
            case 1:
            case 10:
                player.CurrentProfession = 0;
                break;
            case 11:
                player.CurrentProfession = 1;
                break;
            case 12:
                player.CurrentProfession = 2;
                break;
        }

        player._ClothesToClass();//临时让衣服改变职业

        AfterAnySelectionChanged();

    }//捏人界面玩家点击单个皮肤选项左右之后

    public void AfterAnySelectionChanged()
    {
        player.SetSkin();     // 更新外观
        UpdateUI();           // 刷UI文字
        player.SaveCurrent(); // 存当前
        RefreshSaveSlots();   // 刷存档列表
    }

    #region 捏人界面启动多语言

    private int RaceOptionFromHat_Simple(int hat)
    {
        switch (hat)
        {
            case 1: return (int)RaceOption.Human;
            case 2: return (int)RaceOption.Elf;
            case 3: return (int)RaceOption.HighElf;
            case 4: return (int)RaceOption.RabbitBlack;
            case 10: return (int)RaceOption.RabbitWhite;
            case 12: return (int)RaceOption.Demon;
            case 11: return (int)RaceOption.HighDemon;
            case 8: return (int)RaceOption.Oni;   // 鬼族
            case 5: return (int)RaceOption.Deer;  // 鹿族


            // 兼容舊檔：6,7,9 以前的兔族耳，統一歸為黑兔
            default:
                if (hat == 6 || hat == 7 || hat == 9)
                    return (int)RaceOption.RabbitBlack;
                return (int)RaceOption.Human;
        }
    }

    void UpdateUI()
    {
        nameInputField.text = player.currentSaveName;

        //hairLabel.text = $"Hair_{player.YYY_headIndex}";
        //eyesLabel.text = $"Eyes_{player.YYY_eyesIndex}";
        //raceLabel.text = $"Race_{player.YYY_hatIndex}";
        //classLabel.text = $"Class_{player.YYY_bodyIndex}";

        hairLabel.text = $"{LHair()}_{player.YYY_headIndex}";
        eyesLabel.text = $"{LEyes()}_{player.YYY_eyesIndex}";

        int ro = RaceOptionFromHat_Simple(player.YYY_hatIndex);
        raceOptionIndex = ro; // 保持一致
        raceLabel.text = $"{LRaceName(ro)}";
        //raceLabel.text = $"{LRace()}_{LRaceName(raceOptionIndex)}";

        classLabel.text = $"{LClassName(player.CurrentProfession)}";

    }//捏人界面UI显示

    // 语言：0 日语 1 简中 2 繁中 3 英语 4 韩语
    private int Lang => PlayerPrefs.GetInt("language");

    private static readonly string[,] LABELS = new string[,]
    {
    //        Hair     Eyes  
    /*JP*/ { "髪型",   "瞳"},
    /*CN*/ { "头发",   "眼睛"},
    /*TC*/ { "頭髮",   "眼睛" },
    /*EN*/ { "Hair",   "Eyes"},
    /*KR*/ { "머리",   "눈" }
    };

    // 语言：0 日 1 简中 2 繁中 3 英 4 韩
    private static readonly string[,] RACE_NAMES = new string[,]
    {
    // Human,     Elf,      HighElf,            RabbitBlack,              RabbitWhite,               Demon,     HighDemon
    { "人間",    "エルフ",  "<color=#ADD8E6>ハイエルフ</color>",        "北方ラビット",      "南方ラビット",      "魔族",     "<color=#ADD8E6>上位魔族</color>", "鬼族",  "鹿族" },   // JP
    { "人类",    "精灵",    "<color=#ADD8E6>高等精灵</color>",          "北方兔族",          "南方兔族",          "魔族",     "<color=#ADD8E6>高等魔族</color>", "鬼族",  "鹿族" },   // CN
    { "人類",    "精靈",    "<color=#ADD8E6>高等精靈</color>",          "北方兔族",          "南方兔族",          "魔族",     "<color=#ADD8E6>高等魔族</color>", "鬼族",  "鹿族" },   // TC
    { "Human",   "Elf",     "<color=#ADD8E6>High Elf</color>",         "Northern Rabbit",  "Southern Rabbit", "Demon",   "<color=#ADD8E6>High Demon</color>","Oni", "Deer"  },     // EN
    { "인간",    "엘프",    "<color=#ADD8E6>하이 엘프</color>",         "북부 토끼족",       "남부 토끼족",       "마족",     "<color=#ADD8E6>상위 마족</color>", "오니", "사슴족" }  // KR
    };

    private static readonly string[,] CLASS_NAMES = new string[,]
    {
    //            Warrior    Archer     Mage
    /*JP*/ { "戦士",      "弓手",     "魔法使い" },
    /*CN*/ { "战士",      "射手",     "法师" },
    /*TC*/ { "戰士",      "射手",     "法師" },
    /*EN*/ { "Warrior",   "Archer",   "Mage" },
    /*KR*/ { "전사",      "궁수",     "마법사" }
    };


    #region  种族介绍提示
    // 9 个种族顺序：Human, Elf, HighElf, RabbitBlack, RabbitWhite, Demon, HighDemon, Oni, Deer
    private static readonly string[,] RACE_DESC = new string[,]
    {
{ // JP
"大陸で最も好戦的な種族。<color=#ADD8E6>体力</color>と<color=#ADD8E6>近接戦闘</color>に優れる。\n"+
"<color=#FF8800>【狩猟】敵撃破時に追加経験値を得る可能性</color>",

"<color=#ADD8E6>射撃</color>と<color=#ADD8E6>体力</color>に長ける種族。現在は多くが人間に隷属している。\n"+
"<color=#FF8800>【精密】射撃武器で低HPの敵を即死させる可能性</color>",

"希少な高位エルフ。<color=#ADD8E6>魔法</color>と<color=#ADD8E6>近接</color>に優れる。\n"+
"<color=#FF8800>【狩猟】敵撃破時に追加経験値を得る可能性</color>\n"+
"<color=#FF8800>【精密】射撃武器で低HPの敵を即死させる可能性</color>\n"+
"<color=#FF8800>【隠秘化】体力を消費して半透明化し、一時的に敵の視線をそらす</color>",

"数が多い兎族。敏捷だが体力が低く、<color=#ADD8E6>射撃</color>と<color=#ADD8E6>近接</color>が得意。\n"+
"<color=#FF8800>【敏捷】回避/ダッシュが体力を消費しない場合があり、少量回復する</color>",

"温和な兎族の一派。敏捷で体力は低いが、<color=#ADD8E6>魔法</color>と<color=#ADD8E6>射撃</color>に優れる。\n"+
"<color=#FF8800>【敏捷】回避/ダッシュが体力を消費しない場合があり、少量回復する</color>",

"深淵の混血。<color=#ADD8E6>体力</color>と<color=#ADD8E6>魔法</color>に優れ、魔族化が可能。\n"+
"<color=#FF8800>【魔族化】最大HP1/2、攻撃力の1/4を吸収回復</color>",

"純血の上位魔族。<color=#ADD8E6>魔法</color>に特化し、強力な儀式を操る。\n"+
"<color=#FF8800>【狩猟】敵撃破時に追加経験値を得る可能性</color>\n"+
"<color=#FF8800>【魔族化】最大HP1/2、攻撃力の1/4を吸収回復</color>\n"+
"<color=#FF8800>【自然】回復時に追加でHPを多く回復する</color>",

"魔族の傍系で地底旧都の血を引く。男鬼族は屈強で醜く、女鬼族は希少で異様に美しい。<color=#ADD8E6>近接火力</color>に優れる。\n"+
"<color=#FF8800>【剛毅】ガードしていなくても一度だけ攻撃を完全無効化する可能性</color>",

"古い森の精霊に近しい種族。従順な鹿族の奴隷は幸運を呼ぶとされる。高い<color=#ADD8E6>体力</color>を誇る。\n"+
"<color=#FF8800>【辟邪】凍結・毒・火傷・麻痺などの状態異常を無効化</color>\n"+
"<color=#FF8800>【自然】回復時に追加でHPを多く回復する</color>"
},

// CN(简体)
{
"整片大陆上最好战的种族，四处征服和奴役其他种族。她们在<color=#ADD8E6>生命值</color>与<color=#ADD8E6>近战</color>上有优势。\n"+
"<color=#FF8800>【狩猎】在击败敌人后一定几率额外经验</color>",

"<color=#ADD8E6>射击</color>与<color=#ADD8E6>生命值</color>上有优势的种族，多数被人类奴役。\n"+
"<color=#FF8800>【精准】射击武器对低生命值敌人有几率一击必杀</color>",

"精灵中的珍稀品种，具有强大的<color=#ADD8E6>法术</color>与<color=#ADD8E6>近战</color>能力。\n"+
"<color=#FF8800>【狩猎】击败敌人后有几率额外经验</color>\n"+
"<color=#FF8800>【精准】射击对低生命值敌人有几率一击必杀</color>\n"+
"<color=#FF8800>【隐秘化】消耗体力隐身，短时间转移敌人视线</color>",

"数量众多且敏捷的兔族，生命值较低，擅长<color=#ADD8E6>射击</color>与<color=#ADD8E6>近战</color>。\n"+
"<color=#FF8800>【敏捷】闪避/冲刺可能不消耗体力并恢复少量体力</color>",

"温顺的草原兔族，敏捷但生命值较低，擅长<color=#ADD8E6>法术</color>与<color=#ADD8E6>射击</color>。\n"+
"<color=#FF8800>【敏捷】闪避/冲刺可能不消耗体力并恢复少量体力</color>",

"深渊的混血，擅长<color=#ADD8E6>生命值</color>和<color=#ADD8E6>法术</color>，可进入魔族化。\n"+
"<color=#FF8800>【魔族化】最大生命 50%，吸收攻击力 25% 生命值</color>",

"纯血上位魔族，擅长<color=#ADD8E6>法术</color>，能操控强力仪式。\n"+
"<color=#FF8800>【狩猎】击败敌人后有额外经验概率</color>\n"+
"<color=#FF8800>【魔族化】最大生命 50%，吸收攻击力 25% 生命值</color>\n"+
"<color=#FF8800>【自然】恢复生命时额外恢复部分生命值</color>",

"魔族的旁系，来源于地底旧都。男性鬼族强壮丑陋，女性鬼族稀少却异常貌美，擅长<color=#ADD8E6>近战</color>。\n"+
"<color=#FF8800>【坚韧】一定几率在未防御时完全免疫一次伤害</color>",

"与古老森林灵魂亲近的种族。传闻听话的鹿族女奴能带来好运，拥有高<color=#ADD8E6>生命值</color>。\n"+
"<color=#FF8800>【辟邪】不会被冻结、中毒、灼烧、麻痹等异常状态</color>\n"+
"<color=#FF8800>【自然】恢复生命时额外恢复部分生命值</color>"
},

// TC(繁中)
{
"尚武的人類種族，在<color=#ADD8E6>生命</color>與<color=#ADD8E6>近戰</color>上佔優勢。\n"+
"<color=#FF8800>【狩獵】擊敗敵人後有機率獲得額外經驗</color>",

"擅長<color=#ADD8E6>射擊</color>與<color=#ADD8E6>魔法</color>，多數已被人類奴役。\n"+
"<color=#FF8800>【精準】射擊武器對低生命值敵人有機率一擊必殺</color>",

"稀有的高等精靈，擅長<color=#ADD8E6>魔法</color>與<color=#ADD8E6>近戰</color>。\n"+
"<color=#FF8800>【狩獵】擊敗敵人後有額外經驗機率</color>\n"+
"<color=#FF8800>【精準】射擊可對低HP敵人一擊必殺</color>\n"+
"<color=#FF8800>【隱秘化】消耗體力進入隱身，短時間轉移敵人視線</color>",

"大量的兔族，敏捷但生命值低，擅長<color=#ADD8E6>射擊</color>與<color=#ADD8E6>近戰</color>。\n"+
"<color=#FF8800>【敏捷】閃避/衝刺有機率不消耗體力並恢復少量體力</color>",

"草原鹿族溫順敏捷，擅長<color=#ADD8E6>魔法</color>與<color=#ADD8E6>射擊</color>。\n"+
"<color=#FF8800>【敏捷】閃避/衝刺可能不消耗體力並恢復體力</color>",

"來自深淵的混血種族，擅長<color=#ADD8E6>生命值</color>與<color=#ADD8E6>魔法</color>。\n"+
"<color=#FF8800>【魔族化】最大生命值50%，攻擊吸血25%</color>",

"純血上位魔族，魔法能力極強。\n"+
"<color=#FF8800>【狩獵】擊敗敵人可獲得額外經驗</color>\n"+
"<color=#FF8800>【魔族化】HP50%，吸收攻擊力25%</color>\n"+
"<color=#FF8800>【自然】生命恢復時額外回復</color>",

"魔族旁系，來自地底舊都。男性鬼族粗暴，女性鬼族稀有且妖豔，擅長<color=#ADD8E6>近戰</color>。\n"+
"<color=#FF8800>【堅韌】未防禦時有機率完全免疫一次傷害</color>",

"與森林精靈親近的鹿族，常被視為帶來幸運的奴隸。擁有極高<color=#ADD8E6>生命值</color>。\n"+
"<color=#FF8800>【辟邪】免疫凍結、中毒、灼燒、麻痺等異常</color>\n"+
"<color=#FF8800>【自然】生命恢復時額外回復</color>"
},

// EN
{
"Militant humans with strong <color=#ADD8E6>HP</color> and <color=#ADD8E6>Melee</color>.\n"+
"<color=#FF8800>[Hunt] Chance for bonus EXP on kill</color>",

"Skilled in <color=#ADD8E6> ranged</color> &  <color=#ADD8E6>HP</color>, mostly enslaved.\n"+
"<color=#FF8800>[Precision] Ranged may insta-kill low HP enemies</color>",

"Rare high elves with strong <color=#ADD8E6>Magic</color> & <color=#ADD8E6>Melee</color>.\n"+
"<color=#FF8800>[Hunt] Bonus EXP on kill</color>\n"+
"<color=#FF8800>[Precision] Chance for ranged instant kill</color>\n"+
"<color=#FF8800>[Veil] Consume stamina to become translucent and divert enemy attention briefly</color>",

"Agile but low HP; excels in <color=#ADD8E6>Ranged</color> & <color=#ADD8E6>Melee</color>.\n"+
"<color=#FF8800>[Agility] Dodge/Dash may cost no stamina & restore some</color>",

"Gentle agile rabbits skilled in <color=#ADD8E6>Magic</color> & <color=#ADD8E6>Ranged</color>.\n"+
"<color=#FF8800>[Agility] Dodge/Dash may cost no stamina & restore some</color>",

"Abyssal hybrids with high <color=#ADD8E6>HP</color> & <color=#ADD8E6>Magic</color>.\n"+
"<color=#FF8800>[Demon Form] Max HP 50%; absorb 25% damage as HP</color>",

"Pure-blood upper demons specialized in <color=#ADD8E6>Magic</color>.\n"+
"<color=#FF8800>[Hunt] Bonus EXP on kill</color>\n"+
"<color=#FF8800>[Demon Form] Max HP 50%, absorb 25%</color>\n"+
"<color=#FF8800>[Nature] Gain extra HP whenever healed</color>",

"Offshoots of demons from an underground old capital. Males are strong & rough, females are rare and beautiful. Strong <color=#ADD8E6>Melee</color>.\n"+
"<color=#FF8800>[Tenacity] May completely ignore one hit even without guarding</color>",

"A race close to ancient forest spirits. A gentle deer slave is said to bring luck. High <color=#ADD8E6>HP</color>.\n"+
"<color=#FF8800>[Ward] Immune to Freeze, Poison, Burn, Paralysis</color>\n"+
"<color=#FF8800>[Nature] Gain extra HP whenever healed</color>"
},

// KR
{
"호전적인 인간족. <color=#ADD8E6>체력</color>과 <color=#ADD8E6>근접전</color>에 강함.\n"+
"<color=#FF8800>[사냥] 처치 시 추가 경험치 획득 가능</color>",

"사격과 마법에 능하며 다수는 노예 상태.\n"+
"<color=#FF8800>[정밀] 저HP 적 즉사 가능</color>",

"희귀한 상위 엘프. <color=#ADD8E6>마법</color>과 <color=#ADD8E6>근접전</color> 모두 강함.\n"+
"<color=#FF8800>[사냥] 처치 시 추가 경험치</color>\n"+
"<color=#FF8800>[정밀] 저HP 즉사 가능</color>\n"+
"<color=#FF8800>[은밀화] 체력을 소모하여 투명화하고 잠시 적의 시선을 돌린다</color>",

"민첩하지만 체력이 낮아 <color=#ADD8E6>사격</color>과 <color=#ADD8E6>근접전</color>에 특화.\n"+
"<color=#FF8800>[민첩] 회피/대시 시 스태미나 미소모 및 회복</color>",

"순한 성격의 토끼족. <color=#ADD8E6>마법</color>과 <color=#ADD8E6>사격</color>에 강함.\n"+
"<color=#FF8800>[민첩] 회피/대시 시 스태미나 미소모 및 회복</color>",

"심연의 혼혈. <color=#ADD8E6>체력</color>과 <color=#ADD8E6>마법</color>이 뛰어나며 마족화 가능.\n"+
"<color=#FF8800>[마족화] HP25%, 공격력50% 흡혈</color>",

"순혈 상위 마족. <color=#ADD8E6>마법</color>에 특화됨.\n"+
"<color=#FF8800>[사냥] 처치 시 추가 경험치</color>\n"+
"<color=#FF8800>[마족화] HP25%, 흡혈50%</color>\n"+
"<color=#FF8800>[자연] 회복 시 추가 HP 회복</color>",

"마족의 방계로 지하 옛도시 출신. 남성은 거칠고 강하며 여성은 드물고 아름답다. 뛰어난 <color=#ADD8E6>근접전</color> 능력을 보유.\n"+
"<color=#FF8800>[강인] 가드 없이도 한 번의 공격을 완전 무효화 가능</color>",

"고대 숲의 정령과 가까운 종족. 순종적인 사슴족 노예는 행운을 가져온다고 함. 높은 <color=#ADD8E6>체력</color> 보유.\n"+
"<color=#FF8800>[벽사] 빙결·중독·화상·마비 등 상태 이상 면역</color>\n"+
"<color=#FF8800>[자연] 회복 시 추가 HP 회복</color>"
},
    };
    private static readonly string[] LUNA_LOCK = new string[]
  {
        "このキャラクターは外見・衣装・種族などを変更できません。",
        "此角色无法更换外貌、服装、种族等。",
        "此角色無法更換外貌、服裝、種族等。",
        "This character’s appearance, outfit, and race cannot be changed.",
        "이 캐릭터는 외형·의상·종족 등을 변경할 수 없습니다。"
  };
    // 由 hatIndex 反推 7个种族的 UI 索引（你当前的简化规则）
    #endregion



    private string LHair() => LABELS[Lang, 0];
    private string LEyes() => LABELS[Lang, 1];


    private string LRaceName(int idx) => RACE_NAMES[Lang, idx];
    private string LClassName(int idx) => CLASS_NAMES[Lang, idx];

    #endregion


    public void OnConfirmNameInput()
    {
        if (player.currentSaveName == "ルナ" || player.currentSaveName == "露娜" || player.currentSaveName == "Luna" || player.currentSaveName == "루나") { return; }


        string newName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(newName))
            return; // 不处理空输入

        string oldName = player.currentSaveName;

        // 避免重复操作
        if (oldName == newName) return;

        // 如果该名称已存在（可选：检查冲突）
        if (SaveManager.HasSave(newName))
        {
            Debug.LogWarning("已存在此命名的存档！");
            return;
        }

        // 创建新存档数据（复制当前捏人数据）
        PlayerSaveData data = new PlayerSaveData
        {
            characterName = newName,
            headIndex = player.YYY_headIndex,
            eyesIndex = player.YYY_eyesIndex,
            bodyIndex = player.YYY_bodyIndex,
            legsIndex = player.YYY_legsIndex,
            hatIndex = player.YYY_hatIndex,
            weaponIndex = player.weaponIndex,

            professionIndex = player.CurrentProfession,

            level = 1,
            exp = 0,
            maxHP = UnityEngine.Random.Range(1000, 1200),
            meleeDamage = UnityEngine.Random.Range(50, 100),
            shootDamage = UnityEngine.Random.Range(50, 100),
            spellDamage = UnityEngine.Random.Range(50, 100),


            weaponAtk = 0,
            armorDef = 0,
            stockingDef = 0,




        };

        //武器还是得分开，法术武器伤害最高，其次近战武器，其次远程武器
        switch (data.professionIndex)
        {
            case 0:
                data.weaponAtk = 100;
                data.armorDef = 30;
                data.stockingDef = 20;
                break;
            case 1:
                data.weaponAtk = 70;
                data.armorDef = 40;
                data.stockingDef = 40;
                break;
            case 2:
                data.weaponAtk = 120;
                data.armorDef = 15;
                data.stockingDef = 15;
                break;
        }

        SaveManager.Save(data); // ✅ 保存新名字存档
        SaveManager.DeleteSave(oldName); // 🗑️ 删除旧存档

        player.currentSaveName = newName; // 更新记录
        Debug.Log($"名称更换成功：{oldName} → {newName}");

        RefreshSaveSlots();//每次单独更新名称也需要刷新存档界面

    }// 玩家输入新名字，调用此函数（将当前玩家身上的值重新带入更改名字的存档）

    public void OnConfirm()
    {
        ApplyRaceBonus();//在点击确定的时候增加种族加成

        //抹去当前名称，下次捏人再度选中名称
        player.currentSaveName = null;

        //显示存档界面，隐藏捏人界面
        CreateCavans.SetActive(false);
        SaveCavans.SetActive(true);


        // ✅ 先刷新存档UI
        RefreshSaveSlots();//每次确定种族天赋后也需要刷新存档界面

        // ✅ 等待一帧后再选中第一个（或最新一个）存档，保证UI已生成完
        Invoke(nameof(SelectNewestSlotSafe), 0.05f);

        //UpdateCurrentSelection(currentIndex);//完成捏人后再一次回到当前选中



        // UI状态恢复

        CurrentChooseList = 2;//返回存档界面

        //再度把捏人的检索回到名字
        CreatNewcurrentIndex = 0;
        UpdateHighlight();

        //重新恢复上下可移动
        isInputing = false;




    }//玩家点击Ok



    public void ApplyRaceBonus()
    {
        switch (player.YYY_hatIndex)
        {
            case 1:
                player.maxHealth += 500;
                player.MeleeDamage += 50;
                break;
            case 2:
                player.maxHealth += 500;
                player.ShootDamage += 50;
                break;
            case 3:
                player.SpellDamage += 50;
                player.MeleeDamage += 50;
                break;
            case 4:
                player.MeleeDamage += 50;
                player.ShootDamage += 50;
                break;
            case 10:
                player.ShootDamage += 50;
                player.SpellDamage += 50;
                break;
            case 12:
                player.maxHealth += 500;
                player.SpellDamage += 50;
                break;
            case 11:
                player.SpellDamage += 100;
                break;
            case 8:
                player.MeleeDamage += 100;
                break;
            case 5:
                player.maxHealth += 500;
                break;
        }

        player.SaveCurrent();//存种族加成

        Debug.Log("增加种族加成");
    }



    [Header("告知RoomGenerator产生队友")]
    public RoomGenerator _RoomGenerator;
    bool isCreateFriend = false;
    bool isShowTitle = false;
    public void OpenCloseMenu()
    {
        Debug.Log("OpenCloseMenu");

        if (CurrentMode == 0)
        {
            //ToCGPage();//打开CG界面
            Invoke("ToCGPage", 0.1f);
        }

        if (CurrentMode == 1)
        {
            if (SaveManager.CountSaves() > 0) // 没有任何存档无法开始
            {
                //战斗中无法开始

                // && player.speed == 0//你知道为什么要这里加这个吗，因为手机端，点开菜单按钮是可以绕开的
                if (GameFlowData.BulletCanThroughtWall == false)
                {
                    if (!isPause)
                    {


                        if (GameFlowData.nextScene == "Arena" || GameFlowData.nextScene == "Dungeon")
                        {
                            //提示次模式中无法打开菜单
                            AudioManager.instance.AudioPlay(AudioManager.instance.SE_Reba);

                            _RoomGenerator.ShowInformationOfStage(8);

                            return;
                        }


                        MainCamera.SetInteger("View", 0);
                        Common_All.SetActive(false);
                        ShowSaveCavans.SetActive(true);



                        player.isInputBlocked = true;//切断玩家的方向攻击等输入

                        RefreshSaveSlots();//只有在打开存档菜单时更新

                        CurrentChooseList = 2;

                        player.CheckDemonMode();//从魔族化变回

                        isPause = true;
                    }
                    else
                    {



                        MainCamera.SetInteger("View", 2);
                        Common_All.SetActive(true);
                        ShowSaveCavans.SetActive(false);


                        player.isInputBlocked = false;//恢复玩家的方向攻击等输入

                        player.currentSaveName = currentSelectedSlot.Data.characterName;//开始游戏时，将这个存档名称带入Player




                        //只能生成一次队友目前的模式中只有这两种允许队友
                        if (GameFlowData.nextScene == "Dungeon" || GameFlowData.nextScene == "Arena")
                        {
                            if (!isCreateFriend) { _RoomGenerator.SetAllFriends(); isCreateFriend = true; }
                        }

                        if (!isShowTitle)
                        {

                            StageInformation.SetActive(true);

                            isShowTitle = true;


                            ResetBuffs();//开始的时候Buff全部清零


                        }//只展示一次关卡信息



                        CurrentChooseList = -4;

                        isPause = false;
                    }

                    //ToDo 【修改】这里是OpenClose

                    //isPause = !isPause;

                    //isPause = false;
                }
                else
                {

                    //提示战斗中无法打开菜单
                    AudioManager.instance.AudioPlay(AudioManager.instance.SE_Reba);

                    _RoomGenerator.ShowInformationOfStage(5);
                }




            }
            else
            {

                //提示需要创建角色
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Reba);

                _RoomGenerator.ShowInformationOfStage(3);
            }


        }



    }//从存档界面进入游戏界面(如果没有存档无法这么做)，再从游戏界面进入存档界面

    public void To_CGScence()
    {

        CurrentChooseList = 6;

        ShowSaveCavans.SetActive(false);
        MainCamera.SetInteger("View", 1);
    }




    #endregion



    /// <summary>
    /// 存档显示
    /// </summary>
    #region
    [Header("寻找玩家")]
    public GameObject _Player;//玩家
    public Player player;

    [Header("存档显示")]
    public GameObject saveSlotPrefab;
    public Transform saveSlotParent;

    void Start()
    {
        //ToDo 【修改】再度强制去除BulletCanThroughtWall
        GameFlowData.BulletCanThroughtWall = false;//每次场景刷新的时候这个清掉



        Show_bestWave();//显示决斗场最高记录

        Show_DungeonRecord();//显示地下城最高记录



        // 初始化音量
        //SetBGMVolune(BGMVolume);
        //SetSEVolune(SEVolume);
        LoadVolumes();


        CGUnclockStart();//检测CG解锁
        ChapterUnclockStart();//检测Chapter解锁
        CG_End_UnclockStart();//检测CG结局解锁

        string folder = Application.persistentDataPath + "/Saves/";
        if (!Directory.Exists(folder)) return;

        foreach (string file in Directory.GetFiles(folder, "save_*.json"))
        {
            string json = File.ReadAllText(file);
            PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);

            GameObject slot = Instantiate(saveSlotPrefab, saveSlotParent);
            slot.GetComponent<SaveSlotUI>().SetInfo(data, skinParts);  // ✅ 新增头像预览

            //SetCurrentSlot(slot.GetComponent<SaveSlotUI>());// 自动选中

            saveSlots.Add(slot.GetComponent<SaveSlotUI>());//把这个存档加入列表
        }

        UpdateCurrentSelection(0);  // 初始化列表内选中第一个

        UpdateScrollLimits();//更新上下翻页范围





        //隐藏buff
        ResetBuffs();



        // 1) 每日登录触发：若是新的一天 -> 给所有存档+接客并返回总收益
        if (DailyLogin.TryGrantForToday(perServiceReward, out int added, out int income))
        {
            if (income > 0)
                ChangeMoney(income); // ★ 发钱一次
        }

        // 3)（可选）如果当前在调教所界面，显示今日统计绿字
        UpdateCgDailyTexts();



        // 读取当前难度，默认 0（简单）
        currentDifficulty = PlayerPrefs.GetInt(PREF_KEY, 0);
        UpdateDifficultyUI();

        CheckcoverPanel();//封面只显示一次


        /////////////////////////////////////////////////【电脑控制/Steam】/////////////////////////////////////////////////
        InitScreenMode();//读取窗口设置


    }//读取，显示存档

    #region 当前选中的是不是露娜
    private static readonly string[] LunaNames = { "ルナ", "露娜", "Luna", "루나" };
    private bool IsLuna(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        name = name.Trim();
        foreach (var n in LunaNames)
            if (string.Equals(name, n, StringComparison.OrdinalIgnoreCase))
                return true;
        return false;
    }
    #endregion

    [Header("创建新角色需要花费显示")]
    public Text CreateCostText; // 显示价码牌用的UI文本
    public void UpdateCreateCostText()
    {
        int saveCount = SaveManager.CountSaves();
        int nextCost = 0;
        Debug.Log("目前奴隶数" + saveCount);
        if (saveCount == 0) nextCost = 0;
        else nextCost = 1000 * saveCount;


        #region
        int currentMoney = PlayerPrefs.GetInt("Money", 0);

        // 金币够则黄色，不够红色
        string colorTag = currentMoney >= nextCost ? "#FFD700" : "#FF4040";
        string costText = $"<color={colorTag}>{nextCost}</color>";
        #endregion


        // 文字多语言（建议统一管理）
        switch (PlayerPrefs.GetInt("language"))
        {
            case 0: // 日语
                CreateCostText.text = $"次の奴隷生成費用：{costText} 金貨";
                break;
            case 1: // 简体
                CreateCostText.text = $"创建下一个奴隶需要：{costText} 金币";
                break;
            case 2: // 繁体
                CreateCostText.text = $"建立下一個奴隸需要：{costText} 金幣";
                break;
            case 3: // 英语
                CreateCostText.text = $"Next creation cost: {costText} gold";
                break;
            case 4: // 韩语
                CreateCostText.text = $"다음 노예 생성 비용: {costText} 골드";
                break;
        }
    }//更新当前创建奴隶费用
    public void CreateNewSave()
    {

        int currentMoney = PlayerPrefs.GetInt("Money", 0);
        int saveCount = SaveManager.CountSaves(); // 当前已有存档数

        int cost = 0; // 生成花费


        #region  人数上限锁
        int maxSaves = 15; // 最大可创建奴隶数
        // 判断数量上限
        if (saveCount >= maxSaves)
        {
            Debug.Log("已达最大奴隶数量！");
            player.frameEvents._Attack_pai1();
            _RoomGenerator.ShowInformationOfStage(-3);
            return;
        }
        #endregion



        // 费用规则
        if (saveCount == 0) cost = 0;
        else cost = 1000 * saveCount; // 1个=1000, 2个=2000, 3个=3000, ...


        // 检查金币是否足够
        if (currentMoney >= cost)
        {
            // 扣钱
            ChangeMoney(-cost);

            // 创建新角色（原逻辑）
            #region   跳出捏人界面等

            player.currentSaveName = null;   // ← 防止 SaveCurrent 误把老档当“当前档”

            player._CreateNewSkin();

            #region 种族选项需要根据耳朵来设置
            raceOptionIndex = RaceOptionFromHat_Simple(player.YYY_hatIndex);
            if (IsLuna(player.currentSaveName)) { IntroduceOfRace.text = LUNA_LOCK[Lang]; } else { ApplyRaceSelectionSimple(); }//预先设置提示词
            UpdateUI();
            #endregion

            RefreshSaveSlots();

            //显示捏人界面，隐藏存档界面
            SaveCavans.SetActive(false);
            CreateCavans.SetActive(true);

            UpdateUI();//更新捏人界面UI

            CurrentChooseList = 1;//进入捏人界面


            nameInputField.ActivateInputField(); // ✅ 激活输入框并进入编辑

            #endregion




            Debug.Log($"已花费 {cost} 金币创建新角色（当前共有 {saveCount + 1} 个角色）");


            // 更新显示价码
            UpdateCreateCostText();//购买完
        }
        else
        {
            Debug.Log("金币不足，无法创建新角色！");
            player.frameEvents._Attack_pai1();
            _RoomGenerator.ShowInformationOfStage(-2);
        }






    }//点击【＋】就会随机存档

    public void RefreshSaveSlots()//新增存档，新增存档时更换名字，新增存档时更换皮肤，删除存档,播放CG
    {

        saveSlots.Clear();  // 清空之前列表

        // 清除已有的
        foreach (Transform child in saveSlotParent)
        {
            Destroy(child.gameObject);
        }

        string folder = Application.persistentDataPath + "/Saves/";
        if (!Directory.Exists(folder)) return;

        foreach (string file in Directory.GetFiles(folder, "save_*.json"))
        {
            string json = File.ReadAllText(file);
            PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);

            GameObject slot = Instantiate(saveSlotPrefab, saveSlotParent);
            slot.GetComponent<SaveSlotUI>().SetInfo(data, skinParts);  // ✅ 新增头像预览



            //SetCurrentSlot(slot.GetComponent<SaveSlotUI>());// 这个是让最后一个高亮显示

            saveSlots.Add(slot.GetComponent<SaveSlotUI>());//把这个存档加入列表
        }


        //UpdateCurrentSelection(0);  // 初始化列表内选中第一个

        Invoke("UpdateScrollLimits", 0.1f);//更新上下翻页范围【一定要延迟一下，等物体销毁光重置光】



    }//刷新当前存档UI

    public void SelectFirstSlotSafe()
    {
        if (saveSlots.Count > 0)
        {
            UpdateCurrentSelection(0);
        }
        else
        {
            currentSelectedSlot = null;
            currentIndex = 0;
        }
    }//检测当前存档有没有，有的话选中第一个

    public void SelectNewestSlotSafe()
    {
        if (saveSlots.Count == 0) return;

        // 如果是新增角色，默认选中最后一个
        int newestIndex = saveSlots.Count - 1;

        UpdateCurrentSelection(newestIndex);
    }//检测新增的角色，如果是新增角色，默认选中最后一个

    //////////////////////列表显示存档，方向键切换当前选中按钮//////////////////////////////////
    public List<SaveSlotUI> saveSlots = new List<SaveSlotUI>();
    public int currentIndex = 0;

    public void UpdateCurrentSelection(int newIndex)
    {
        if (saveSlots.Count == 0) return;

        // 清理已销毁的引用
        saveSlots.RemoveAll(s => s == null);

        newIndex = Mathf.Clamp(newIndex, 0, saveSlots.Count - 1);

        if (currentSelectedSlot != null)
            currentSelectedSlot.SetHighlight(false);

        currentIndex = newIndex;
        currentSelectedSlot = saveSlots[currentIndex];
        currentSelectedSlot.SetHighlight(true);

        currentSelectedSlot.DelayChoose();//高亮显示与展示当前角色绑定，一旦高亮选中，马上导入这个角色信息

    }//切换选中当前角色


    [Header("删除存档")]
    public GameObject MakeSureDeleteCurrentSave;


    public void DeleteCurrentSelection()
    {
        // if (saveSlots.Count == 0) return;
        if (currentSelectedSlot == null) return;

        // 先删除角色，再加钱（防止残留重复触发）
        currentSelectedSlot.DeleteCurrentSave();
        // 加钱（用你的 ChangeMoney；若不在同脚本，换成 UIManager.instance.ChangeMoney(...))
        ChangeMoney(pendingDeletePrice);

        Invoke("CancelDelete", 0.1f);//目前暂时这么做，以防确定按太快直接跳到捏人界面


        UpdateCreateCostText();//更新价码牌

    }//删除这个角色

    public void CancelDelete()
    {
        MakeSureDeleteCurrentSave.SetActive(false);
        CurrentChooseList = 2;//返回存档界面
    }

    int pendingDeletePrice;//当前奴隶估价
    string pendingDeleteName;//当前性奴名字
    public Text ConfirmText; // 弹窗里的文本
    public void TryDelete()
    {
        MakeSureDeleteCurrentSave.SetActive(true);
        CurrentChooseList = -1;//弹出确认删除存档框


        //更改提示，是否出售这个性奴？你会获得XX金钱
        PlayerSaveData data = SaveManager.Load(currentSelectedSlot.Data.characterName);
        pendingDeleteName = currentSelectedSlot.Data.characterName;
        pendingDeletePrice = SlavePricing.CalcPrice(data);


        // 限制：若只剩1个存档，强制为0
        int saveCount = SaveManager.CountSaves(); // 当前已有存档数
        if (saveCount <= 1)
            pendingDeletePrice = 0;



        int lang = PlayerPrefs.GetInt("language");
        if (ConfirmText != null)
            ConfirmText.text = SellTexts.Build(lang, pendingDeleteName, pendingDeletePrice);

    }

    //////////////////////高亮显示//////////////////////////////////

    [HideInInspector]
    public SaveSlotUI currentSelectedSlot = null;

    public void SetCurrentSlot(SaveSlotUI newSlot)
    {
        if (currentSelectedSlot != null)
        {
            currentSelectedSlot.SetHighlight(false);
        }

        currentSelectedSlot = newSlot;
        currentSelectedSlot.SetHighlight(true);
    }

    //////////////////////头像贴图显示//////////////////////////////////

    public SkinPartsDatabase skinParts;


    //////////////////////上下翻页显示//////////////////////////////////
    public RectTransform saveContainer; // Save_All


    private int itemsPerRow = 5;//一行5个
    private int rowsVisible = 2; //可见2行
    private int totalItems => saveContainer.childCount;//目前的存档数

    public int totalRows => Mathf.CeilToInt((float)totalItems / itemsPerRow);//存档需要几行显示
    public int currentRow = 0;//目前你在第几行(0最上面 1下面一行 以此类推)

    public GameObject ScrollUp_Button, ScrollDown_Button;//上下翻转按钮

    public float rowHeight = 350f; //每行的高度

    void UpdateScrollLimits()
    {
        //Debug.Log("目前的存档数" + totalItems);
        //Debug.Log("目前需要几行显示所有存档" + totalRows);

        // 回到最上面
        currentRow = 0;
        saveContainer.anchoredPosition = new Vector2(saveContainer.anchoredPosition.x, 0);

        // 更新按钮显示
        UpdateScrollButtons();

    }//更新存档数确认翻页

    void UpdateScrollButtons()
    {
        // 顶部不能再上翻
        if (currentRow == 0) { ScrollUp_Button.SetActive(false); }
        else { ScrollUp_Button.SetActive(true); }

        // 底部不能再下翻
        if (currentRow < totalRows - rowsVisible) { ScrollDown_Button.SetActive(true); }
        else { ScrollDown_Button.SetActive(false); }

        if (saveSlotParent.childCount <= 10)
        {
            ScrollDown_Button.SetActive(false);
            //Debug.Log("目前需要几行显示所有存档[!!!!]" + totalRows);
        }//存档数小于等于10也不能下翻

    }//更新按钮

    public void ScrollUp()
    {
        currentRow--;

        saveContainer.anchoredPosition = new Vector2(saveContainer.anchoredPosition.x, saveContainer.anchoredPosition.y - 345);
        UpdateScrollButtons();

    }

    public void ScrollDown()
    {
        currentRow++;

        saveContainer.anchoredPosition = new Vector2(saveContainer.anchoredPosition.x, saveContainer.anchoredPosition.y + 345);
        UpdateScrollButtons();

    }


    #endregion


    /// <summary>
    /// 语言设置，声音设置
    /// </summary>
    #region
    [Header("删除全存档")]
    public GameObject MakeSureDeleteCurrentSave_All;

    public void TryDelete_All()
    {
        MakeSureDeleteCurrentSave_All.SetActive(true);
        CurrentChooseList = -2;//弹出确认删除存档框



        SettingPagecurrentIndex = 4;
        UpdateSettingPage_Highlight();
    }

    public void CancelDelete_All()
    {
        MakeSureDeleteCurrentSave_All.SetActive(false);
        CurrentChooseList = 3;//返回设置界面
    }

    public void ReStart_DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("删除存档");


        //删除C盘所有角色存档
        SaveManager.DeleteAllSaves();


        ReLoadScene();

    }//删除存档

    public void SetLanguage(int Number)
    {
        LanguagePagecurrentIndex = Number;

        SetLanguage_2();
    }
    void SetLanguage_2()
    {
        PlayerPrefs.SetInt("language", LanguagePagecurrentIndex);

        ReLoadScene();
    }

    public AudioMixer audioMixer;
    public AudioMixer BGM_Mixer;


    public Image BGM_Bar;
    public Image SE_Bar;

    public float BGMVolume = 0f;
    public float SEVolume = 0f;

    private const float MinVolume = -80f;
    private const float MaxVolume = 0f;

    // PlayerPrefs Key
    private const string KEY_BGM = "BGMVolume";
    private const string KEY_SE = "SEVolume";

    //=======================
    //   读取保存的音量
    //=======================
    private void LoadVolumes()
    {
        BGMVolume = PlayerPrefs.GetFloat(KEY_BGM, -10f);  // 默认值你自己决定
        SEVolume = PlayerPrefs.GetFloat(KEY_SE, -10f);

        // 应用到Mixer + UI
        SetBGMVolune(BGMVolume, save: false);
        SetSEVolune(SEVolume, save: false);
    }

    //=======================
    //   保存音量
    //=======================
    private void SaveVolumes()
    {
        PlayerPrefs.SetFloat(KEY_BGM, BGMVolume);
        PlayerPrefs.SetFloat(KEY_SE, SEVolume);
    }


    //-------- SE --------
    public void SetSEVolune(float value, bool save = true)
    {
        SEVolume = Mathf.Clamp(value, MinVolume, MaxVolume);

        audioMixer.SetFloat("MainVolume", SEVolume);
        SE_Bar.fillAmount = Mathf.InverseLerp(MinVolume, MaxVolume, SEVolume);

        if (save) SaveVolumes();
    }

    public void SE_Up()
    {
        SetSEVolune(SEVolume + 10f);
        Debug.Log("拉高 SE 音量：" + SEVolume);
    }

    public void SE_Down()
    {
        SetSEVolune(SEVolume - 10f);
        Debug.Log("降低 SE 音量：" + SEVolume);
    }

    //-------- BGM --------
    public void SetBGMVolune(float value, bool save = true)
    {
        BGMVolume = Mathf.Clamp(value, MinVolume, MaxVolume);

        BGM_Mixer.SetFloat("BGMVolume", BGMVolume);
        BGM_Bar.fillAmount = Mathf.InverseLerp(MinVolume, MaxVolume, BGMVolume);

        if (save) SaveVolumes();
    }

    public void BGM_Up()
    {
        SetBGMVolune(BGMVolume + 10f);
        Debug.Log("拉高 BGM 音量：" + BGMVolume);
    }

    public void BGM_Down()
    {
        SetBGMVolune(BGMVolume - 10f);
        Debug.Log("降低 BGM 音量：" + BGMVolume);
    }

    #endregion



    /// <summary>
    /// CG界面选中
    /// </summary>
    #region
    public List<CGOptionUI> cgButtons = new List<CGOptionUI>();
    int CGcurrentIndex = 0;

    public GameObject CG_BackButton;//处于CG观赏中后退按钮

    void CGUnclockStart()
    {
        foreach (var btn in cgButtons)
        {
            btn.SetUnlockedFromPrefs();
        }

        // 查找第一个已解锁的
        for (int i = 0; i < cgButtons.Count; i++)
        {
            if (cgButtons[i].unlocked)
            {
                currentIndex = i;
                break;
            }
        }
        UpdateHighlight();
    }//开始检测CG解锁数
    void MoveSelection(int direction)
    {
        // 取消旧高亮
        cgButtons[CGcurrentIndex].SetHighlight(false);

        // 循环查找下一个已解锁的项
        int max = cgButtons.Count;
        for (int i = 1; i < max; i++)
        {
            int newIndex = (CGcurrentIndex + direction * i + max) % max;
            if (cgButtons[newIndex].unlocked)
            {
                CGcurrentIndex = newIndex;
                break;
            }
        }

        // 更新高亮
        UpdateHighlight_CG();
    }//切换当前选中

    void UpdateHighlight_CG()
    {
        for (int i = 0; i < cgButtons.Count; i++)
        {
            cgButtons[i].SetHighlight(i == CGcurrentIndex);
        }
    }


    public void PlayPlayerCG(string CGName)
    {
        // 先把当前选中改成这次点的按钮
        int idx = cgButtons.FindIndex(b => b.unlocked && b.cgKey == CGName);
        if (idx >= 0)
        {
            CGcurrentIndex = idx;
            UpdateHighlight_CG();
        }

        player.ForCGRandomEnemySkin();
        player.frameEvents.audioS.Stop();


        if (!string.IsNullOrEmpty(CGName))
        {
            player.anim.Play("CG/" + CGName);
        }

        CG_BackButton.SetActive(true);//显示CG观赏后退按钮

        //增加接客侍奉次数记录
        //player.currentSaveName = currentSelectedSlot.Data.characterName;//先带进去,让存档
        //player.ServiceCount();
        //
        ////唯一需要实时更新的
        //RefreshSaveSlots();

        //currentSelectedSlot.Choose();
        //currentSelectedSlot.CurrentArmorDefence.text = currentSelectedSlot.Data.serviceCount.ToString();
        //currentSelectedSlot.SetInfo(currentSelectedSlot.Data, skinParts);
    }

    #endregion


    /// <summary>
    /// Chapter界面选中
    /// </summary>
    #region
    public List<CGOptionUI> chapterButtons = new List<CGOptionUI>();
    int ChapterCurrentIndex = 0;

    void ChapterUnclockStart()
    {
        foreach (var btn in chapterButtons)
        {
            btn.SetUnlockedFromPrefs();
        }

        // 查找第一个已解锁的
        for (int i = 0; i < chapterButtons.Count; i++)
        {
            if (chapterButtons[i].unlocked)
            {
                currentIndex = i;
                break;
            }
        }
        UpdateHighlight();
    }//开始检测CG解锁数
    void MoveSelection_2(int direction)
    {
        // 取消旧高亮
        chapterButtons[ChapterCurrentIndex].SetHighlight(false);

        // 循环查找下一个已解锁的项
        int max = chapterButtons.Count;
        for (int i = 1; i < max; i++)
        {
            int newIndex = (ChapterCurrentIndex + direction * i + max) % max;
            if (chapterButtons[newIndex].unlocked)
            {
                ChapterCurrentIndex = newIndex;
                break;
            }
        }

        // 更新高亮
        UpdateHighlight_Chapter();
    }//切换当前选中

    void UpdateHighlight_Chapter()
    {
        for (int i = 0; i < chapterButtons.Count; i++)
        {
            chapterButtons[i].SetHighlight(i == ChapterCurrentIndex);
        }
    }

    public void PlayAVG(string ChapterName)
    {
        // 先把当前选中改成这次点的按钮
        int idx = chapterButtons.FindIndex(b => b.unlocked && b.cgKey == ChapterName);
        if (idx >= 0)
        {
            ChapterCurrentIndex = idx;
            UpdateHighlight_Chapter();
        }


        switch (ChapterName)
        {
            case "CG_01":
                dialogSystem.animation_number = 101;
                break;
            case "CG_02":
                dialogSystem.animation_number = 102;
                break;
            case "CG_03":
                dialogSystem.animation_number = 103;
                break;
            case "CG_04":
                dialogSystem.animation_number = 104;
                break;
            case "CG_05":
                dialogSystem.animation_number = 105;
                break;
            case "CG_06":
                dialogSystem.animation_number = 106;
                break;
            case "CG_07":
                dialogSystem.animation_number = 107;
                break;
            case "CG_08":
                dialogSystem.animation_number = 108;
                break;

            case "Chapter_01":
                dialogSystem.animation_number = 1001;
                break;
            case "Chapter_02":
                dialogSystem.animation_number = 1002;
                break;
            case "Chapter_03":
                dialogSystem.animation_number = 1003;
                break;
            case "Chapter_04":
                dialogSystem.animation_number = 1004;
                break;
            case "Chapter_05":
                dialogSystem.animation_number = 1005;
                break;
            case "Chapter_06":
                dialogSystem.animation_number = 1006;
                break;
            case "Chapter_07":
                dialogSystem.animation_number = 1007;
                break;
            case "Chapter_08":
                dialogSystem.animation_number = 1008;
                break;
            case "Chapter_09":
                dialogSystem.animation_number = 1009;
                break;
            case "Chapter_10":
                dialogSystem.animation_number = 1010;
                break;
            case "Chapter_11":
                dialogSystem.animation_number = 1011;
                break;
            case "Chapter_12":
                dialogSystem.animation_number = 1012;
                break;
            case "Chapter_13":
                dialogSystem.animation_number = 1013;
                break;
        }






    }
    #endregion


    /// <summary>
    /// 结局CG调教所界面选中
    /// </summary>
    #region
    public List<CGOptionUI> cg_End_Buttons = new List<CGOptionUI>();
    public int CG_End_currentIndex = 0;

    void CG_End_UnclockStart()
    {
        foreach (var btn in cg_End_Buttons)
        {
            btn.SetUnlockedFromPrefs();
        }

        // 查找第一个已解锁的
        for (int i = 0; i < cg_End_Buttons.Count; i++)
        {
            if (cg_End_Buttons[i].unlocked)
            {
                currentIndex = i;
                break;
            }
        }
        UpdateHighlight();
    }//开始检测CG解锁数
    void MoveSelection_3(int direction)
    {
        // 取消旧高亮
        cg_End_Buttons[CG_End_currentIndex].SetHighlight(false);


        // 循环查找下一个已解锁的项
        int max = cg_End_Buttons.Count;
        for (int i = 1; i < max; i++)
        {
            int newIndex = (CG_End_currentIndex + direction * i + max) % max;
            if (cg_End_Buttons[newIndex].unlocked)
            {
                CG_End_currentIndex = newIndex;
                break;
            }
        }

        // 更新高亮
        UpdateHighlight_CG_End();




    }//切换当前选中

    public void UpdateHighlight_CG_End()
    {
        for (int i = 0; i < cg_End_Buttons.Count; i++)
        {
            cg_End_Buttons[i].SetHighlight(i == CG_End_currentIndex);

            // 显示/隐藏对应介绍
            if (i < IntroduceOfCG.Count)
            {
                IntroduceOfCG[i].SetActive(i == CG_End_currentIndex);
            }
        }



        //不知道什么原因，CGOptionUI那里就是不执行，没有办法只能在切换阶段先这么做了
        switch (cg_End_Buttons[CG_End_currentIndex].CG_Number)
        {
            case 0:
                GameFlowData.nextScene = "CG";
                break;
            case 1:
                GameFlowData.nextScene = "CG_AVG_01";
                break;
            case 2:
                GameFlowData.nextScene = "CG_AVG_02";
                break;
            case 3:
                GameFlowData.nextScene = "CG_AVG_03";
                break;
            case 4:
                GameFlowData.nextScene = "CG_AVG_04";
                break;
            case 5:
                GameFlowData.nextScene = "CG_AVG_05";
                break;
            case 6:
                GameFlowData.nextScene = "CG_AVG_06";
                break;
            case 7:
                GameFlowData.nextScene = "CG_AVG_07";
                break;
            case 8:
                GameFlowData.nextScene = "CG_AVG_08";
                break;
        }
        Debug.Log("目前的选中的cgkey:" + cg_End_Buttons[CG_End_currentIndex].cgKey);

        Debug.Log("目前的NextScene" + GameFlowData.nextScene);
    }

    public List<GameObject> IntroduceOfCG = new List<GameObject>();

    #endregion


    /// <summary>
    /// 菜单层面多端输入
    /// </summary>
    #region
    [SerializeField] private InputActionAsset inputActions;
    private InputAction moveAction;//十字键
    private InputAction confirmAction;//J键
    private InputAction cancelAction;//K键
    private InputAction createAction;//Space键
    private InputAction deleteAction;//E键
    private InputAction pauseAction;//Esc键
    private InputAction menuAction;//L键


    private void OnEnable()
    {
        moveAction = inputActions.FindAction("Move");
        confirmAction = inputActions.FindAction("Attack");
        cancelAction = inputActions.FindAction("Dodge");
        createAction = inputActions.FindAction("Run");
        deleteAction = inputActions.FindAction("Interact");
        pauseAction = inputActions.FindAction("Pause");
        menuAction = inputActions.FindAction("Menu");

        moveAction.performed += OnMove;
        confirmAction.started += OnConfirm;
        cancelAction.started += OnCancel;
        createAction.started += OnCreate;
        deleteAction.started += OnDelete;
        pauseAction.started += OnPause;
        menuAction.started += OnMenu;


        SetMouse();


        // 延迟启用
        StartCoroutine(EnableInputsWithDelay(1f));
    }

    //这里过1秒左右登录,防止玩家一上来乱按
    private IEnumerator EnableInputsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        moveAction.Enable();
        confirmAction.Enable();
        cancelAction.Enable();
        createAction.Enable();
        deleteAction.Enable();
        pauseAction.Enable();
        menuAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        confirmAction.started -= OnConfirm;
        cancelAction.started -= OnCancel;
        createAction.started -= OnCreate;
        deleteAction.started -= OnDelete;
        pauseAction.started -= OnPause;
        menuAction.canceled -= OnMenu;

        moveAction.Disable();
        confirmAction.Disable();
        cancelAction.Disable();
        createAction.Disable();
        deleteAction.Disable();
        pauseAction.Disable();
        menuAction.Disable();
    }

    //冷却时间
    private float inputCooldown2 = 0.2f;
    private float lastInputTime2 = -999f;

    //处于打字的时候不能上下移动
    public bool isInputing = false;

    private void OnMove(InputAction.CallbackContext ctx)
    {

        #region 冷却时间
        if (Time.time - lastInputTime2 < inputCooldown2)
            return;

        lastInputTime2 = Time.time;
        #endregion

        //按下任意键
        if (coverPanel.activeSelf)
        {
            OnCoverClicked();
            return;
        }



        if (player.isInputBlocked && !isInputing)
        {
            Vector2 dir = ctx.ReadValue<Vector2>();

            //局内商店
            if (CurrentChooseList == -8)
            {

                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {

                    MoveSelection_Shop(-1);            // y>0 往上 -> 索引减
                }
                else if (dir.y < -0.5f)
                {

                    MoveSelection_Shop(+1);            // y<0 往下 -> 索引加
                }
            }

            //三选一界面
            if (CurrentChooseList == -5)
            {

                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    MoveSelection_Bonus(1);

                }
                else if (dir.x < -0.5f)
                {
                    MoveSelection_Bonus(-1);

                }
            }


            //主菜单界面
            if (CurrentChooseList == 0)
            {
                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    HomePagecurrentIndex = Mathf.Clamp(HomePagecurrentIndex - 1, 0, 9);
                    UpdateHomePage_Highlight();


                }
                else if (dir.y < -0.5f)
                {
                    HomePagecurrentIndex = Mathf.Clamp(HomePagecurrentIndex + 1, 0, 9);
                    UpdateHomePage_Highlight();


                }

                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    HomePagecurrentIndex = Mathf.Clamp(HomePagecurrentIndex - 4, 0, 8);
                    UpdateHomePage_Highlight();
                }
                else if (dir.x < -0.5f)
                {
                    HomePagecurrentIndex = Mathf.Clamp(HomePagecurrentIndex + 4, 0, 8);
                    UpdateHomePage_Highlight();
                }
            }

            //存档界面
            if (CurrentChooseList == 2)
            {
                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    UpdateCurrentSelection(currentIndex + 1);
                }
                else if (dir.x < -0.5f)
                {
                    UpdateCurrentSelection(currentIndex - 1);
                }


                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {

                    if (currentIndex - 5 < 0)
                    {
                        UpdateCurrentSelection(0);
                    }
                    else
                    {
                        UpdateCurrentSelection(currentIndex - 5);
                    }

                    if (ScrollUp_Button.activeSelf) { ScrollUp(); }

                }
                else if (dir.y < -0.5f)
                {

                    if (currentIndex + 5 > saveSlots.Count)
                    {
                        UpdateCurrentSelection(saveSlots.Count);
                    }
                    else
                    {
                        UpdateCurrentSelection(currentIndex + 5);
                    }
                    if (currentIndex >= 6 && ScrollDown_Button.activeSelf) { ScrollDown(); }

                }

            }

            //捏人界面
            if (CurrentChooseList == 1)
            {
                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    switch (CreatNewcurrentIndex)
                    {
                        case 1: HairRight.onClick.Invoke(); break;
                        case 2: EyesRight.onClick.Invoke(); break;
                        case 3: RaceRight.onClick.Invoke(); break;
                        case 4: ClassRight.onClick.Invoke(); break;
                    }
                }
                else if (dir.x < -0.5f)
                {
                    switch (CreatNewcurrentIndex)
                    {
                        case 1: HairLeft.onClick.Invoke(); break;
                        case 2: EyesLeft.onClick.Invoke(); break;
                        case 3: RaceLeft.onClick.Invoke(); break;
                        case 4: ClassLeft.onClick.Invoke(); break;
                    }
                }



                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    CreatNewcurrentIndex = Mathf.Clamp(CreatNewcurrentIndex - 1, 0, 5);
                    UpdateHighlight();


                }
                else if (dir.y < -0.5f)
                {
                    CreatNewcurrentIndex = Mathf.Clamp(CreatNewcurrentIndex + 1, 0, 5);
                    UpdateHighlight();


                }

            }

            //设置界面
            if (CurrentChooseList == 3)
            {

                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    switch (SettingPagecurrentIndex)
                    {
                        case 0:
                            BGM_Up();
                            break;
                        case 1:
                            SE_Up();
                            break;

                        case 3:
                            ScreenMode_Right();
                            break;

                    }

                }
                else if (dir.x < -0.5f)
                {

                    switch (SettingPagecurrentIndex)
                    {


                        case 0:
                            BGM_Down();
                            break;
                        case 1:
                            SE_Down();
                            break;

                        case 3:
                            ScreenMode_Left();
                            break;
                    }
                }


                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    SettingPagecurrentIndex = Mathf.Clamp(SettingPagecurrentIndex - 1, 0, 4);
                    UpdateSettingPage_Highlight();


                }
                else if (dir.y < -0.5f)
                {
                    SettingPagecurrentIndex = Mathf.Clamp(SettingPagecurrentIndex + 1, 0, 4);
                    UpdateSettingPage_Highlight();


                }
            }

            //语言界面
            if (CurrentChooseList == 4)
            {
                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    LanguagePagecurrentIndex = Mathf.Clamp(LanguagePagecurrentIndex - 1, 0, 4);
                    UpdateLanguagePage_Highlight();


                }
                else if (dir.y < -0.5f)
                {
                    LanguagePagecurrentIndex = Mathf.Clamp(LanguagePagecurrentIndex + 1, 0, 4);
                    UpdateLanguagePage_Highlight();


                }
            }

            //CG界面
            if (CurrentChooseList == 5)
            {
                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    MoveSelection(-1);

                }
                else if (dir.y < -0.5f)
                {

                    MoveSelection(1);
                }

                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    MoveSelection(9);


                }
                else if (dir.x < -0.5f)
                {
                    MoveSelection(-9);

                }
            }

            //游戏模式界面
            if (CurrentChooseList == 7)
            {
                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    ModePagecurrentIndex = Mathf.Clamp(ModePagecurrentIndex + 1, 0, 2);
                    UpdateModePage_Highlight();


                }
                else if (dir.x < -0.5f)
                {
                    ModePagecurrentIndex = Mathf.Clamp(ModePagecurrentIndex - 1, 0, 2);
                    UpdateModePage_Highlight();


                }


                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {

                    NextDifficulty();
                }
                else if (dir.y < -0.5f)
                {

                    PrevDifficulty();
                }
            }

            //Chapter界面
            if (CurrentChooseList == 8)
            {
                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    MoveSelection_2(-1);

                }
                else if (dir.y < -0.5f)
                {

                    MoveSelection_2(1);
                }

                // 当前菜单项内的左右切换
                if (dir.x > 0.5f)
                {
                    MoveSelection_2(7);


                }
                else if (dir.x < -0.5f)
                {
                    MoveSelection_2(-7);

                }
            }

            AudioManager.instance.AudioPlay(AudioManager.instance.Attack_pai1);

            //CG结局调教所界面
            if (CurrentChooseList == 11)
            {
                // 当前菜单项内的上下切换
                if (dir.y > 0.5f)
                {
                    MoveSelection_3(-1);

                }
                else if (dir.y < -0.5f)
                {

                    MoveSelection_3(1);
                }


            }


        }

    }


    public GameObject HideGameObjectWhenChangeName;
    public GameObject Prompt_Enter, Ok_Name;
    public void OnChangeName()
    {
        isInputing = true;
        CreatNewcurrentIndex = 0;
        UpdateHighlight();

        HideGameObjectWhenChangeName.SetActive(false);
        Prompt_Enter.SetActive(true);
        Ok_Name.SetActive(true);
    }//打字的时候锁住上下移动
    public void OnChangeNameOver()
    {
        isInputing = false;

        HideGameObjectWhenChangeName.SetActive(true);
        Prompt_Enter.SetActive(false);
        Ok_Name.SetActive(false);

    }//打字的时候锁住上下移动

    [Obsolete]
    private void OnConfirm(InputAction.CallbackContext ctx)
    {
        //按下任意键
        if (coverPanel.activeSelf)
        {
            OnCoverClicked();
            return;
        }





        if (player.isInputBlocked)
        {
            // 可选：进入下一级菜单、确认开始游戏等


            //局内商店
            if (CurrentChooseList == -8)
            {
                // 防止空列表或空按钮
                if (shopItemButtons.Count > 0 && shopItemButtons[shopCurrentIndex] != null)
                {
                    TryBuyItem(shopItemButtons[shopCurrentIndex]); // 你已有的购买函数
                }

            }

            //战败投降界面
            if (CurrentChooseList == -7)
            {
                Time.timeScale = 1f;
                Invoke("ReLoadScene", 0.1f);
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }



            //局内被捕获状态
            if (CurrentChooseList == -6 && CanPushEndUI)
            {

                ToEnd_Surrender_Cavans();
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }


            //三选一界面
            if (CurrentChooseList == -5)
            {

                Invoke("ConfirmBonus", 0.1f);
            }




            //只要暂停菜单显示，攻击键按下就是触发这里
            if (CurrentChooseList == -3)
            {
                //ReLoadScene();

                SavePageToHomePage();

                return;
            }


            //确认删除全部存档界面
            if (CurrentChooseList == -2)
            {
                ReStart_DeleteAll();//删除存档重刷场景
            }

            //确认删除界面
            if (CurrentChooseList == -1)
            {
                DeleteCurrentSelection();//删除角色
            }

            //主菜单界面
            if (CurrentChooseList == 0)
            {
                switch (HomePagecurrentIndex)
                {
                    case 0:
                        Invoke("ToModePage", 0.1f);//进入游戏模式界面
                        break;
                    case 1:
                        //ToSavePageButton(0);//开始游戏进入存档界面(CG)
                        //HomePageToCGPage();
                        Invoke("ToCG_EndPage", 0.1f);//进入CG结局调教所界面
                        break;
                    case 2:
                        Invoke("ToSettingPage", 0.1f);//进入设置界面
                        break;
                    case 3:
                        ExitGame();
                        break;
                    case 4:
                        Invoke("ToThanksPage", 0.1f);//进入感谢界面
                        break;
                    case 5:
                        OpenURL_Ci_en();
                        break;
                    case 6:
                        OpenURL_Patreon();
                        break;
                    case 7:
                        OpenURL_Steam();
                        break;
                    case 8:
                        OpenURL_Discord();
                        break;
                    case 9:
                        OpenURL_YYY();
                        break;
                }

                AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
            }

            //存档界面
            if (CurrentChooseList == 2)
            {
                if (CurrentMode == 0)
                {
                    //ToCGPage();//打开CG界面
                    Invoke("ToCGPage", 0.1f);
                }

                if (CurrentMode == 1)
                {
                    //CreateNewSave();//新建角色

                    OpenCloseMenu();
                }


                AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
            }

            //捏人界面
            if (CurrentChooseList == 1)
            {

                if (CreatNewcurrentIndex == 0)
                {
                    //编辑名称
                    nameInputField.ActivateInputField(); // ✅ 激活输入框并进入编辑
                    OnChangeName();//这个时候就需要遮住了
                }
                if (CreatNewcurrentIndex == 5)
                {
                    //点 OK
                    okButton.onClick.Invoke();
                    AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
                    isInputing = false;
                }
            }

            //设置界面
            if (CurrentChooseList == 3)
            {
                switch (SettingPagecurrentIndex)
                {

                    case 2:
                        Invoke("ToLanguagePage", 0.1f);//进入设置界面
                        break;
                    case 4:
                        Invoke("TryDelete_All", 0.1f);//进入确认删除全部存档界面
                        break;
                }

                AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
            }

            //语言界面
            if (CurrentChooseList == 4)
            {
                SetLanguage_2();
            }

            //CG界面
            if (CurrentChooseList == 5)
            {
                cgButtons[CGcurrentIndex].PlayCG();
            }

            //游戏模式选择界面
            if (CurrentChooseList == 7)
            {
                switch (ModePagecurrentIndex)
                {
                    case 0:
                        ToStoryStage();
                        break;
                    case 1:
                        ToOneToOneStage();
                        break;
                    case 2:
                        ToDungeonStage();
                        break;
                }

                AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
            }

            //Chapter界面
            if (CurrentChooseList == 8)
            {
                chapterButtons[ChapterCurrentIndex].PlayAVG();
            }

            //AVG章节界面
            if (CurrentChooseList == 9)
            {
                Invoke("Delay_AVG_ShowText", 0.1f);
            }


            //结算界面
            if (CurrentChooseList == 10)
            {
                NextStage();
                AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
            }

            //CG结局调教所界面
            if (CurrentChooseList == 11)
            {
                cgButtons[CG_End_currentIndex].PlayCG_End();
            }
        }


    }//键盘J    xbox手柄B      ps手柄O

    private void OnCancel(InputAction.CallbackContext ctx)
    {
        //按下任意键
        if (coverPanel.activeSelf)
        {
            OnCoverClicked();
            return;
        }



        if (player.isInputBlocked)
        {
            // 可选：退出菜单、返回上一级等

            //局内商店界面
            if (CurrentChooseList == -8)
            {
                Invoke("CloseShop", 0.1f);
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }


            //战败投降界面
            if (CurrentChooseList == -7)
            {
                Time.timeScale = 1f;
                Invoke("ChooseSurrender", 0.1f);
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }


            //局内被捕获状态
            if (CurrentChooseList == -6 && CanPushEndUI)
            {
                ToEnd_Surrender_Cavans();
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }


            //只要暂停菜单显示，闪避键按下就是触发这里
            if (CurrentChooseList == -3)
            {
                ContinueGame();
                AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
                return;
            }



            //确认删除全部存档界面
            if (CurrentChooseList == -2)
            {
                Invoke("CancelDelete_All", 0.1f);
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            //确认删除界面
            if (CurrentChooseList == -1)
            {
                Invoke("CancelDelete", 0.1f);
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            //存档界面
            if (CurrentChooseList == 2)
            {
                //只有CG界面才可以在存档界面选择退出
                if (CurrentMode == 0)
                {
                    SavePageToHomePage();
                }



                //PauseGame();

                //AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            //设置界面//Mode游戏模式界面//CG结局调教所界面//感谢名单界面
            if (CurrentChooseList == 3 || CurrentChooseList == 7 || CurrentChooseList == 11 || CurrentChooseList == 12)
            {
                ToHomePage();
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            //语言设置界面
            if (CurrentChooseList == 4)
            {
                ToSettingPage();
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }
            //CG界面
            if (CurrentChooseList == 5)
            {
                Invoke("ToSavePage", 0.1f);//开始游戏进入存档界面(CG)
                CurrentMode = 0;
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }
            //CG鉴赏中
            if (CurrentChooseList == 6)
            {
                Invoke("ToCGPage", 0.1f);//开始游戏进入存档界面(CG)
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            //Chapter章节界面
            if (CurrentChooseList == 8)
            {
                ToModePage();
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            //AVG章节界面
            if (CurrentChooseList == 9)
            {
                dialogSystem.ChangeStory();
            }

            //结算界面
            if (CurrentChooseList == 10)
            {
                SavePageToHomePage();
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }
        }

    }//键盘K      xbox手柄A       ps手柄X

    private void OnCreate(InputAction.CallbackContext ctx)
    {
        //按下任意键
        if (coverPanel.activeSelf)
        {
            OnCoverClicked();
            return;
        }


        if (player.isInputBlocked)
        {
            // 可选：删除存档
            if (CurrentChooseList == 2)
            {
                CreateNewSave();//新建角色
            }
            AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
        }

    }//键盘Space    xbox手柄左肩键       ps手柄左肩键

    private void OnDelete(InputAction.CallbackContext ctx)
    {
        //按下任意键
        if (coverPanel.activeSelf)
        {
            OnCoverClicked();
            return;
        }



        if (player.isInputBlocked)
        {
            // 可选：删除存档
            if (CurrentChooseList == 2)
            {
                currentSelectedSlot.Delete();//跳出是否删除存档界面
                //DeleteCurrentSelection();
            }

        }

    }//键盘E    xbox手柄X       ps手柄□
    private void OnPause(InputAction.CallbackContext ctx)
    {

        //按下任意键
        if (coverPanel.activeSelf)
        {
            OnCoverClicked();
            return;
        }


        //捏人界面
        if (CurrentChooseList == 1)
        {

            OnChangeNameOver();
            return;
        }

        //只要暂停菜单显示，闪避键按下就是触发这里
        if (CurrentChooseList == -4)
        {

            PauseGame();
            return;
        }

        if (CurrentChooseList == -3)
        {

            ContinueGame();
            return;
        }


        AudioManager.instance.AudioPlay(AudioManager.instance.Bullet_AK);


    }//键盘ESC      xbox手柄——        ps手柄opt


    //在玩家方向上有输入的时候不能打开菜单
    //public bool PlayerIsMoving = false;
    public GameObject LockOfMenu;

    private void OnMenu(InputAction.CallbackContext ctx)
    {

        //按下任意键(好像报错)
        //if (coverPanel.activeSelf)
        //{
        //    OnCoverClicked();
        //    return;
        //}



        //if (CurrentChooseList == -4 && CurrentChooseList == 2)
        //{
        //     
        //    
        //    
        //    
        //    OpenCloseMenu();
        //    AudioManager.instance.AudioPlay(AudioManager.instance.Bullet_AK);
        //}




    }//键盘L      xbox手柄Y        ps手柄△


    #endregion



    /// <summary>
    /// 跳转网页/退出游戏
    /// </summary>
    #region
    public void ExitGame()
    {
        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 3;
        UpdateHomePage_Highlight();

        Debug.Log("Exiting game...");

        Application.Quit();

        //退出强制打开商店页面
        OpenURL_DLsite_Fanza();
    }


    public void OpenURL_Ci_en()
    {
        Application.OpenURL("https://ci-en.dlsite.com/creator/16247");


        //改油管链接
        //Application.OpenURL("https://www.youtube.com/watch?v=7GJzbKH4WjU");


        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 5;
        UpdateHomePage_Highlight();
    }

    public void OpenURL_Patreon()
    {
        Application.OpenURL("https://www.patreon.com/c/FTGirl");

        //按照Steam这边要求改成Pixiv
        //Application.OpenURL("https://www.pixiv.net/users/38416908");


        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 6;
        UpdateHomePage_Highlight();
    }
    public void OpenURL_Steam()
    {
        //Application.OpenURL("https://store.steampowered.com/search/?developer=FT%20Girl%20Studio");
        Application.OpenURL("https://store.steampowered.com/app/4086970/Crossdresser_Killer/");

        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 7;
        UpdateHomePage_Highlight();
    }

    public void OpenURL_Discord()
    {
        Application.OpenURL("https://discord.gg/bc49G5Xcq9");

        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 8;
        UpdateHomePage_Highlight();
    }



    public void OpenURL_YYY()
    {
        Application.OpenURL("https://x.com/Detective_ye");

        //钮按下后绿色选中也会过去
        HomePagecurrentIndex = 9;
        UpdateHomePage_Highlight();
    }


    public void OpenURL_DLsite_Fanza()
    {
        //Application.OpenURL("https://www.dlsite.com/maniax/announce/=/product_id/RJ01484541.html");
        //Application.OpenURL("https://www.dmm.co.jp/dc/doujin/-/detail/=/cid=d_678786/?utm_source=twitter&utm_medium=social_tpost&utm_campaign=start&utm_term=d_678786&utm_content=doujin");
        //Application.OpenURL("https://store.steampowered.com/app/4086970/Crossdresser_Killer/");
    }
    #endregion


    /// <summary>
    /// 三选一界面
    /// </summary>
    #region
    [Header("三选一界面")]
    public List<BonusSlotUI> BonusButtons = new List<BonusSlotUI>();
    int BonusCurrentIndex = 0;

    void MoveSelection_Bonus(int direction)
    {
        // 取消旧高亮
        BonusButtons[BonusCurrentIndex].SetHighlight(false);

        // 循环查找下一个已解锁的项
        int max = BonusButtons.Count;
        for (int i = 1; i < max; i++)
        {
            int newIndex = (BonusCurrentIndex + direction * i + max) % max;
            if (BonusButtons[newIndex].isActiveAndEnabled)
            {
                BonusCurrentIndex = newIndex;
                break;
            }
        }

        //显示对应文本
        Bonus_description.text = BonusButtons[BonusCurrentIndex].description;

        // 更新高亮
        UpdateHighlight_Bonus();


    }//切换当前选中

    void UpdateHighlight_Bonus()
    {
        for (int i = 0; i < BonusButtons.Count; i++)
        {
            BonusButtons[i].SetHighlight(i == BonusCurrentIndex);
        }
    }



    public Text Bonus_description;
    public GameObject BonusCavans;
    bool CanChooseItem = false;//延迟半秒防止按太快

    void CanChoose()
    {
        CanChooseItem = true;



    }


    public void ShowBonusCavans()
    {

        CurrentChooseList = -5;
        player.isInputBlocked = true;//切断玩家的方向攻击等输入

        //Time.timeScale = 0f;//三选一界面延迟暂停，防止位移导致问题
        player.isFrozen = true;//因为三选一界面一旦暂停无法移动光标，所以用这个方法控制住

        BonusCavans.SetActive(true);

        // 先全部隐藏
        foreach (var btn in BonusButtons)
        {
            btn.gameObject.SetActive(false);
        }

        // 随机选出3个不同的奖励
        List<int> usedIndex = new List<int>();
        int max = Mathf.Min(3, BonusButtons.Count);

        for (int i = 0; i < max; i++)
        {
            //int rand;
            //do
            //{
            //    rand = UnityEngine.Random.Range(0, BonusButtons.Count);
            //
            //} while (usedIndex.Contains(rand));
            //usedIndex.Add(rand);
            //
            //BonusButtons[rand].gameObject.SetActive(true);
            //BonusButtons[rand].ReNewBonus(); // 重新生成数值 & 描述



            int rand = GetWeightedRandomIndex(usedIndex);
            usedIndex.Add(rand);

            BonusButtons[rand].gameObject.SetActive(true);
            BonusButtons[rand].ReNewBonus();
        }

        // 设置初始选中项 = 第一个显示的奖励
        for (int i = 0; i < BonusButtons.Count; i++)
        {
            if (BonusButtons[i].gameObject.activeSelf)
            {
                BonusCurrentIndex = i;
                break;
            }
        }

        Bonus_description.text = BonusButtons[BonusCurrentIndex].description;
        UpdateHighlight_Bonus();

        Invoke("CanChoose", 0.5f);//延迟半秒



        for (int i = 0; i < BonusButtons.Count; i++)
        {
            BonusButtons[i].index = i;
        }

    }



    private int GetWeightedRandomIndex(List<int> usedIndex)
    {
        // 权重数组（可自行调整）
        // index 10 = 奴隶市场, index 11 = 回血魔法阵
        float[] weights = new float[BonusButtons.Count];
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = 1f; // 默认权重1
        }

        // 提高这两个选项的出现率
        if (weights.Length > 9) weights[9] = 3f; // 奴隶市场 ×3几率
        if (weights.Length > 10) weights[10] = 3f; // 回血 ×3几率

        // 去掉已经抽中的项
        foreach (int idx in usedIndex)
        {
            weights[idx] = 0f;
        }

        // 计算总权重
        float totalWeight = 0f;
        for (int i = 0; i < weights.Length; i++)
            totalWeight += weights[i];

        // 抽取
        float randomValue = UnityEngine.Random.value * totalWeight;
        float cumulative = 0f;

        for (int i = 0; i < weights.Length; i++)
        {
            cumulative += weights[i];
            if (randomValue <= cumulative)
                return i;
        }

        // 保底返回
        return 0;
    }//单独提高指定选项






    public void ConfirmBonus()
    {
        if (CanChooseItem)
        {
            BonusButtons[BonusCurrentIndex].ApplyBonus();
            CanChooseItem = false;
        }

    }//确定奖励

    public void HideBonusCavans()
    {
        CurrentChooseList = -4;
        player.isInputBlocked = false;
        Time.timeScale = 1f;

        player.isFrozen = false;//因为三选一界面一旦暂停无法移动光标，所以用这个方法控制住

        BonusCavans.SetActive(false);
    }



    public void SelectBonusByIndex(int idx)
    {
        if (idx < 0 || idx >= BonusButtons.Count) return;
        if (!BonusButtons[idx].gameObject.activeSelf) return;

        // 更新选中索引
        BonusCurrentIndex = idx;

        // 显示说明
        Bonus_description.text = BonusButtons[idx].description;

        // 更新高亮
        UpdateHighlight_Bonus();
    }

    #endregion


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

    [Header("经验值等级")]
    public Image ExperienceBar;

    // 用于闪烁控制
    private float flashTimer = 0f;


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

        // 多段颜色插值：蓝 → 绿 → 黄 → 红
        Color baseColor;

        if (fillPercent < 0.33f) // 0%~33%：蓝到绿
        {
            baseColor = Color.Lerp(new Color(0f, 0.5f, 1f), Color.green, fillPercent / 0.33f);
        }
        else if (fillPercent < 0.66f) // 33%~66%：绿到黄
        {
            baseColor = Color.Lerp(Color.green, Color.yellow, (fillPercent - 0.33f) / 0.33f);
        }
        else // 66%~100%：黄到红
        {
            baseColor = Color.Lerp(Color.yellow, Color.red, (fillPercent - 0.66f) / 0.34f);
        }

        // 高暴击值闪烁（红黄闪）
        if (fillPercent > 0.9f)
        {
            flashTimer += Time.deltaTime * 4f; // 闪烁速度
            float alpha = Mathf.Abs(Mathf.Sin(flashTimer));
            Color flashColor = Color.Lerp(baseColor, Color.yellow, alpha); // 红黄闪烁
            CriticalBar.color = flashColor;
        }
        else
        {
            CriticalBar.color = baseColor;
        }
    }

    public void UpdateExperienceBar(int curAmount, int maxAmount)
    {
        ExperienceBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

    #endregion



    /// <summary>
    /// Buff等小图标显示
    /// </summary>
    #region
    [Header("Buff等小图标显示")]
    public Image SwordBuffIcon;
    public Text SwordBuffText;
    public Image PistolBuffIcon;
    public Text PistolBuffText;
    public Image StaffBuffIcon;
    public Text StaffBuffText;

    public void UpdateBuffUI()
    {
        // 剑士Buff
        if (GameFlowData.Sword_Buff >= 2)
        {
            SwordBuffIcon.gameObject.SetActive(true);
            SwordBuffText.text = GameFlowData.Sword_Buff.ToString();
        }
        else
        {
            SwordBuffIcon.gameObject.SetActive(false);
        }

        // 枪手Buff
        if (GameFlowData.Pistol_Buff >= 2)
        {
            PistolBuffIcon.gameObject.SetActive(true);
            PistolBuffText.text = GameFlowData.Pistol_Buff.ToString();
        }
        else
        {
            PistolBuffIcon.gameObject.SetActive(false);
        }

        // 法师Buff
        if (GameFlowData.Staff_Buff >= 2)
        {
            StaffBuffIcon.gameObject.SetActive(true);
            StaffBuffText.text = GameFlowData.Staff_Buff.ToString();
        }
        else
        {
            StaffBuffIcon.gameObject.SetActive(false);
        }
    }
    public void ResetBuffs()
    {
        GameFlowData.Sword_Buff = 0;
        GameFlowData.Pistol_Buff = 0;
        GameFlowData.Staff_Buff = 0;

        SwordBuffIcon.gameObject.SetActive(false);
        PistolBuffIcon.gameObject.SetActive(false);
        StaffBuffIcon.gameObject.SetActive(false);
    }
    #endregion


    /// <summary>
    /// 商店与更改金币位置
    /// </summary>
    #region
    [Header("金币")]
    public Text MoneyText;
    public Text MoneyText_2;
    public void ChangeMoney(int amount, bool UseVoice = true)
    {
        // 取当前值
        int currentMoney = PlayerPrefs.GetInt("Money", 0);

        // 修改
        currentMoney += amount;
        if (currentMoney < 0) currentMoney = 0;   // 防止出现负数

        // 存回 PlayerPrefs
        PlayerPrefs.SetInt("Money", currentMoney);
        PlayerPrefs.Save();

        // 更新 UI
        MoneyText.text = currentMoney.ToString();
        MoneyText_2.text = currentMoney.ToString();

        //Debug.Log("目前存档里的钱币: " + currentMoney);

        if (UseVoice) { AudioManager.instance.AudioPlay(AudioManager.instance.SE_Reji); }

    }


    [Header("商店界面")]
    public GameObject ShowShopCavans;
    private RBQ currentRBQ; // 在 UIManager 顶部声明
    public void OpenShopMenu(RBQ rbq)
    {
        currentRBQ = rbq; // ✅ 记录当前商店

        player.characterSkin.HideSkeleton();
        MainCamera.SetInteger("View", 0);
        Common_All.SetActive(false);
        ShowShopCavans.SetActive(true);
        player.isInputBlocked = true;//切断玩家的方向攻击等输入



        CurrentChooseList = -8;//局内商店界面

        //player.CheckDemonMode();//从魔族化变回

        // 👇 新增
        BuildShopFromRBQ(rbq);

    }

    [Obsolete]
    public void CloseShop()
    {
        player.characterSkin.ShowSkeleton();
        MainCamera.SetInteger("View", 2);
        Common_All.SetActive(true);
        ShowShopCavans.SetActive(false);
        player.isInputBlocked = false;//恢复玩家的方向攻击等输入


        CurrentChooseList = -4;


        // ✅ 恢复交互提示
        RBQ rbq = FindObjectOfType<RBQ>(); // 只要场景里存在当前商店RBQ就行
        if (rbq != null)
        {
            rbq.ReenablePrompt(player);
        }
    }











    public Transform shopItemParent;
    public GameObject shopItemPrefab; // 预制体：带按钮、图标、价格Text、描述Text
    public List<ShopItemUI> shopItemButtons = new List<ShopItemUI>();
    public Text shopDescription;
    int shopCurrentIndex = 0;

    #region   手机上点击高亮选中，然后可购买
    public Button buyButton; // ✅ 购买按钮（UI上新建一个按钮并拖进来）
                             // 当点击商品按钮时（来自 ShopItemUI）
    public void SelectShopByUI(ShopItemUI clicked)
    {
        if (clicked == null) return;

        int idx = shopItemButtons.IndexOf(clicked);
        if (idx < 0) return;

        shopCurrentIndex = idx;
        UpdateHighlight_Shop();
        shopDescription.text = clicked.data.description;

        // ✅ 让购买按钮亮起
        if (buyButton) buyButton.interactable = true;
    }
    public void OnBuyButtonClick()
    {
        // 防止误触
        if (shopItemButtons.Count == 0) return;
        if (shopCurrentIndex < 0 || shopCurrentIndex >= shopItemButtons.Count) return;

        var item = shopItemButtons[shopCurrentIndex];
        if (item == null) return;

        TryBuyItem(item);
    }
    #endregion


    public void BuildShopFromRBQ(RBQ rbq)
    {
        // 清空旧UI
        foreach (Transform child in shopItemParent)
            Destroy(child.gameObject);

        shopItemButtons.Clear();

        // 创建UI
        foreach (var item in rbq.shopItems)
        {
            GameObject btn = Instantiate(shopItemPrefab, shopItemParent);
            ShopItemUI ui = btn.GetComponent<ShopItemUI>();
            ui.Setup(item);
            shopItemButtons.Add(ui);
        }

        // 默认选中第一个
        //if (shopItemButtons.Count > 0)
        //{
        //    shopCurrentIndex = 0;
        //    UpdateShopHighlight();
        //    shopDescription.text = shopItemButtons[0].data.description;
        //}

        // 初始选中
        if (shopItemButtons.Count > 0)
        {
            shopCurrentIndex = 0;
            UpdateHighlight_Shop();
            shopDescription.text = shopItemButtons[shopCurrentIndex].data.description;
        }
        else
        {
            shopCurrentIndex = 0;
            shopDescription.text = "";
        }
    }

    void MoveSelection_Shop(int direction /* +1=向下, -1=向上 */)
    {
        if (shopItemButtons.Count == 0) return;

        // 取消旧高亮
        shopItemButtons[shopCurrentIndex].SetHighlight(false);

        int max = shopItemButtons.Count;
        for (int i = 1; i <= max; i++)
        {
            int newIndex = (shopCurrentIndex + direction * i + max) % max;
            if (shopItemButtons[newIndex] != null && shopItemButtons[newIndex].gameObject.activeSelf)
            {
                shopCurrentIndex = newIndex;
                break;
            }
        }

        // 更新高亮与说明
        UpdateHighlight_Shop();
        shopDescription.text = shopItemButtons[shopCurrentIndex].data.description;
    }

    void UpdateHighlight_Shop()
    {
        for (int i = 0; i < shopItemButtons.Count; i++)
        {
            if (shopItemButtons[i] == null) continue;
            shopItemButtons[i].SetHighlight(i == shopCurrentIndex);
        }
    }

    [Obsolete]
    public void TryBuyItem(ShopItemUI itemUI)
    {
        int money = PlayerPrefs.GetInt("Money", 0);
        var data = itemUI.data;
        Player player = UIManager.instance.player;

        if (money < data.price)
        {
            Debug.Log("金币不足");
            _RoomGenerator.ShowInformationOfStage(-2);
            player.frameEvents._Attack_pai1();
            return;
        }

        // 扣钱
        ChangeMoney(-data.price);

        switch (data.type)
        {
            case ShopItemData.ItemType.Sword:
                player.PickupWeapon(data.index, 0);
                player.CurrentWeaponPower += data.value;
                player.SaveCurrent();
                break;

            case ShopItemData.ItemType.Pistol:
                player.PickupWeapon(data.index, 1);
                player.CurrentWeaponPower += data.value;
                player.SaveCurrent();
                break;

            case ShopItemData.ItemType.Staff:
                player.PickupWeapon(data.index, 2);
                player.CurrentWeaponPower += data.value;
                player.SaveCurrent();
                break;

            case ShopItemData.ItemType.Clothes:
                player.YYY_bodyIndex = data.index;
                player.CurrentArmorDefence += data.value;
                player.SaveCurrent();
                player.SetSkin();//衣服需要更换
                break;

            case ShopItemData.ItemType.Stockings:
                player.YYY_legsIndex = data.index;
                player.CurrentStockingDefence += data.value;
                player.SaveCurrent();
                player.SetSkin();//衣服需要更换
                break;

            case ShopItemData.ItemType.Slave:

                //_RoomGenerator.SetFriend(0);
                currentRBQ.SaveFriend();
                CloseShop(); // 或者留在界面
                break;

            case ShopItemData.ItemType.Potion:
                player.RestoreHealth(data.value);
                break;
        }

        // 声音、反馈
        player.frameEvents._SE_Clothes();

        // 移除商品按钮
        Destroy(itemUI.gameObject);
        shopItemButtons.Remove(itemUI);

        // ✅ 同步货架库存（让 RBQ 实体隐藏）
        if (currentRBQ != null)
        {
            currentRBQ.RemoveItemFromShelf(data.type);
        }




        // UI 移除
        int removedIndex = shopItemButtons.IndexOf(itemUI);
        Destroy(itemUI.gameObject);
        shopItemButtons.Remove(itemUI);

        // 购买后如果空了就关闭商店；否则校正当前索引并刷新高亮/说明
        if (shopItemButtons.Count == 0)
        {
            shopDescription.text = "";
            CloseShop(); // 或者留在界面
        }
        else
        {
            // 让选中落到“刚移除的那个位置”，若超界则回退一位
            if (removedIndex < 0) removedIndex = 0;
            if (removedIndex >= shopItemButtons.Count) removedIndex = shopItemButtons.Count - 1;
            shopCurrentIndex = removedIndex;

            UpdateHighlight_Shop();
            shopDescription.text = shopItemButtons[shopCurrentIndex].data.description;
        }


    }

    #endregion


    /// <summary>
    /// 区域背景音乐
    /// </summary>
    #region
    [Header("区域BGM")]
    public BGM BGM;//用于背景音乐

    public void PlayBossMusic()
    {
        BGM.AudioPlayBossMusic(-1);
    }
    public void PlayDungeonBGM()
    {

        BGM.AudioPlayDungeonMusic(-1);

    }
    public void PlayRuinsBGM()
    {

        BGM.AudioPlayRuinsMusic(-1);

    }

    public void PlayBackgroundMusic()
    {
        BGM.AudioPlayBackgroundMusic(-1);
    }

    #endregion


    /// <summary>
    /// 敌人产生台词
    /// </summary>
    #region
    [Header("敌人台词")]
    public List<GameObject> Man_In_Game_DialogueList;//男性敌人刷出台词
    public List<GameObject> Girl_In_Game_DialogueList;//女性敌人刷出台词

    public List<GameObject> Boss_Captain_In_Game_DialogueList;//守卫队长刷出台词
    public List<GameObject> Boss_Captain_Skill_In_Game_DialogueList;//守卫队长技能刷出台词
    public List<GameObject> Boss_Captain_Die_In_Game_DialogueList;//守卫队长死亡刷出台词

    public List<GameObject> Boss_Selene_In_Game_DialogueList;//赛琳娜刷出台词
    public List<GameObject> Boss_Selene_Skill_In_Game_DialogueList;//赛琳娜技能刷出台词
    public List<GameObject> Boss_Selene_Skill2_In_Game_DialogueList;//赛琳娜技能刷出台词2
    public List<GameObject> Boss_Selene_Die_In_Game_DialogueList;//赛琳娜死亡刷出台词

    public List<GameObject> Boss_DarkMage_In_Game_DialogueList;//黑魔导士刷出台词
    public List<GameObject> Boss_DarkMage_Skill_In_Game_DialogueList;//黑魔导士技能刷出台词
    public List<GameObject> Boss_DarkMage_Die_In_Game_DialogueList;//黑魔导士死亡刷出台词

    public List<GameObject> Boss_Warden_In_Game_DialogueList;//典狱长刷出台词
    public List<GameObject> Boss_Warden_Die_In_Game_DialogueList;//典狱长刷出台词

    public List<GameObject> Boss_CombatNun_In_Game_DialogueList;//首席战斗修女刷出台词
    public List<GameObject> Boss_CombatNun_Skill_In_Game_DialogueList;//首席战斗修女技能刷出台词
    public List<GameObject> Boss_CombatNun_Die_In_Game_DialogueList;//首席战斗修女死亡刷出台词


    public List<GameObject> Boss_Alexis_In_Game_DialogueList;//皇太子亚历克西斯刷出台词
    public List<GameObject> Boss_Alexis_Skill_In_Game_DialogueList;//皇太子亚历克西斯技能刷出台词
    public List<GameObject> Boss_Alexis_Die_In_Game_DialogueList;//皇太子亚历克西斯死亡刷出台词


    public List<GameObject> Boss_Morgan_In_Game_DialogueList;//宰相摩尔根刷出台词
    public List<GameObject> Boss_Morgan_Skill_In_Game_DialogueList;//宰相摩尔根技能刷出台词

    public List<GameObject> Boss_Dominus_Skill_In_Game_DialogueList;//皇帝多米纳斯技能刷出台词
    public List<GameObject> Boss_Dominus_Die_In_Game_DialogueList;//皇帝多米纳斯死亡刷出台词

    public List<GameObject> Boss_SwordDancer_In_Game_DialogueList;//奴隶剑舞姬刷出台词
    public List<GameObject> Boss_SwordDancer_Skill_In_Game_DialogueList;//奴隶剑舞姬技能刷出台词
    public List<GameObject> Boss_SwordDancer_Die_In_Game_DialogueList;//奴隶剑舞姬死亡刷出台词

    private bool dialogueShowing = false;  // 是否有台词正在显示
    private GameObject currentDialogue = null; // 当前正在显示的台词
    private Coroutine dialogueRoutine = null;  // 当前协程引用

    // 敌人调用这个接口
    private Dictionary<string, int> lastIndexDict = new Dictionary<string, int>();

    public void ShowDialogue(string type)
    {
        // 先强制隐藏正在显示的台词
        if (currentDialogue != null)
        {
            if (dialogueRoutine != null) StopCoroutine(dialogueRoutine);
            currentDialogue.SetActive(false);
            currentDialogue = null;
        }

        List<GameObject> pool = null;
        switch (type)
        {
            case "Man": pool = Man_In_Game_DialogueList; break;
            case "Girl": pool = Girl_In_Game_DialogueList; break;

            case "Boss_Captain": pool = Boss_Captain_In_Game_DialogueList; break;
            case "Boss_Captain_Skill": pool = Boss_Captain_Skill_In_Game_DialogueList; break;
            case "Boss_Captain_Die": pool = Boss_Captain_Die_In_Game_DialogueList; break;

            case "Boss_Selene": pool = Boss_Selene_In_Game_DialogueList; break;
            case "Boss_Selene_Skill": pool = Boss_Selene_Skill_In_Game_DialogueList; break;
            case "Boss_Selene_Skill2": pool = Boss_Selene_Skill2_In_Game_DialogueList; break;
            case "Boss_Selene_Die": pool = Boss_Selene_Die_In_Game_DialogueList; break;

            case "Boss_DarkMage": pool = Boss_DarkMage_In_Game_DialogueList; break;
            case "Boss_DarkMage_Skill": pool = Boss_DarkMage_Skill_In_Game_DialogueList; break;
            case "Boss_DarkMage_Die": pool = Boss_DarkMage_Die_In_Game_DialogueList; break;

            case "Boss_Warden": pool = Boss_Warden_In_Game_DialogueList; break;
            case "Boss_Warden_Die": pool = Boss_Warden_Die_In_Game_DialogueList; break;

            case "Boss_CombatNun": pool = Boss_CombatNun_In_Game_DialogueList; break;
            case "Boss_CombatNun_Skill": pool = Boss_CombatNun_Skill_In_Game_DialogueList; break;
            case "Boss_CombatNun_Die": pool = Boss_CombatNun_Die_In_Game_DialogueList; break;

            case "Boss_Alexis": pool = Boss_Alexis_In_Game_DialogueList; break;
            case "Boss_Alexis_Skill": pool = Boss_Alexis_Skill_In_Game_DialogueList; break;
            case "Boss_Alexis_Die": pool = Boss_Alexis_Die_In_Game_DialogueList; break;

            case "Boss_Morgan": pool = Boss_Morgan_In_Game_DialogueList; break;
            case "Boss_Morgan_Skill": pool = Boss_Morgan_Skill_In_Game_DialogueList; break;

            case "Boss_Dominus_Skill": pool = Boss_Dominus_Skill_In_Game_DialogueList; break;
            case "Boss_Dominus_Die": pool = Boss_Dominus_Die_In_Game_DialogueList; break;

            case "Boss_SwordDancer": pool = Boss_SwordDancer_In_Game_DialogueList; break;
            case "Boss_SwordDancer_Skill": pool = Boss_SwordDancer_Skill_In_Game_DialogueList; break;
            case "Boss_SwordDancer_Die": pool = Boss_SwordDancer_Die_In_Game_DialogueList; break;
        }

        if (pool == null || pool.Count == 0) return;

        int index;
        int lastIndex = -1;

        // 取出这个类别上次的 index（如果有）
        if (lastIndexDict.TryGetValue(type, out var savedIndex))
        {
            lastIndex = savedIndex;
        }

        // 抽一个，避免和上次相同
        do
        {
            index = UnityEngine.Random.Range(0, pool.Count);
        } while (pool.Count > 1 && index == lastIndex);

        // 记录这次的 index
        lastIndexDict[type] = index;

        GameObject dialogue = pool[index];
        dialogueRoutine = StartCoroutine(ShowRoutine(dialogue));
    }

    private IEnumerator ShowRoutine(GameObject dialogue)
    {
        currentDialogue = dialogue;
        dialogue.SetActive(true);
        dialogueShowing = true;

        float showTime = 5f; // 台词显示5秒
        yield return new WaitForSeconds(showTime);

        dialogue.SetActive(false);
        dialogueShowing = false;
        currentDialogue = null;
        dialogueRoutine = null;
    }
    #endregion


    /// <summary>
    /// 每日自动接客
    /// </summary>
    #region
    public static class DailyLogin
    {
        const string LAST_LOGIN_KEY = "LastLoginDate";
        const string TODAY_COUNT_KEY = "TodayServiceAdded";
        const string TODAY_INCOME_KEY = "TodayServiceIncome";
        const string TODAY_STATS_DATE_KEY = "TodayStatsDate";

        static string Today() => System.DateTime.Now.ToString("yyyy-MM-dd");

        public static bool IsNewDay()
        {
            string last = PlayerPrefs.GetString(LAST_LOGIN_KEY, "");
            return last != Today();
        }

        /// <summary>
        /// 结算：若是新的一天，则为所有存档随机 +0~3 接客，返回总新增与总收益，并写入 PlayerPrefs。
        /// </summary>
        public static bool TryGrantForToday(int perServiceReward, out int totalAdded, out int totalIncome)
        {
            totalAdded = 0;
            totalIncome = 0;

            if (!IsNewDay())
            {
                // 同一天重复进入：从 PlayerPrefs 取回已记的统计，方便 UI 显示
                if (PlayerPrefs.GetString(TODAY_STATS_DATE_KEY, "") == Today())
                {
                    totalAdded = PlayerPrefs.GetInt(TODAY_COUNT_KEY, 0);
                    totalIncome = PlayerPrefs.GetInt(TODAY_INCOME_KEY, 0);
                }
                return false;
            }

            string folder = Application.persistentDataPath + "/Saves/";
            if (Directory.Exists(folder))
            {
                foreach (var file in Directory.GetFiles(folder, "save_*.json"))
                {
                    var json = File.ReadAllText(file);
                    var data = JsonUtility.FromJson<PlayerSaveData>(json);
                    if (data == null || string.IsNullOrEmpty(data.characterName)) continue;

                    int add = UnityEngine.Random.Range(0, 4); // 0~3
                    if (add > 0)
                    {
                        if (data.serviceCount < 0) data.serviceCount = 0;
                        data.serviceCount += add;
                        File.WriteAllText(file, JsonUtility.ToJson(data, true));
                    }
                    totalAdded += add;
                }
            }

            totalIncome = Mathf.Max(0, totalAdded * Mathf.Max(0, perServiceReward));

            // 标记今天 & 记录今日统计
            PlayerPrefs.SetString(LAST_LOGIN_KEY, Today());
            PlayerPrefs.SetString(TODAY_STATS_DATE_KEY, Today());
            PlayerPrefs.SetInt(TODAY_COUNT_KEY, totalAdded);
            PlayerPrefs.SetInt(TODAY_INCOME_KEY, totalIncome);
            PlayerPrefs.Save();

            return true;
        }

        /// <summary>读取今日统计（非新的一天也可用来显示 UI）。</summary>
        public static void GetTodayStats(out int totalAdded, out int totalIncome)
        {
            if (PlayerPrefs.GetString(TODAY_STATS_DATE_KEY, "") == Today())
            {
                totalAdded = PlayerPrefs.GetInt(TODAY_COUNT_KEY, 0);
                totalIncome = PlayerPrefs.GetInt(TODAY_INCOME_KEY, 0);
            }
            else
            {
                totalAdded = 0; totalIncome = 0;
            }
        }
    }

    // 配置：每次接客的收益（你也可以从配置表/难度读取）
    [SerializeField] int perServiceReward = 30;

    // 如果你只在“调教所界面（GameFlowData.nextScene == CG）”显示绿字：
    // 拖引用两个 Text（绿色样式）
    [SerializeField] UnityEngine.UI.Text cgServiceCountText;
    [SerializeField] UnityEngine.UI.Text cgServiceIncomeText;


    public void UpdateCgDailyTexts()
    {
        bool isCG = string.Equals(GameFlowData.nextScene, "CG", System.StringComparison.OrdinalIgnoreCase);

        if (cgServiceCountText != null) cgServiceCountText.gameObject.SetActive(isCG);
        if (cgServiceIncomeText != null) cgServiceIncomeText.gameObject.SetActive(isCG);

        if (!isCG) return;

        DailyLogin.GetTodayStats(out int added, out int income);
        int lang = PlayerPrefs.GetInt("language", 1);

        string sCount, sIncome;
        switch (lang)
        {
            case 0: // JP
                sCount = $"本日の接客数：{added}人";
                sIncome = $"本日の収益：+{income}";
                break;
            case 1: // ZH-CN
                sCount = $"今天总接客数：{added}人";
                sIncome = $"今天总接客收益：＋{income}";
                break;
            case 2: // ZH-TW
                sCount = $"今日總接客數：{added}人";
                sIncome = $"今日收益：＋{income}";
                break;
            case 3: // EN
                sCount = $"Today's services: {added}";
                sIncome = $"Today's income: +{income}";
                break;
            case 4: // KO
                sCount = $"오늘 접객 수: {added}명";
                sIncome = $"오늘 수입: +{income}";
                break;
            default:
                sCount = $"今天总接客数：{added}人";
                sIncome = $"今天总接客收益：＋{income}";
                break;
        }

        if (cgServiceCountText) cgServiceCountText.text = sCount;
        if (cgServiceIncomeText) cgServiceIncomeText.text = sIncome;
    }


    #endregion


    /// <summary>
    /// 切换难度
    /// </summary>
    #region
    [Header("难度显示 Text")]
    public GameObject easyText;       // Easy
    public GameObject commonText;     // Common / Normal
    public GameObject difficultText;  // Difficult / Hard


    private int currentDifficulty; // 0:Easy 1:Common 2:Difficult
    private const string PREF_KEY = "Difficulty";



    public void NextDifficulty()
    {
        currentDifficulty++;
        if (currentDifficulty > 2) currentDifficulty = 0; // 循环
        PlayerPrefs.SetInt(PREF_KEY, currentDifficulty);
        PlayerPrefs.Save();
        UpdateDifficultyUI();
    }


    public void PrevDifficulty()
    {
        currentDifficulty--;
        if (currentDifficulty < 0) currentDifficulty = 2; // 循环
        PlayerPrefs.SetInt(PREF_KEY, currentDifficulty);
        PlayerPrefs.Save();
        UpdateDifficultyUI();
    }


    private void UpdateDifficultyUI()
    {

        switch (currentDifficulty)
        {
            case 0: // Easy
                easyText.SetActive(true);
                commonText.SetActive(false);
                difficultText.SetActive(false);
                break;

            case 1: // Common
                easyText.SetActive(false);
                commonText.SetActive(true);
                difficultText.SetActive(false);
                break;

            case 2: // Difficult
                easyText.SetActive(false);
                commonText.SetActive(false);
                difficultText.SetActive(true);
                break;
        }
    }
    #endregion


    /// <summary>
    /// 显示隐藏鼠标
    /// </summary>
    #region

    private InputAction mouseDeltaAction;// 鼠标移动
    private InputAction mouseClickAction;// 鼠标点击（可选）

    void SetMouse()
    {
        mouseDeltaAction = inputActions.FindAction("MouseDelta"); // 下面会说怎么加
        mouseClickAction = inputActions.FindAction("MouseClick"); // 可选

        mouseDeltaAction.performed += OnMouseMove;
        mouseClickAction.performed += OnMouseClick;

        mouseDeltaAction?.Enable();
        mouseClickAction?.Enable();



        //藏鼠标控制
        moveAction.performed += OnKeyboardOrGamepadInput;
        confirmAction.performed += OnKeyboardOrGamepadInput;
        cancelAction.started += OnKeyboardOrGamepadInput;
        createAction.started += OnKeyboardOrGamepadInput;
        deleteAction.started += OnKeyboardOrGamepadInput;
        pauseAction.started += OnKeyboardOrGamepadInput;
        menuAction.started += OnKeyboardOrGamepadInput;





    }


    // 键盘 / 手柄操作 → 隐藏鼠标
    private void OnKeyboardOrGamepadInput(InputAction.CallbackContext ctx)
    {
        var device = ctx.control.device;

        // 只在键盘/手柄时才隐藏鼠标
        //if (device is Keyboard || device is Gamepad)
        //{
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.Locked; // 或 Locked/Confined，看你游戏需求
        //}

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // 或 Locked/Confined，看你游戏需求

    }



    // 鼠标移动 → 显示鼠标
    private void OnMouseMove(InputAction.CallbackContext ctx)
    {
        Vector2 delta = ctx.ReadValue<Vector2>();
        if (delta.sqrMagnitude > 0.01f)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    // 鼠标点击也可以当成“切回鼠标模式”
    private void OnMouseClick(InputAction.CallbackContext ctx)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    #endregion


    /// <summary>
    /// 地下城模式连胜
    /// </summary>
    #region
    [Header("地下城连胜纪录")]
    public Text DungeonRecordText;

    void Show_DungeonRecord()
    {
        int currentStreak = PlayerPrefs.GetInt("Dungeon_Streak", 0);
        int bestStreak = PlayerPrefs.GetInt("Dungeon_BestStreak", 0);

        string colorTag = "#FFD700"; // 金色，和角斗场保持一致
        int lang = PlayerPrefs.GetInt("language");

        switch (lang)
        {
            case 0: // 日语
                DungeonRecordText.text =
                    $"連勝：<color={colorTag}>{currentStreak}</color>　/　最高：<color={colorTag}>{bestStreak}</color>";
                break;

            case 1: // 简体中文
                DungeonRecordText.text =
                    $"连胜：<color={colorTag}>{currentStreak}</color>　/　最高：<color={colorTag}>{bestStreak}</color>";
                break;

            case 2: // 繁体中文
                DungeonRecordText.text =
                    $"連勝：<color={colorTag}>{currentStreak}</color>　/　最高：<color={colorTag}>{bestStreak}</color>";
                break;

            case 3: // 英语
                DungeonRecordText.text =
                    $"Current: <color={colorTag}>{currentStreak}</color> / Best: <color={colorTag}>{bestStreak}</color>";
                break;

            case 4: // 韩语
                DungeonRecordText.text =
                    $"현재 연승: <color={colorTag}>{currentStreak}</color> / 최고: <color={colorTag}>{bestStreak}</color>";
                break;
        }
    }


    public void Dungeon_Streak_AddOne()
    {
        int streak = PlayerPrefs.GetInt("Dungeon_Streak", 0) + 1;
        PlayerPrefs.SetInt("Dungeon_Streak", streak);

        int best = PlayerPrefs.GetInt("Dungeon_BestStreak", 0);
        if (streak > best)
            PlayerPrefs.SetInt("Dungeon_BestStreak", streak);

        PlayerPrefs.Save();

        Debug.Log($"地下城连胜：{streak}  最高纪录：{PlayerPrefs.GetInt("Dungeon_BestStreak")}");


    }
    public void Dungeon_Streak_ToZero()
    {
        int streak = PlayerPrefs.GetInt("Dungeon_Streak", 0);
        Debug.Log("地下城死亡，连胜从 " + streak + " → 0");

        PlayerPrefs.SetInt("Dungeon_Streak", 0);
        PlayerPrefs.Save();

        Invoke(nameof(ShowDungeon_Streak), 1.5f);
    }

    void ShowDungeon_Streak()
    {
        _RoomGenerator.ShowInformationOfStage(12);
    }


    #endregion


    /// <summary>
    /// 成就端口
    /// </summary>
    #region
    /////////////////////////////////////////////////【电脑控制/Steam】/////////////////////////////////////////////////
    public void Achieventment_ACH_FIRST_MISSION()
    {
        UnlockSteamAchievement("ACH_FIRST_MISSION");
    } //【初めての潜入】第1章クリア

    public void Achieventment_ACH_BEAT_PRINCESS()
    {
        UnlockSteamAchievement("ACH_BEAT_PRINCESS");
    } //【王女との対峙】王女セリーネ撃破

    public void Achieventment_ACH_SEE_TRUTH()
    {
        UnlockSteamAchievement("ACH_SEE_TRUTH");
    } //【真実の目撃者】帝国の正体を知る

    public void Achieventment_ACH_MELEE_MASTER()
    {
        UnlockSteamAchievement("ACH_MELEE_MASTER");
    } //【近接戦の達人】一局内で近接攻撃で敵を10体撃破。

    public void Achieventment_ACH_SHOOT_MASTER()
    {
        UnlockSteamAchievement("ACH_SHOOT_MASTER");
    } //【射撃術の達人】一局内で射撃攻撃で敌を10体撃破。

    public void Achieventment_ACH_MAGIC_MASTER()
    {
        UnlockSteamAchievement("ACH_MAGIC_MASTER");
    } //【魔術行使の達人】一局内で魔法攻撃で敌を10体撃破。

    public void Achieventment_ACH_RESCUE_3()
    {
        UnlockSteamAchievement("ACH_RESCUE_3");
    } //【救出の連鎖】一局内で奴隷を3人以上解放。

    public void Achieventment_ACH_UNLOCK_HIGHELF()
    {
        UnlockSteamAchievement("ACH_UNLOCK_HIGHELF");
    } //【高等精霊の目覚め】高等精霊のキャラクターを解放。






    // 本局里已经触发过的成就（防止自己 UI 重复弹）
    private HashSet<string> _localUnlockedAchievements = new HashSet<string>();


    private void UnlockSteamAchievement(string achievementID)
    {
        // 本地已经触发过了，就不要再弹 UI 了
        //  if (_localUnlockedAchievements.Contains(achievementID))
        //  {
        //      Debug.Log($"成就 {achievementID} 已在本局触发过，直接忽略");
        //      return;
        //  }
        //
        //  _localUnlockedAchievements.Add(achievementID);
        //
        //  // 这里是你原来的“成就弹窗”逻辑
        //  Debug.Log("成就弹窗：" + achievementID);
        //  // TODO: 如果你有成就UI动画，在这里调用，例如：
        //  // ShowAchievementPopup(achievementID);
        //
        //  // 提交给 Steam
        //  if (SteamManager.Initialized)
        //  {
        //      bool success = SteamUserStats.SetAchievement(achievementID);
        //      if (success)
        //      {
        //          SteamUserStats.StoreStats();   // 把成就保存到 Steam
        //          Debug.Log("成功解锁 Steam 成就：" + achievementID);
        //      }
        //      else
        //      {
        //          Debug.LogError("Steam 成就解锁失败：" + achievementID);
        //      }
        //  }
        //  else
        //  {
        //      Debug.LogWarning("Steam API 未初始化，只进行本地弹窗：" + achievementID);
        //  }
    }



    #endregion

    /// <summary>
    /// Buff介绍按钮
    /// </summary>
    #region
    [Header("Buff介绍按钮")]
    public Text tooltipText;          // 或 TextMeshProUGUI
    public GameObject canvasGroup;   // 控制显隐
    public float showDuration = 2f;   // 显示时间

    // 语言：0=JP, 1=CN, 2=TC, 3=EN, 4=KR
    public int lang = 0;

    private Coroutine showCoroutine;

    public void ShowBuff(int buffIndex)
    {
        string desc = GetBuffDesc(buffIndex, lang);
        tooltipText.text = desc;

        if (showCoroutine != null)
            StopCoroutine(showCoroutine);

        showCoroutine = StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        canvasGroup.SetActive(true);
        //canvasGroup.blocksRaycasts = false; // 不挡操作也行

        yield return new WaitForSeconds(showDuration);

        HideImmediate();
    }

    public void HideImmediate()
    {
        canvasGroup.SetActive(false);
    }

    //=====================
    //  Buff 文本表
    //=====================
    private string GetBuffDesc(int buffIndex, int lang)
    {
        // buffIndex:
        // 0=狩猎, 1=精准, 2=敏捷, 3=魔族化, 4=坚韧, 5=辟邪, 6=自然, 7=隐秘化

        switch (lang)
        {
            case 0: // JP
                return GetBuffDesc_JP(buffIndex);
            case 1: // CN
                return GetBuffDesc_CN(buffIndex);
            case 2: // TC
                return GetBuffDesc_TC(buffIndex);
            case 3: // EN
                return GetBuffDesc_EN(buffIndex);
            case 4: // KR
                return GetBuffDesc_KR(buffIndex);
            default:
                return GetBuffDesc_EN(buffIndex);
        }
    }

    //========== JP ==========
    private string GetBuffDesc_JP(int id)
    {
        switch (id)
        {
            case -1:
                return $"<color=#FF8800>【近接補正】現在の近接ダメージ：×{ GameFlowData.Sword_Buff:0.0}</color>";
            case -2:
                return $"<color=#FF8800>【射撃補正】現在の射撃ダメージ：×{ GameFlowData.Pistol_Buff:0.0}</color>";
            case -3:
                return $"<color=#FF8800>【魔術補正】現在の魔法ダメージ：×{ GameFlowData.Staff_Buff:0.0}</color>";

            case 0: return "<color=#FF8800>【狩猟】敵撃破時に追加経験値を得る可能性</color>";
            case 1: return "<color=#FF8800>【精密】射撃武器で低HPの敵を即死させる可能性</color>";
            case 2: return "<color=#FF8800>【敏捷】回避/ダッシュが体力を消費しない場合があり、少量回復する</color>";
            case 3: return "<color=#FF8800>【魔族化】最大HP1/2、攻撃力の1/4を吸収回復</color>";
            case 4: return "<color=#FF8800>【剛毅】ガードしていなくても一度だけ攻撃を完全無効化する可能性</color>";
            case 5: return "<color=#FF8800>【辟邪】凍結・毒・火傷・麻痺などの状態異常を無効化</color>";
            case 6: return "<color=#FF8800>【自然】回復時に追加でHPを多く回復する</color>";
            case 7: return "<color=#FF8800>【隠秘化】体力を消費して半透明化し、一時的に敵の視線をそらす</color>";
            default: return "";
        }
    }

    //========== CN ==========
    private string GetBuffDesc_CN(int id)
    {
        switch (id)
        {
            case -1:
                return $"<color=#FF8800>【近战加成】当前近战伤害：×{GameFlowData.Sword_Buff:0.0}</color>";
            case -2:
                return $"<color=#FF8800>【射击加成】当前射击伤害：×{GameFlowData.Pistol_Buff:0.0}</color>";
            case -3:
                return $"<color=#FF8800>【法术加成】当前法术伤害：×{GameFlowData.Staff_Buff:0.0}</color>";

            case 0: return "<color=#FF8800>【狩猎】在击败敌人后一定几率额外经验</color>";
            case 1: return "<color=#FF8800>【精准】射击武器对低生命值敌人有几率一击必杀</color>";
            case 2: return "<color=#FF8800>【敏捷】闪避/冲刺可能不消耗体力并恢复少量体力</color>";
            case 3: return "<color=#FF8800>【魔族化】最大生命 50%，吸收攻击力 25% 生命值</color>";
            case 4: return "<color=#FF8800>【坚韧】一定几率在未防御时完全免疫一次伤害</color>";
            case 5: return "<color=#FF8800>【辟邪】不会被冻结、中毒、灼烧、麻痹等异常状态</color>";
            case 6: return "<color=#FF8800>【自然】恢复生命时额外恢复部分生命值</color>";
            case 7: return "<color=#FF8800>【隐秘化】消耗体力隐身，短时间转移敌人视线</color>";
            default: return "";
        }
    }

    //========== TC ==========
    private string GetBuffDesc_TC(int id)
    {
        switch (id)
        {
            case -1:
                return $"<color=#FF8800>【近戰加成】當前近戰傷害：×{GameFlowData.Sword_Buff:0.0}</color>";
            case -2:
                return $"<color=#FF8800>【射擊加成】當前射擊傷害：×{GameFlowData.Pistol_Buff:0.0}</color>";
            case -3:
                return $"<color=#FF8800>【法術加成】當前法術傷害：×{GameFlowData.Staff_Buff:0.0}</color>";

            case 0: return "<color=#FF8800>【狩獵】擊敗敵人後有機率獲得額外經驗</color>";
            case 1: return "<color=#FF8800>【精準】射擊武器對低生命值敵人有機率一擊必殺</color>";
            case 2: return "<color=#FF8800>【敏捷】閃避/衝刺有機率不消耗體力並恢復少量體力</color>";
            case 3: return "<color=#FF8800>【魔族化】最大生命值50%，攻擊吸血25%</color>";
            case 4: return "<color=#FF8800>【堅韌】未防禦時有機率完全免疫一次傷害</color>";
            case 5: return "<color=#FF8800>【辟邪】免疫凍結、中毒、灼燒、麻痺等異常</color>";
            case 6: return "<color=#FF8800>【自然】生命恢復時額外回復</color>";
            case 7: return "<color=#FF8800>【隱秘化】消耗體力進入隱身，短時間轉移敵人視線</color>";
            default: return "";
        }
    }

    //========== EN ==========
    private string GetBuffDesc_EN(int id)
    {
        switch (id)
        {
            case -1:
                return $"<color=#FF8800>[Melee Bonus] Current Melee Damage: ×{GameFlowData.Sword_Buff:0.0}</color>";
            case -2:
                return $"<color=#FF8800>[Ranged Bonus] Current Ranged Damage: ×{GameFlowData.Pistol_Buff:0.0}</color>";
            case -3:
                return $"<color=#FF8800>[Magic Bonus] Current Magic Damage: ×{GameFlowData.Staff_Buff:0.0}</color>";

            case 0: return "<color=#FF8800>[Hunt] Chance for bonus EXP on kill</color>";
            case 1: return "<color=#FF8800>[Precision] Ranged may insta-kill low HP enemies</color>";
            case 2: return "<color=#FF8800>[Agility] Dodge/Dash may cost no stamina & restore some</color>";
            case 3: return "<color=#FF8800>[Demon Form] Max HP 50%; absorb 25% damage as HP</color>";
            case 4: return "<color=#FF8800>[Tenacity] May completely ignore one hit even without guarding</color>";
            case 5: return "<color=#FF8800>[Ward] Immune to Freeze, Poison, Burn, Paralysis</color>";
            case 6: return "<color=#FF8800>[Nature] Gain extra HP whenever healed</color>";
            case 7: return "<color=#FF8800>[Veil] Consume stamina to become translucent and divert enemy attention briefly</color>";
            default: return "";
        }
    }

    //========== KR ==========
    private string GetBuffDesc_KR(int id)
    {
        switch (id)
        {
            case -1:
                return $"<color=#FF8800>[근접 보너스] 현재 근접 데미지: ×{GameFlowData.Sword_Buff:0.0}</color>";
            case -2:
                return $"<color=#FF8800>[사격 보너스] 현재 사격 데미지: ×{GameFlowData.Pistol_Buff:0.0}</color>";
            case -3:
                return $"<color=#FF8800>[마법 보너스] 현재 마법 데미지: ×{GameFlowData.Staff_Buff:0.0}</color>";

            case 0: return "<color=#FF8800>[사냥] 처치 시 추가 경험치 획득 가능</color>";
            case 1: return "<color=#FF8800>[정밀] 저HP 적을 사격으로 즉사시킬 수 있음</color>";
            case 2: return "<color=#FF8800>[민첩] 회피/대시 시 스태미나가 들지 않고 조금 회복될 수 있음</color>";
            case 3: return "<color=#FF8800>[마족화] 최대 HP 50%, 가한 피해의 25%를 흡혈</color>";
            case 4: return "<color=#FF8800>[강인] 가드하지 않아도 한 번의 공격을 완전 무효화할 수 있음</color>";
            case 5: return "<color=#FF8800>[벽사] 빙결·중독·화상·마비 등 상태 이상 면역</color>";
            case 6: return "<color=#FF8800>[자연] 회복 시 추가 체력을 회복</color>";
            case 7: return "<color=#FF8800>[은밀화] 체력을 소모하여 투명화, 잠시 적의 시선을 돌린다</color>";
            default: return "";
        }
    }

    #endregion


    /// <summary>
    /// 窗口化
    /// </summary>
    #region
    [Header("窗口化")]
    public int screenModeIndex = 0;  // 0=全屏, 1=窗口化, 2=无边框
    public Text txt_ScreenMode;      // 显示文本

    private const string KEY_SCREEN_MODE = "ScreenMode";

    public void ToScreenSetting() 
    {
        SettingPagecurrentIndex =3;
        UpdateSettingPage_Highlight();
    }

    public void InitScreenMode()
    {
        // 手机跳过，不读也不写
        if (Application.isMobilePlatform)
        {
            txt_ScreenMode.text = "";
            return;
        }

        // 读取保存的模式，默认 0（全屏）
        screenModeIndex = PlayerPrefs.GetInt(KEY_SCREEN_MODE, 0);

        ApplyScreenMode();
        UpdateScreenModeText();
    }

    // 左箭头
    public void ScreenMode_Left()
    {
        if (Application.isMobilePlatform)
            return;

        screenModeIndex--;
        if (screenModeIndex < 0) screenModeIndex = 2;

        ApplyScreenMode();
        UpdateScreenModeText();

        PlayerPrefs.SetInt(KEY_SCREEN_MODE, screenModeIndex);
    }

    // 右箭头
    public void ScreenMode_Right()
    {
        if (Application.isMobilePlatform)
            return;

        screenModeIndex++;
        if (screenModeIndex > 2) screenModeIndex = 0;

        ApplyScreenMode();
        UpdateScreenModeText();

        PlayerPrefs.SetInt(KEY_SCREEN_MODE, screenModeIndex);
    }

    // 切换实际画面模式
    private void ApplyScreenMode()
    {
        switch (screenModeIndex)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; // 全屏
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed; // 窗口化
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow; // 无边框
                break;
        }
    }

    // 更新UI显示文本
    private void UpdateScreenModeText()
    {
        int lang = PlayerPrefs.GetInt("language", 0);  // 0–4

        txt_ScreenMode.text =
            ScreenModeNames[lang, screenModeIndex];
    }

    // 语言：0=JP, 1=CN, 2=TC, 3=EN, 4=KR
    private string[,] ScreenModeNames = new string[,]
    {
    // JP
    { "全画面", "ウィンドウ", "ボーダーレス" },

    // CN 简体
    { "全屏模式", "窗口化", "无边框全屏" },

    // TC 繁中
    { "全螢幕", "視窗化", "無邊框全螢幕" },

    // EN
    { "Fullscreen", "Windowed", "Borderless" },

    // KR
    { "전체화면", "창 모드", "무테 전체화면" }
    };

    #endregion


}

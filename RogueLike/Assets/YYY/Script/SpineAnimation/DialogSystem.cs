using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;

    private Dictionary<int, TextAsset> textAssets = new Dictionary<int, TextAsset>();


    public int index;
    public float textSpeed;
    bool textFinished;//是否完成打字
    bool cancelTyping;//取消打字
    List<string> textList = new List<string>();


    [Header("动画控制器")]
    public int animation_number;
    public Animator Background;
    public GameObject TheImage;//背景板，场景CG结局不能遮挡
    public Animator Black_CG;//专门用于CG的黑屏淡入淡出


    [Header("对话，背景，角色")]
    public GameObject TextButton;

    public Image BG_Image;
    public Sprite Story_00, 
                  Story_01, Story_02, Story_03, Story_04, Story_05, Story_06, Story_07, Story_08, Story_09, Story_10,
                  Story_11, Story_12, Story_13, Story_14, Story_15, Story_16, Story_17, Story_18, Story_19, Story_20, 
                  Story_21, Story_22, Story_23, Story_24, Story_25, Story_26, Story_27, Story_28,
                  Story_29, Story_30, Story_31, Story_32, Story_33, Story_34,
                  Story_35, Story_36, Story_37;


    private void OnEnable()
    {

        //textLabel.text = textList[index];
        //index++;
        Invoke("Read",0.1f);

    }//一开始不会产生空白，OnEnable会在Start之前，Awake之后被调用


    public void ForceEndDialogue()
    {
        // 清除当前对话状态
        textList.Clear();
        index = 0;

        // 设置 textFinished 为 true，以便退出正在进行的协程
        textFinished = true;

        // 将对话系统 UI 隐藏
        gameObject.SetActive(false);

        Debug.Log("对话已强制结束并重置");


    }//强制关闭对话

    void Read()
    {
        // Clear the existing dictionary to avoid key conflicts
        textAssets.Clear();

        switch (PlayerPrefs.GetInt("language"))
        {
            case 0:
                textAssets.Add(101, Resources.Load<TextAsset>("TXT_Japanese/J_CG_1"));


                textAssets.Add(1001, Resources.Load<TextAsset>("TXT_Japanese/J_Story_1"));
                textAssets.Add(1002, Resources.Load<TextAsset>("TXT_Japanese/J_Story_2"));
                textAssets.Add(1003, Resources.Load<TextAsset>("TXT_Japanese/J_Story_3"));
                textAssets.Add(1004, Resources.Load<TextAsset>("TXT_Japanese/J_Story_4"));
                textAssets.Add(1005, Resources.Load<TextAsset>("TXT_Japanese/J_Story_5"));
                textAssets.Add(1006, Resources.Load<TextAsset>("TXT_Japanese/J_Story_6"));
                textAssets.Add(1007, Resources.Load<TextAsset>("TXT_Japanese/J_Story_7"));
                textAssets.Add(1008, Resources.Load<TextAsset>("TXT_Japanese/J_Story_8"));
                textAssets.Add(1009, Resources.Load<TextAsset>("TXT_Japanese/J_Story_9"));
                textAssets.Add(1010, Resources.Load<TextAsset>("TXT_Japanese/J_Story_10"));
                textAssets.Add(1011, Resources.Load<TextAsset>("TXT_Japanese/J_Story_11"));
                textAssets.Add(1012, Resources.Load<TextAsset>("TXT_Japanese/J_Story_12"));
                textAssets.Add(1013, Resources.Load<TextAsset>("TXT_Japanese/J_Story_13"));
                break;


            case 1:
                textAssets.Add(1001, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_1"));
                textAssets.Add(1002, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_2"));
                textAssets.Add(1003, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_3"));
                textAssets.Add(1004, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_4"));
                textAssets.Add(1005, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_5"));
                textAssets.Add(1006, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_6"));
                textAssets.Add(1007, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_7"));
                textAssets.Add(1008, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_8"));
                textAssets.Add(1009, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_9"));
                textAssets.Add(1010, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_10"));
                textAssets.Add(1011, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_11"));
                textAssets.Add(1012, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_12"));
                textAssets.Add(1013, Resources.Load<TextAsset>("TXT_Simplified_Chinese/C1_Story_13"));
                break;


            case 2:
                textAssets.Add(1001, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_1"));
                textAssets.Add(1002, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_2"));
                textAssets.Add(1003, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_3"));
                textAssets.Add(1004, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_4"));
                textAssets.Add(1005, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_5"));
                textAssets.Add(1006, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_6"));
                textAssets.Add(1007, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_7"));
                textAssets.Add(1008, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_8"));
                textAssets.Add(1009, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_9"));
                textAssets.Add(1010, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_10"));
                textAssets.Add(1011, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_11"));
                textAssets.Add(1012, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_12"));
                textAssets.Add(1013, Resources.Load<TextAsset>("TXT_Traditional_Chinese/C2_Story_13"));
                break;


            case 3:
                textAssets.Add(1001, Resources.Load<TextAsset>("TXT_English/E_Story_1"));
                textAssets.Add(1002, Resources.Load<TextAsset>("TXT_English/E_Story_2"));
                textAssets.Add(1003, Resources.Load<TextAsset>("TXT_English/E_Story_3"));
                textAssets.Add(1004, Resources.Load<TextAsset>("TXT_English/E_Story_4"));
                textAssets.Add(1005, Resources.Load<TextAsset>("TXT_English/E_Story_5"));
                textAssets.Add(1006, Resources.Load<TextAsset>("TXT_English/E_Story_6"));
                textAssets.Add(1007, Resources.Load<TextAsset>("TXT_English/E_Story_7"));
                textAssets.Add(1008, Resources.Load<TextAsset>("TXT_English/E_Story_8"));
                textAssets.Add(1009, Resources.Load<TextAsset>("TXT_English/E_Story_9"));
                textAssets.Add(1010, Resources.Load<TextAsset>("TXT_English/E_Story_10"));
                textAssets.Add(1011, Resources.Load<TextAsset>("TXT_English/E_Story_11"));
                textAssets.Add(1012, Resources.Load<TextAsset>("TXT_English/E_Story_12"));
                textAssets.Add(1013, Resources.Load<TextAsset>("TXT_English/E_Story_13"));
                break;


            case 4:
                textAssets.Add(1001, Resources.Load<TextAsset>("TXT_Korean/K_Story_1"));
                textAssets.Add(1002, Resources.Load<TextAsset>("TXT_Korean/K_Story_2"));
                textAssets.Add(1003, Resources.Load<TextAsset>("TXT_Korean/K_Story_3"));
                textAssets.Add(1004, Resources.Load<TextAsset>("TXT_Korean/K_Story_4"));
                textAssets.Add(1005, Resources.Load<TextAsset>("TXT_Korean/K_Story_5"));
                textAssets.Add(1006, Resources.Load<TextAsset>("TXT_Korean/K_Story_6"));
                textAssets.Add(1007, Resources.Load<TextAsset>("TXT_Korean/K_Story_7"));
                textAssets.Add(1008, Resources.Load<TextAsset>("TXT_Korean/K_Story_8"));
                textAssets.Add(1009, Resources.Load<TextAsset>("TXT_Korean/K_Story_9"));
                textAssets.Add(1010, Resources.Load<TextAsset>("TXT_Korean/K_Story_10"));
                textAssets.Add(1011, Resources.Load<TextAsset>("TXT_Korean/K_Story_11"));
                textAssets.Add(1012, Resources.Load<TextAsset>("TXT_Korean/K_Story_12"));
                textAssets.Add(1013, Resources.Load<TextAsset>("TXT_Korean/K_Story_13"));
                break;

        }

      




        // 使用字典查找相应的 TextAsset
        if (textAssets.TryGetValue(animation_number, out TextAsset selectedText))
        {
            GetTextFormFile(selectedText);
        }
        else
        {
            Debug.LogError("No TextAsset found for animation_number: " + animation_number);
        }

        textFinished = true;
        StartCoroutine(SetTextUI());
    }

    public void ShowText()
    {
        if (textFinished && !cancelTyping)
        {
            if (index >= textList.Count) // 添加边界检查
            {

                //暂时先别消失，等场景跳转完毕
                //gameObject.SetActive(false);
                index = 0;

                ChangeStory();//结束重刷场景

                Debug.Log("对话已结束");
                return;
            }

            if (gameObject.activeSelf)
            {
                StartCoroutine(SetTextUI());
            }
        }
        else if (!textFinished)
        {
            cancelTyping = !cancelTyping;
        }

    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear(); index = 0;//首先将列表内的字符清空

        var lineDate = file.text.Split('\n');//以回车切割每一段

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }

    string Prompt;

    IEnumerator SetTextUI()
    {

        AudioManager.instance.AudioPlay(AudioManager.instance.Attack_pai1);

        if (index >= textList.Count)
        {
            Debug.LogWarning("index 超出 textList 范围");
            yield break;
        }

        textFinished = false;
        textLabel.text = "";

        string nextLine = textList[index].Trim();


        #region  当前提示词与后提示词不同 展示淡入动画
        // 只有是 Story 类型才触发
        if (nextLine.StartsWith("Story_") && Prompt != null)
        {
            if (Prompt != nextLine && Prompt.StartsWith("Story_"))//台词颜色不触发淡入特效
            {
                Background.SetTrigger("FadeIn");
                yield return new WaitForSeconds(0.3f);
            }
        }

        if (nextLine.StartsWith("Story_")) { Prompt = nextLine; }
       

        #endregion

        //判断一整行的字符是
        Text text = textLabel;
        switch (textList[index].Trim().ToString())
        {
            #region  CG结局调用

            case "Black":
                //透明CG通用，CG开场必然说这句
                Background.gameObject.SetActive(false);
                TheImage.SetActive(false);

                //起个头
                UIManager.instance.MainCamera.Play("CG_Camera_01");

               
                index++;
                break;

            case "Black_FadeOut":
                Black_CG.SetBool("Black", false);
                index++;
                break;
            case "Black_FadeIn":
                Black_CG.SetBool("Black",true);
                index++;
                break;
            case "--------------------Start--------------------":
                UIManager.instance.MainCamera.SetTrigger("Next");
                //生成存档内全体角色到达指定位置
                UIManager.instance._RoomGenerator.DelayCreatSetFriend_RBQ();
                index++;
                break;
            case "--------------------NEXT--------------------":
                UIManager.instance.MainCamera.SetTrigger("Next");

                index++;
                break;
            case "--------------------NEXT_BlackFadeOut_1--------------------":
                UIManager.instance.MainCamera.SetTrigger("Next");
                Black_CG.SetBool("Black", false);
                UIManager.instance._RoomGenerator.ArrangeRBQ();
                index++;
                break;
 
            case "--------------------NEXT_BlackFadeOut_2--------------------":
                UIManager.instance.MainCamera.SetTrigger("Next");
                Black_CG.SetBool("Black", false);
                UIManager.instance._RoomGenerator.SetRBQSide();//全体设置为正面

                UIManager.instance._RoomGenerator.cg_Manager.Creat_Man();//生成侧面群体

                GameObject clone = GameObject.Find("HideItem(Clone)");
                if (clone != null)
                {
                    Destroy(clone);
                }
                else
                {
                    Debug.LogWarning("没有找到 HideItem(Clone)");
                }
                index++;
                break;

            case "--------------------NEXT_BlackFadeOut_3--------------------":
                UIManager.instance.MainCamera.SetTrigger("Next");
                Black_CG.SetBool("Black", false);
                UIManager.instance._RoomGenerator.SetRBQFront2();//全体设置为正面强奸

                UIManager.instance._RoomGenerator.cg_Manager.Creat_HideItem();//生成正面群体

                GameObject clone2 = GameObject.Find("Man(Clone)");
                if (clone2 != null)
                {
                    Destroy(clone2);
                }
                else
                {
                    Debug.LogWarning("没有找到 Man(Clone)");
                }

                index++;
                break;

            case "--------------------NEXT_BlackFadeOut_4--------------------":
                UIManager.instance.MainCamera.SetTrigger("Next");
                Black_CG.SetBool("Black", false);
                UIManager.instance._RoomGenerator.SetRBQFront();//全体设置为正面

                GameObject clone3 = GameObject.Find("People_SE");
                if (clone3 != null)
                {
                    Destroy(clone3);
                }
                else
                {
                    Debug.LogWarning("没有找到 Man(Clone)");
                }
                GameObject clone4 = GameObject.Find("HideItem(Clone)");
                if (clone4 != null)
                {
                    Destroy(clone4);
                }
                else
                {
                    Debug.LogWarning("没有找到 HideItem(Clone)");
                }

                UIManager.instance._RoomGenerator.SkyBoxNumber = 0;//晚上
                UIManager.instance._RoomGenerator.SetFog();

                index++;
                break;

            #endregion


            #region  字的颜色

            case "BG":
                text.color = Color.white;

                index++;
                break;





            //case "Girl":
            //    text.color = new Color(1.0f, 0.0f, 1.0f, 1.0f);//粉色
            //    index++;
            //    break;

            case "MAN":
                text.color = new Color(0.0f, 0.68f, 0.93f, 1.0f);//蓝色(市民群众士兵)

                index++;
                break;
            case "Orange":
                text.color = new Color(1.0f, 0.5f, 0.0f, 1.0f); // 橙色(女性敌人)
                index++;
                break;
            case "DeepRed":
                text.color = new Color(0.8f, 0.2f, 0.2f, 1.0f); // 深红色(性奴)
                index++;
                break;

            case "Gray":
                text.color = new Color(0.7f, 0.75f, 0.8f, 1.0f); // 亮灰色(露娜)
                index++;
                break;
            case "DarkRed":   
                text.color = new Color(1.0f, 0.2f, 0.5f, 1.0f); //浅红色(王女）
                index++;
                break;
            case "LightBlue":
                text.color = new Color(0.68f, 0.85f, 0.9f, 1.0f); // 浅蓝色（皇太子）
                index++;
                break;
            case "Yellow":
                text.color = new Color(1.0f, 1.0f, 0.0f, 1.0f); // 黄色（宰相/皇帝）
                index++;
                break;

            case "Green":
                text.color = new Color(0.0f, 1.0f, 0.0f, 1.0f); // 绿色（魔族女干部）
                index++;
                break;




            case "Gold":
                text.color = new Color(1.0f, 0.84f, 0.0f, 1.0f); // 金色（叛变战姬大队长）
                index++;
                break;


      
            case "Purple":
                text.color = new Color(0.7f, 0.3f, 0.7f, 1.0f); // 紫色 (女记者)
                index++;
                break;


            #endregion


            #region 图片

            case "Story_00":
                BG_Image.sprite = Story_00;
                text.color = Color.white;
                index++;
                break;
            case "Story_01":
                BG_Image.sprite = Story_01;
                text.color = Color.white;
                index++;
                break;
            case "Story_02":
                BG_Image.sprite = Story_02;
                text.color = Color.white;
                index++;
                break;
            case "Story_03":
                BG_Image.sprite = Story_03;
                text.color = Color.white;
                index++;
                break;
            case "Story_04":
                BG_Image.sprite = Story_04;
                text.color = Color.white;
                index++;
                break;
            case "Story_05":
                BG_Image.sprite = Story_05;
                text.color = Color.white;
                index++;
                break;
            case "Story_06":
                BG_Image.sprite = Story_06;
                text.color = Color.white;
                index++;
                break;
            case "Story_07":
                BG_Image.sprite = Story_07;
                text.color = Color.white;
                index++;
                break;
            case "Story_08":
                BG_Image.sprite = Story_08;
                text.color = Color.white;
                index++;
                break;
            case "Story_09":
                BG_Image.sprite = Story_09;
                text.color = Color.white;
                index++;
                break;
            case "Story_10":
                BG_Image.sprite = Story_10;
                text.color = Color.white;
                index++;
                break;
            case "Story_11":
                BG_Image.sprite = Story_11;
                text.color = Color.white;
                index++;
                break;
            case "Story_12":
                BG_Image.sprite = Story_12;
                text.color = Color.white;
                index++;
                break;
            case "Story_13":
                BG_Image.sprite = Story_13;
                text.color = Color.white;
                index++;
                break;
            case "Story_14":
                BG_Image.sprite = Story_14;
                text.color = Color.white;
                index++;
                break;
            case "Story_15":
                BG_Image.sprite = Story_15;
                text.color = Color.white;
                index++;
                break;
            case "Story_16":
                BG_Image.sprite = Story_16;
                text.color = Color.white;
                index++;
                break;
            case "Story_17":
                BG_Image.sprite = Story_17;
                text.color = Color.white;
                index++;
                break;
            case "Story_18":
                BG_Image.sprite = Story_18;
                text.color = Color.white;
                index++;
                break;
            case "Story_19":
                BG_Image.sprite = Story_19;
                text.color = Color.white;
                index++;
                break;
            case "Story_20":
                BG_Image.sprite = Story_20;
                text.color = Color.white;
                index++;
                break;
            case "Story_21":
                BG_Image.sprite = Story_21;
                text.color = Color.white;
                index++;
                break;
            case "Story_22":
                BG_Image.sprite = Story_22;
                text.color = Color.white;
                index++;
                break;
            case "Story_23":
                BG_Image.sprite = Story_23;
                text.color = Color.white;
                index++;
                break;
            case "Story_24":
                BG_Image.sprite = Story_24;
                text.color = Color.white;
                index++;
                break;
            case "Story_25":
                BG_Image.sprite = Story_25;
                text.color = Color.white;
                index++;
                break;
            case "Story_26":
                BG_Image.sprite = Story_26;
                text.color = Color.white;
                index++;
                break;
            case "Story_27":
                BG_Image.sprite = Story_27;
                text.color = Color.white;
                index++;
                break;
            case "Story_28":
                BG_Image.sprite = Story_28;
                text.color = Color.white;
                index++;
                break;
            case "Story_29":
                BG_Image.sprite = Story_29;
                text.color = Color.white;
                index++;
                break;
            case "Story_30":
                BG_Image.sprite = Story_30;
                text.color = Color.white;
                index++;
                break;
            case "Story_31":
                BG_Image.sprite = Story_31;
                text.color = Color.white;
                index++;
                break;
            case "Story_32":
                BG_Image.sprite = Story_32;
                text.color = Color.white;
                index++;
                break;
            case "Story_33":
                BG_Image.sprite = Story_33;
                text.color = Color.white;
                index++;
                break;
            case "Story_34":
                BG_Image.sprite = Story_34;
                text.color = Color.white;
                index++;
                break;
            case "Story_35":
                BG_Image.sprite = Story_35;
                text.color = Color.white;
                index++;
                break;
            case "Story_36":
                BG_Image.sprite = Story_36;
                text.color = Color.white;
                index++;
                break;
            case "Story_37":
                BG_Image.sprite = Story_37;
                text.color = Color.white;
                index++;
                break;


                #endregion
        }


        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }

        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;


        
    }


    //快进按钮触发在这里
    public void ChangeStory()
    {
        //gameObject.SetActive(false);
        //UIManager.instance.ToSavePageButton(1);

        AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);


        switch (animation_number)
        {
            case 1001:
                GameFlowData.nextScene = "Story_01";
                break;
            case 1002:
                GameFlowData.nextScene = "Story_02";
                break;
            case 1003:
                GameFlowData.nextScene = "Story_03";
                break;
            case 1004:
                GameFlowData.nextScene = "Story_04";
                break;
            case 1005:
                GameFlowData.nextScene = "Story_05";
                break;
            case 1006:
                GameFlowData.nextScene = "Story_06";
                break;
            case 1007:
                GameFlowData.nextScene = "Story_07";
                break;
            case 1008:
                GameFlowData.nextScene = "Story_08";
                break;
            case 1009:
                GameFlowData.nextScene = "Story_09";
                break;
            case 1010:
                GameFlowData.nextScene = "Story_10";
                break;
            case 1011:
                GameFlowData.nextScene = "Story_11";
                break;
            case 1012:
                GameFlowData.nextScene = "Story_12";
                break;
            case 1013:
            case 101:
                GameFlowData.nextScene = "";//返回主菜单
                break;
        }

        UIManager.instance.ReLoadScene();
    }




}

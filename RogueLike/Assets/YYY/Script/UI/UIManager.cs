using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

using System;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    void Awake()
    {
        instance = this;
    }
    /// <summary>
    /// 主菜单
    /// </summary>
    #region
    [Header("主菜单")]
    public GameObject Common_All;//移动血条等
    public GameObject NextButton;//播放结局动画
    public GameObject Loading;
    public void Ending_UI()
    {
        Common_All.SetActive(false);
        NextButton.SetActive(true);


    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Loading.SetActive(true);
    }

    [Header("主菜单界面层级")]
    public int CurrentChooseList = 0;//-1确认是否删除存档  0 存档界面   1捏人界面
    public int CreatNewcurrentIndex = 0;//0 名称 1眼睛  2头  3种族  4职业  5确定
    [SerializeField] private GameObject[] highlightObjs; // 捏人界面高亮显示

    private void UpdateHighlight()
    {
        for (int i = 0; i < highlightObjs.Length; i++)
        {
            highlightObjs[i].SetActive(i == CreatNewcurrentIndex);
        }
    }

    [SerializeField] private Button HairLeft, HairRight;
    [SerializeField] private Button EyesLeft, EyesRight;
    [SerializeField] private Button RaceLeft, RaceRight;
    [SerializeField] private Button ClassLeft, ClassRight;

    public Button okButton;

    #endregion

    /// <summary>
    /// 捏人菜单
    /// </summary>
    #region
    [Header("各类菜单")]
    public bool isPause = true;//一开始就Menu界面
    public Animator MainCamera;//控制摄像机拉近远离
    public Animator ShowSaveCavansAnim;//黑幕显示背景

    public GameObject SaveCavans, CreateCavans;//存档界面,捏人界面

    [Header("捏人界面UI")]
    public InputField nameInputField; // 绑定在 Inspector 里

    public Text hairLabel;
    public Text eyesLabel;
    public Text raceLabel;
    public Text classLabel;



    public void OnHairLeft() { ChangeSkin(ref player.YYY_headIndex, 1, 13, -1); }
    public void OnHairRight() { ChangeSkin(ref player.YYY_headIndex, 1, 13, +1); }

    public void OnEyesLeft() { ChangeSkin(ref player.YYY_eyesIndex, 1, 13, -1); }
    public void OnEyesRight() { ChangeSkin(ref player.YYY_eyesIndex, 1, 13, +1); }

    public void OnRaceLeft() { ChangeSkin(ref player.YYY_hatIndex, 1, 4, -1); }
    public void OnRaceRight() { ChangeSkin(ref player.YYY_hatIndex, 1, 4, +1); }

    public void OnClassLeft() { ChangeSkin(ref player.YYY_bodyIndex, 10, 12, -1); player.PlayNormalAttack(); }
    public void OnClassRight() { ChangeSkin(ref player.YYY_bodyIndex, 10, 12, +1); player.PlayNormalAttack(); }
    void ChangeSkin(ref int index, int min, int max, int delta)
    {
        index += delta;
        if (index < min) index = max;
        if (index > max) index = min;

        player.SetSkin();         // 更新角色外观
        UpdateUI();               // 更新文字
        player.SaveCurrent();     // 存一份当前皮肤到缓存/存档

        RefreshSaveSlots();//刷新存档界面



        player._ClothesToClass();//临时让衣服改变职业



    }//捏人界面玩家点击单个皮肤选项左右之后

    void UpdateUI()
    {
        nameInputField.text = player.currentSaveName;

        hairLabel.text = $"Hair_{player.YYY_headIndex}";
        eyesLabel.text = $"Eyes_{player.YYY_eyesIndex}";
        raceLabel.text = $"Race_{player.YYY_hatIndex}";
        classLabel.text = $"Class_{player.YYY_bodyIndex}";
    }//捏人界面UI显示



    public void OnConfirmNameInput()
    {
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
            weaponIndex = player.weaponIndex
        };

        SaveManager.Save(data); // ✅ 保存新名字存档
        SaveManager.DeleteSave(oldName); // 🗑️ 删除旧存档

        player.currentSaveName = newName; // 更新记录
        Debug.Log($"名称更换成功：{oldName} → {newName}");

        RefreshSaveSlots();//每次单独更新名称也需要刷新存档界面

    }// 玩家输入新名字，调用此函数

    public void OnConfirm()
    {

        //抹去当前名称，下次捏人再度选中名称
        player.currentSaveName = null;

        //显示存档界面，隐藏捏人界面
        CreateCavans.SetActive(false);
        SaveCavans.SetActive(true);

        UpdateCurrentSelection(currentIndex);//完成捏人后再一次回到当前选中

        CurrentChooseList = 0;//返回存档界面

        //再度把捏人的检索回到名字
        CreatNewcurrentIndex = 0;
        UpdateHighlight();
    }//玩家点击Ok


    public void OpenCloseMenu()
    {
        if (!isPause)
        {
            MainCamera.SetBool("Track", true);
            Common_All.SetActive(false);
            ShowSaveCavansAnim.gameObject.SetActive(true);
            ShowSaveCavansAnim.SetBool("Track", true);


            player.isInputBlocked = true;//切断玩家的方向攻击等输入


        }
        else
        {
            MainCamera.SetBool("Track", false);
            Common_All.SetActive(true);
            ShowSaveCavansAnim.gameObject.SetActive(false);
            ShowSaveCavansAnim.SetBool("Track", false);

            player.isInputBlocked = false;//恢复玩家的方向攻击等输入


        }

        isPause = !isPause;
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

    }//读取，显示存档

    public void CreateNewSave()
    {
        player._CreateNewSkin();
        RefreshSaveSlots();

        //显示捏人界面，隐藏存档界面
        SaveCavans.SetActive(false);
        CreateCavans.SetActive(true);

        UpdateUI();//更新捏人界面UI

        CurrentChooseList = 1;//进入捏人界面
    }//点击【＋】就会随机存档

    public void RefreshSaveSlots()//新增存档，新增存档时更换名字，新增存档时更换皮肤，删除存档
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


    //////////////////////列表显示存档，方向键切换当前选中按钮//////////////////////////////////
    public List<SaveSlotUI> saveSlots = new List<SaveSlotUI>();
    public int currentIndex = 0;

    public void UpdateCurrentSelection(int newIndex)
    {
        if (saveSlots.Count == 0) return;

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
        if (saveSlots.Count == 0) return;

        currentSelectedSlot.DeleteCurrentSave();

        Invoke("CancelDelete",0.1f);//目前暂时这么做，以防确定按太快直接跳到捏人界面

    }//删除这个角色

    public void CancelDelete() 
    {
        MakeSureDeleteCurrentSave.SetActive(false);
        CurrentChooseList = 0;//返回存档界面
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
        Debug.Log("目前的存档数" + totalItems);
        Debug.Log("目前需要几行显示所有存档" + totalRows);

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

        if (saveSlotParent.childCount <= 10) { ScrollDown_Button.SetActive(false); Debug.Log("目前需要几行显示所有存档[!!!!]" + totalRows); }//存档数小于等于10也不能下翻

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
    /// 菜单层面多端输入
    /// </summary>
    #region
    [SerializeField] private InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction confirmAction;
    private InputAction cancelAction;
    private InputAction deleteAction;
    private InputAction pauseAction;

    private void OnEnable()
    {
        moveAction = inputActions.FindAction("Move");
        confirmAction = inputActions.FindAction("Attack");  // 或者用名为 "Submit"
        cancelAction = inputActions.FindAction("Dodge");    // 或者用名为 "Cancel"
        deleteAction = inputActions.FindAction("Run");    // 或者用名为 "Delete"
        pauseAction = inputActions.FindAction("Pause");

        moveAction.performed += OnMove;
        confirmAction.started += OnConfirm;
        cancelAction.started += OnCancel;
        deleteAction.started += OnDelete;
        pauseAction.started += OnPause;

        moveAction.Enable();
        confirmAction.Enable();
        cancelAction.Enable();
        deleteAction.Enable();
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        confirmAction.started -= OnConfirm;
        cancelAction.started -= OnCancel;
        deleteAction.started -= OnDelete;
        pauseAction.started -= OnPause;

        moveAction.Disable();
        confirmAction.Disable();
        cancelAction.Disable();
        deleteAction.Disable();
        pauseAction.Disable();
    }

    //冷却时间
    private float inputCooldown2 = 0.2f;
    private float lastInputTime2 = -999f;

    private void OnMove(InputAction.CallbackContext ctx)
    {

        #region 冷却时间
        if (Time.time - lastInputTime2 < inputCooldown2)
            return;

        lastInputTime2 = Time.time;
        #endregion

        if (player.isInputBlocked)
        {
            Vector2 dir = ctx.ReadValue<Vector2>();


            if (CurrentChooseList == 0)
            {
                if (dir.x != 0)
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

                }
                else
                {
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
            }
            else
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
                    //CreatNewcurrentIndex++;

                }
                else if (dir.y < -0.5f)
                {
                    CreatNewcurrentIndex = Mathf.Clamp(CreatNewcurrentIndex + 1, 0, 5);
                    UpdateHighlight();
                    //CreatNewcurrentIndex--;

                }

            }





            AudioManager.instance.AudioPlay(AudioManager.instance.Attack_pai1);
        }




    }

    private void OnConfirm(InputAction.CallbackContext ctx)
    {
        if (player.isInputBlocked)
        {
            // 可选：进入下一级菜单、确认开始游戏等
            if (CurrentChooseList == -1)
            {
                DeleteCurrentSelection();//删除角色
            }
            if (CurrentChooseList == 0)
            {
                CreateNewSave();
                AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
            }
            if (CurrentChooseList == 1)
            {

                if (CreatNewcurrentIndex == 0)
                {
                    //编辑名称
                    nameInputField.ActivateInputField(); // ✅ 激活输入框并进入编辑
                }
                if (CreatNewcurrentIndex == 5)
                {
                    //点 OK
                    okButton.onClick.Invoke();
                    AudioManager.instance.AudioPlay(AudioManager.instance.Attack_katana_draw);
                }
            }



           
        }


    }

    private void OnCancel(InputAction.CallbackContext ctx)
    {
        if (player.isInputBlocked)
        {
            // 可选：退出菜单、返回上一级等
            if (CurrentChooseList == -1)
            {
                CancelDelete();//取消删除
                AudioManager.instance.AudioPlay(AudioManager.instance.SE_Glass);
            }

            
        }

    }

    private void OnDelete(InputAction.CallbackContext ctx)
    {
        if (player.isInputBlocked)
        {
            // 可选：删除存档
            if (CurrentChooseList == 0)
            {
                currentSelectedSlot.Delete();//跳出是否删除存档界面
                //DeleteCurrentSelection();
            }
          
        }

    }
    private void OnPause(InputAction.CallbackContext ctx)
    {


        // 可选：暂停继续游戏
        if (CurrentChooseList == 0)
        {
            OpenCloseMenu();
        }
       

        AudioManager.instance.AudioPlay(AudioManager.instance.Bullet_AK);
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
    #endregion



}

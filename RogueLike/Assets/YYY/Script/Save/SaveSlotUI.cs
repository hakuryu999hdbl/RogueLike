using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{

    /// <summary>
    /// 存档数值显示
    /// </summary>
    #region
    [Header("存档数值的显示")]
    public Text nameText;
    public Text timeText;

    public Text LevelText;
    public Image ExpBar;
    public Text HealthText;
    public Text MeleeDamage;
    public Text ShootDamage;
    public Text SpellDamage;

    public Text CurrentWeapon;   //武器名称
    public Text CurrentWeaponPower;    // 武器攻击值
    public Text CurrentArmor;    //衣服名称
    public Text CurrentArmorDefence;      // 衣服防御值
    public Text CurrentStocking;   //丝袜名称
    public Text CurrentStockingDefence;   // 丝袜防御值

    //需要展示名称，暂时记录
    public int WeaponID;
    public int ClothesID;
    public int StockingID;

    public int ProfessionID;//职业

    public Image Hat;
    public Image Hair;
    public Image Eyes;
    public Image Mouse;
    public Image Head;
    public Image Clothes;
    public Image Longhair;
    public Image Ponytail;
    [Header("武器小图标切换")]
    public Image Weapon_Icon;
    public Sprite Sword, Arrow, Staff;
    public Image Level_Icon;

    [Header("存档数值本身")]
    public PlayerSaveData Data;//大家需要通过UIManager找你

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

        //需要展示名称，暂时记录
        WeaponID = Data.weaponIndex;
        ClothesID = Data.bodyIndex;
        StockingID = Data.legsIndex;

        ProfessionID = Data.professionIndex;//读取职业

        nameText.text = Data.characterName;
        timeText.text = Data.lastSavedTime;


        Hat.sprite = database.HatSprites[data.hatIndex - 1];
        Hair.sprite = database.HairSprites[data.headIndex - 1];
        Eyes.sprite = database.EyesSprites[data.eyesIndex - 1];
        Mouse.sprite = database.MouseSprites[data.headIndex - 1];
        Head.sprite = database.HeadSprites[data.headIndex - 1];
        Clothes.sprite = database.ClothesSprites[data.bodyIndex - 1]; // Body决定Clothes
        Longhair.sprite = database.LonghairSprites[data.headIndex - 1];
        Ponytail.sprite = database.PonytailSprites[data.headIndex - 1];



        LevelText.text = Data.level.ToString();
        UpdateExpBar(Data.exp, Data.level * 1000);
        HealthText.text = Data.maxHP.ToString();

        CurrentWeapon.text = GetWeaponName(PlayerPrefs.GetInt("language")).ToString();
        MeleeDamage.text = Data.meleeDamage.ToString();
        CurrentArmor.text = GetClothesName(PlayerPrefs.GetInt("language")).ToString();
        ShootDamage.text = Data.shootDamage.ToString();
        CurrentStocking.text = GetStockingName(PlayerPrefs.GetInt("language")).ToString();
        SpellDamage.text = Data.spellDamage.ToString();

        CurrentWeaponPower.text = Data.weaponAtk.ToString();
        CurrentArmorDefence.text = Data.armorDef.ToString();
        CurrentStockingDefence.text = Data.stockingDef.ToString();

        // 新增颜色：
        HealthText.color = HealthColor(Data.maxHP);

        MeleeDamage.color = AttackColor(Data.meleeDamage);
        ShootDamage.color = AttackColor(Data.shootDamage);
        SpellDamage.color = AttackColor(Data.spellDamage);

        // 武器：名称 + 数值 同色
        {
            var c = WeaponColor(Data.weaponAtk);
            ApplyPairColor(CurrentWeapon, CurrentWeaponPower, c);
        }

        // 衣服：名称 + 防御 同色
        {
            var c = ArmorLikeColor(Data.armorDef);
            ApplyPairColor(CurrentArmor, CurrentArmorDefence, c);
        }

        // 丝袜：名称 + 防御 同色
        {
            var c = ArmorLikeColor(Data.stockingDef);
            ApplyPairColor(CurrentStocking, CurrentStockingDefence, c);
        }


        switch (ProfessionID)
        {
            case 0:
                Weapon_Icon.sprite = Sword;
                break;
            case 1:
                Weapon_Icon.sprite = Arrow;
                break;
            case 2:
                Weapon_Icon.sprite = Staff;
                break;
        }
        switch (Data.level)
        {
            case 1:
            case 2:
            case 3:
                Level_Icon.sprite = database.LevelSprites[0];
                break;
            case 4:
            case 5:
            case 6:
                Level_Icon.sprite = database.LevelSprites[1];
                break;
            case 7:
            case 8:
            case 9:
                Level_Icon.sprite = database.LevelSprites[2];
                break;
            case 10:
            case 11:
            case 12:
                Level_Icon.sprite = database.LevelSprites[3];
                break;
            case 13:
            case 14:
            case 15:
                Level_Icon.sprite = database.LevelSprites[4];
                break;
            case 16:
            case 17:
            case 18:
                Level_Icon.sprite = database.LevelSprites[5];
                break;
            case 19:
            case 20:
            case 21:
                Level_Icon.sprite = database.LevelSprites[6];
                break;
            case 22:
            case 23:
            case 24:
            default:
                Level_Icon.sprite = database.LevelSprites[7];
                break;
        }

        highlightFrame.SetActive(false); // 初始隐藏
    }//导入皮肤


    public void UpdateExpBar(int curAmount, int maxAmount)
    {
        ExpBar.fillAmount = (float)curAmount / (float)maxAmount;

    }



    public static class WeaponNameDatabase
    {
        // 语言顺序：日文、简中、繁中、英文、韩文
        public static readonly Dictionary<int, string[][]> WeaponNames = new Dictionary<int, string[][]>()
    {
        {
            0, new string[][] // 近战武器
            {
                new string[] { "", "ダガー", "ブロードソード", "長柄両刃斧", "長槍", "長柄斧", "氷結の剣", "黒鉄の刺剣", "溶岩の剣", "雷を引く剣", "古代の大剣" }, // 日
                new string[] { "", "匕首", "阔剑", "长柄双刃斧", "长枪", "长柄斧", "冻结剑", "黑铁刺剑", "熔岩剑", "引雷剑", "古重剑" }, // 简
                new string[] { "", "匕首", "闊劍", "長柄雙刃斧", "長槍", "長柄斧", "凍結劍", "黑鐵刺劍", "熔岩劍", "引雷劍", "古重劍" }, // 繁
                new string[] { "", "Dagger", "Broad Sword", "Double Axe", "Spear", "Poleaxe", "Frost Sword", "Black Iron Rapier", "Lava Blade", "Thunderblade", "Ancient Greatsword" }, // 英
                new string[] { "", "단검", "브로드소드", "장병 도끼", "장창", "폴액스", "동결의 검", "흑철 레이피어", "용암검", "번개를 부르는 검", "고대 대검" } // 韩
            }
        },
        {
            1, new string[][] // 射击武器
            {
                new string[] { "", "軽弩", "重弩", "複合弩", "火縄複合銃", "火縄短銃", "火縄長銃", "フリントロック銃", "バヨネット銃", "火縄黄銅銃", "銀象嵌銃" },
                new string[] { "", "轻弩", "重弩", "复合弩", "火绳复合枪", "火绳短枪", "火绳长枪", "燧发枪", "刺刀火枪", "火绳黄铜枪", "镶银火枪" },
                new string[] { "", "輕弩", "重弩", "複合弩", "火繩複合槍", "火繩短槍", "火繩長槍", "燧發槍", "刺刀火槍", "火繩黃銅槍", "鑲銀火槍" },
                new string[] { "", "Light Crossbow", "Heavy Crossbow", "Compound Bow", "Matchlock Rifle", "Short Matchlock", "Long Matchlock", "Flintlock", "Bayonet Musket", "Brass Matchlock", "Silver-Inlaid Musket" },
                new string[] { "", "경궁", "중궁", "복합궁", "화승총", "단화승총", "장화승총", "화승총", "총검총", "황동 화승총", "은장총" }
            }
        },
        {
            2, new string[][] // 法杖类
            {
                new string[] { "", "黄木の杖", "ハーピーの杖", "ルビーの杖", "サファイアの杖", "トパーズの杖", "氷の杖", "アメジストの杖", "エメラルドの杖", "雷の杖", "古代の杖" },
                new string[] { "", "黄木短杖", "鹰身短杖", "红宝石短杖", "蓝宝石短杖", "黄玉短杖", "冰冻法杖", "紫水晶法杖", "翡翠法杖", "雷霆法杖", "古木法杖" },
                new string[] { "", "黃木短杖", "鷹身短杖", "紅寶石短杖", "藍寶石短杖", "黃玉短杖", "冰凍法杖", "紫水晶法杖", "翡翠法杖", "雷霆法杖", "古木法杖" },
                new string[] { "", "Yellowwood Wand", "Harpy Wand", "Ruby Wand", "Sapphire Wand", "Topaz Wand", "Frost Wand", "Amethyst Wand", "Emerald Wand", "Thunder Wand", "Ancient Wand" },
                new string[] { "", "황목 지팡이", "하피 지팡이", "루비 지팡이", "사파이어 지팡이", "토파즈 지팡이", "얼음 지팡이", "자수정 지팡이", "에메랄드 지팡이", "천둥 지팡이", "고대 지팡이" }
            }
        }
    };
    }
    public string GetWeaponName(int language)
    {
        if (WeaponNameDatabase.WeaponNames.TryGetValue(ProfessionID, out string[][] langTable))
        {
            if (language >= 0 && language < langTable.Length)
            {
                string[] names = langTable[language];
                if (WeaponID >= 1 && WeaponID < names.Length)
                    return names[WeaponID];
            }
        }

        return "未知武器";
    }

    public string GetClothesName(int language)
    {
        //目前衣服还没有做全,暂时用这个方法转换为对应的编号
        int index = 0;
        switch (ClothesID)
        {
            case 1:
                index = 4;
                break;
            case 2:
                index = 3;
                break;
            case 10:
                index = 0;
                break;
            case 11:
                index = 1;
                break;
            case 12:
                index = 2;
                break;
        }


        if (index >= 0 && index < EquipmentNameDatabase.ClothesNames[language].Length)
            return EquipmentNameDatabase.ClothesNames[language][index + 1];

        return "未知盔甲";
    }

    public string GetStockingName(int language)
    {
        //目前衣服还没有做全,暂时用这个方法转换为对应的编号
        int index = 0;
        switch (StockingID)
        {
            case 1:
                index = 4;
                break;
            case 2:
                index = 3;
                break;
            case 10:
                index = 0;
                break;
            case 11:
                index = 1;
                break;
            case 12:
                index = 2;
                break;
        }



        if (index >= 0 && index < EquipmentNameDatabase.StockingNames[language].Length)
            return EquipmentNameDatabase.StockingNames[language][index + 1];

        return "未知丝袜";
    }
    public static class EquipmentNameDatabase
    {
        public static readonly string[][] ClothesNames = new string[][]
        {
        new string[] { "", "女性用鎧", "盗賊の軽装", "魔導士団制服", "暗殺マント", "なし" },
        new string[] { "", "女式盔甲", "盗贼便服", "魔导士团制服", "刺客斗篷", "无" },
        new string[] { "", "女式盔甲", "盜賊便服", "魔導士團制服", "刺客斗篷", "無" },
        new string[] { "", "Women's Armor", "Rogue Outfit", "Mage Order Uniform", "Assassin Cloak", "None" },
        new string[] { "", "여성용 갑옷", "도적 복장", "마도기사단 제복", "암살망토", "없음" }
        };

        public static readonly string[][] StockingNames = new string[][]
        {
        new string[] { "", "ストッキング腿铠", "ソックスブーツ", "ガーターソックス", "ニーブーツ", "なし" },
        new string[] { "", "丝袜腿甲", "黑色长袜靴", "蕾丝吊带袜", "过膝袜短靴", "无" },
        new string[] { "", "絲襪腿甲", "黑色長襪靴", "蕾絲吊帶襪", "過膝襪短靴", "無" },
        new string[] { "", "Leg Armor", "Long Boots", "Garter Stockings", "Knee Boots", "None" },
        new string[] { "", "스타킹 레그 아머", "검은색 롱삭스 부츠", "검은색 레이스 가터 스타킹", "하이부츠", "없음" }
        };
    }



    #region 数值颜色
    // 可按需换成你的项目色值
    static readonly Color WHITE = Color.white;
    static readonly Color GREEN = new Color32(46, 204, 113, 255);
    static readonly Color BLUE = new Color32(52, 152, 219, 255);
    static readonly Color PURPLE = new Color32(155, 89, 182, 255);
    static readonly Color GOLD = new Color32(255, 215, 0, 255); // #FFD700
    static readonly Color RED = new Color32(231, 76, 60, 255);

    static Color PickColor(int v, (int maxExclusive, Color c)[] rules, Color overflow)
    {
        for (int i = 0; i < rules.Length; i++)
            if (v < rules[i].maxExclusive) return rules[i].c;
        return overflow; // X 红
    }

    public static void ApplyPairColor(Text nameText, Text valueText, Color c)
    {
        if (nameText) nameText.color = c;
        if (valueText) valueText.color = c;
    }

    // —— 阈值规则（严格按你提供的区间顺序）——
    public static Color HealthColor(int hp)
    {
        var rules = new (int, Color)[] {
            (1500, WHITE), (2000, BLUE), (5000, GREEN),
            (8000, PURPLE), (10000, GOLD),
        };
        return PickColor(hp, rules, RED);
    }

    // 近战 / 射击 / 法术
    public static Color AttackColor(int atk)
    {
        var rules = new (int, Color)[] {
            (100, WHITE), (200, BLUE), (500, GREEN),
            (800, PURPLE), (1000, GOLD),
        };
        return PickColor(atk, rules, RED);
    }

    // 武器（名称与攻击值同色）
    public static Color WeaponColor(int wp)
    {
        var rules = new (int, Color)[] {
            (300, WHITE), (500, BLUE), (800, GREEN),
            (1000, PURPLE), (1500, GOLD),
        };
        return PickColor(wp, rules, RED);
    }

    // 衣服/丝袜（名称与防御值同色）
    public static Color ArmorLikeColor(int def)
    {
        var rules = new (int, Color)[] {
            (50, WHITE), (100, BLUE), (200, GREEN),
            (500, PURPLE), (800, GOLD),
        };
        return PickColor(def, rules, RED);
    }


    #endregion

    #endregion


    /// <summary>
    /// 外部衔接
    /// </summary>
    #region
    public void SetHighlight(bool on)
    {
        highlightFrame.SetActive(on);


    }//高亮显示

    public void DelayChoose()
    {
        Invoke("Choose", 0.1f);
    }

    public void Choose()
    {
        player.ApplySaveData(Data);
        UIManager.instance.SetCurrentSlot(this); // 通知UIManager进行高亮更新


        player._ClothesToClass();//临时让衣服改变职业

        // AudioManager.instance.AudioPlay(AudioManager.instance.Attack_pai1);

    }//选择这个档的皮肤

    public void Delete()
    {
        UIManager.instance.TryDelete();
    }//弹出确认删除存档框
    public void DeleteCurrentSave()
    {
        // SaveManager.DeleteSave(Data.characterName);
        // player.ClearSkin(); // ✅ 清除当前皮肤
        //
        // Destroy(this.gameObject);
        //
        //
        // UIManager.instance.RefreshSaveSlots(); // ✅ 删除后刷新
        //
        // UIManager.instance.UpdateCurrentSelection(UIManager.instance.currentIndex);//刷新列表后也是选中当前
        //
        // AudioManager.instance.AudioPlay(AudioManager.instance.Effect_tear1);


        // 1️⃣ 安全删除存档文件
        SaveManager.DeleteSave(Data.characterName);

        // 2️⃣ 从UI列表移除自己（防止残留）
        if (UIManager.instance.saveSlots.Contains(this))
            UIManager.instance.saveSlots.Remove(this);

        // 3️⃣ 清除皮肤表现
        player.ClearSkin();

        // 4️⃣ 销毁自己
        Destroy(gameObject);

        // 5️⃣ 延迟刷新整个列表，给 Destroy 一帧时间
        UIManager.instance.Invoke(nameof(UIManager.instance.RefreshSaveSlots), 0.05f);

        // 6️⃣ 延迟重新选中第一个存档，确保高亮存在
        UIManager.instance.Invoke(nameof(UIManager.instance.SelectFirstSlotSafe), 0.1f);

        AudioManager.instance.AudioPlay(AudioManager.instance.Effect_tear1);



    }//删除这个档的皮肤

    public void ClickVoice()
    {
        AudioManager.instance.AudioPlay(AudioManager.instance.Attack_pai1);
    }
    #endregion
}

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
    public Image Level_Icon;
    public Image Weapon_Icon,Clothes_Icon,Stocking_Icon;
    public Sprite Sword, Arrow, Staff; public Sprite None;//用于不表示

    public List<GameObject> targets;

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




        if (GameFlowData.nextScene == "CG")
        {
            switch (PlayerPrefs.GetInt("language"))
            {
                case 0: // Japanese
                    CurrentWeapon.text = "監禁調教日数";
                    CurrentArmor.text = "接客奉仕回数";
                    CurrentStocking.text = "敗北凌辱回数";
                    break;

                case 1: // Simplified Chinese
                    CurrentWeapon.text = "监禁调教天数";
                    CurrentArmor.text = "接客侍奉次数";
                    CurrentStocking.text = "战败凌辱次数";
                    break;

                case 2: // Traditional Chinese
                    CurrentWeapon.text = "監禁調教天數";
                    CurrentArmor.text = "接客侍奉次數";
                    CurrentStocking.text = "戰敗凌辱次數";
                    break;

                case 3: // English
                    CurrentWeapon.text = "Captivity Days";
                    CurrentArmor.text = "Service Count";
                    CurrentStocking.text = "Defeat/Humiliation Count";
                    break;

                case 4: // Korean
                    CurrentWeapon.text = "감금 조교 일수";
                    CurrentArmor.text = "접객 봉사 횟수";
                    CurrentStocking.text = "패배 능욕 횟수";
                    break;

            }


            int jailDays = SaveManager.DateUtil.DaysSinceYYYYMMDD(data.firstSavedDate);
            CurrentWeaponPower.text = jailDays.ToString();//监禁天数
            CurrentArmorDefence.text = Data.serviceCount.ToString();//接客次数
            CurrentStockingDefence.text = Data.defeatCount.ToString();//战败次数



            //这些数字保持白色
            CurrentWeapon.color = Color.white;
            CurrentArmor.color = Color.white;
            CurrentStocking.color = Color.white;
            CurrentWeaponPower.color = Color.white;
            CurrentArmorDefence.color = Color.white;
            CurrentStockingDefence.color = Color.white;


            Weapon_Icon.sprite = None;
            Clothes_Icon.sprite = None;
            Stocking_Icon.sprite = None;

            //把上方数值也隐藏
            foreach (var go in targets)
                if (go) go.SetActive(false);
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

            case 3:
                index = 5;
                break;
            case 4:
                index = 6;
                break;
            case 5:
                index = 7;
                break;
            case 6:
                index = 8;
                break;
            case 7:
                index = 9;
                break;



            case 8:
                index = 10;
                break;

            case 9:
                index = 11;
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


            case 13:
                index = 12;
                break;

            case 14:
                index = 13;
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

            case 3:
                index = 5;
                break;
            case 4:
                index = 6;
                break;
            case 5:
                index = 7;
                break;
            case 6:
                index = 8;
                break;
            case 7:
                index = 9;
                break;



            case 8:
                index = 10;
                break;

            case 9:
                index = 11;
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



            case 13:
                index = 12;
                break;

            case 14:
                index = 13;
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
   // 日语
    new string[] { "", "女性用鎧", "盗賊の軽装", "魔導士団制服", "暗殺マント", "なし", "ギルドスーツ", "クロスボウ服", "紅の礼装", "ゴシックドレス", "シスター服", "白魔導礼装", "女傭兵の服", "メイドスカート", "金飾りの白ランジェリー" },
    // 简体中文
    new string[] { "", "女式盔甲", "盗贼便服", "魔导士团制服", "刺客斗篷", "无", "公会套裙", "轻装弩手服", "绯红礼裙", "哥特长裙", "修女服", "白魔仪礼服", "女佣兵服", "女仆短裙", "金饰白内衣" },
    // 繁体中文
    new string[] { "", "女式鎧甲", "盜賊便服", "魔導士團制服", "刺客斗篷", "無", "公會套裙", "輕裝弩手服", "緋紅禮裙", "哥德長裙", "修女服", "白魔儀禮服", "女傭兵服", "女僕短裙", "金飾白內衣" },
    // 英语
    new string[] { "", "Women's Armor", "Rogue Outfit", "Mage Order Uniform", "Assassin Cloak", "None", "Guild Dress", "Crossbow Outfit", "Crimson Dress", "Gothic Dress", "Nun Outfit", "White Mage Robe", "Merc Wear", "Maid Skirt", "Gold Lingerie" },
    // 韩语
    new string[] { "", "여성용 갑옷", "도적 복장", "마도사단 제복", "암살 망토", "없음", "길드 드레스", "석궁 복장", "진홍색 드레스", "고딕 드레스", "수녀복", "백마도 의례복", "여용병 복장", "메이드 미니스커트", "금장 흰색 란제리" }
};

        public static readonly string[][] StockingNames = new string[][]
{
     // 日语
    new string[] { "", "ストッキング腿鎧", "ソックスブーツ", "ガーターソックス", "ニーブーツ", "なし", "ロングソックスブーツ", "薄黒ストッキング", "黒ヒール", "黒ブーツ", "白ニーソ", "オレンジ足掛けソックス", "黒足掛けソックス", "黒ネットストッキング", "白足掛けソックス" },
    // 简体中文
    new string[] { "", "丝袜腿甲", "黑色长袜靴", "蕾丝吊带袜", "过膝袜短靴", "无", "长袜靴", "薄黑长袜", "黑色高跟鞋", "黑色短靴", "白丝过膝袜", "橘色脚踩袜", "黑色脚踩袜", "黑网袜", "白色脚踩袜" },
    // 繁体中文
    new string[] { "", "絲襪腿甲", "黑色長襪靴", "蕾絲吊帶襪", "過膝襪短靴", "無", "長襪靴", "薄黑長襪", "黑色高跟鞋", "黑色短靴", "白絲過膝襪", "橘色腳踩襪", "黑色腳踩襪", "黑網襪", "白色腳踩襪" },
    // 英语
    new string[] { "", "Leg Armor", "Long Boots", "Garter Stockings", "Knee Boots", "None", "Sock Boots", "Thin Black Stockings", "Black Heels", "Short Boots", "White Thigh-highs", "Orange Stirrup Socks", "Black Stirrup Socks", "Black Fishnets", "White Stirrup Socks" },
    // 韩语
    new string[] { "", "스타킹 레그 아머", "롱삭스 부츠", "가터 스타킹", "하이부츠", "없음", "삭스 부츠", "얇은 검정 스타킹", "검은 하이힐", "짧은 부츠", "하얀 니삭스", "오렌지 발걸이 양말", "검은 발걸이 양말", "검은 망사 스타킹", "하얀 발걸이 양말" }
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
            (1500, WHITE), (1700, BLUE), (2000, GREEN),
            (2200, PURPLE), (2500, GOLD),
        };
        return PickColor(hp, rules, RED);
    }

    // 近战 / 射击 / 法术
    public static Color AttackColor(int atk)
    {
        var rules = new (int, Color)[] {
            (100, WHITE), (150, BLUE), (170, GREEN),
            (200, PURPLE), (250, GOLD),
        };
        return PickColor(atk, rules, RED);
    }

    // 武器（名称与攻击值同色）
    public static Color WeaponColor(int wp)
    {
        var rules = new (int, Color)[] {
            (150, WHITE), (200, BLUE), (250, GREEN),
            (300, PURPLE), (350, GOLD),
        };
        return PickColor(wp, rules, RED);
    }

    // 衣服/丝袜（名称与防御值同色）
    public static Color ArmorLikeColor(int def)
    {
        var rules = new (int, Color)[] {
            (50, WHITE), (100, BLUE), (150, GREEN),
            (200, PURPLE), (250, GOLD),
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
        //player.ApplySaveData(Data);
        //UIManager.instance.SetCurrentSlot(this); // 通知UIManager进行高亮更新
        //
        //
        //player._ClothesToClass();//临时让衣服改变职业

        // AudioManager.instance.AudioPlay(AudioManager.instance.Attack_pai1);


        // 1) 记录“切换前”的血量比例
        float prevRatio = 1f;
        if (player != null && player.maxHealth > 0)
            prevRatio = Mathf.Clamp01((float)player.currentHealth / player.maxHealth);

        // 2) 载入新角色数据，并决定是否继承血量
        //    逻辑：如果上一名不是满血（<0.999），就继承；满血则全满
        bool preserve = prevRatio < 0.999f;

        player.ApplySaveData(Data, preserveHealth: preserve, prevHealthRatio: prevRatio);

        // 3) UI 高亮逻辑照旧
        UIManager.instance.SetCurrentSlot(this);
        player._ClothesToClass();


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

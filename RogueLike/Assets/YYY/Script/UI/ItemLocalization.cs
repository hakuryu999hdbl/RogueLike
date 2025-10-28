using UnityEngine;

public static class ItemLocalization
{
    public static string GetName(ShopItemData.ItemType type, int id, int language)
    {
        switch (type)
        {
            // ======= 剑系 =======
            case ShopItemData.ItemType.Sword:
                switch (id)
                {
                    case 1: return Lang(language, "匕首", "匕首", "Dagger", "단검");
                    case 2: return Lang(language, "阔剑", "闊劍", "Broadsword", "브로드소드");
                    case 3: return Lang(language, "双刃斧", "雙刃斧", "Twin Axe", "양날도끼");
                    case 4: return Lang(language, "长枪", "長槍", "Spear", "창");
                    case 5: return Lang(language, "长柄斧", "長柄斧", "Halberd", "할버드");
                    case 6: return Lang(language, "冻结剑", "凍結劍", "Ice Blade", "빙결검");
                    case 7: return Lang(language, "黑铁刺剑", "黑鐵刺劍", "Iron Rapier", "흑철 레이피어");
                    case 8: return Lang(language, "熔岩剑", "熔岩劍", "Lava Sword", "용암검");
                    case 9: return Lang(language, "引雷剑", "引雷劍", "Storm Blade", "뇌격검");
                    case 10: return Lang(language, "古重剑", "古重劍", "Ancient Greatsword", "고대 대검");
                }
                break;

            // ======= 枪系 =======
            case ShopItemData.ItemType.Pistol:
                switch (id)
                {
                    case 1: return Lang(language, "轻弩", "輕弩", "Light Crossbow", "경노");
                    case 2: return Lang(language, "重弩", "重弩", "Heavy Crossbow", "중노");
                    case 3: return Lang(language, "复合弩", "複合弩", "Compound Crossbow", "복합노");
                    case 4: return Lang(language, "火绳复合枪", "火繩複合槍", "Matchlock Rifle", "화승총");
                    case 5: return Lang(language, "火绳短枪", "火繩短槍", "Short Matchlock", "단소총");
                    case 6: return Lang(language, "火绳长枪", "火繩長槍", "Long Matchlock", "장화승총");
                    case 7: return Lang(language, "燧发枪", "燧發槍", "Flintlock", "부싯돌총");
                    case 8: return Lang(language, "刺刀火枪", "刺刀火槍", "Bayonet Rifle", "총검");
                    case 9: return Lang(language, "黄铜火枪", "黃銅火槍", "Brass Musket", "황동 머스킷");
                    case 10: return Lang(language, "镶银火枪", "鑲銀火槍", "Silver Musket", "은 장식 머스킷");
                }
                break;

            // ======= 法杖 =======
            case ShopItemData.ItemType.Staff:
                switch (id)
                {
                    case 1: return Lang(language, "黄木短杖", "黃木短杖", "Elm Wand", "황목 단봉");
                    case 2: return Lang(language, "鹰身短杖", "鷹身短杖", "Harpie Wand", "하피 단봉");
                    case 3: return Lang(language, "红宝石短杖", "紅寶石短杖", "Ruby Wand", "루비 완드");
                    case 4: return Lang(language, "蓝宝石短杖", "藍寶石短杖", "Sapphire Wand", "사파이어 완드");
                    case 5: return Lang(language, "黄玉短杖", "黃玉短杖", "Topaz Wand", "토파즈 완드");
                    case 6: return Lang(language, "冰冻法杖", "冰凍法杖", "Frost Staff", "빙결 지팡이");
                    case 7: return Lang(language, "紫水晶法杖", "紫水晶法杖", "Amethyst Staff", "자수정 스태프");
                    case 8: return Lang(language, "翡翠法杖", "翡翠法杖", "Jade Staff", "비취 스태프");
                    case 9: return Lang(language, "雷霆法杖", "雷霆法杖", "Thunder Staff", "뇌정 스태프");
                    case 10: return Lang(language, "古木法杖", "古木法杖", "Ancient Staff", "고목 스태프");
                }
                break;

            // ======= 衣服 =======
            case ShopItemData.ItemType.Clothes:
                switch (id)
                {
                    case 10: return Lang(language, "女式盔甲", "女式盔甲", "Women's Armor", "여성용 갑옷");
                    case 11: return Lang(language, "盗贼便服", "盜賊便服", "Rogue Outfit", "도적 복장");
                    case 12: return Lang(language, "魔导士团制服", "魔導士團制服", "Mage Order Uniform", "마도기사단 제복");
                    case 2: return Lang(language, "刺客斗篷", "刺客斗篷", "Assassin Cloak", "암살자 망토");
                    case 3: return Lang(language, "公会套裙", "公會套裙", "Guild Skirt", "길드 치마");
                    case 4: return Lang(language, "轻装弩手服", "輕裝弩手服", "Arbalist Outfit", "석궁병 복장");
                    case 5: return Lang(language, "绯红礼裙", "緋紅禮裙", "Crimson Dress", "진홍색 드레스");
                    case 6: return Lang(language, "哥特长裙", "哥特長裙", "Gothic Dress", "고딕 드레스");
                    case 7: return Lang(language, "修女服", "修女服", "Nun Habit", "수녀복");
                }
                break;

            // ======= 丝袜 =======
            case ShopItemData.ItemType.Stockings:
                switch (id)
                {
                    case 10: return Lang(language, "丝袜腿甲", "絲襪腿甲", "Leg Guard Stockings", "스타킹 레그아머");
                    case 11: return Lang(language, "黑色长袜靴", "黑色長襪靴", "Black Long Boots", "검은 장부츠");
                    case 12: return Lang(language, "蕾丝吊带袜", "蕾絲吊帶襪", "Lace Stockings", "레이스 스타킹");
                    case 2: return Lang(language, "过膝袜短靴", "過膝襪短靴", "Knee-high Boots", "니하이 부츠");
                    case 3: return Lang(language, "长袜靴", "長襪靴", "Long Boots", "롱부츠");
                    case 4: return Lang(language, "薄黑长袜", "薄黑長襪", "Sheer Black Stockings", "얇은 검은 스타킹");
                    case 5: return Lang(language, "黑色高跟鞋", "黑色高跟鞋", "Black Heels", "검은 하이힐");
                    case 6: return Lang(language, "黑色短靴", "黑色短靴", "Short Boots", "단부츠");
                    case 7: return Lang(language, "白丝过膝袜", "白絲過膝襪", "White Thigh-highs", "하얀 니삭스");
                }
                break;

            // ======= 特殊 =======
            case ShopItemData.ItemType.Slave:
                return Lang(language, "性奴", "性奴", "Slave", "성노예");
        }

        return "???";
    }

    public static string GetDescription(ShopItemData.ItemType type, int id, int language)
    {
        // 你可以稍微简单些，通用规则：
        if (id <= 10) return Lang(language, "基础近战武器", "基礎近戰武器", "Basic melee weapon", "기초 근접무기");
        if (id >= 101 && id < 200) return Lang(language, "远程武器，可攻击远处敌人", "遠程武器，可攻擊遠處敵人", "Ranged weapon", "원거리 무기");
        if (id >= 201 && id < 300) return Lang(language, "法杖，用于施放魔法", "法杖，用於施放魔法", "Staff for magic casting", "마법 사용용 지팡이");
        if (id >= 11_00) return Lang(language, "防具，提升防御力", "防具，提升防禦力", "Armor, increases defense", "방어구, 방어력 증가");
        if (id >= 10_00) return Lang(language, "饰品，增加魅力或防御", "飾品，增加魅力或防禦", "Accessory, adds charm or defense", "장식품, 매력 또는 방어력 증가");
        if (id == 999) return Lang(language, "获得一个新的奴隶", "獲得一個新的奴隸", "Gain a new slave ally", "새 노예 동료 획득");
        return "";
    }

    private static string Lang(int lang, string zh, string zh_tw, string en, string kr, string jp = "")
    {
        switch (lang)
        {
            case 0: return jp == "" ? zh : jp;
            case 1: return zh;
            case 2: return zh_tw;
            case 3: return en;
            case 4: return kr;
        }
        return jp;
    }
}
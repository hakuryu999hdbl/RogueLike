using UnityEngine;

public static class ItemLocalization
{
    public static string GetName(ShopItemData.ItemType type, int id, int language)
    {
        switch (type)
        {
            // ======= 剑系（近战武器） =======
            case ShopItemData.ItemType.Sword:
                switch (id)
                {
                    case 1: return Lang(language, "ダガー", "匕首", "匕首", "Dagger", "단검");
                    case 2: return Lang(language, "ブロードソード", "阔剑", "闊劍", "Broad Sword", "브로드소드");
                    case 3: return Lang(language, "長柄両刃斧", "长柄双刃斧", "長柄雙刃斧", "Double Axe", "장병 도끼");
                    case 4: return Lang(language, "長槍", "长枪", "長槍", "Spear", "장창");
                    case 5: return Lang(language, "長柄斧", "长柄斧", "長柄斧", "Poleaxe", "폴액스");
                    case 6: return Lang(language, "氷結の剣", "冻结剑", "凍結劍", "Frost Sword", "동결의 검");
                    case 7: return Lang(language, "黒鉄の刺剣", "黑铁刺剑", "黑鐵刺劍", "Black Iron Rapier", "흑철 레이피어");
                    case 8: return Lang(language, "溶岩の剣", "熔岩剑", "熔岩劍", "Lava Blade", "용암검");
                    case 9: return Lang(language, "雷を引く剣", "引雷剑", "引雷劍", "Thunderblade", "번개를 부르는 검");
                    case 10: return Lang(language, "古代の大剣", "古重剑", "古重劍", "Ancient Greatsword", "고대 대검");
                }
                break;

            // ======= 枪系（射击武器） =======
            case ShopItemData.ItemType.Pistol:
                switch (id)
                {
                    case 1: return Lang(language, "軽弩", "轻弩", "輕弩", "Light Crossbow", "경궁");
                    case 2: return Lang(language, "重弩", "重弩", "重弩", "Heavy Crossbow", "중궁");
                    case 3: return Lang(language, "複合弩", "复合弩", "複合弩", "Compound Bow", "복합궁");
                    case 4: return Lang(language, "火縄複合銃", "火绳复合枪", "火繩複合槍", "Matchlock Rifle", "화승총");
                    case 5: return Lang(language, "火縄短銃", "火绳短枪", "火繩短槍", "Short Matchlock", "단화승총");
                    case 6: return Lang(language, "火縄長銃", "火绳长枪", "火繩長槍", "Long Matchlock", "장화승총");
                    case 7: return Lang(language, "フリントロック銃", "燧发枪", "燧發槍", "Flintlock", "화승총");
                    case 8: return Lang(language, "バヨネット銃", "刺刀火枪", "刺刀火槍", "Bayonet Musket", "총검총");
                    case 9: return Lang(language, "火縄黄銅銃", "火绳黄铜枪", "火繩黃銅槍", "Brass Matchlock", "황동 화승총");
                    case 10: return Lang(language, "銀象嵌銃", "镶银火枪", "鑲銀火槍", "Silver-Inlaid Musket", "은장총");
                }
                break;

            // ======= 法杖 =======
            case ShopItemData.ItemType.Staff:
                switch (id)
                {
                    case 1: return Lang(language, "黄木の杖", "黄木短杖", "黃木短杖", "Yellowwood Wand", "황목 지팡이");
                    case 2: return Lang(language, "ハーピーの杖", "鹰身短杖", "鷹身短杖", "Harpy Wand", "하피 지팡이");
                    case 3: return Lang(language, "ルビーの杖", "红宝石短杖", "紅寶石短杖", "Ruby Wand", "루비 지팡이");
                    case 4: return Lang(language, "サファイアの杖", "蓝宝石短杖", "藍寶石短杖", "Sapphire Wand", "사파이어 지팡이");
                    case 5: return Lang(language, "トパーズの杖", "黄玉短杖", "黃玉短杖", "Topaz Wand", "토파즈 지팡이");
                    case 6: return Lang(language, "氷の杖", "冰冻法杖", "冰凍法杖", "Frost Wand", "얼음 지팡이");
                    case 7: return Lang(language, "アメジストの杖", "紫水晶法杖", "紫水晶法杖", "Amethyst Wand", "자수정 지팡이");
                    case 8: return Lang(language, "エメラルドの杖", "翡翠法杖", "翡翠法杖", "Emerald Wand", "에메랄드 지팡이");
                    case 9: return Lang(language, "雷の杖", "雷霆法杖", "雷霆法杖", "Thunder Wand", "천둥 지팡이");
                    case 10: return Lang(language, "古代の杖", "古木法杖", "古木法杖", "Ancient Wand", "고대 지팡이");
                }
                break;

            // ======= 衣服 =======
            case ShopItemData.ItemType.Clothes:
                switch (id)
                {
                    // SaveSlotUI 中 index = 1,2,3,... 的映射
                    case 10: return Lang(language, "女性用鎧", "女式盔甲", "女式鎧甲", "Women's Armor", "여성용 갑옷");          // 1
                    case 11: return Lang(language, "盗賊の軽装", "盗贼便服", "盜賊便服", "Rogue Outfit", "도적 복장");            // 2
                    case 12: return Lang(language, "魔導士団制服", "魔导士团制服", "魔導士團制服", "Mage Order Uniform", "마도사단 제복");        // 3
                    case 2: return Lang(language, "暗殺マント", "刺客斗篷", "刺客斗篷", "Assassin Cloak", "암살 망토");            // 4
                    case 3: return Lang(language, "ギルドスーツ", "公会套裙", "公會套裙", "Guild Dress", "길드 드레스");          // 6
                    case 4: return Lang(language, "クロスボウ服", "轻装弩手服", "輕裝弩手服", "Crossbow Outfit", "석궁 복장");            // 7
                    case 5: return Lang(language, "紅の礼装", "绯红礼裙", "緋紅禮裙", "Crimson Dress", "진홍색 드레스");        // 8
                    case 6: return Lang(language, "ゴシックドレス", "哥特长裙", "哥德長裙", "Gothic Dress", "고딕 드레스");          // 9
                    case 7: return Lang(language, "シスター服", "修女服", "修女服", "Nun Outfit", "수녀복");               // 10
                    case 8: return Lang(language, "白魔導礼装", "白魔仪礼服", "白魔儀禮服", "White Mage Robe", "백마도 의례복");        // 11
                    case 9: return Lang(language, "女傭兵の服", "女佣兵服", "女傭兵服", "Merc Wear", "여용병 복장");          // 12
                    case 13: return Lang(language, "メイドスカート", "女仆短裙", "女僕短裙", "Maid Skirt", "메이드 미니스커트");    // 13
                    case 14: return Lang(language, "金飾りの白ランジェリー", "金饰白内衣", "金飾白內衣", "Gold Lingerie", "금장 흰색 란제리");     // 14
                }
                break;

            // ======= 丝袜 =======
            case ShopItemData.ItemType.Stockings:
                switch (id)
                {
                    case 10: return Lang(language, "ストッキング腿鎧", "丝袜腿甲", "絲襪腿甲", "Leg Armor", "스타킹 레그 아머");     // 1
                    case 11: return Lang(language, "ソックスブーツ", "黑色长袜靴", "黑色長襪靴", "Long Boots", "롱삭스 부츠");         // 2
                    case 12: return Lang(language, "ガーターソックス", "蕾丝吊带袜", "蕾絲吊帶襪", "Garter Stockings", "가터 스타킹");         // 3
                    case 2: return Lang(language, "ニーブーツ", "过膝袜短靴", "過膝襪短靴", "Knee Boots", "하이부츠");             // 4
                    case 3: return Lang(language, "ロングソックスブーツ", "长袜靴", "長襪靴", "Sock Boots", "삭스 부츠");           // 6
                    case 4: return Lang(language, "薄黒ストッキング", "薄黑长袜", "薄黑長襪", "Thin Black Stockings", "얇은 검정 스타킹");     // 7
                    case 5: return Lang(language, "黒ヒール", "黑色高跟鞋", "黑色高跟鞋", "Black Heels", "검은 하이힐");         // 8
                    case 6: return Lang(language, "黒ブーツ", "黑色短靴", "黑色短靴", "Short Boots", "짧은 부츠");           // 9
                    case 7: return Lang(language, "白ニーソ", "白丝过膝袜", "白絲過膝襪", "White Thigh-highs", "하얀 니삭스");           // 10
                    case 8: return Lang(language, "オレンジ足掛けソックス", "橘色脚踩袜", "橘色腳踩襪", "Orange Stirrup Socks", "오렌지 발걸이 양말");   // 11
                    case 9: return Lang(language, "黒足掛けソックス", "黑色脚踩袜", "黑色腳踩襪", "Black Stirrup Socks", "검은 발걸이 양말");     // 12
                    case 13: return Lang(language, "黒ネットストッキング", "黑网袜", "黑網襪", "Black Fishnets", "검은 망사 스타킹");     // 13
                    case 14: return Lang(language, "白足掛けソックス", "白色脚踩袜", "白色腳踩襪", "White Stirrup Socks", "하얀 발걸이 양말");     // 14
                }
                break;

            // ======= 特殊 =======
            case ShopItemData.ItemType.Slave:
                return Lang(language, "性奴隷二人組", "两名性奴隶", "兩名性奴隸", "2 Slaves", "성노예 2명");
        }

        return "???";
    }

    public static string GetDescription(ShopItemData.ItemType type, int id, int language)
    {
        switch (type)
        {
            // ======= 剑系 =======
            case ShopItemData.ItemType.Sword:
                switch (id)
                {
                    case 1:
                        return Lang(language,
                    "小型だが扱いやすい短剣。暗殺者や盗賊が好んで使う。",
                    "小巧锋利的短刃，易于操控，盗贼与刺客的挚爱。",
                    "小巧鋒利的短刃，易於操控，盜賊與刺客的摯愛。",
                    "A small but deadly dagger favored by assassins and rogues.",
                    "작고 날카로운 단검. 암살자와 도적이 애용한다.");
                    case 2:
                        return Lang(language,
                    "幅広の刃を持つ古典的な剣。重く、威力は絶大。",
                    "宽刃沉重的传统长剑，斩击之势厚重如山。",
                    "寬刃沉重的傳統長劍，斬擊之勢厚重如山。",
                    "A broad and heavy blade that delivers devastating slashes.",
                    "넓은 칼날을 가진 무거운 장검. 일격의 위력이 크다.");
                    case 3:
                        return Lang(language,
                    "二枚の刃で構成された戦斧。猛攻を好む者に向く。",
                    "双刃构造的战斧，适合嗜血的狂战士。",
                    "雙刃構造的戰斧，適合嗜血的狂戰士。",
                    "A twin-bladed axe made for those who crave carnage.",
                    "쌍날 도끼. 피를 갈망하는 광전사에게 어울린다.");
                    case 4:
                        return Lang(language,
                    "長いリーチを誇る槍。貫通力に優れる。",
                    "拥有极长攻击距离的长枪，贯穿之力惊人。",
                    "擁有極長攻擊距離的長槍，貫穿之力驚人。",
                    "A spear with exceptional reach and piercing power.",
                    "긴 리치와 관통력을 자랑하는 창.");
                    case 5:
                        return Lang(language,
                    "刃と鉤を併せ持つ斧。集団戦で真価を発揮する。",
                    "兼具斧刃与钩刃的长柄战斧，能轻松撕裂护甲。",
                    "兼具斧刃與鉤刃的長柄戰斧，能輕鬆撕裂護甲。",
                    "A halberd combining axe and hook, perfect for breaking armor.",
                    "도끼날과 갈고리가 결합된 할버드. 갑옷을 찢는 데 특화.");
                    case 6:
                        return Lang(language,
                    "氷の魔力を宿す剣。触れるものを凍てつかせる。",
                    "蕴含冰之魔力的剑，挥舞间寒气逼人。",
                    "蘊含冰之魔力的劍，揮舞間寒氣逼人。",
                    "A blade imbued with ice magic that freezes its foes.",
                    "얼음의 마력을 품은 검. 적을 얼려버린다.");
                    case 7:
                        return Lang(language,
                    "黒鉄で鍛えられた刺突剣。鋭く、軽い。",
                    "由黑铁打造的刺剑，轻巧而致命。",
                    "由黑鐵打造的刺劍，輕巧而致命。",
                    "A black-iron rapier, sharp and swift.",
                    "흑철로 만들어진 레이피어. 가볍고 치명적이다.");
                    case 8:
                        return Lang(language,
                    "灼熱の刃を持つ熔岩剣。触れれば肉が焼ける。",
                    "炽热的熔岩流淌其上，能灼烧敌人的血肉。",
                    "熾熱的熔岩流淌其上，能灼燒敵人的血肉。",
                    "A sword of molten fury, burning all it touches.",
                    "용암이 흐르는 검. 닿는 것은 모두 탄다.");
                    case 9:
                        return Lang(language,
                    "雷の精霊が宿る剣。振るうたびに稲妻が走る。",
                    "蕴含雷之灵力，每次挥动都伴随闪电。",
                    "蘊含雷之靈力，每次揮動都伴隨閃電。",
                    "A blade charged with storm energy that crackles with each swing.",
                    "번개의 정령이 깃든 검. 휘두를 때마다 번개가 친다.");
                    case 10:
                        return Lang(language,
                   "古代の鍛冶技術で作られた重剣。圧倒的な重量感。",
                   "古代工艺打造的巨剑，沉重无比，却蕴含惊人力量。",
                   "古代工藝打造的巨劍，沉重無比，卻蘊含驚人力量。",
                   "An ancient greatsword of massive weight and unmatched power.",
                   "고대의 기술로 만들어진 대검. 압도적인 중량과 힘을 지닌다.");
                }
                break;

            // ======= 枪系 =======
            case ShopItemData.ItemType.Pistol:
                switch (id)
                {
                    case 1:
                        return Lang(language,
                    "軽く扱いやすい弩。初心者にも人気。",
                    "轻便易操控的轻弩，射速极高。",
                    "輕便易操控的輕弩，射速極高。",
                    "A light crossbow easy to handle, favored by beginners.",
                    "가볍고 다루기 쉬운 경노. 초보자에게 인기가 높다.");
                    case 2:
                        return Lang(language,
                    "重弩。威力は絶大だが装填が遅い。",
                    "威力巨大的重弩，换箭速度较慢。",
                    "威力巨大的重弩，換箭速度較慢。",
                    "A heavy crossbow that trades speed for power.",
                    "강력하지만 장전이 느린 중노.");
                    case 3:
                        return Lang(language,
                    "複合構造の弩。射程と精度が高い。",
                    "复合机械结构，使射程与精度兼得。",
                    "複合機械結構，使射程與精度兼得。",
                    "A compound crossbow balancing power and accuracy.",
                    "복합 구조의 석궁. 사거리와 정확도가 높다.");
                    case 4:
                        return Lang(language,
                    "火薬と弩を組み合わせた奇妙な銃。",
                    "融合弩与火绳的奇异武器，火光耀眼。",
                    "融合弩與火繩的奇異武器，火光耀眼。",
                    "A hybrid matchlock crossbow that spits both bolts and fire.",
                    "화약과 석궁이 결합된 기묘한 무기.");
                    case 5:
                        return Lang(language,
                    "短い火縄銃。携帯しやすいが射程は短い。",
                    "短管火绳枪，轻便灵巧但射程有限。",
                    "短管火繩槍，輕便靈巧但射程有限。",
                    "A compact matchlock, easy to carry but limited in range.",
                    "가볍지만 사거리가 짧은 단소총.");
                    case 6:
                        return Lang(language,
                    "長火縄銃。貫通力と精度に優れる。",
                    "长火绳枪，精准而强力。",
                    "長火繩槍，精準而強力。",
                    "A long matchlock rifle known for precision and power.",
                    "긴 화승총. 관통력과 명중률이 뛰어나다.");
                    case 7:
                        return Lang(language,
                    "近代的な燧発銃。信頼性が高い。",
                    "近代燧发枪，火光闪烁间夺人性命。",
                    "近代燧發槍，火光閃爍間奪人性命。",
                    "A flintlock pistol with high reliability.",
                    "근대식 부싯돌총. 신뢰성이 높다.");
                    case 8:
                        return Lang(language,
                    "銃剣付きの火器。突撃にも対応。",
                    "带刺刀的火枪，可射可刺。",
                    "帶刺刀的火槍，可射可刺。",
                    "A bayonet rifle for both shooting and stabbing.",
                    "총검이 달린 화기. 사격과 찌르기 모두 가능.");
                    case 9:
                        return Lang(language,
                    "黄銅で装飾された銃。見た目は豪華。",
                    "黄铜制成的火枪，华丽又厚重。",
                    "黃銅製成的火槍，華麗又厚重。",
                    "A brass musket, elegant yet powerful.",
                    "황동으로 장식된 머스킷. 우아하면서도 강력하다.");
                    case 10:
                        return Lang(language,
                   "銀で飾られた銃。儀礼用だが実戦でも使える。",
                   "镶银装饰的火枪，外观与威力并存。",
                   "鑲銀裝飾的火槍，外觀與威力並存。",
                   "A silver-inlaid musket, as beautiful as it is deadly.",
                   "은으로 장식된 머스킷. 아름답고 치명적이다.");
                }
                break;

            // ======= 法杖 =======
            case ShopItemData.ItemType.Staff:
                switch (id)
                {
                    case 1:
                        return Lang(language,
                    "黄木で作られた簡素な杖。魔力伝導率は低い。",
                    "由黄木制成的简朴法杖，魔力导性一般。",
                    "由黃木製成的簡樸法杖，魔力導性一般。",
                    "A simple elm wand with modest magic conduction.",
                    "황목으로 만든 단순한 완드. 마력 전달력이 낮다.");
                    case 2:
                        return Lang(language,
                    "鷹の羽で飾られた杖。風魔法に適す。",
                    "饰有鹰羽的短杖，擅长操纵风之力。",
                    "飾有鷹羽的短杖，擅長操縱風之力。",
                    "A wand adorned with feathers, attuned to wind magic.",
                    "매 깃털로 장식된 완드. 바람 마법에 적합하다.");
                    case 3:
                        return Lang(language,
                    "紅宝石が輝く杖。炎の魔法を増幅する。",
                    "镶嵌红宝石的法杖，能强化火焰魔法。",
                    "鑲嵌紅寶石的法杖，能強化火焰魔法。",
                    "A ruby wand that amplifies fire spells.",
                    "루비가 박힌 완드. 화염 마법을 강화한다.");
                    case 4:
                        return Lang(language,
                    "青い宝石を冠した杖。冷気を操る力を持つ。",
                    "顶端镶嵌蓝宝石，散发寒冷之气。",
                    "頂端鑲嵌藍寶石，散發寒冷之氣。",
                    "A sapphire wand radiating icy aura.",
                    "사파이어가 장식된 완드. 냉기를 다룬다.");
                    case 5:
                        return Lang(language,
                    "黄玉が埋め込まれた杖。雷を引き寄せる。",
                    "黄玉嵌入的法杖，可召唤闪电之力。",
                    "黃玉嵌入的法杖，可召喚閃電之力。",
                    "A topaz wand that channels lightning.",
                    "토파즈가 박힌 완드. 번개를 끌어온다.");
                    case 6:
                        return Lang(language,
                    "氷結の魔力を帯びた杖。触れると冷たい。",
                    "散发寒气的冰冻法杖，连握持都困难。",
                    "散發寒氣的冰凍法杖，連握持都困難。",
                    "A frost staff so cold it numbs the hand.",
                    "차가운 냉기를 품은 지팡이.");
                    case 7:
                        return Lang(language,
                    "紫水晶の杖。精神系魔法の増幅に優れる。",
                    "紫水晶法杖，强化精神系魔法。",
                    "紫水晶法杖，強化精神系魔法。",
                    "An amethyst staff enhancing psychic power.",
                    "자수정 스태프. 정신계 마법을 강화한다.");
                    case 8:
                        return Lang(language,
                    "翡翠で装飾された杖。生命力の循環を司る。",
                    "翡翠法杖，蕴含自然与生命之息。",
                    "翡翠法杖，蘊含自然與生命之息。",
                    "A jade staff embodying nature’s vitality.",
                    "비취 스태프. 생명의 힘을 다룬다.");
                    case 9:
                        return Lang(language,
                    "雷鳴を呼ぶ杖。空気が震える。",
                    "引雷之杖，随挥舞伴随雷鸣。",
                    "引雷之杖，隨揮舞伴隨雷鳴。",
                    "A thunder staff that shakes the air itself.",
                    "천둥을 부르는 스태프.");
                    case 10:
                        return Lang(language,
                   "古木の枝から削り出された杖。静かな力を秘める。",
                   "由古木雕成的法杖，蕴藏宁静的魔力。",
                   "由古木雕成的法杖，蘊藏寧靜的魔力。",
                   "An ancient wooden staff filled with serene power.",
                   "고목으로 깎은 스태프. 고요한 힘을 품는다.");
                }
                break;

            // ======= 衣服 =======
            case ShopItemData.ItemType.Clothes:
                switch (id)
                {
                    case 10:
                        return Lang(language,
                   "胸元を包む銀の開胸鎧。美と戦気を両立する。",
                   "酥胸包裹在银色开胸紧身软甲中，曲线与力量并存。",
                   "酥胸包裹在銀色開胸緊身軟甲中，曲線與力量並存。",
                   "A silver open-chest armor combining allure and defense.",
                   "은빛 개흉 갑옷. 아름다움과 전투력을 겸비.");
                    case 11:
                        return Lang(language,
                   "軽装の盗賊服。機動性と静音性に優れる。",
                   "为盗贼量身设计的轻便服装，行走无声。",
                   "為盜賊量身設計的輕便服裝，行走無聲。",
                   "A rogue’s garb designed for silent movement.",
                   "도적의 가벼운 복장. 조용한 움직임에 적합하다.");
                    case 12:
                        return Lang(language,
                   "魔導士団の制服。秩序と魔法の象徴。",
                   "魔导士团的象征制服，优雅而庄严。",
                   "魔導士團的象徵制服，優雅而莊嚴。",
                   "Uniform of the Mage Order, symbol of discipline.",
                   "마도기사단 제복. 질서와 마법의 상징.");
                    case 2:
                        return Lang(language,
                    "暗殺者のマント。影の中に溶け込む。",
                    "刺客的斗篷，使人隐于黑暗。",
                    "刺客的斗篷，使人隱於黑暗。",
                    "An assassin’s cloak that blends with the night.",
                    "암살자의 망토. 어둠 속에 숨는다.");
                    case 3:
                        return Lang(language,
                    "公会員のための華やかなスカート。",
                    "公会套裙，象征荣誉与地位。",
                    "公會套裙，象徵榮譽與地位。",
                    "Guild skirt, symbol of status.",
                    "길드 치마. 명예의 상징.");
                    case 4:
                        return Lang(language,
                    "弩兵用の軽装服。機動戦向け。",
                    "为弩手设计的轻甲服，适合快速作战。",
                    "為弩手設計的輕甲服，適合快速作戰。",
                    "Light armor for arbalists, suited for mobility.",
                    "석궁병용 경갑옷. 기동전에 적합.");
                    case 5:
                        return Lang(language,
                    "緋紅のドレス。華麗で妖艶。",
                    "绯红礼裙，艳丽如焰。",
                    "緋紅禮裙，艷麗如焰。",
                    "A crimson dress radiating seductive charm.",
                    "진홍색 드레스. 요염한 아름다움.");
                    case 6:
                        return Lang(language,
                    "黒を基調としたゴシックドレス。",
                    "哥特长裙，优雅而神秘。",
                    "哥特長裙，優雅而神秘。",
                    "A gothic gown shrouded in mystery.",
                    "고딕 드레스. 우아하고 신비롭다.");
                    case 7:
                        return Lang(language,
                    "清廉なシスター服。だが裾に血の跡が。",
                    "纯白修女服，却染有血迹。",
                    "純白修女服，卻染有血跡。",
                    "A nun’s robe, pure yet stained.",
                    "수녀복. 순백이지만 피로 물들었다.");
                    case 8:
                        return Lang(language,
                        "白魔導士の礼装。浄化と癒しの象徴。",
                        "白魔导士的仪礼长袍，是净化与治愈的象征。",
                        "白魔導士的儀禮長袍，是淨化與治癒的象徵。",
                        "Ceremonial white mage robe, symbol of healing.",
                        "백마도사의 예복. 정화와 치유의 상징이다。");

                    case 9:
                        return Lang(language,
                        "幾多の戦場をくぐり抜けた女傭兵の戦装束。",
                        "久经沙场的女佣兵战斗服，布料上满是磨损痕迹。",
                        "久經沙場的女傭兵戰鬥服，布料上滿是磨損痕跡。",
                        "Battle gear of a seasoned female mercenary.",
                        "수많은 전장을 넘긴 여용병의 전투복이다。");

                    case 13:
                        return Lang(language,
                        "動きやすい短めのメイドスカート。翻る裾が愛らしい。",
                        "方便行动的短款女仆裙，掀起时格外惹眼。",
                        "方便行動的短款女僕裙，掀起時格外惹眼。",
                        "A short maid skirt made for quick, graceful service.",
                        "움직이기 좋은 짧은 메이드 스커트. 팔랑이는 자락이 사랑스럽다。");

                    case 14:
                        return Lang(language,
                        "金の装飾が施された白いランジェリー。隠すより魅せるための衣。",
                        "金饰点缀的白色内衣，比起遮掩更像是展示身体的道具。",
                        "金飾點綴的白色內衣，比起遮掩更像是展示身體的道具。",
                        "White lingerie with gold trim, made to show rather than hide.",
                        "금장 장식이 더해진 흰 란제리. 숨기기보다 드러내기 위한 옷이다。");
                }
                break;

            // ======= 丝袜 =======
            case ShopItemData.ItemType.Stockings:
                switch (id)
                {
                    case 10:
                        return Lang(language,
                   "脚を守る金属入りストッキング。",
                   "嵌有护甲的战斗丝袜，既美观又实用。",
                   "嵌有護甲的戰鬥絲襪，既美觀又實用。",
                   "Battle stockings reinforced with metal plates.",
                   "금속이 박힌 전투용 스타킹.");
                    case 11:
                        return Lang(language,
                   "黒の長靴。太腿を美しく魅せる。",
                   "黑色长袜靴，修饰腿部曲线。",
                   "黑色長襪靴，修飾腿部曲線。",
                   "Black long boots emphasizing the thighs.",
                   "검은 장부츠. 다리를 돋보이게 한다.");
                    case 12:
                        return Lang(language,
                   "繊細なレースの吊りストッキング。",
                   "蕾丝吊带袜，性感的象征。",
                   "蕾絲吊帶襪，性感的象徵。",
                   "Delicate lace garters symbolizing seduction.",
                   "레이스 가터. 유혹의 상징.");
                    case 2:
                        return Lang(language,
                    "膝上まで覆う柔らかなブーツ。",
                    "过膝短靴，柔软贴肤。",
                    "過膝短靴，柔軟貼膚。",
                    "Knee-high boots of soft leather.",
                    "부드러운 니하이 부츠.");
                    case 3:
                        return Lang(language,
                    "長靴。実用的だが女性らしさも。",
                    "长袜靴，兼具实用与美感。",
                    "長襪靴，兼具實用與美感。",
                    "Long boots balancing beauty and function.",
                    "롱부츠. 실용적이면서도 세련됨.");
                    case 4:
                        return Lang(language,
                    "薄黒のストッキング。肌が透けて見える。",
                    "薄黑丝袜，若隐若现的诱惑。",
                    "薄黑絲襪，若隱若現的誘惑。",
                    "Sheer black stockings, teasingly transparent.",
                    "얇은 검은 스타킹. 은근한 유혹.");
                    case 5:
                        return Lang(language,
                    "黒いハイヒール。危険な香りを放つ。",
                    "黑色高跟鞋，散发危险气息。",
                    "黑色高跟鞋，散發危險氣息。",
                    "Black heels exuding a dangerous charm.",
                    "검은 하이힐. 위험한 매력.");
                    case 6:
                        return Lang(language,
                    "短い黒靴。実用的だが可愛らしい。",
                    "黑色短靴，简洁而时尚。",
                    "黑色短靴，簡潔而時尚。",
                    "Short black boots, simple yet stylish.",
                    "검은 단부츠. 실용적이고 세련됨.");
                    case 7:
                        return Lang(language,
                    "白いニーソックス。清純の象徴。",
                    "白丝过膝袜，纯洁的象征。",
                    "白絲過膝襪，純潔的象徵。",
                    "White thigh-highs, symbol of purity.",
                    "하얀 니삭스. 순수의 상징.");
                    case 8:
                        return Lang(language,
                        "明るいオレンジ色の足掛けソックス。元気で眩しい印象に。",
                        "橘色脚踩袜，让整条腿看起来活泼而醒目。",
                        "橘色腳踩襪，讓整條腿看起來活潑而醒目。",
                        "Orange stirrup socks giving a lively, eye-catching look.",
                        "밝은 오렌지색 발걸이 양말. 경쾌한 인상을 준다。");

                    case 9:
                        return Lang(language,
                        "黒い足掛けソックス。脚線美をくっきりと強調する。",
                        "黑色脚踩袜，清晰勾勒出腿部线条。",
                        "黑色腳踩襪，清晰勾勒出腿部線條。",
                        "Black stirrup socks that sharply define the legs.",
                        "검은 발걸이 양말. 다리 라인을 또렷이 드러낸다。");

                    case 13:
                        return Lang(language,
                        "網目のストッキング。露骨なほど挑発的な一品。",
                        "大网眼黑丝袜，几乎称得上是公然挑逗。",
                        "大網眼黑絲襪，幾乎稱得上是公然挑逗。",
                        "Black fishnet stockings, openly provocative.",
                        "검은 망사 스타킹. 노골적인 유혹이 묻어난다。");

                    case 14:
                        return Lang(language,
                        "白い足掛けソックス。清純さの中に密かな色気を忍ばせる。",
                        "白色脚踩袜，看似清纯，却暗藏一点危险的气息。",
                        "白色腳踩襪，看似清純，卻暗藏一點危險的氣息。",
                        "White stirrup socks, purity laced with a hint of allure.",
                        "하얀 발걸이 양말. 순수함 속에 은근한 관능이 있다。");
                }
                break;

            // ======= 特殊 =======
            case ShopItemData.ItemType.Slave:
                return Lang(language,
                    "鎖に繋がれた罪人。だがその瞳には希望が残る。",
                    "被锁链束缚的奴隶，眼中仍闪烁微光。",
                    "被鎖鏈束縛的奴隸，眼中仍閃爍微光。",
                    "A chained captive whose eyes still hold a glimmer of hope.",
                    "사슬에 묶인 노예. 그 눈엔 아직 희망이 남아 있다.");
        }

        return "";
    }

    private static string Lang(int lang, string jp, string zh, string zh_tw, string en, string kr)
    {
        switch (lang)
        {
            case 0: return jp;      // 日语
            case 1: return zh;   // 简体中文
            case 2: return zh_tw;   // 繁体中文
            case 3: return en;      // 英语
            case 4: return kr;      // 韩语
            default: return jp;
        }
    }











    #region     三选一界面菜单


    // ★ 武器升级描述（剑 / 弩 / 法杖）
    public static string GetWeaponPickupDesc(
        ShopItemData.ItemType type,
        int itemId,
        int currentPower,
        int addPower)
    {
        int language = PlayerPrefs.GetInt("language", 0);

        // 原始名字、说明
        string rawName = GetName(type, itemId, language);
        string rawDesc = GetDescription(type, itemId, language);

        // 高亮颜色
        string coloredName = $"<color=#80FFFF>{rawName}</color>";
        string coloredDesc = string.IsNullOrEmpty(rawDesc)
            ? ""
            : $"\n<color=#80FFFF>{rawDesc}</color>";

        switch (type)
        {
            case ShopItemData.ItemType.Sword:
                return Lang(language,
                    // JP
                    $"傍らの武器棚から「{coloredName}」を見つけた……\n" +
                    $"現在の武器攻撃力 <color=#ADD8E6>{currentPower}</color> → " +
                    $"攻撃力上昇 <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    // ZH-CN
                    $"你从一旁的武器架子上找到了一把「{coloredName}」……\n" +
                    $"目前你的武器攻击力 <color=#ADD8E6>{currentPower}</color> → " +
                    $"攻击力上升 <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    // ZH-TW
                    $"你從一旁的武器架子上找到了一把「{coloredName}」……\n" +
                    $"目前你的武器攻擊力 <color=#ADD8E6>{currentPower}</color> → " +
                    $"攻擊力上升 <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    // EN
                    $"You found “{coloredName}” on a nearby rack…\n" +
                    $"Current Weapon Power <color=#ADD8E6>{currentPower}</color> → " +
                    $"Attack <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    // KR
                    $"옆의 무기 선반에서 「{coloredName}」를 발견했다…\n" +
                    $"현재 무기 공격력 <color=#ADD8E6>{currentPower}</color> → " +
                    $"공격력 상승 <color=#80FFFF>+{addPower}</color>{coloredDesc}"
                );

            case ShopItemData.ItemType.Pistol:
                return Lang(language,
                    $"箱の中から「{coloredName}」を見つけた……\n" +
                    $"現在の武器攻撃力 <color=#ADD8E6>{currentPower}</color> → " +
                    $"攻撃力上昇 <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    $"你从箱子里找到了一把「{coloredName}」……\n" +
                    $"目前你的武器攻击力 <color=#ADD8E6>{currentPower}</color> → " +
                    $"攻击力上升 <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    $"你從箱子裡找到了一把「{coloredName}」……\n" +
                    $"目前你的武器攻擊力 <color=#ADD8E6>{currentPower}</color> → " +
                    $"攻擊力上升 <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    $"You found “{coloredName}” in a chest…\n" +
                    $"Current Weapon Power <color=#ADD8E6>{currentPower}</color> → " +
                    $"Attack <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    $"상자 속에서 「{coloredName}」를 발견했다…\n" +
                    $"현재 무기 공격력 <color=#ADD8E6>{currentPower}</color> → " +
                    $"공격력 상승 <color=#80FFFF>+{addPower}</color>{coloredDesc}"
                );

            case ShopItemData.ItemType.Staff:
                return Lang(language,
                    $"戦いの中、倒れた魔法使いが「{coloredName}」を託した……\n" +
                    $"現在の武器攻撃力 <color=#ADD8E6>{currentPower}</color> → " +
                    $"攻撃力上昇 <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    $"战斗中一名女法师在死前将「{coloredName}」交给了你……\n" +
                    $"目前你的武器攻击力 <color=#ADD8E6>{currentPower}</color> → " +
                    $"攻击力上升 <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    $"戰鬥中一名女法師在死前將「{coloredName}」交給了你……\n" +
                    $"目前你的武器攻擊力 <color=#ADD8E6>{currentPower}</color> → " +
                    $"攻擊力上升 <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    $"In battle, a dying sorceress gave you “{coloredName}”…\n" +
                    $"Current Weapon Power <color=#ADD8E6>{currentPower}</color> → " +
                    $"Attack <color=#80FFFF>+{addPower}</color>{coloredDesc}",
                    $"전투 중 죽어가던 여마법사가 「{coloredName}」를 건네주었다…\n" +
                    $"현재 무기 공격력 <color=#ADD8E6>{currentPower}</color> → " +
                    $"공격력 상승 <color=#80FFFF>+{addPower}</color>{coloredDesc}"
                );
        }

        return "";
    }

    // ★ 衣服 / 丝袜升级描述
    public static string GetArmorPickupDesc(
        ShopItemData.ItemType type,   // Clothes 或 Stockings
        int itemId,
        int currentDef,
        int addDef)
    {
        int language = PlayerPrefs.GetInt("language", 0);

        string rawName = GetName(type, itemId, language);
        string rawDesc = GetDescription(type, itemId, language);

        string coloredName = $"<color=#80FFFF>{rawName}</color>";
        string coloredDesc = string.IsNullOrEmpty(rawDesc)
            ? ""
            : $"\n<color=#80FFFF>{rawDesc}</color>";

        if (type == ShopItemData.ItemType.Clothes)
        {
            return Lang(language,
                $"敵の服「{coloredName}」を剥ぎ取り着替えた……\n" +
                $"現在の衣服防御力 <color=#ADD8E6>{currentDef}</color> → 防御力上昇 <color=#80FFFF>+{addDef}</color>{coloredDesc}",
                $"你剥下敌人身上的「{coloredName}」并且换上了……\n" +
                $"目前你的衣服防御力 <color=#ADD8E6>{currentDef}</color> → 衣服防御力上升 <color=#80FFFF>+{addDef}</color>{coloredDesc}",
                $"你剝下敵人身上的「{coloredName}」並且換上了……\n" +
                $"目前你的衣服防禦力 <color=#ADD8E6>{currentDef}</color> → 衣服防禦力上升 <color=#80FFFF>+{addDef}</color>{coloredDesc}",
                $"You stripped the enemy’s “{coloredName}” and put it on…\n" +
                $"Current Cloth Defense <color=#ADD8E6>{currentDef}</color> → Defense <color=#80FFFF>+{addDef}</color>{coloredDesc}",
                $"적의 옷 「{coloredName}」을 벗겨 입었다…\n" +
                $"현재 옷 방어력 <color=#ADD8E6>{currentDef}</color> → 방어력 상승 <color=#80FFFF>+{addDef}</color>{coloredDesc}"
            );
        }
        else // Stockings
        {
            return Lang(language,
                $"敵のストッキング「{coloredName}」を剥ぎ取り履き替えた……\n" +
                $"現在のストッキング防御力 <color=#ADD8E6>{currentDef}</color> → 防御力上昇 <color=#80FFFF>+{addDef}</color>{coloredDesc}",
                $"你剥下敌人身上的「{coloredName}」并且换上了……\n" +
                $"目前你的丝袜防御力 <color=#ADD8E6>{currentDef}</color> → 丝袜防御力上升 <color=#80FFFF>+{addDef}</color>{coloredDesc}",
                $"你剝下敵人身上的「{coloredName}」並且換上了……\n" +
                $"目前你的絲襪防禦力 <color=#ADD8E6>{currentDef}</color> → 絲襪防禦力上升 <color=#80FFFF>+{addDef}</color>{coloredDesc}",
                $"You stripped the enemy’s “{coloredName}” stockings and wore them…\n" +
                $"Current Stocking Defense <color=#ADD8E6>{currentDef}</color> → Defense <color=#80FFFF>+{addDef}</color>{coloredDesc}",
                $"적의 스타킹 「{coloredName}」을 벗겨 신었다…\n" +
                $"현재 스타킹 방어력 <color=#ADD8E6>{currentDef}</color> → 방어력 상승 <color=#80FFFF>+{addDef}</color>{coloredDesc}"
            );
        }
    }

    #endregion
}
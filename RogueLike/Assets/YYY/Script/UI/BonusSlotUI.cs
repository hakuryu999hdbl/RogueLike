using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSlotUI : MonoBehaviour
{
    public enum BonusType
    {
        Gold,
        Exp,


        WeaponUpgrade_Sword,
        WeaponUpgrade_Pistol,
        WeaponUpgrade_Staff,
        ClothesUpgrade,
        StockingUpgrade,

        NewSlave,
        NewSoldier,

        Shop,
        MagicCircle,
        Sword_Buff,
        Pistol_Buff,
        Staff_Buff,

        // …以后随时扩展
    }
    public BonusType type;

    public string description;
    public int value;  // 可以是数值，比如金钱=100, exp=300
    public int WeaponIndex;//武器编号1~10
    public int WeaponPower;//武器数值

    public int ClothesStockingIndex;//防具编号2,10~12
    public int ClothesStockingDefence;//武器数值

    public GameObject highlightObj; // 高亮显示的物体，比如绿色描边

    public int index; // 由UIManager初始化时赋值


    // Start is called before the first frame update
    public void ReNewBonus()
    {
        value = Random.Range(100, 300);
        WeaponIndex= Random.Range(1, 11);
        WeaponPower = Random.Range(3, 10);



        // 随机选择一个防具编号（2, 10, 11, 12）
        int[] validIndexes = { 2, 3, 4, 5, 6, 7, 10, 11, 12 };
        ClothesStockingIndex = validIndexes[Random.Range(0, validIndexes.Length)];

        ClothesStockingDefence = Random.Range(3, 10);

        switch (type)
        {
            case BonusType.Gold:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "周囲を探し、<color=#ADD8E6>" + value + "</color> ゴールドを見つけた。";
                        break;
                    case 1: // 简体
                        description = "你搜索四周，找到了<color=#ADD8E6>" + value + "</color>点金币";
                        break;
                    case 2: // 繁体
                        description = "你搜尋四周，找到了<color=#ADD8E6>" + value + "</color>點金幣";
                        break;
                    case 3: // 英语
                        description = "You searched around and found <color=#ADD8E6>" + value + "</color> gold.";
                        break;
                    case 4: // 韩语
                        description = "주위를 수색하여 <color=#ADD8E6>" + value + "</color> 골드를 발견했다.";
                        break;
                }
                break;

            case BonusType.Exp:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "先程の戦闘で追加で <color=#ADD8E6>" + value + "</color> 経験値を獲得した。";
                        break;
                    case 1: // 简体
                        description = "刚才的战斗让你额外获得<color=#ADD8E6>" + value + "</color>点经验";
                        break;
                    case 2: // 繁体
                        description = "剛才的戰鬥讓你額外獲得<color=#ADD8E6>" + value + "</color>點經驗";
                        break;
                    case 3: // 英语
                        description = "The recent battle granted you an extra <color=#ADD8E6>" + value + "</color> EXP.";
                        break;
                    case 4: // 韩语
                        description = "방금 전 전투에서 추가로 <color=#ADD8E6>" + value + "</color> 경험치를 얻었다.";
                        break;
                }
                break;

            case BonusType.WeaponUpgrade_Sword:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "傍らの武器棚から剣を見つけた……\n現在の武器攻撃力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 攻撃力上昇 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 1: // 简体
                        description = "你从一旁的武器架子上找到了一把剑……\n目前你的武器攻击力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 攻击力上升 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 2: // 繁体
                        description = "你從一旁的武器架子上找到了一把劍……\n目前你的武器攻擊力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 攻擊力上升 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 3: // 英语
                        description = "You found a sword on a nearby rack…\nCurrent Weapon Power <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → Attack <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 4: // 韩语
                        description = "옆의 무기 선반에서 검을 발견했다…\n현재 무기 공격력 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 공격력 상승 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                }
                break;

            case BonusType.WeaponUpgrade_Pistol:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "箱の中から銃を見つけた……\n現在の武器攻撃力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 攻撃力上昇 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 1: // 简体
                        description = "你从箱子里找到一把枪……\n目前你的武器攻击力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 攻击力上升 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 2: // 繁体
                        description = "你從箱子裡找到一把槍……\n目前你的武器攻擊力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 攻擊力上升 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 3: // 英语
                        description = "You found a pistol in a chest…\nCurrent Weapon Power <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → Attack <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 4: // 韩语
                        description = "상자 속에서 권총을 발견했다…\n현재 무기 공격력 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 공격력 상승 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                }
                break;

            case BonusType.WeaponUpgrade_Staff:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "戦いの中、倒れた魔法使いが杖を託した……\n現在の武器攻撃力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 攻撃力上昇 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 1: // 简体
                        description = "战斗中一名女法师在死前将法杖给了你……\n目前你的武器攻击力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 攻击力上升 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 2: // 繁体
                        description = "戰鬥中一名女法師在死前將法杖給了你……\n目前你的武器攻擊力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 攻擊力上升 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 3: // 英语
                        description = "In battle, a dying sorceress gave you her staff…\nCurrent Weapon Power <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → Attack <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                    case 4: // 韩语
                        description = "전투 중 죽어가던 여마법사가 지팡이를 건네주었다…\n현재 무기 공격력 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentWeaponPower
                                      + "</color> → 공격력 상승 <color=#80FFFF>+" + WeaponPower + "</color>";
                        break;
                }
                break;

            case BonusType.ClothesUpgrade:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "敵の服を剥ぎ取り着替えた……\n現在の衣服防御力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentArmorDefence
                                      + "</color> → 防御力上昇 <color=#80FFFF>+" + ClothesStockingDefence + "</color>";
                        break;
                    case 1: // 简体
                        description = "你剥下敌人身上的衣服并且换上了……\n目前你的衣服防御力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentArmorDefence
                                      + "</color> → 衣服防御力上升 <color=#80FFFF>+" + ClothesStockingDefence + "</color>";
                        break;
                    case 2: // 繁体
                        description = "你剝下敵人身上的衣服並且換上了……\n目前你的衣服防禦力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentArmorDefence
                                      + "</color> → 衣服防禦力上升 <color=#80FFFF>+" + ClothesStockingDefence + "</color>";
                        break;
                    case 3: // 英语
                        description = "You stripped the enemy’s clothes and put them on…\nCurrent Cloth Defense <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentArmorDefence
                                      + "</color> → Defense <color=#80FFFF>+" + ClothesStockingDefence + "</color>";
                        break;
                    case 4: // 韩语
                        description = "적의 옷을 벗겨 입었다…\n현재 옷 방어력 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentArmorDefence
                                      + "</color> → 방어력 상승 <color=#80FFFF>+" + ClothesStockingDefence + "</color>";
                        break;
                }
                break;

            case BonusType.StockingUpgrade:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "敵のストッキングを剥ぎ取り履き替えた……\n現在のストッキング防御力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentStockingDefence
                                      + "</color> → 防御力上昇 <color=#80FFFF>+" + ClothesStockingDefence + "</color>";
                        break;
                    case 1: // 简体
                        description = "你剥下敌人身上的丝袜并且换上了……\n目前你的丝袜防御力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentStockingDefence
                                      + "</color> → 丝袜防御力上升 <color=#80FFFF>+" + ClothesStockingDefence + "</color>";
                        break;
                    case 2: // 繁体
                        description = "你剝下敵人身上的絲襪並且換上了……\n目前你的絲襪防禦力 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentStockingDefence
                                      + "</color> → 絲襪防禦力上升 <color=#80FFFF>+" + ClothesStockingDefence + "</color>";
                        break;
                    case 3: // 英语
                        description = "You stripped the enemy’s stockings and wore them…\nCurrent Stocking Defense <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentStockingDefence
                                      + "</color> → Defense <color=#80FFFF>+" + ClothesStockingDefence + "</color>";
                        break;
                    case 4: // 韩语
                        description = "적의 스타킹을 벗겨 신었다…\n현재 스타킹 방어력 <color=#ADD8E6>"
                                      + UIManager.instance.player.CurrentStockingDefence
                                      + "</color> → 방어력 상승 <color=#80FFFF>+" + ClothesStockingDefence + "</color>";
                        break;
                }
                break;

            case BonusType.NewSlave:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "救出された奴隷が自ら進んで仲間に加わった。";
                        break;
                    case 1: // 简体
                        description = "一名被解救的性奴隶自愿加入你们队伍。";
                        break;
                    case 2: // 繁体
                        description = "一名被解救的性奴隸自願加入你們隊伍。";
                        break;
                    case 3: // 英语
                        description = "A rescued slave willingly joins your party.";
                        break;
                    case 4: // 韩语
                        description = "구출된 노예 한 명이 자발적으로 당신의 파티에 합류했다.";
                        break;
                }
                break;

            case BonusType.NewSoldier:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "奴隷に同情した兵士が自ら進んで仲間に加わった。";
                        break;
                    case 1: // 简体
                        description = "一名同情奴隶的士兵自愿加入你们队伍。";
                        break;
                    case 2: // 繁体
                        description = "一名同情奴隸的士兵自願加入你們隊伍。";
                        break;
                    case 3: // 英语
                        description = "A soldier who sympathized with the slaves willingly joined your party.";
                        break;
                    case 4: // 韩语
                        description = "노예들에게 동정심을 가진 한 병사가 자발적으로 당신의 파티에 합류했다.";
                        break;
                }
                break;

            case BonusType.Shop:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "前方で奴隷商人を見つけた。性奴だけでなく、武器や装備も取り扱っているようだ。";
                        break;
                    case 1: // 简体
                        description = "你们在前面发现一个奴隶商人，除了性奴之外还贩卖一些武器和装备。";
                        break;
                    case 2: // 繁体
                        description = "你們在前方發現一名奴隸商人，他除了販售性奴外，似乎也在販賣武器與裝備。";
                        break;
                    case 3: // 英语
                        description = "You encounter a slave merchant ahead. He trades not only in slaves but also in weapons and equipment.";
                        break;
                    case 4: // 韩语
                        description = "앞쪽에서 노예 상인을 발견했다. 그는 성노예뿐 아니라 무기와 장비도 거래하는 듯하다.";
                        break;
                }
                break;

            case BonusType.MagicCircle:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "前方に生命を回復する魔法陣を発見した。しかし、その力は長くは続かない。";
                        break;
                    case 1: // 简体
                        description = "你们在前面发现一个回复生命的魔法阵，但是魔法阵不会持续太久。";
                        break;
                    case 2: // 繁体
                        description = "你們在前方發現一個恢復生命的魔法陣，但它的力量似乎不會持續太久。";
                        break;
                    case 3: // 英语
                        description = "You discover a healing magic circle ahead, but its power won’t last for long.";
                        break;
                    case 4: // 韩语
                        description = "앞쪽에서 생명을 회복하는 마법진을 발견했다. 하지만 그 힘은 오래 지속되지 않는다.";
                        break;
                }
                break;

            case BonusType.Sword_Buff:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "通りすがりの魔族の少女があなたたちの近接武器に魔法をかけてくれた。\n<color=#ADD8E6>短時間、あなたと仲間の近接攻撃力が大幅に上昇する。</color>";
                        break;
                    case 1: // 简体
                        description = "一位路过的魔族少女给你们的近战武器附魔。\n<color=#ADD8E6>短期你和你的队友近战伤害大幅上升</color>，当前层数：" + GameFlowData.Sword_Buff;
                        break;
                    case 2: // 繁体
                        description = "一位路過的魔族少女替你們的近戰武器施加了附魔。\n<color=#ADD8E6>短時間內，你與隊友的近戰傷害大幅提升。</color>";
                        break;
                    case 3: // 英语
                        description = "A passing demon girl enchants your melee weapons.\n<color=#ADD8E6>For a short time, you and your allies deal greatly increased melee damage.</color>";
                        break;
                    case 4: // 韩语
                        description = "지나가던 마족 소녀가 너희의 근접 무기에 마법을 걸었다.\n<color=#ADD8E6>잠시 동안 너와 동료의 근접 공격력이 크게 상승한다.</color>";
                        break;
                }
                break;

            case BonusType.Pistol_Buff:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "通りすがりの懲戒シスターがあなたたちの射撃武器に祝福を与えた。\n<color=#ADD8E6>短時間、あなたと仲間の射撃攻撃力が大幅に上昇する。</color>";
                        break;
                    case 1: // 简体
                        description = "一位路过的惩戒修女给你们的射击武器附魔。\n<color=#ADD8E6>短期你和你的队友射击伤害大幅上升</color>" + GameFlowData.Pistol_Buff;
                        break;
                    case 2: // 繁体
                        description = "一位路過的懲戒修女替你們的射擊武器施加了附魔。\n<color=#ADD8E6>短時間內，你與隊友的射擊傷害大幅提升。</color>";
                        break;
                    case 3: // 英语
                        description = "A passing Penitent Sister blesses your ranged weapons.\n<color=#ADD8E6>For a short time, you and your allies deal greatly increased ranged damage.</color>";
                        break;
                    case 4: // 韩语
                        description = "지나가던 징벌 수녀가 너희의 사격 무기에 축복을 내렸다.\n<color=#ADD8E6>잠시 동안 너와 동료의 사격 피해가 크게 증가한다.</color>";
                        break;
                }
                break;

            case BonusType.Staff_Buff:
                switch (PlayerPrefs.GetInt("language"))
                {
                    case 0: // 日语
                        description = "通りすがりの懲戒シスターがあなたたちの杖に魔力を込めた。\n<color=#ADD8E6>短時間、あなたと仲間の魔法攻撃力が大幅に上昇する。</color>";
                        break;
                    case 1: // 简体
                        description = "一位路过的惩戒修女给你们的法杖武器附魔。\n<color=#ADD8E6>短期你和你的队友法术伤害大幅上升</color>" + GameFlowData.Staff_Buff;
                        break;
                    case 2: // 繁体
                        description = "一位路過的懲戒修女替你們的法杖施加了附魔。\n<color=#ADD8E6>短時間內，你與隊友的法術傷害大幅提升。</color>";
                        break;
                    case 3: // 英语
                        description = "A passing Penitent Sister imbues your staves with magic.\n<color=#ADD8E6>For a short time, you and your allies deal greatly increased spell damage.</color>";
                        break;
                    case 4: // 韩语
                        description = "지나가던 징벌 수녀가 너희의 지팡이에 마력을 주입했다.\n<color=#ADD8E6>잠시 동안 너와 동료의 마법 피해가 크게 증가한다.</color>";
                        break;
                }
                break;
        }

        


    }

    public void SetHighlight(bool on)
    {
        highlightObj.SetActive(on); // 只在解锁时允许显示高亮
    }

    #region 手机端按键需求
    public void TouchBonusButton() 
    {
        // 直接告诉UIManager我被点了
        UIManager.instance.SelectBonusByIndex(index);
    }

    #endregion


    public void ApplyBonus()
    {
        switch (type)
        {
            case BonusType.Gold:
                UIManager.instance.ChangeMoney(value);
                break;
            case BonusType.Exp:
                UIManager.instance.player.ChangeExperience(value);
                break;





            case BonusType.WeaponUpgrade_Sword:

                // 先保存血量比例
                float ratio = Mathf.Clamp01((float)UIManager.instance.player.currentHealth / Mathf.Max(1, UIManager.instance.player.maxHealth));



                // 给玩家加属性
                UIManager.instance.player.PickupWeapon(WeaponIndex, 0); // 剑，剑士
                int WeaponAtk = UIManager.instance.player.CurrentWeaponPower;
                WeaponAtk += WeaponPower;
                UIManager.instance.player.CurrentWeaponPower = WeaponAtk;
                UIManager.instance.player.SaveCurrent();


                // ✅ 再次刷新并保持血量
                if (ratio < 0.999f)
                    UIManager.instance.player.ApplySaveData(SaveManager.Load(UIManager.instance.player.currentSaveName), true, ratio);


                UIManager.instance.player.frameEvents._SE_Clothes();

                break;

            case BonusType.WeaponUpgrade_Pistol:


                // 先保存血量比例
                float ratio2 = Mathf.Clamp01((float)UIManager.instance.player.currentHealth / Mathf.Max(1, UIManager.instance.player.maxHealth));


                // 给玩家加属性
                UIManager.instance.player.PickupWeapon(WeaponIndex, 1); // 枪，射手
                int WeaponAtk2 = UIManager.instance.player.CurrentWeaponPower;
                WeaponAtk2 += WeaponPower;
                UIManager.instance.player.CurrentWeaponPower = WeaponAtk2;
                UIManager.instance.player.SaveCurrent();


                // ✅ 再次刷新并保持血量
                if (ratio2 < 0.999f)
                    UIManager.instance.player.ApplySaveData(SaveManager.Load(UIManager.instance.player.currentSaveName), true, ratio2);



                UIManager.instance.player.frameEvents._SE_Clothes();

                break;

            case BonusType.WeaponUpgrade_Staff:


                // 先保存血量比例
                float ratio3 = Mathf.Clamp01((float)UIManager.instance.player.currentHealth / Mathf.Max(1, UIManager.instance.player.maxHealth));



                // 给玩家加属性
                UIManager.instance.player.PickupWeapon(WeaponIndex, 2); //杖，法师
                int WeaponAtk3 = UIManager.instance.player.CurrentWeaponPower;
                WeaponAtk3 += WeaponPower;
                UIManager.instance.player.CurrentWeaponPower = WeaponAtk3;
                UIManager.instance.player.SaveCurrent();


                // ✅ 再次刷新并保持血量
                if (ratio3 < 0.999f)
                    UIManager.instance.player.ApplySaveData(SaveManager.Load(UIManager.instance.player.currentSaveName), true, ratio3);


                UIManager.instance.player.frameEvents._SE_Clothes();

                break;


            case BonusType.ClothesUpgrade:

                // 先保存血量比例
                float ratio4 = Mathf.Clamp01((float)UIManager.instance.player.currentHealth / Mathf.Max(1, UIManager.instance.player.maxHealth));


                UIManager.instance.player.YYY_bodyIndex = ClothesStockingIndex; UIManager.instance.player.SetSkin();
                int ArmorDef = UIManager.instance.player.CurrentArmorDefence;
                ArmorDef += ClothesStockingDefence;
                UIManager.instance.player.CurrentArmorDefence = ArmorDef;
                UIManager.instance.player.SaveCurrent();


                // ✅ 再次刷新并保持血量
                if (ratio4 < 0.999f)
                    UIManager.instance.player.ApplySaveData(SaveManager.Load(UIManager.instance.player.currentSaveName), true, ratio4);



                UIManager.instance.player.frameEvents._SE_Clothes();

                break;

            case BonusType.StockingUpgrade:

                // 先保存血量比例
                float ratio5 = Mathf.Clamp01((float)UIManager.instance.player.currentHealth / Mathf.Max(1, UIManager.instance.player.maxHealth));




                UIManager.instance.player.YYY_legsIndex = ClothesStockingIndex; UIManager.instance.player.SetSkin();
                int StockingDef = UIManager.instance.player.CurrentStockingDefence;
                StockingDef += ClothesStockingDefence;
                UIManager.instance.player.CurrentStockingDefence = StockingDef;
                UIManager.instance.player.SaveCurrent();


                // ✅ 再次刷新并保持血量
                if (ratio5 < 0.999f)
                    UIManager.instance.player.ApplySaveData(SaveManager.Load(UIManager.instance.player.currentSaveName), true, ratio5);


                UIManager.instance.player.frameEvents._SE_Clothes();

                break;




            case BonusType.NewSlave:
                UIManager.instance._RoomGenerator.SetFriend(0);
                break;
            case BonusType.NewSoldier:
                UIManager.instance._RoomGenerator.SetFriend(1);
                break;

          

            case BonusType.Shop:
                UIManager.instance._RoomGenerator.playerRoom.SetShop();
                break;


            case BonusType.MagicCircle:
                UIManager.instance._RoomGenerator.playerRoom.SetMagicCircle();
                break;

            case BonusType.Sword_Buff:

                if (GameFlowData.Sword_Buff == 0)
                    GameFlowData.Sword_Buff = 2; // 初次激活为2
                else
                    GameFlowData.Sword_Buff++;   // 之后每次+1

                UIManager.instance.UpdateBuffUI();

                break;
            case BonusType.Pistol_Buff:

                if (GameFlowData.Pistol_Buff == 0)
                    GameFlowData.Pistol_Buff = 2;// 初次激活为2
                else
                    GameFlowData.Pistol_Buff++;// 之后每次+1

                UIManager.instance.UpdateBuffUI();

                break;
            case BonusType.Staff_Buff:

                if (GameFlowData.Staff_Buff == 0)
                    GameFlowData.Staff_Buff = 2;// 初次激活为2
                else
                    GameFlowData.Staff_Buff++;// 之后每次+1

                UIManager.instance.UpdateBuffUI();

                break;
        }

        UIManager.instance.HideBonusCavans();

    }//按下领取按钮触发
}

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

                // 给玩家加属性
                UIManager.instance.player.PickupWeapon(WeaponIndex, 0); // 剑，剑士
                int WeaponAtk = UIManager.instance.player.CurrentWeaponPower;
                WeaponAtk += WeaponPower;
                UIManager.instance.player.CurrentWeaponPower = WeaponAtk;
                UIManager.instance.player.SaveCurrent();

                UIManager.instance.player.frameEvents._SE_Clothes();

                break;

            case BonusType.WeaponUpgrade_Pistol:

                // 给玩家加属性
                UIManager.instance.player.PickupWeapon(WeaponIndex, 1); // 枪，射手
                int WeaponAtk2 = UIManager.instance.player.CurrentWeaponPower;
                WeaponAtk2 += WeaponPower;
                UIManager.instance.player.CurrentWeaponPower = WeaponAtk2;
                UIManager.instance.player.SaveCurrent();

                UIManager.instance.player.frameEvents._SE_Clothes();

                break;

            case BonusType.WeaponUpgrade_Staff:

                // 给玩家加属性
                UIManager.instance.player.PickupWeapon(WeaponIndex, 2); //杖，法师
                int WeaponAtk3 = UIManager.instance.player.CurrentWeaponPower;
                WeaponAtk3 += WeaponPower;
                UIManager.instance.player.CurrentWeaponPower = WeaponAtk3;
                UIManager.instance.player.SaveCurrent();


                UIManager.instance.player.frameEvents._SE_Clothes();

                break;


            case BonusType.ClothesUpgrade:
                UIManager.instance.player.YYY_bodyIndex = ClothesStockingIndex; UIManager.instance.player.SetSkin();
                int ArmorDef = UIManager.instance.player.CurrentArmorDefence;
                ArmorDef += ClothesStockingDefence;
                UIManager.instance.player.CurrentArmorDefence = ArmorDef;
                UIManager.instance.player.SaveCurrent();

                UIManager.instance.player.frameEvents._SE_Clothes();
            
                break;

            case BonusType.StockingUpgrade:

                UIManager.instance.player.YYY_legsIndex = ClothesStockingIndex; UIManager.instance.player.SetSkin();
                int StockingDef = UIManager.instance.player.CurrentStockingDefence;
                StockingDef += ClothesStockingDefence;
                UIManager.instance.player.CurrentStockingDefence = StockingDef;
                UIManager.instance.player.SaveCurrent();

                UIManager.instance.player.frameEvents._SE_Clothes();

                break;




            case BonusType.NewSlave:
                UIManager.instance._RoomGenerator.SetFriend(0);
                break;
            case BonusType.NewSoldier:
                UIManager.instance._RoomGenerator.SetFriend(1);
                break;
        }

        UIManager.instance.HideBonusCavans();

    }//按下领取按钮触发
}

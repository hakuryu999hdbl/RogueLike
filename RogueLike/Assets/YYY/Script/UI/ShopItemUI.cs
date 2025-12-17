using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    //public Image icon;
    public Text nameText;
    public Text priceText;
    public Text valueText;
    public GameObject highlight;
    public ShopItemData data;

    public void Setup(ShopItemData d)
    {
        data = d;
        //icon.sprite = d.icon;
        nameText.text = d.displayName;
        priceText.text = d.price.ToString();


        if (d.value == 0) { valueText.text = "".ToString(); }//性奴没有增强表示
        else 
        {
            string prefix = "";

            switch (d.type)
            {
                case ShopItemData.ItemType.Sword:
                case ShopItemData.ItemType.Pistol:
                case ShopItemData.ItemType.Staff:
                    prefix = "ATK+";
                    break;

                case ShopItemData.ItemType.Clothes:
                case ShopItemData.ItemType.Stockings:
                    prefix = "DEF+";
                    break;

                case ShopItemData.ItemType.Slave:
                    prefix = ""; // 性奴无加成
                    break;
            }

            valueText.text = "  " + prefix + d.value.ToString();
        }
        

        highlight.SetActive(false);
    }

    public void SetHighlight(bool on)
    {
        highlight.SetActive(on);
    }

    // ✅ 现在点击只会“选中”而不购买
    public void OnClickBuy()
    {
        //UIManager.instance.TryBuyItem(this);

        UIManager.instance.SelectShopByUI(this);
    }
}

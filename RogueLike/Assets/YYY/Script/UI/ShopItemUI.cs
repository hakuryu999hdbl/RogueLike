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
        valueText.text = "  +" + d.value.ToString();

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

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public Item item;
    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;

    public void Setup(Item newItem)
    {
        item = newItem;

        if (icon != null) icon.sprite = item.itemIcon;
        if (nameText != null) nameText.text = item.itemName;
        if (priceText != null) priceText.text = item.price + " érme";
    }

    public void OnSlotClicked()
    {
        ShopManager.Instance.ShowConfirmPanel(item);
    }
}
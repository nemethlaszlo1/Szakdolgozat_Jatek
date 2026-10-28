using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public Image icon;
    public Button slotButton;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        slotButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        slotButton.interactable = false;
    }

    public void OnSlotClicked()
    {
        if (item != null)
        {
            InventoryManager.Instance.OpenActionPanel(this);
        }
    }
}
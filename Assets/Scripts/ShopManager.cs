using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("UI Elemek")]
    public GameObject shopPanel;
    public ShopSlot[] shopSlots;

    [Header("Megerősítő Ablak")]
    public GameObject confirmPanel;
    public TextMeshProUGUI buyButtonText;
    public TextMeshProUGUI sellButtonText;

    private Item currentItemToBuy;

    void Awake()
    {
        Instance = this;
        if (shopPanel != null) shopPanel.SetActive(false);
        if (confirmPanel != null) confirmPanel.SetActive(false);
    }

    public void OpenShop(List<Item> itemsForSale)
    {
        shopPanel.SetActive(true);

        for (int i = 0; i < shopSlots.Length; i++)
        {
            if (i < itemsForSale.Count)
            {
                shopSlots[i].gameObject.SetActive(true);
                shopSlots[i].Setup(itemsForSale[i]);
            }
            else
            {
                shopSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        confirmPanel.SetActive(false);
    }

    public void ShowConfirmPanel(Item item)
    {
        currentItemToBuy = item;

        if (buyButtonText != null) buyButtonText.text = "Megvesz: " + item.price;
        if (sellButtonText != null) sellButtonText.text = "Elad: " + item.price;

        confirmPanel.SetActive(true);
    }

    public void BuyItem()
    {
        GameManager gm = Object.FindFirstObjectByType<GameManager>();

        if (gm != null && gm.coins >= currentItemToBuy.price)
        {
            if (InventoryManager.Instance.AddItem(currentItemToBuy))
            {
                gm.coins -= currentItemToBuy.price;
                gm.UpdateUI();
                Debug.Log("Megvetted: " + currentItemToBuy.itemName);
            }
            else
            {
                Debug.Log("Tele van a táskád!");
            }
        }
        else
        {
            Debug.Log("Nincs elég érméd!");
        }
        confirmPanel.SetActive(false);
    }

    public void SellItem()
    {
        GameManager gm = Object.FindFirstObjectByType<GameManager>();

        if (InventoryManager.Instance.RemoveItemByType(currentItemToBuy))
        {
            gm.coins += currentItemToBuy.price;
            gm.UpdateUI();
            Debug.Log("Eladtad: " + currentItemToBuy.itemName);
        }
        else
        {
            Debug.Log("Nincs nálad ilyen tárgy!");
        }
        confirmPanel.SetActive(false);
    }

    public void CancelPurchase()
    {
        confirmPanel.SetActive(false);
    }
}
using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public GameObject inventoryPanel;
    public GameObject actionPanel;
    public Transform slotsParent;
    public Item[] startingItems;

    private InventorySlot[] slots;
    private bool isInventoryOpen = false;
    private InventorySlot selectedSlot;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        slots = slotsParent.GetComponentsInChildren<InventorySlot>();
        actionPanel.SetActive(false);
        foreach (Item item in startingItems)
        {
            AddItem(item);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
        actionPanel.SetActive(false);
    }
    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(itemToAdd);
                return true;
            }
        }
        Debug.Log("Tele van az inventory!"); //debug
        return false;
    }

    public void OpenActionPanel(InventorySlot slot)
    {
        selectedSlot = slot;
        actionPanel.SetActive(true);
    }

    public void UseSelectedItem()
    {
        if (selectedSlot != null && selectedSlot.item != null)
        {
            Item itemToUse = selectedSlot.item;

            if (itemToUse.isConsumable)
            {
                FindObjectOfType<GameManager>().Heal(itemToUse.healAmount);
                selectedSlot.ClearSlot();
            }
            else if (itemToUse.isWeapon)
            {
                PlayerCombat combat = FindObjectOfType<PlayerCombat>();
                Item oldWeapon = combat.currentWeapon;

                combat.EquipWeapon(itemToUse);

                if (oldWeapon != null)
                {
                    selectedSlot.AddItem(oldWeapon);
                }
                else
                {
                    selectedSlot.ClearSlot();
                }
            }
        }
        actionPanel.SetActive(false);
    }

    public void DropSelectedItem()
    {
        if (selectedSlot != null)
        {
            Debug.Log(selectedSlot.item.itemName + " eldobva."); //debug
            selectedSlot.ClearSlot();
        }
        actionPanel.SetActive(false);
    }

    public void CancelAction()
    {
        actionPanel.SetActive(false);
    }
}
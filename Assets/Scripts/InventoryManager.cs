using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;

    private bool isInventoryOpen = false;

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

        if (isInventoryOpen)
        {
            Debug.Log("Inventory megnyitva.");
        }
    }
}
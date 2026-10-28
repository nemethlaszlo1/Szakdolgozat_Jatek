using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item itemData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bool wasPickedUp = InventoryManager.Instance.AddItem(itemData);

            if (wasPickedUp)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Tele van a táska: " + itemData.itemName); //debug
            }
        }
    }
}
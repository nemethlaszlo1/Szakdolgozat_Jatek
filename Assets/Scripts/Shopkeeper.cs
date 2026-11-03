using UnityEngine;
using System.Collections.Generic;

public class Shopkeeper : MonoBehaviour
{
    public List<Item> itemsToSell = new List<Item>();

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShopManager.Instance.OpenShop(itemsToSell);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Nyomj E-t a vásárláshoz!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (ShopManager.Instance != null)
            {
                ShopManager.Instance.CloseShop();
            }
        }
    }
}
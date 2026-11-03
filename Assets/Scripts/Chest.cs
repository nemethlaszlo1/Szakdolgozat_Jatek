using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ChestLoot
{
    public Item item;
    [Range(0f, 100f)]
    public float dropChance = 50f;
}

public class Chest : MonoBehaviour
{
    public bool isLocked = false;
    public string requiredKeyName = "Lada_Kulcs";

    public List<ChestLoot> possibleLoot = new List<ChestLoot>();
    public Sprite openSprite;

    private bool playerInRange = false;
    private bool isOpened = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerInRange && !isOpened && Input.GetKeyDown(KeyCode.E))
        {
            TryOpen();
        }
    }

    void TryOpen()
    {
        if (isLocked)
        {
            if (InventoryManager.Instance.ConsumeKey(requiredKeyName))
            {
                OpenChest();
            }
            else
            {
                Debug.Log("Zárva!");
            }
        }
        else
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        isOpened = true;
        if (openSprite != null) spriteRenderer.sprite = openSprite;

        bool gotAnything = false;

        foreach (ChestLoot loot in possibleLoot)
        {
            float roll = Random.Range(0f, 100f);

            if (roll <= loot.dropChance)
            {
                if (InventoryManager.Instance.AddItem(loot.item))
                {
                    Debug.Log("Kaptál egy tárgyat: " + loot.item.itemName);
                    gotAnything = true;
                }
                else
                {
                    Debug.Log("Tele a táskád! A többi jutalom elveszett.");
                    break;
                }
            }
        }

        if (!gotAnything)
        {
            Debug.Log("A láda üres volt!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerInRange = false;
    }
}
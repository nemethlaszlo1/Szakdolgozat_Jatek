using UnityEngine;
using System.Collections.Generic;

// tárgyat és a hozzá tartozó esélyt
[System.Serializable]
public class LootItem
{
    public GameObject itemPrefab; // fizikai tárgy
    [Range(0, 100)]
    public float dropChance;      // esély (%)
}

public class LootSpawner : MonoBehaviour
{
    public List<LootItem> lootTable = new List<LootItem>();

    public void DropLoot()
    {
        foreach (LootItem loot in lootTable)
        {
            float randomChance = Random.Range(0f, 100f);

            if (randomChance <= loot.dropChance)
            {
                Instantiate(loot.itemPrefab, transform.position, Quaternion.identity);

                return;
            }
        }
    }
}
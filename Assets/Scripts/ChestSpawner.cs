using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [Header("Spawn Beállítások")]
    public float spawnChance = 15f;

    public GameObject[] simpleChests;
    public GameObject[] betterChests;

    void Start()
    {
        if (Random.Range(0f, 100f) > spawnChance)
        {
            Destroy(gameObject);
            return;
        }

        GameObject chestToSpawn = null;
        float typeRoll = Random.Range(0f, 100f);

        // 20% esély jobb láda, 80% esély sima láda
        if (typeRoll < 20f && betterChests.Length > 0)
        {
            chestToSpawn = betterChests[Random.Range(0, betterChests.Length)];
        }
        else if (simpleChests.Length > 0)
        {
            chestToSpawn = simpleChests[Random.Range(0, simpleChests.Length)];
        }

        if (chestToSpawn != null)
        {
            Instantiate(chestToSpawn, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Beállítások")]
    public float spawnChance = 60f;

    [Header("Szörny")]
    public GameObject[] weakEnemies;
    public GameObject[] strongEnemies;
    public GameObject[] miniBosses;

    void Start()
    {
        if (Random.Range(0f, 100f) > spawnChance)//spawnol valami?
        {
            Destroy(gameObject);
            return;
        }

        GameObject enemyToSpawn = null;
        float typeRoll = Random.Range(0f, 100f);

        // típusok 5% miniboss, 25% erős, 70% gyenge
        if (typeRoll < 5f && miniBosses.Length > 0)
        {
            enemyToSpawn = miniBosses[Random.Range(0, miniBosses.Length)];
        }
        else if (typeRoll < 30f && strongEnemies.Length > 0)
        {
            enemyToSpawn = strongEnemies[Random.Range(0, strongEnemies.Length)];
        }
        else if (weakEnemies.Length > 0)
        {
            enemyToSpawn = weakEnemies[Random.Range(0, weakEnemies.Length)];
        }

        if (enemyToSpawn != null)
        {
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
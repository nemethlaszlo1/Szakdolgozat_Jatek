using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public RoomTemplates templates;
    public int maxRooms = 40;
    public float gridSize = 15f;

    public Dictionary<Vector2Int, GameObject> grid = new Dictionary<Vector2Int, GameObject>();
    private Queue<RoomSpawner> spawnerQueue = new Queue<RoomSpawner>();

    private int spawnedRooms = 0;
    private bool treasureSpawned = false;

    void Start()
    {
        templates = GetComponent<RoomTemplates>();
        grid.Add(Vector2Int.zero, GameObject.FindGameObjectWithTag("StarterRoom"));

        RoomSpawner[] starterSpawners = FindObjectsOfType<RoomSpawner>();
        foreach (RoomSpawner spawner in starterSpawners)
        {
            spawnerQueue.Enqueue(spawner);
        }

        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        while (spawnerQueue.Count > 0)
        {
            RoomSpawner currentDoor = spawnerQueue.Dequeue();

            if (currentDoor.spawned) continue;

            Vector2Int gridPos = new Vector2Int(
                Mathf.RoundToInt(currentDoor.transform.position.x / gridSize),
                Mathf.RoundToInt(currentDoor.transform.position.y / gridSize)
            );

            if (grid.ContainsKey(gridPos))
            {
                currentDoor.spawned = true;
                Destroy(currentDoor.gameObject);
                continue;
            }

            if (spawnedRooms < maxRooms)
            {
                // spawner fizikai elhelyezkedésének kiszámítása a szülőtől
                int calculatedDirection = DetermineDirection(currentDoor.transform);

                GameObject roomToSpawn = GetPrefab(calculatedDirection, currentDoor.mustSpawnCorridor);

                if (roomToSpawn != null)
                {
                    Vector3 exactPos = new Vector3(gridPos.x * gridSize, gridPos.y * gridSize, 0f);
                    GameObject newRoom = Instantiate(roomToSpawn, exactPos, roomToSpawn.transform.rotation);

                    grid.Add(gridPos, newRoom);
                    spawnedRooms++;

                    RoomSpawner[] newSpawners = newRoom.GetComponentsInChildren<RoomSpawner>();
                    foreach (RoomSpawner spawner in newSpawners)
                    {
                        spawnerQueue.Enqueue(spawner);
                    }
                }
            }
            else
            {
                Vector3 exactPos = new Vector3(gridPos.x * gridSize, gridPos.y * gridSize, 0f);
                GameObject closedWall = Instantiate(templates.closedRoom, exactPos, Quaternion.identity);
                grid.Add(gridPos, closedWall);
            }

            currentDoor.spawned = true;
            Destroy(currentDoor.gameObject);
        }
    }

    // irány meghatározása a spawner helyi pozíciója alapján
    int DetermineDirection(Transform spawnerTransform)
    {
        // spawner pozíciójának lekérése a szülőhöz képest
        Vector3 localPos = spawnerTransform.localPosition;

        if (localPos.y > 5f) return 2;      // Észak
        if (localPos.y < -5f) return 1;     // Dél
        if (localPos.x > 5f) return 3;      // Kelet
        if (localPos.x < -5f) return 4;     // Nyugat

        //ha valamiért pont középen lenne
        return 1;
    }

    GameObject GetPrefab(int openingDirection, bool mustSpawnCorridor)
    {
        GameObject[] pool = null;

        // irányok fixelése, ahogy elozo verzioban irtam
        if (mustSpawnCorridor)
        {
            if (openingDirection == 1) pool = templates.bottomCorridors;
            else if (openingDirection == 2) pool = templates.topCorridors;
            else if (openingDirection == 3) pool = templates.rightCorridors;
            else if (openingDirection == 4) pool = templates.leftCorridors;
        }
        else
        {
            if (openingDirection == 1) pool = templates.bottomRooms;
            else if (openingDirection == 2) pool = templates.topRooms;
            else if (openingDirection == 3) pool = templates.rightRooms;
            else if (openingDirection == 4) pool = templates.leftRooms;
        }

        if (pool == null || pool.Length == 0) return null;

        int rand = Random.Range(0, pool.Length);
        GameObject selected = pool[rand];

        if (selected != null && selected.name.Contains("TreasureRoom"))
        {
            if (treasureSpawned) return pool[0];
            else
            {
                treasureSpawned = true;
                return selected;
            }
        }
        return selected;
    }
}
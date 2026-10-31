using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    public float spawnDelay = 0.5f;
    public bool mustSpawnCorridor = false;

    private RoomTemplates templates;
    public bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("GameController").GetComponent<RoomTemplates>();
        Invoke("Spawn", spawnDelay);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            // Ha foglalt a hely, megállunk
            if (IsPositionOccupied(transform.position))
            {
                spawned = true;
                Destroy(gameObject);
                return;
            }

            if (templates.rooms.Count < templates.maxRooms)
            {
                GameObject roomToSpawn = null;

                //folyosó lerak.
                if (mustSpawnCorridor)
                {
                    if (openingDirection == 1) { roomToSpawn = GetPrefab(templates.bottomCorridors); }
                    else if (openingDirection == 2) { roomToSpawn = GetPrefab(templates.topCorridors); }
                    else if (openingDirection == 3) { roomToSpawn = GetPrefab(templates.rightCorridors); }
                    else if (openingDirection == 4) { roomToSpawn = GetPrefab(templates.leftCorridors); }
                }
                //szoba lerak
                else
                {
                    if (openingDirection == 1) { roomToSpawn = GetPrefab(templates.bottomRooms); }
                    else if (openingDirection == 2) { roomToSpawn = GetPrefab(templates.topRooms); }
                    else if (openingDirection == 3) { roomToSpawn = GetPrefab(templates.rightRooms); }
                    else if (openingDirection == 4) { roomToSpawn = GetPrefab(templates.leftRooms); }
                }

                if (roomToSpawn != null)
                {
                    Instantiate(roomToSpawn, transform.position, roomToSpawn.transform.rotation);
                }
            }
            else
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
            }
            spawned = true;
        }
    }


    GameObject GetPrefab(GameObject[] roomArray)
    {
        if (roomArray.Length == 0) return null;

        int rand = Random.Range(0, roomArray.Length);
        GameObject selectedRoom = roomArray[rand];

        if (selectedRoom.name.Contains("TreasureRoom"))
        {
            if (templates.treasureRoomSpawned)
            {
                return roomArray[0];
            }
            else
            {
                templates.treasureRoomSpawned = true;
                return selectedRoom;
            }
        }

        return selectedRoom;
    }

    bool IsPositionOccupied(Vector2 targetPos)
    {
        foreach (Vector2 pos in templates.occupiedPositions)
        {
            if (Vector2.Distance(pos, targetPos) < 1f)
            {
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoomSpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false)
            {
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
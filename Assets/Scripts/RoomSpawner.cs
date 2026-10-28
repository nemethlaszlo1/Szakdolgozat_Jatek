using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("GameController").GetComponent<RoomTemplates>();
        Invoke("Spawn", Random.Range(0.1f, 0.2f));
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (templates.rooms.Count < templates.maxRooms)
            {
                if (openingDirection == 1) { rand = Random.Range(0, templates.bottomRooms.Length); Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation); }
                else if (openingDirection == 2) { rand = Random.Range(0, templates.topRooms.Length); Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation); }
                else if (openingDirection == 3) { rand = Random.Range(0, templates.rightRooms.Length); Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation); }
                else if (openingDirection == 4) { rand = Random.Range(0, templates.leftRooms.Length); Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation); }
            }
            else
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoomSpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
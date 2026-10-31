using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    //szobak
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    //folyosok
    public GameObject[] bottomCorridors;
    public GameObject[] topCorridors;
    public GameObject[] leftCorridors;
    public GameObject[] rightCorridors;

    public GameObject closedRoom;
    public GameObject bossRoom;

    public int maxRooms = 30;

    [HideInInspector]
    public List<GameObject> rooms;

    public List<Vector2> occupiedPositions = new List<Vector2>();
    public bool treasureRoomSpawned = false;
}
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    //szobák
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    //zsákutca szobák
    public GameObject closedRoom;
    public GameObject bossRoom;

    public int maxRooms = 10;

    [HideInInspector]
    public List<GameObject> rooms;
}
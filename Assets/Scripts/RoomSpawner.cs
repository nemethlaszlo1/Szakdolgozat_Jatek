using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    public bool mustSpawnCorridor = false;

    [HideInInspector]
    public bool spawned = false;
}
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("GameController").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
}
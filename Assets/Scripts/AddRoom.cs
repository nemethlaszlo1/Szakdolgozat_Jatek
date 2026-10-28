using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("GameController").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);

        bool alreadyInList = false;
        foreach (Vector2 pos in templates.occupiedPositions)
        {
            if (Vector2.Distance(pos, myPos) < 1f)
            {
                alreadyInList = true;
                break;
            }
        }

        if (!alreadyInList)
        {
            templates.occupiedPositions.Add(myPos);
        }
    }
}
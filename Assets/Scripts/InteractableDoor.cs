using UnityEngine;

public class InteractableDoor : MonoBehaviour
{

    public Sprite closedDoorSprite; // zárt ajtó kép
    public Sprite openDoorSprite;   // nyitott

    public BoxCollider2D solidCollider; // fizikai fal

    private SpriteRenderer spriteRenderer;
    private bool isPlayerNear = false; 
    private bool isOpen = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (!isOpen && closedDoorSprite != null)
        {
            spriteRenderer.sprite = closedDoorSprite;
            solidCollider.enabled = true;
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        spriteRenderer.sprite = openDoorSprite;
        solidCollider.enabled = false;
        Debug.Log("Ajtó kinyitva!");
    }

    void CloseDoor()
    {
        isOpen = false;
        spriteRenderer.sprite = closedDoorSprite;
        solidCollider.enabled = true;
        Debug.Log("Ajtó bezárva!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
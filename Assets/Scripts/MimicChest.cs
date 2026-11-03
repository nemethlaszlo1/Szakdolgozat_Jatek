using UnityEngine;

public class MimicChest : MonoBehaviour
{
    [Header("A szörny adatai")]
    public GameObject mimicEnemyPrefab;
    public bool attackImmediately = false;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && !attackImmediately && Input.GetKeyDown(KeyCode.E))
        {
            RevealMimic();
        }
    }

    void RevealMimic()
    {
        Debug.Log("Mimic lespawnolva!");
        if (mimicEnemyPrefab != null)
        {
            Instantiate(mimicEnemyPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            if (attackImmediately)
            {
                RevealMimic();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerInRange = false;
    }
}
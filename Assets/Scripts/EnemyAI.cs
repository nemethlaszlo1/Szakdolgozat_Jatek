using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Beállítások")]
    public float speed = 2f;
    public float chaseDistance = 5f;
    public int damageAmount = 20;

    private Transform playerTransform;
    private GameManager gameManager;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance < chaseDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.TakeDamage(damageAmount);
            }
        }
    }
}
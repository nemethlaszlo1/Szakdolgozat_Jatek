using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Beállítások")]
    public float speed = 2f;
    public float chaseDistance = 5f;
    public int damageAmount = 20;
    public float damageInterval = 2f; //2mp (default)
    private float nextDamageTime = 0f;
    public LayerMask obstacleLayer;

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
                RaycastHit2D hit = Physics2D.Linecast(transform.position, playerTransform.position, obstacleLayer);
                if (hit.collider == null || hit.collider.CompareTag("Player"))
                {
                    transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameManager != null && Time.time >= nextDamageTime)
            {
                gameManager.TakeDamage(damageAmount);
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
}
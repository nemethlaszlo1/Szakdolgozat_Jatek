using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement.x > 0) //karakter forgás
        {
            transform.localScale = new Vector3(1, 1, 1); // jobb
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // bal
        }
    }

    void FixedUpdate()
    {
        float currentSpeed = speed;
        if (gm != null)
        {
            currentSpeed += gm.extraSpeed;
        }
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }
}
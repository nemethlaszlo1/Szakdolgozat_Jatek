using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageInterval = 2f; //2mp

    private GameManager gm;
    private float nextDamageTime = 0f;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= nextDamageTime && gm != null)
            {
                gm.TakeDamage(damageAmount);
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
}
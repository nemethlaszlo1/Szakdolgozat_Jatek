using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int xpDropAmount = 50;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Ellenfél sebződött! Hátralévő HP: " + currentHealth); //debug

        StartCoroutine(FlashRed());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.color = Color.white;
        }
    }

    void Die()
    {
        Debug.Log("Ellenfél meghalt.");

        //xp
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.AddXP(xpDropAmount); //szörny xp dobás
        }

        //loot
        LootSpawner spawner = GetComponent<LootSpawner>();
        if (spawner != null)
        {
            spawner.DropLoot();
        }

        Destroy(gameObject);
    }
}
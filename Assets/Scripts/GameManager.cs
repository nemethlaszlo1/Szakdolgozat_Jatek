using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public int coins = 0;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinText;

    public float respawnDelay = 2f;

    private Vector2 startPosition;
    private GameObject playerObject;
    private bool isDead = false;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            startPosition = playerObject.transform.position;
        }

        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && !isDead)
        {
            HalveHealthDebug();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health < 0) health = 0;

        UpdateUI();

        if (health <= 0)
        {
            StartCoroutine(PlayerDeathRoutine());
        }
    }

    IEnumerator PlayerDeathRoutine()
    {
        isDead = true;

        PlayerMovement movement = playerObject.GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        SpriteRenderer sprite = playerObject.GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        health = maxHealth;
        isDead = false;

        playerObject.transform.position = startPosition;

        if (sprite != null) sprite.enabled = true;
        if (movement != null) movement.enabled = true;

        UpdateUI();
    }

    public void Heal(int amount)
    {
        if (isDead) return;
        health += amount;
        if (health > maxHealth) health = maxHealth;
        UpdateUI();
    }

    public void AddCoin(int amount)
    {
        if (isDead) return;
        coins += amount;
        UpdateUI();
    }

    void HalveHealthDebug()
    {
        int damage = health - (health / 2);
        TakeDamage(damage);
    }

    public void UpdateUI()
    {
        if (healthText != null)
        {
            if (health <= 0)
            {
                healthText.text = "MEGHALTÁL!";
            }
            else
            {
                healthText.text = "Életerő: " + health + " / " + maxHealth;
            }
        }

        if (coinText != null)
        {
            coinText.text = "Érmék: " + coins;
        }
    }
}
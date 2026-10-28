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
    public TMPro.TextMeshProUGUI levelText;
    public TMPro.TextMeshProUGUI statPointText;

    public float respawnDelay = 2f;

    private Vector2 startPosition;
    private GameObject playerObject;
    private bool isDead = false;

    public GameObject statPanel;
    public int extraDamage = 0;
    public float extraSpeed = 0f;

    // statok, szint stb.
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public int statPoints = 0;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            startPosition = playerObject.transform.position;
        }

        UpdateUI();
        UpdateXPUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && !isDead) //debugra
        {
            HalveHealthDebug();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (statPanel != null)
            {
                statPanel.SetActive(!statPanel.activeSelf);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health < 0) health = 0;

        UpdateUI();

        if (playerObject != null)
        {
            PlayerCombat combatScript = playerObject.GetComponent<PlayerCombat>();
            if (combatScript != null)
            {
                StartCoroutine(combatScript.PlayerFlashRed());
            }
        }

        if (health <= 0)
        {
            StartCoroutine(PlayerDeathRoutine());
        }
    }

    // Halál és respawn rutin
    IEnumerator PlayerDeathRoutine()
    {
        isDead = true;

        PlayerMovement movement = playerObject.GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        SpriteRenderer sprite = playerObject.GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.enabled = false;

        PlayerCombat combat = playerObject.GetComponent<PlayerCombat>(); // harc és kard eltüntetés halálkor fix.
        if (combat != null)
        {
            combat.enabled = false;
            combat.weaponPivot.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(respawnDelay);

        health = maxHealth;
        isDead = false;

        playerObject.transform.position = startPosition;

        if (sprite != null) sprite.enabled = true;
        if (movement != null) movement.enabled = true;

        if (combat != null) //harc visszakapcs.
        {
            combat.enabled = true;
            combat.weaponPivot.gameObject.SetActive(true);
        }

        UpdateUI();
    }

    public void Heal(int amount)
    {
        if (isDead) return;
        health += amount;
        if (health > maxHealth) health = maxHealth;
        UpdateUI();
    }
    
    public void AddXP(int amount)
    {
        currentXP += amount;
        Debug.Log("Kaptál " + amount + " XP-t! Jelenlegi XPd: " + currentXP + "/" + xpToNextLevel);

        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateXPUI();
    }

    private void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;
        statPoints++;

        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f);

        Heal(maxHealth / 2);

        Debug.Log("SZINTLÉPÉS: " + currentLevel + ". szint! Stat pontok: " + statPoints);

        UpdateXPUI();
    }

    public void AddCoin(int amount)
    {
        if (isDead) return;
        coins += amount;
        UpdateUI();
    }

     //debug, hp felezo
    void HalveHealthDebug()
    {
        int damage = health - (health / 2);
        TakeDamage(damage);
    }

    // UI frissítése
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

    // Stat UI frissítése
    public void UpdateXPUI()
    {
        if (levelText != null)
        {
            levelText.text = "Szint: " + currentLevel + " (" + currentXP + "/" + xpToNextLevel + " XP)";
        }

        if (statPointText != null)
        {
            if (statPoints > 0)
            {
                statPointText.text = "Stat pontok: " + statPoints;
                statPointText.gameObject.SetActive(true);
            }
            else
            {
                statPointText.gameObject.SetActive(false);//nincs pont ne mutatja feleslegesen
            }
        }
    }

    // Stat fejlesztések
    public void UpgradeHealth()
    {
        if (statPoints > 0)
        {
            statPoints--;
            maxHealth += 20;
            Heal(20);
            UpdateXPUI();
            Debug.Log("Életerő fejlesztve! Új Max HP: " + maxHealth);
        }
    }

    public void UpgradeDamage()
    {
        if (statPoints > 0)
        {
            statPoints--;
            extraDamage += 5;
            UpdateXPUI();
            Debug.Log("Sebzés fejlesztve! Bónusz: +" + extraDamage);
        }
    }

    public void UpgradeSpeed()
    {
        if (statPoints > 0)
        {
            statPoints--;
            extraSpeed += 0.5f;
            UpdateXPUI();
            Debug.Log("Sebesség fejlesztve! Bónusz: +" + extraSpeed);
        }
    }
}
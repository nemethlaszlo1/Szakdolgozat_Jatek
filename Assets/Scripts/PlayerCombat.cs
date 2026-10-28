using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Beállítások")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayers;

    public Transform weaponPivot;
    public SpriteRenderer weaponSpriteRenderer;
    private SpriteRenderer playerSprite;
    private bool isAttacking = false;

    public Item currentWeapon;
    private float nextAttackTime = 0f;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();

        if (currentWeapon != null)
        {
            EquipWeapon(currentWeapon);
        }
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
            {
                StartCoroutine(SwingSword());
                Attack();

                float cooldown = 0.5f;
                if (currentWeapon != null)
                {
                    cooldown = currentWeapon.attackSpeed;
                }

                nextAttackTime = Time.time + cooldown;
            }
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        int totalDamage = attackDamage;
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            totalDamage += gm.extraDamage;
        }

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(totalDamage);
            }
        }
    }

    IEnumerator SwingSword()    // ütés anim.
    {
        isAttacking = true;
        weaponPivot.localRotation = Quaternion.Euler(0, 0, -45);
        yield return new WaitForSeconds(0.05f);

        weaponPivot.localRotation = Quaternion.Euler(0, 0, 45);
        yield return new WaitForSeconds(0.1f);

        weaponPivot.localRotation = Quaternion.Euler(0, 0, 0);
        isAttacking = false;
    }

    public IEnumerator PlayerFlashRed() // sérülés anim.
    {
        playerSprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        playerSprite.color = Color.white;
    }

    //fegyvercsere
    public void EquipWeapon(Item newWeapon)
    {
        if (newWeapon.isWeapon)
        {
            currentWeapon = newWeapon;
            attackDamage = newWeapon.attackDamage;
            attackRange = newWeapon.attackRange;

            // karakter kezében lévő kard -> új kard sprite
            if (weaponSpriteRenderer != null)
            {
                weaponSpriteRenderer.sprite = newWeapon.itemIcon;
            }

            Debug.Log("Új fegyver felszerelve: " + newWeapon.itemName + " (Sebzés: " + attackDamage + ")");
        }
    }

    void OnDrawGizmosSelected() // debug
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Beállítások")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayers;      // ellenfél rétege

    public Transform weaponPivot;
    private SpriteRenderer playerSprite;
    private bool isAttacking = false;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(SwingSword());
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }
    }

    IEnumerator SwingSword()    //ütés anim.
    {
        isAttacking = true;
        weaponPivot.localRotation = Quaternion.Euler(0, 0, -45);
        yield return new WaitForSeconds(0.05f);

        weaponPivot.localRotation = Quaternion.Euler(0, 0, 45);
        yield return new WaitForSeconds(0.1f);

        weaponPivot.localRotation = Quaternion.Euler(0, 0, 0);
        isAttacking = false;
    }

    public IEnumerator PlayerFlashRed() //sérülés anim.
    {
        playerSprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        playerSprite.color = Color.white;
    }

    void OnDrawGizmosSelected() //debug
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
using UnityEngine;
using System.Collections;

public class WeaponAI : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage = 25;
    public float attackCooldown = 1.5f;
    public LayerMask playerLayer;

    public Transform weaponPivot;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= attackRange && Time.time >= nextAttackTime)
        {
            if (!isAttacking)
            {
                StartCoroutine(SwingWeapon());
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            GameManager gm = Object.FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.TakeDamage(attackDamage);
            }
        }
    }

    IEnumerator SwingWeapon()
    {
        isAttacking = true;

        weaponPivot.localRotation = Quaternion.Euler(0, 0, -45);
        yield return new WaitForSeconds(0.1f);

        weaponPivot.localRotation = Quaternion.Euler(0, 0, 45);
        yield return new WaitForSeconds(0.15f);

        weaponPivot.localRotation = Quaternion.Euler(0, 0, 0);
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    public int healAmount = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gm = FindObjectOfType<GameManager>();

            if (gm != null)
            {
                if (gm.health < gm.maxHealth)
                {
                    gm.Heal(healAmount);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("A játékos élete max (nem veszi fel).");
                }
            }
        }
    }
}
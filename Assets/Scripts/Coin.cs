using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gm = FindObjectOfType<GameManager>();

            if (gm != null)
            {
                gm.AddCoin(coinValue);
            }

            Destroy(gameObject);
        }
    }
}
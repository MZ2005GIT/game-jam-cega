using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        // Check if the object hitting the coin is the Player
        if (col.CompareTag("Player"))
        {
            // 1. Play Sound (if you have your AudioManager ready)
            if (AudioManager.Instance != null) AudioManager.Instance.PlayCoinSound();

            // 2. Add Score via the Singleton
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(value);
            }

            // 3. Disappear immediately
            Destroy(gameObject);
        }
    }
}
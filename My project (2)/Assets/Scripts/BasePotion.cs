using UnityEngine;

public abstract class BasePotion : MonoBehaviour
{
    // Every potion will use this to identify the player
    protected abstract void ApplyEffect(GameObject player);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                ApplyEffect(other.gameObject);
                player.FlashPickupPanel();
                AudioManager.Instance.PlayPotionSound();
                Destroy(gameObject); // All potions disappear on use
            }
        }
    }
}
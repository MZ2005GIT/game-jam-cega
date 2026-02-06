using UnityEngine;

public class SpeedPotion : BasePotion
{
    [Header("Speed Settings")]
    [SerializeField] private float speedMultiplier = 1.5f; // 50% faster
    [SerializeField] private float boostDuration = 5f;

    protected override void ApplyEffect(GameObject player)
    {
        // Find the PlayerMovement component
        PlayerMovement movement = player.GetComponent<PlayerMovement>();

        if (movement != null)
        {
            // Call the Coroutine method we created earlier
            movement.ApplySpeedBoost(speedMultiplier, boostDuration);
            Debug.Log("Speed Boost Activated!");
        }
    }
}
using UnityEngine;

public class HealthPotion : BasePotion
{
    [SerializeField] private float healAmount = 20f;

    protected override void ApplyEffect(GameObject player)
    {
        // Get the PlayerHealth component we know exists
        PlayerHealth health = player.GetComponent<PlayerHealth>();

        if (health != null)
        {
            // You might need to add a 'Heal' or 'RestoreHealth' 
            // method to your PlayerHealth script
            health.Heal(healAmount); 
            Debug.Log("Healed for " + healAmount);
        }
    }
}
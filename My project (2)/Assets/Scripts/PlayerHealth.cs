using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        AudioManager.Instance.PlayLoseHealthSound();
        
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0f)
        {
            GameManager.Instance.PlayerLost();
            AudioManager.Instance.PlayLostSound();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > 100f) 
        {
            currentHealth = 100f;
        }
                
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        healthBar.SetHealth(currentHealth);
    }
}

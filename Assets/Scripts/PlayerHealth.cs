using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + (int)amount, maxHealth);
        Debug.Log($"[PlayerHealth] Healed {amount}. Current: {currentHealth}/{maxHealth}");
    }

    void Die()
    {
        Debug.Log("Player died");
        PlayerDeathHandler.HandleDeath(gameObject);
    }
}
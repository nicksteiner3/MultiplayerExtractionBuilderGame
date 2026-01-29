using UnityEngine;

public class Bot : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f; // Shoots every 1 second
    public Vector3 shootDirection = Vector3.forward;

    [Header("Health")]
    public int maxHealth = 50;
    private int currentHealth;

    private float nextFireTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;

        // If no fire point assigned, use bot's position
        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    void Update()
    {
        // Fire projectile on timer
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Fire()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("[Bot] No projectile prefab assigned!");
            return;
        }

        // Spawn projectile ahead of the bot to avoid immediate collision
        Vector3 spawnOffset = transform.TransformDirection(shootDirection.normalized) * 1f;
        Vector3 spawnPos = firePoint.position + spawnOffset;

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Projectile proj = projectile.GetComponent<Projectile>();
        
        if (proj != null)
        {
            // Use the bot's forward direction (or specified shoot direction)
            Vector3 direction = transform.TransformDirection(shootDirection.normalized);
            proj.SetDirection(direction);
            proj.SetOriginCollider(GetComponent<Collider>());
        }

        Debug.Log($"[Bot] Fired projectile from {spawnPos}");
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"[Bot] Took {amount} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("[Bot] Died");
        
        // Notify challenge system
        if (ChallengeManager.Instance != null)
        {
            ChallengeManager.Instance.OnBotKilled();
        }

        Destroy(gameObject);
    }
}

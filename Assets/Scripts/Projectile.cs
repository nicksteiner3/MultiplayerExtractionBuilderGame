using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 5f;

    private Vector3 direction;
    private Collider projectileCollider;
    private Collider originCollider;

    void Start()
    {
        projectileCollider = GetComponent<Collider>();
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    public void SetOriginCollider(Collider collider)
    {
        originCollider = collider;
        if (projectileCollider != null && originCollider != null)
        {
            Physics.IgnoreCollision(projectileCollider, originCollider);
            Debug.Log("[Projectile] Ignoring collision with origin");
        }
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Projectile] OnTriggerEnter hit: {other.gameObject.name}");

        // Check if hit player
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log($"[Projectile] Hit player for {damage} damage");
            Destroy(gameObject);
            return;
        }

        // Check if hit bot
        Bot bot = other.GetComponent<Bot>();
        if (bot != null)
        {
            bot.TakeDamage(damage);
            Debug.Log($"[Projectile] Hit bot for {damage} damage");
            Destroy(gameObject);
            return;
        }

        // Destroy on any collision except the origin source
        if (other != originCollider)
        {
            Debug.Log($"[Projectile] Destroying on collision with {other.gameObject.name}");
            Destroy(gameObject);
        }
    }
}

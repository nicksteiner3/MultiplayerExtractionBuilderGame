using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [Header("Setup")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    private PlayerWeapons playerWeapons;
    private Camera playerCamera;
    private float lastFireTime = 0f;

    void Start()
    {
        playerWeapons = GetComponent<PlayerWeapons>();
        playerCamera = Camera.main;

        if (firePoint == null)
        {
            // Create a virtual fire point 0.5 units above the player
            GameObject firePointObj = new GameObject("FirePoint");
            firePointObj.transform.SetParent(transform);
            firePointObj.transform.localPosition = new Vector3(0, 0.5f, 0);
            firePoint = firePointObj.transform;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryFire();
        }
    }

    void TryFire()
    {
        if (playerWeapons == null || playerWeapons.GetEquippedWeapons().Count == 0)
        {
            Debug.LogWarning("[WeaponSystem] No weapon equipped!");
            return;
        }

        // Fire the first equipped weapon
        WeaponData weapon = playerWeapons.GetEquippedWeapons()[0];
        
        // Check fire rate cooldown
        if (Time.time < lastFireTime + weapon.fireRate)
            return;

        Fire(weapon);
        lastFireTime = Time.time;
    }

    void Fire(WeaponData weapon)
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("[WeaponSystem] No projectile prefab assigned!");
            return;
        }

        // Fire in camera forward direction (where player is looking)
        Vector3 direction = playerCamera != null ? playerCamera.transform.forward : transform.forward;
        direction = direction.normalized;

        // Spawn projectile ahead of the fire point to avoid collision with player
        Vector3 spawnPos = firePoint.position + direction * 1f;
        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Projectile proj = projectile.GetComponent<Projectile>();
        
        if (proj != null)
        {
            // Set damage from weapon data
            proj.damage = (int)weapon.damage;

            proj.SetDirection(direction);
            proj.SetOriginCollider(GetComponent<Collider>());
        }

        Debug.Log($"[WeaponSystem] Fired {weapon.weaponName}");
    }
}

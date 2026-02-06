using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Weapon")]
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private Image weaponIcon;

    private PlayerHealth playerHealth;
    private PlayerWeapons playerWeapons;

    private void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        var temp = FindObjectsByType<PlayerHealth>(FindObjectsSortMode.None);

        if (playerHealth == null)
            Debug.LogError("[HUD] PlayerHealth not found in scene!");
        if (playerWeapons == null)
            Debug.LogError("[HUD] PlayerWeapons not found in scene!");
    }

    private void Update()
    {
        if (playerHealth != null)
            UpdateHealthDisplay();

        if (playerWeapons != null) 
            UpdateWeaponDisplay();
    }

    private void UpdateHealthDisplay()
    {
        int current = playerHealth.currentHealth;
        int max = playerHealth.maxHealth;

        // Update health bar fill
        if (healthBar != null)
            healthBar.fillAmount = (float)current / max;

        // Update health text
        if (healthText != null)
            healthText.text = $"{current}/{max}";
    }

    private void UpdateWeaponDisplay()
    {
        var equippedWeapons = playerWeapons.GetEquippedWeapons();

        if (equippedWeapons.Count == 0)
        {
            if (weaponText != null)
                weaponText.text = "No Weapon";
            if (weaponIcon != null)
                weaponIcon.sprite = null;
            return;
        }

        // Display first equipped weapon
        WeaponData weapon = equippedWeapons[0];

        if (weaponText != null)
            weaponText.text = weapon.weaponName;

        //if (weaponIcon != null)
            //weaponIcon.sprite = weapon.icon;
    }
}

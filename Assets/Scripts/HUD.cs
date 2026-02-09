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

    [Header("Crosshair")]
    [SerializeField] private bool showCrosshair = true;
    [SerializeField] private Image crosshairImage;
    [SerializeField] private Vector2 crosshairSize = new Vector2(4f, 4f);
    [SerializeField] private Color crosshairColor = new Color(1f, 1f, 1f, 0.5f);

    [Header("Interact Prompt")]
    [SerializeField] private TextMeshProUGUI interactPromptText;

    private PlayerHealth playerHealth;
    private PlayerWeapons playerWeapons;
    private PlayerInteraction playerInteraction;

    private void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerWeapons = FindFirstObjectByType<PlayerWeapons>();
        playerInteraction = FindFirstObjectByType<PlayerInteraction>();

        if (showCrosshair)
            EnsureCrosshair();

        if (interactPromptText != null)
            interactPromptText.gameObject.SetActive(false);

        if (playerHealth == null)
            Debug.LogError("[HUD] PlayerHealth not found in scene!");
        if (playerWeapons == null)
            Debug.LogError("[HUD] PlayerWeapons not found in scene!");
        if (playerInteraction == null)
            Debug.LogError("[HUD] PlayerInteraction not found in scene!");
    }

    private void Update()
    {
        if (playerHealth != null)
            UpdateHealthDisplay();

        if (playerWeapons != null) 
            UpdateWeaponDisplay();

        if (playerInteraction != null)
            UpdateInteractPrompt();
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

    private void EnsureCrosshair()
    {
        if (crosshairImage != null)
        {
            ApplyCrosshairStyle(crosshairImage);
            return;
        }

        var canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            canvas = FindFirstObjectByType<Canvas>();

        if (canvas == null)
        {
            Debug.LogWarning("[HUD] No Canvas found for crosshair.");
            return;
        }

        var go = new GameObject("CrosshairDot", typeof(RectTransform), typeof(Image));
        go.transform.SetParent(canvas.transform, false);
        go.transform.SetAsFirstSibling(); // Render behind other UI elements

        crosshairImage = go.GetComponent<Image>();
        ApplyCrosshairStyle(crosshairImage);
    }

    private void ApplyCrosshairStyle(Image img)
    {
        if (img == null) return;

        var rt = img.rectTransform;
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
        rt.sizeDelta = crosshairSize;

        img.raycastTarget = false;
        img.color = crosshairColor;

        if (img.sprite == null)
        {
            var sprite = Sprite.Create(Texture2D.whiteTexture,
                new Rect(0, 0, Texture2D.whiteTexture.width, Texture2D.whiteTexture.height),
                new Vector2(0.5f, 0.5f));
            img.sprite = sprite;
        }
    }

    private void UpdateInteractPrompt()
    {
        if (interactPromptText == null) return;

        var interactable = playerInteraction.GetCurrentInteractable();
        if (interactable != null)
        {
            string objectName = "Object";
            if (interactable is MonoBehaviour mb)
                objectName = mb.gameObject.name;
            
            interactPromptText.text = $"[E] {objectName}";
            interactPromptText.gameObject.SetActive(true);
        }
        else
        {
            interactPromptText.gameObject.SetActive(false);
        }
    }
}

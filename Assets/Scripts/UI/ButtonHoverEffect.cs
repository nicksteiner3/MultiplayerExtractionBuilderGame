using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI buttonText;

    [Header("Normal State Sprites")]
    [SerializeField] private Sprite normalSprite; // Your orange outline sprite
    [SerializeField] private Color normalTextColor = new Color(1f, 0.55f, 0.2f, 1f); // Orange text

    [Header("Hover State Sprites")]
    [SerializeField] private Sprite hoverSprite; // Your filled orange sprite (or null to keep same)
    [SerializeField] private Color hoverTextColor = Color.white;
    private Color hoverBackgroundColor;

    private void Start()
    {
        // Auto-find components if not assigned
        if (buttonImage == null)
            buttonImage = GetComponent<Image>();

        if (buttonText == null)
            buttonText = GetComponentInChildren<TextMeshProUGUI>();

        // Store the current sprite as normal if not set
        if (normalSprite == null && buttonImage != null)
            normalSprite = buttonImage.sprite;

        hoverBackgroundColor = buttonImage.color;

        // Set initial state
        ApplyNormalState();
    }

    private void OnEnable()
    {
        ApplyNormalState();
    }

    private void OnDisable()
    {
        ApplyNormalState();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ApplyHoverState();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ApplyNormalState();
    }

    private void ApplyHoverState()
    {
        // Change sprite if hover sprite is assigned
        if (buttonImage != null)
        {
            if (hoverSprite != null)
                buttonImage.sprite = hoverSprite;
            
            buttonImage.color = hoverBackgroundColor;
        }

        // Change text to white
        if (buttonText != null)
            buttonText.color = hoverTextColor;
    }

    private void ApplyNormalState()
    {
        // Restore normal sprite and color
        if (buttonImage != null)
        {
            if (normalSprite != null)
                buttonImage.sprite = normalSprite;
            
            buttonImage.color = Color.white; // Reset to white to show sprite as-is
        }

        // Change text to orange
        if (buttonText != null)
            buttonText.color = normalTextColor;
    }
}

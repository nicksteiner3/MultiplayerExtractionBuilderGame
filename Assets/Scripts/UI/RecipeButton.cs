using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class RecipeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text label;

    private RecipeData recipe;
    private ManufacturingUI parentUI;
    private GameObject currentTooltip;

    public void Init(RecipeData recipe, ManufacturingUI parent)
    {
        this.recipe = recipe;
        parentUI = parent;

        if (label != null && recipe != null)
            label.text = recipe.recipeName;

        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => 
            {
                HideTooltip();
                parentUI.SelectRecipe(recipe);
            });
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (recipe == null) return;

        // Show tooltip with recipe details
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }

    private void ShowTooltip()
    {
        if (currentTooltip != null) return;

        // Create tooltip background panel
        var tooltipGo = new GameObject("RecipeTooltip");
        tooltipGo.transform.SetParent(transform.root, false); // Parent to root canvas

        var rt = tooltipGo.AddComponent<RectTransform>();
        
        // Position to the right of this button
        var buttonRt = GetComponent<RectTransform>();
        var buttonWorldPos = buttonRt.position;
        var buttonWidth = buttonRt.rect.width;
        rt.position = buttonWorldPos + new Vector3(buttonWidth + 10, 0, 0);
        rt.sizeDelta = new Vector2(150f, 100f);

        var img = tooltipGo.AddComponent<Image>();
        img.color = new Color(0, 0, 0, 0.8f);
        img.raycastTarget = false; // Let pointer exit fire on the button

        // Create child object for text
        var textGo = new GameObject("Text");
        textGo.transform.SetParent(tooltipGo.transform, false);

        var textRt = textGo.AddComponent<RectTransform>();
        textRt.anchorMin = Vector2.zero;
        textRt.anchorMax = Vector2.one;
        textRt.offsetMin = new Vector2(10, 10);
        textRt.offsetMax = new Vector2(-10, -10);

        var txt = textGo.AddComponent<TextMeshProUGUI>();

        string costText;
        if (recipe.materialInputs != null && recipe.materialInputs.Count > 0)
        {
            // List all material inputs
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < recipe.materialInputs.Count; i++)
            {
                var mi = recipe.materialInputs[i];
                if (mi?.material != null)
                {
                    sb.Append($"{mi.material.name}: {mi.amount}");
                    if (i < recipe.materialInputs.Count - 1) sb.Append("\n");
                }
            }
            costText = sb.ToString();
        }
        else
        {
            costText = "No cost defined";
        }

        txt.text = $"<b>{recipe.recipeName}</b>\n" +
                   costText + "\n" +
                   $"Time: {recipe.craftTime}s";
        txt.alignment = TextAlignmentOptions.TopLeft;
        txt.fontSize = 14;
        txt.raycastTarget = false; // Avoid blocking hover exit

        currentTooltip = tooltipGo;
    }

    private void HideTooltip()
    {
        if (currentTooltip != null)
        {
            Destroy(currentTooltip);
            currentTooltip = null;
        }
    }
}

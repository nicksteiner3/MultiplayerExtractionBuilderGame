using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    private RecipeData recipe;
    private AbilityManufacturingUI parentUI;

    public void Init(RecipeData recipe, AbilityManufacturingUI parent)
    {
        this.recipe = recipe;
        parentUI = parent;

        if (label != null && recipe != null)
            label.text = recipe.recipeName;

        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => parentUI.CraftAbility(recipe));
        }
    }
}

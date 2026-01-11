using UnityEngine;
using System.Linq;

public class AbilityManufacturingUI : MonoBehaviour
{
    [SerializeField] private GameObject abilityManufacturingUi;

    [Header("Runtime Recipe List")]
    [SerializeField] private Transform recipesContainer; // parent to instantiate buttons under
    [SerializeField] private GameObject recipeButtonPrefab; // prefab with Button + RecipeButton + TMP label
    [SerializeField] private string resourcesPath = "Recipes"; // folder under Resources to load RecipeData from

    private FabricatorMachine currentMachine;

    private void OnEnable()
    {
        // Populate when UI opens
        PopulateRecipeList();
    }

    private void Update()
    {
        if (abilityManufacturingUi.gameObject.activeSelf)
            HandleWindowClosure();
    }

    public void SetMachine(FabricatorMachine machine)
    {
        currentMachine = machine;
    }

    public void CraftAbility(RecipeData recipe)
    {
        if (currentMachine == null)
        {
            Debug.LogError("No FabricatorMachine found");
            return;
        }

        currentMachine.StartRecipe(recipe);
    }

    private void HandleWindowClosure()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseWindow();
        }
    }

    public void CloseWindow()
    {
        abilityManufacturingUi.SetActive(false);

        var controller = FindFirstObjectByType<FPSController>();
        if (controller)
            controller.UnfreezePlayer();
    }

    private void PopulateRecipeList()
    {
        if (recipesContainer == null || recipeButtonPrefab == null)
            return;

        // Clear existing
        for (int i = recipesContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(recipesContainer.GetChild(i).gameObject);
        }

        // Load all RecipeData assets from Resources/<resourcesPath>
        var recipes = Resources.LoadAll<RecipeData>(resourcesPath);
        if (recipes == null || recipes.Length == 0)
            return;

        // Order by name for deterministic listing
        int iterator = 0;
        foreach (var recipe in recipes.OrderBy(r => r.recipeName))
        {
            var go = Instantiate(recipeButtonPrefab, recipesContainer);
            var btn = go.GetComponent<RecipeButton>();
            if (btn != null)
            {
                btn.Init(recipe, this);
            }

            var rt = go.GetComponent<RectTransform>();
            if (rt != null)
            {
                var anchored = rt.anchoredPosition;
                anchored.y = 150f - 35f * iterator; // first = 150, second = 115, third = 80, etc.
                rt.anchoredPosition = anchored;
            }

            iterator++;
        }
    }
}

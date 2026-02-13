using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class ManufacturingUI : MonoBehaviour
{
    [SerializeField] private GameObject abilityManufacturingUi;

    [Header("Recipe Selection View")]
    [SerializeField] private GameObject recipeSelectionPanel; // Recipe Selection Panel to toggle
    [SerializeField] private Transform recipesContainer; // parent to instantiate buttons under
    [SerializeField] private GameObject recipeButtonPrefab; // prefab with Button + RecipeButton + TMP label
    [SerializeField] private string resourcesPath = "Recipes"; // folder under Resources to load RecipeData from

    [Header("Crafting View")]
    [SerializeField] private GameObject craftingPanel; // Panel showing current recipe being crafted
    [SerializeField] private TMP_Text currentRecipeLabel;
    [SerializeField] private TMP_Text recipeDetailsLabel; // Shows craft time, salvage cost, etc.
    [SerializeField] private Image progressBar; // Visual progress of current craft
    [SerializeField] private TMP_Text progressTimeLabel; // Shows "5.2s / 20s"
    [SerializeField] private Button cancelButton;

    [Header("Machine Info")]
    [SerializeField] private TMP_Text machineStatusLabel; // Shows "Fabricator - 10 Power"

    private FabricatorMachine currentMachine;
    private bool isInCraftingView = false;

    private void OnEnable()
    {
        // Only populate recipes on first enable (when starting recipe selection view)
        if (!isInCraftingView)
        {
            PopulateRecipeList();
        }
    }

    private void Update()
    {
        if (abilityManufacturingUi.gameObject.activeSelf)
        {
            HandleWindowClosure();

            // Update progress display if crafting
            if (isInCraftingView && currentMachine != null && currentMachine.IsCrafting())
            {
                UpdateCraftingDisplay();
            }
        }
    }

    public void SetMachine(FabricatorMachine machine)
    {
        currentMachine = machine;

        if (machineStatusLabel != null && machine != null)
            machineStatusLabel.text = $"Fabricator - {machine.PowerConsumption} Power";
    }

    public void SelectRecipe(RecipeData recipe)
    {
        if (currentMachine == null)
        {
            Debug.LogError("No FabricatorMachine found");
            return;
        }

        // Check if we have sufficient power before switching UI
        if (!currentMachine.HasSufficientPower())
        {
            Debug.LogWarning("Insufficient power to start crafting");
            return;
        }

        // Check if we have sufficient inputs (let FabricatorMachine validate)
        if (!currentMachine.HasInputsForRecipe(recipe))
        {
            Debug.LogWarning("Insufficient materials for recipe");
            return;
        }

        // All checks passed, now switch UI and start crafting
        isInCraftingView = true;
        recipeSelectionPanel.SetActive(false);
        craftingPanel.SetActive(true);

        // Start crafting
        currentMachine.StartRecipe(recipe);

        // Update display
        currentRecipeLabel.text = recipe.recipeName;
        
        // Build cost string from materialInputs
        string costText = "No cost";
        if (recipe.materialInputs != null && recipe.materialInputs.Count > 0)
        {
            var costs = new System.Collections.Generic.List<string>();
            foreach (var input in recipe.materialInputs)
            {
                if (input.material != null)
                    costs.Add($"{input.material.name}: {input.amount}");
            }
            costText = string.Join(", ", costs);
        }
        
        recipeDetailsLabel.text = $"{costText} | Time: {recipe.craftTime}s";

        if (cancelButton != null)
        {
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(() => CancelCrafting());
        }
    }

    private void UpdateCraftingDisplay()
    {
        var recipe = currentMachine.GetCurrentRecipe();
        if (recipe == null) return;

        float progress = currentMachine.GetProgress();
        float craftTime = recipe.craftTime;

        // Update progress bar
        if (progressBar != null)
            progressBar.fillAmount = progress / craftTime;

        // Update time label
        if (progressTimeLabel != null)
            progressTimeLabel.text = $"{progress:F1}s / {craftTime}s";
    }

    public void CancelCrafting()
    {
        if (currentMachine != null)
        {
            currentMachine.CancelRecipe();
        }

        // Return to recipe list view
        isInCraftingView = false;
        craftingPanel.SetActive(false);
        recipeSelectionPanel.SetActive(true);
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
                anchored.y = -55f * iterator;
                rt.anchoredPosition = anchored;
            }

            iterator++;
        }
    }
}


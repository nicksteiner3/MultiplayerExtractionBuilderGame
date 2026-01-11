using System;
using UnityEngine;

public class FabricatorMachine : MonoBehaviour
{
    private RecipeData currentRecipe;
    private float progress;
    private PlayerAbilities playerAbilities;

    private void Awake()
    {
        playerAbilities = FindFirstObjectByType<PlayerAbilities>();
    }

    public void StartRecipe(RecipeData recipe)
    {
        if (currentRecipe != null)
        {
            Debug.Log("Machine already running");
            return;
        }

        if (!HasInputs(recipe))
        {
            return;
        }

        ConsumeInputs(recipe);

        currentRecipe = recipe;
        progress = 0f;
    }

    private void Update()
    {
        if (currentRecipe == null) return;

        progress += Time.deltaTime;

        if (progress >= currentRecipe.craftTime)
        {
            CompleteRecipe();
        }
    }

    private void CompleteRecipe()
    {
        if (currentRecipe == null) return;

        var terminal = FindFirstObjectByType<EquipmentUIManager>();
        if (terminal == null)
        {
            Debug.LogError("No Equipment Terminal found to place completed ability.");
            return;
        }

        // Get first free stash slot
        var stashSlot = terminal.GetFirstStashSlot();
        if (stashSlot == null)
        {
            Debug.LogWarning("No free stash slot available. Ability lost!");
            return;
        }

        // Spawn the UI item
        var item = Instantiate(currentRecipe.prefabUIItem); // prefab for this ability
        item.ability = currentRecipe.outputs.ability;
        item.IsEquipped = false;

        stashSlot.PlaceItem(item);

        currentRecipe = null;
    }

    private bool HasInputs(RecipeData recipe)
    {
        if (recipe.inputs.Count == 0)
        {
            Debug.LogException(new Exception($"No inputs for recipe {recipe.name}"));
            return false;
        }

        return SessionState.Instance.stashSalvage >= recipe.inputs[0].amount;
    }

    private void ConsumeInputs(RecipeData recipe)
    {
        SessionState.Instance.stashSalvage -= recipe.inputs[0].amount;
    }
}

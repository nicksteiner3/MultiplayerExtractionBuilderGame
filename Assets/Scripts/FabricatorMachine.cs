using System;
using UnityEngine;

public class FabricatorMachine : MonoBehaviour, IPowered
{
    private RecipeData currentRecipe;
    private float progress;
    private PlayerAbilities playerAbilities;
    
    [Header("Power Settings")]
    public float powerConsumption = 10f; // Power required to run the machine
    
    private bool isPaused = false;
    private bool shouldRepeat = false; // Continues crafting same recipe until cancelled or stash full

    public float PowerConsumption => powerConsumption;

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
            Debug.Log("Insufficient inputs for recipe");
            return;
        }

        // Check if power is available
        if (!HasSufficientPower())
        {
            Debug.Log("Insufficient power to start crafting");
            return;
        }

        // Request power from PowerManager
        if (!PowerManager.Instance.RequestPower(this, powerConsumption))
        {
            Debug.Log("Failed to reserve power for crafting");
            return;
        }

        ConsumeInputs(recipe);

        currentRecipe = recipe;
        progress = 0f;
        isPaused = false;
        shouldRepeat = true;
        Debug.Log($"Fabricator: Started crafting {recipe.recipeName}, consuming {powerConsumption} power");
    }

    private void Update()
    {
        if (currentRecipe == null) return;

        // If power is lost, pause
        if (!HasSufficientPower())
        {
            if (!isPaused)
            {
                isPaused = true;
                OnPowerLost();
            }
            return;
        }
        else if (isPaused)
        {
            isPaused = false;
            OnPowerRestored();
        }

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
            Debug.LogWarning("No free stash slot available. Stopping crafting.");
            // Stash is full, stop crafting
            shouldRepeat = false;
            CancelRecipe();
            return;
        }

        // Spawn the UI item
        var item = Instantiate(currentRecipe.prefabUIItem); // prefab for this ability
        item.ability = currentRecipe.outputs.ability;
        item.IsEquipped = false;

        stashSlot.PlaceItem(item);

        Debug.Log($"Fabricator: Completed crafting {currentRecipe.recipeName}");

        // If we should repeat, start the same recipe again
        if (shouldRepeat && HasInputs(currentRecipe) && HasSufficientPower())
        {
            RecipeData recipeToRepeat = currentRecipe;
            progress = 0f;
            ConsumeInputs(recipeToRepeat);
            Debug.Log($"Fabricator: Auto-queuing next {recipeToRepeat.recipeName}");
            // Don't need to release/request power, we already have it reserved
        }
        else
        {
            // Otherwise, stop crafting
            PowerManager.Instance.ReleasePower(this);
            Debug.Log($"Fabricator: Crafting stopped, released {powerConsumption} power");
            currentRecipe = null;
            shouldRepeat = false;
            isPaused = false;
            progress = 0f;
        }
    }

    public void CancelRecipe()
    {
        if (currentRecipe == null) return;

        shouldRepeat = false;
        PowerManager.Instance.ReleasePower(this);
        Debug.Log($"Fabricator: Crafting cancelled, released {powerConsumption} power");
        currentRecipe = null;
        progress = 0f;
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

    public bool HasSufficientPower()
    {
        return PowerManager.Instance.GetAvailablePower() >= powerConsumption;
    }

    public void OnPowerLost()
    {
        if (currentRecipe != null)
        {
            PowerManager.Instance.ReleasePower(this);
            Debug.Log("Fabricator: Power lost, pausing crafting and releasing power");
        }
    }

    public void OnPowerRestored()
    {
        if (currentRecipe != null)
        {
            PowerManager.Instance.RequestPower(this, powerConsumption);
            Debug.Log("Fabricator: Power restored, resuming crafting and re-requesting power");
        }
    }

    public RecipeData GetCurrentRecipe()
    {
        return currentRecipe;
    }

    public float GetProgress()
    {
        return progress;
    }

    public bool IsCrafting()
    {
        return currentRecipe != null && !isPaused;
    }
}


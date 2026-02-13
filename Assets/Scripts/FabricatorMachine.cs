using System;
using UnityEngine;

public class FabricatorMachine : MonoBehaviour, IPowered
{
    public static event Action<RecipeData> OnAbilityCrafted;
    public static event Action<RecipeData> OnWeaponCrafted;
    
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
            Debug.LogError("No Equipment Terminal found to place completed item.");
            return;
        }

        bool hasAbilityOutput = currentRecipe.outputs != null && currentRecipe.outputs.ability != null;
        bool hasWeaponOutput = currentRecipe.outputs != null && currentRecipe.outputs.weapon != null;

        if (hasAbilityOutput)
        {
            var stashSlot = terminal.GetFirstStashSlot();
            if (stashSlot == null)
            {
                Debug.LogWarning("No free ability stash slot available. Stopping crafting.");
                shouldRepeat = false;
                CancelRecipe();
                return;
            }

            var item = Instantiate(currentRecipe.prefabUIItem);
            item.ability = currentRecipe.outputs.ability;
            item.IsEquipped = false;
            stashSlot.PlaceItem(item);
        }
        else if (hasWeaponOutput)
        {
            var stashSlot = terminal.GetFirstWeaponStashSlot();
            if (stashSlot == null)
            {
                Debug.LogWarning("No free weapon stash slot available. Stopping crafting.");
                shouldRepeat = false;
                CancelRecipe();
                return;
            }

            var item = Instantiate(currentRecipe.prefabWeaponUIItem);
            item.weapon = currentRecipe.outputs.weapon;
            item.IsEquipped = false;
            stashSlot.PlaceItem(item);
        }
        else
        {
            Debug.LogWarning("Recipe completed but has no output defined.");
        }

        Debug.Log($"Fabricator: Completed crafting {currentRecipe.recipeName}");

        // Fire craft events for challenge tracking
        if (hasAbilityOutput)
        {
            OnAbilityCrafted?.Invoke(currentRecipe);
        }
        else if (hasWeaponOutput)
        {
            OnWeaponCrafted?.Invoke(currentRecipe);
        }

        // Notify tutorial if Dash was crafted
        if (TutorialManager.Instance != null && currentRecipe.recipeName.Contains("Dash"))
        {
            TutorialManager.Instance.OnDashCrafted();
        }

        // Notify tutorial if Pistol was crafted
        if (TutorialManager.Instance != null && currentRecipe.recipeName.Contains("Pistol"))
        {
            TutorialManager.Instance.OnPistolCrafted();
        }

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
        if (recipe.materialInputs == null || recipe.materialInputs.Count == 0)
        {
            Debug.LogWarning($"Recipe {recipe.recipeName} has no material inputs defined.");
            return false;
        }

        foreach (var input in recipe.materialInputs)
        {
            if (input.material == null)
            {
                Debug.LogWarning($"Recipe {recipe.recipeName} has a material input slot with no material assigned.");
                return false;
            }

            // Check player inventory for materials
            if (!InventoryManager.Instance.HasMaterial(input.material, input.amount))
            {
                Debug.LogWarning($"Missing material for {recipe.recipeName}: need {input.amount}x {input.material.materialName}.");
                return false;
            }
        }
        return true;
    }

    public bool HasInputsForRecipe(RecipeData recipe)
    {
        return HasInputs(recipe);
    }

    private void ConsumeInputs(RecipeData recipe)
    {
        if (recipe.materialInputs == null || recipe.materialInputs.Count == 0)
            return;

        foreach (var input in recipe.materialInputs)
        {
            InventoryManager.Instance.RemoveFromPlayer(input.material, input.amount);
        }
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


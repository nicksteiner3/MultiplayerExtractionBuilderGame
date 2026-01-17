using System;
using UnityEngine;

public class FabricatorMachine : MonoBehaviour, IPowered
{
    private RecipeData currentRecipe;
    private float progress;
    private PlayerAbilities playerAbilities;
    
    [Header("Power Settings")]
    public float powerConsumption = 10f; // Power per second while crafting
    
    private bool isPaused = false;

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

        ConsumeInputs(recipe);

        currentRecipe = recipe;
        progress = 0f;
        isPaused = false;
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

    public bool HasSufficientPower()
    {
        return PowerManager.Instance.GetAvailablePower() >= powerConsumption;
    }

    public void OnPowerLost()
    {
        Debug.Log("Fabricator: Power lost, pausing crafting");
    }

    public void OnPowerRestored()
    {
        Debug.Log("Fabricator: Power restored, resuming crafting");
    }
}

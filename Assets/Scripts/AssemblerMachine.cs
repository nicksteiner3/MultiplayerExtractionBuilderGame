using System;
using UnityEngine;

public class AssemblerMachine : MonoBehaviour, IPowered
{
    private RecipeData currentRecipe;
    private float progress;
    
    [Header("Power Settings")]
    public float powerConsumption = 15f; // Slightly more expensive than Fabricator
    
    private bool isPaused = false;
    private bool shouldRepeat = false;

    public float PowerConsumption => powerConsumption;

    public void StartRecipe(RecipeData recipe)
    {
        if (currentRecipe != null)
        {
            Debug.Log("Assembler already running");
            return;
        }

        if (recipe.requiredMachine != RecipeData.MachineType.Assembler)
        {
            Debug.Log("This recipe is not for the Assembler");
            return;
        }

        // Check inputs
        if (!HasInputs(recipe))
        {
            Debug.Log("Insufficient inputs for recipe");
            return;
        }

        // Check power
        if (!HasSufficientPower())
        {
            Debug.Log("Insufficient power to start assembly");
            return;
        }

        // Request power
        if (!PowerManager.Instance.RequestPower(this, powerConsumption))
        {
            Debug.Log("Failed to reserve power for assembly");
            return;
        }

        ConsumeInputs(recipe);

        currentRecipe = recipe;
        progress = 0f;
        isPaused = false;
        shouldRepeat = true;
        Debug.Log($"Assembler: Started assembly of {recipe.recipeName}, consuming {powerConsumption} power");
    }

    private void Update()
    {
        if (currentRecipe == null) return;

        // Check power status
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

        // Output materials to stash
        if (currentRecipe.materialOutputs != null)
        {
            foreach (var output in currentRecipe.materialOutputs)
            {
                SessionState.Instance.AddMaterial(output.material, output.amount);
            }
        }

        Debug.Log($"Assembler: Completed assembly of {currentRecipe.recipeName}");

        // Check if we should repeat
        if (shouldRepeat && HasInputs(currentRecipe) && HasSufficientPower())
        {
            RecipeData recipeToRepeat = currentRecipe;
            progress = 0f;
            ConsumeInputs(recipeToRepeat);
            Debug.Log($"Assembler: Auto-queuing next {recipeToRepeat.recipeName}");
        }
        else
        {
            // Stop assembly
            PowerManager.Instance.ReleasePower(this);
            Debug.Log($"Assembler: Assembly stopped, released {powerConsumption} power");
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
        Debug.Log($"Assembler: Assembly cancelled, released {powerConsumption} power");
        currentRecipe = null;
        progress = 0f;
    }

    private bool HasInputs(RecipeData recipe)
    {
        if (recipe.materialInputs == null || recipe.materialInputs.Count == 0)
        {
            Debug.LogWarning($"Recipe {recipe.name} has no material inputs");
            return false;
        }

        foreach (var input in recipe.materialInputs)
        {
            if (!SessionState.Instance.HasMaterial(input.material, input.amount))
            {
                return false;
            }
        }

        return true;
    }

    private void ConsumeInputs(RecipeData recipe)
    {
        if (recipe.materialInputs == null) return;

        foreach (var input in recipe.materialInputs)
        {
            SessionState.Instance.RemoveMaterial(input.material, input.amount);
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
            Debug.Log("Assembler: Power lost, pausing assembly and releasing power");
        }
    }

    public void OnPowerRestored()
    {
        if (currentRecipe != null)
        {
            PowerManager.Instance.RequestPower(this, powerConsumption);
            Debug.Log("Assembler: Power restored, resuming assembly and re-requesting power");
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

    public bool IsAssembling()
    {
        return currentRecipe != null && !isPaused;
    }
}

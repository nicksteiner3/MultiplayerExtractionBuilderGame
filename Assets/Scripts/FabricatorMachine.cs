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
            Debug.Log("Missing inputs");
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
        foreach (var output in currentRecipe.outputs)
        {
            playerAbilities.EquipAbility(output.ability);
            //SessionState.Instance.AddAbilityToSession(output.ability);
        }

        currentRecipe = null;
    }

    private bool HasInputs(RecipeData recipe)
    {
        return SessionState.Instance.stashSalvage >= recipe.inputs[0].amount;
    }

    private void ConsumeInputs(RecipeData recipe)
    {
        SessionState.Instance.stashSalvage -= recipe.inputs[0].amount;
    }
}

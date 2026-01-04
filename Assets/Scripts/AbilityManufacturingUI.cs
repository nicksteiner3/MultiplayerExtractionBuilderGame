using UnityEngine;

public class AbilityManufacturingUI : MonoBehaviour
{
    [SerializeField] private GameObject abilityManufacturingUi;

    private FabricatorMachine currentMachine;

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
}

using UnityEngine;

public class FabricatorTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject manufacturingUI;
    private FabricatorMachine fabricatorMachine;

    private void Awake()
    {
        fabricatorMachine = GetComponent<FabricatorMachine>();
    }

    public void Interact()
    {
        // Lazy-load UI on first interaction
        if (manufacturingUI == null)
        {
            // Try to find by component (including inactive objects)
            var allUIs = FindObjectsByType<ManufacturingUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            if (allUIs.Length > 0)
            {
                manufacturingUI = allUIs[0].gameObject;
                Debug.Log("Found ManufacturingUI component");
            }
            else
            {
                Debug.LogError("FabricatorTerminal: Could not find ManufacturingUI component in scene!");
                return;
            }
        }

        if (fabricatorMachine == null)
        {
            Debug.LogError("fabricatorMachine script not found.");
            return;
        }

        manufacturingUI.SetActive(true);
        manufacturingUI.GetComponent<ManufacturingUI>().SetMachine(fabricatorMachine);

        var controller = FindFirstObjectByType<FPSController>();
        if (controller)
        {
            controller.FreezePlayer();
        }
    }
}
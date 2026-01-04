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
        if (fabricatorMachine == null)
        {
            Debug.LogError("fabricatorMachine script not found.");
            return;
        }

        manufacturingUI.gameObject.SetActive(true);
        manufacturingUI.GetComponentInParent<AbilityManufacturingUI>().SetMachine(fabricatorMachine);

        var controller = FindFirstObjectByType<FPSController>();
        if (controller)
        {
            controller.FreezePlayer();
        }
    }
}
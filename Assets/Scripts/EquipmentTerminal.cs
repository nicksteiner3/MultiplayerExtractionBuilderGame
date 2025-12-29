using UnityEngine;

public class EquipmentTerminal : MonoBehaviour, IInteractable
{
    public GameObject equipmentTerminalUI;
    public void Interact(GameObject interactor)
    {
        equipmentTerminalUI.SetActive(true);

        var controller = FindFirstObjectByType<FPSController>();
        if (controller)
        {
            controller.FreezePlayer();
        }
    }
}

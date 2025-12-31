using UnityEngine;

public class AbilityManufacturingTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject manufacturingUI;

    public void Interact()
    {
        manufacturingUI.SetActive(true);

        var controller = FindFirstObjectByType<FPSController>();
        if (controller)
        {
            controller.FreezePlayer();
        }
    }
}
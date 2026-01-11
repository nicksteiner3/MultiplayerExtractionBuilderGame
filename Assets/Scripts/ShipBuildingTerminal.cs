using UnityEngine;

public class ShipBuildingTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject shipBuildingUIObject;

    public void Interact()
    {
        if (shipBuildingUIObject == null)
        {
            var ui = FindFirstObjectByType<ShipBuildingUI>();
            if (ui != null)
                shipBuildingUIObject = ui.gameObject;
        }

        if (shipBuildingUIObject != null)
        {
            var ui = shipBuildingUIObject.GetComponentInParent<ShipBuildingUI>();

            if (ui != null)
                ui.OpenBuildingMenu();
        }
    }
}

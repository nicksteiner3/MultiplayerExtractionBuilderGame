using UnityEngine;

public class AbilityManufacturingUI : MonoBehaviour
{
    [SerializeField] private AbilityData dashAbility;
    [SerializeField] private AbilityUIItem dashAbilityPrefab;
    [SerializeField] private GameObject abilityManufacturingUi;
    [SerializeField] private int dashCost = 50;

    private void Update()
    {
        if (abilityManufacturingUi.gameObject.activeSelf)
            HandleWindowClosure();
    }

    public void CraftDash()
    {
        var session = SessionState.Instance;
        if (session == null) return;

        if (session.stashSalvage < dashCost)
        {
            Debug.Log("Not enough salvage");
            return;
        }

        var stashSlot = EquipmentUIManager.Instance.GetFirstStashSlot();
        if (stashSlot == null)
        {
            Debug.Log("No free stash slot");
            return;
        }

        // Deduct cost
        session.stashSalvage -= dashCost;

        // Spawn UI item
        var item = Instantiate(dashAbilityPrefab);
        item.ability = dashAbility;
        item.IsEquipped = false;

        stashSlot.PlaceItem(item);
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

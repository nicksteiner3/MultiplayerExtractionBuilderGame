using UnityEngine;
using UnityEngine.EventSystems;

public class StashSlotUI : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag;
        if (dragged == null) return;

        var item = dragged.GetComponent<AbilityUIItem>();
        if (item == null) return;

        if (!item.IsEquipped) return;

        TryUnequip(item);
    }

    private void TryUnequip(AbilityUIItem item)
    {
        var manager = EquipmentUIManager.Instance;
        if (manager == null || manager.PlayerAbilities == null)
            return;

        manager.PlayerAbilities.UnequipAbility(item.ability);

        PlaceItem(item);
    }

    private void PlaceItem(AbilityUIItem item)
    {
        item.transform.SetParent(transform);

        var rt = item.GetComponent<RectTransform>();
        rt.anchoredPosition = Vector2.zero;
        rt.localScale = Vector3.one;

        item.IsEquipped = false;
    }
}

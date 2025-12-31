using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlotUI : MonoBehaviour, IDropHandler
{
    public bool IsEmpty => transform.childCount == 0;

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag;
        if (dragged == null) return;

        var abilityItem = dragged.GetComponent<AbilityUIItem>();
        if (abilityItem == null) return;

        if (!IsEmpty) return;

        TryEquip(abilityItem);
    }

    private void PlaceItem(AbilityUIItem item)
    {
        item.transform.SetParent(transform);

        var rt = item.GetComponent<RectTransform>();
        rt.anchoredPosition = Vector2.zero;
        rt.localScale = Vector3.one;

        item.IsEquipped = true;
    }

    public void TryEquip(AbilityUIItem item)
    {
        var manager = EquipmentUIManager.Instance;
        if (manager == null || manager.PlayerAbilities == null)
            return;

        manager.PlayerAbilities.EquipAbility(item.ability);
        PlaceItem(item);
    }
}
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

        PlaceItem(abilityItem);
    }

    public void PlaceItem(AbilityUIItem item)
    {
        item.transform.SetParent(transform);
        var rt = item.GetComponent<RectTransform>();
        rt.anchoredPosition = Vector2.zero;
        rt.localScale = Vector3.one;
        rt.localRotation = Quaternion.identity;
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponStashSlotUI : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag;
        if (dragged == null) return;

        var item = dragged.GetComponent<WeaponUIItem>();
        if (item == null) return;

        if (!item.IsEquipped) return;

        TryUnequip(item);
    }

    public void TryUnequip(WeaponUIItem item)
    {
        var manager = EquipmentUIManager.Instance;
        if (manager == null || manager.PlayerWeapons == null)
            return;

        manager.PlayerWeapons.UnequipWeapon(item.weapon);

        PlaceItem(item);
    }

    public void PlaceItem(WeaponUIItem item)
    {
        item.transform.SetParent(transform);

        var rt = item.GetComponent<RectTransform>();
        rt.anchoredPosition = Vector2.zero;
        rt.localScale = Vector3.one;

        item.IsEquipped = false;
    }
}

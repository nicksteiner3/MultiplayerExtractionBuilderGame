using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlotUI : MonoBehaviour, IDropHandler
{
    public bool IsEmpty => transform.childCount == 0;

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag;
        if (dragged == null) return;

        var weaponItem = dragged.GetComponent<WeaponUIItem>();
        if (weaponItem == null) return;

        if (!IsEmpty) return;

        TryEquip(weaponItem);
    }

    private void PlaceItem(WeaponUIItem item)
    {
        item.transform.SetParent(transform);

        var rt = item.GetComponent<RectTransform>();
        rt.anchoredPosition = Vector2.zero;
        rt.localScale = Vector3.one;

        item.IsEquipped = true;
    }

    public void TryEquip(WeaponUIItem item)
    {
        var manager = EquipmentUIManager.Instance;
        if (manager == null || manager.PlayerWeapons == null)
            return;

        manager.PlayerWeapons.EquipWeapon(item.weapon);
        PlaceItem(item);
    }
}

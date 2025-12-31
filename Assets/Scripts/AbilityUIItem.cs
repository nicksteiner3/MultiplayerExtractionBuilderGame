using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityUIItem : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public AbilityData ability;

    private Transform originalParent;
    private CanvasGroup canvasGroup;

    private float lastClickTime;
    private const float doubleClickThreshold = 0.3f;

    public bool IsEquipped { get; set; }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    // ---------------- Drag ----------------

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (transform.parent == transform.root)
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
        }
    }

    // ---------------- Double Click ----------------

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            TryAutoEquip();
        }

        lastClickTime = Time.time;
    }

    private void TryAutoEquip()
    {
        if (IsEquipped) return;

        if (EquipmentUIManager.Instance == null) return;

        var slot = EquipmentUIManager.Instance.GetFirstEmptySlot();
        if (slot == null) return;

        slot.TryEquip(this);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUIManager : MonoBehaviour
{
    public static EquipmentUIManager Instance;
    public GameObject UIWindow;
    public PlayerAbilities PlayerAbilities { get; private set; }

    [SerializeField] private List<EquipmentSlotUI> equipmentSlots = new();
    [SerializeField] private List<StashSlotUI> stashSlots = new();

    private void Start()
    {
        PlayerAbilities = FindFirstObjectByType<PlayerAbilities>();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        HandleWindowClosure();
    }

    public EquipmentSlotUI GetFirstEmptySlot()
    {
        foreach (var slot in equipmentSlots)
        {
            if (slot.IsEmpty)
                return slot;
        }
        return null;
    }

    public StashSlotUI GetFirstStashSlot()
    {
        foreach (var slot in stashSlots)
        {
            if (slot.transform.childCount == 0)
                return slot;
        }
        return null;
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
        if (UIWindow.activeSelf)
        {
            UIWindow.SetActive(false);

            var controller = FindFirstObjectByType<FPSController>();
            if (controller)
                controller.UnfreezePlayer();
        }
    }
}
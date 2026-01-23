using System.Collections.Generic;
using UnityEngine;

public class EquipmentUIManager : MonoBehaviour
{
    public static EquipmentUIManager Instance;
    public GameObject equipmentManagerUi;
    public PlayerAbilities PlayerAbilities { get; private set; }
    public PlayerWeapons PlayerWeapons { get; private set; }

    [SerializeField] private List<EquipmentSlotUI> equipmentSlots = new();
    [SerializeField] private List<StashSlotUI> stashSlots = new();
    [Header("Weapons")]
    [SerializeField] private List<WeaponSlotUI> weaponSlots = new();
    [SerializeField] private List<WeaponStashSlotUI> weaponStashSlots = new();

    private void Start()
    {
        PlayerAbilities = FindFirstObjectByType<PlayerAbilities>();
        PlayerWeapons = FindFirstObjectByType<PlayerWeapons>();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (equipmentManagerUi.gameObject.activeSelf)
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

    public WeaponSlotUI GetFirstEmptyWeaponSlot()
    {
        foreach (var slot in weaponSlots)
        {
            if (slot.IsEmpty)
                return slot;
        }
        return null;
    }

    public WeaponStashSlotUI GetFirstWeaponStashSlot()
    {
        foreach (var slot in weaponStashSlots)
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
        if (equipmentManagerUi.activeSelf)
        {
            equipmentManagerUi.SetActive(false);

            var controller = FindFirstObjectByType<FPSController>();
            if (controller)
                controller.UnfreezePlayer();
        }
    }
}
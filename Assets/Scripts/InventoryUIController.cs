using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private GameObject parentUIObject;
    [SerializeField] private Transform containerListParent;
    [SerializeField] private GameObject containerSlotPrefab;
    [SerializeField] private Transform playerInventoryParent;
    [SerializeField] private GameObject playerInventorySlotPrefab;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI inventoryDisplayText; // For tabbed display
    [SerializeField] private TextMeshProUGUI previousTabText;
    [SerializeField] private TextMeshProUGUI nextTabText;
    [SerializeField] private GameObject containerPanel;
    [SerializeField] private RectTransform playerInventoryPanel;
    [SerializeField] private Vector2 standaloneMarginMin = new Vector2(40f, 40f);
    [SerializeField] private Vector2 standaloneMarginMax = new Vector2(40f, 40f);
    [SerializeField] private Image borderImage;
    [SerializeField] private Sprite standaloneBorderSprite;
    [SerializeField] private Sprite splitBorderSprite;

    private ContainerInventory currentContainer;
    private bool isStandaloneMode = false; // True when opened with TAB (no container)
    private FPSController cachedController;
    private Vector2 inventoryPanelAnchorMinDefault;
    private Vector2 inventoryPanelAnchorMaxDefault;
    private Vector2 inventoryPanelOffsetMinDefault;
    private Vector2 inventoryPanelOffsetMaxDefault;
    private bool inventoryPanelLayoutCached = false;

    // Tab system
    private enum InventoryTab { Materials, Abilities, Weapons }
    private InventoryTab currentTab = InventoryTab.Materials;

    void Start()
    {
        if (parentUIObject != null) parentUIObject.SetActive(false);

        cachedController = FindFirstObjectByType<FPSController>();
        
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => Close());
        }

        CacheInventoryPanelLayout();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (parentUIObject != null && parentUIObject.activeSelf)
            {
                Close();
            }
            else
            {
                if (cachedController != null && cachedController.frozen)
                    return;

                OpenStandalone();
            }
        }

        if (parentUIObject != null && parentUIObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }

        // Tab navigation with Q and E
        if (parentUIObject != null && parentUIObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CycleTab(1);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                CycleTab(-1);
            }
        }

        // Update header in real-time if display text available
        if (parentUIObject != null && parentUIObject.activeSelf)
        {
            UpdateTabHeader();
        }
    }

    private void CycleTab(int direction)
    {
        int tabCount = System.Enum.GetValues(typeof(InventoryTab)).Length;
        int currentIndex = (int)currentTab;
        currentIndex = (currentIndex + direction + tabCount) % tabCount;
        currentTab = (InventoryTab)currentIndex;
        RefreshPlayerInventory();
    }

    private void UpdateTabHeader()
    {
        if (inventoryDisplayText != null)
        {
            inventoryDisplayText.text = GetTabHeader();
        }

        if (previousTabText != null)
        {
            previousTabText.text = $"{GetPreviousTabName()} [Q]";
        }

        if (nextTabText != null)
        {
            nextTabText.text = $"{GetNextTabName()} [E]";
        }
    }

    private string GetTabHeader()
    {
        switch (currentTab)
        {
            case InventoryTab.Abilities:
                return "Abilities";
            case InventoryTab.Weapons:
                return "Weapons";
            default:
                return "Materials";
        }
    }

    private string GetPreviousTabName()
    {
        switch (currentTab)
        {
            case InventoryTab.Abilities:
                return "Materials";
            case InventoryTab.Weapons:
                return "Abilities";
            default:
                return "Weapons";
        }
    }

    private string GetNextTabName()
    {
        switch (currentTab)
        {
            case InventoryTab.Abilities:
                return "Weapons";
            case InventoryTab.Weapons:
                return "Materials";
            default:
                return "Abilities";
        }
    }


    public void Open(ContainerInventory container)
    {
        currentContainer = container;
        isStandaloneMode = false;
        ApplyStandaloneLayout(false);
        if (parentUIObject != null) parentUIObject.SetActive(true);
        RefreshContainerList();
        RefreshPlayerInventory();

        if (cachedController == null)
            cachedController = FindFirstObjectByType<FPSController>();
        if (cachedController) cachedController.FreezePlayer();
    }

    public void OpenStandalone()
    {
        currentContainer = null;
        isStandaloneMode = true;
        currentTab = InventoryTab.Materials;
        ApplyStandaloneLayout(true);
        if (parentUIObject != null) parentUIObject.SetActive(true);
        ClearContainerList();
        RefreshPlayerInventory();

        if (cachedController == null)
            cachedController = FindFirstObjectByType<FPSController>();
        if (cachedController) cachedController.FreezePlayer();
    }

    public void Close()
    {
        if (parentUIObject != null) parentUIObject.SetActive(false);
        currentContainer = null;
        isStandaloneMode = false;

        if (cachedController == null)
            cachedController = FindFirstObjectByType<FPSController>();
        if (cachedController) cachedController.UnfreezePlayer();
    }

    private void CacheInventoryPanelLayout()
    {
        if (playerInventoryPanel == null || inventoryPanelLayoutCached)
            return;

        inventoryPanelAnchorMinDefault = playerInventoryPanel.anchorMin;
        inventoryPanelAnchorMaxDefault = playerInventoryPanel.anchorMax;
        inventoryPanelOffsetMinDefault = playerInventoryPanel.offsetMin;
        inventoryPanelOffsetMaxDefault = playerInventoryPanel.offsetMax;
        inventoryPanelLayoutCached = true;
    }

    private void ApplyStandaloneLayout(bool standalone)
    {
        if (containerPanel != null)
        {
            containerPanel.SetActive(!standalone);
        }

        if (borderImage != null)
        {
            borderImage.sprite = standalone ? standaloneBorderSprite : splitBorderSprite;
        }

        if (playerInventoryPanel != null)
        {
            CacheInventoryPanelLayout();

            if (standalone)
            {
                playerInventoryPanel.anchorMin = new Vector2(0f, 0f);
                playerInventoryPanel.anchorMax = new Vector2(1f, 1f);
                playerInventoryPanel.offsetMin = new Vector2(standaloneMarginMin.x, standaloneMarginMin.y);
                playerInventoryPanel.offsetMax = new Vector2(-standaloneMarginMax.x, -standaloneMarginMax.y);
            }
            else
            {
                if (inventoryPanelLayoutCached)
                {
                    playerInventoryPanel.anchorMin = inventoryPanelAnchorMinDefault;
                    playerInventoryPanel.anchorMax = inventoryPanelAnchorMaxDefault;
                    playerInventoryPanel.offsetMin = inventoryPanelOffsetMinDefault;
                    playerInventoryPanel.offsetMax = inventoryPanelOffsetMaxDefault;
                }
            }
        }
    }

    private void RefreshContainerList()
    {
        if (containerListParent == null || containerSlotPrefab == null)
            return;

        ClearContainerList();

        if (currentContainer == null) return;

        List<ContainerInventory.ContainerStack> contents = currentContainer.GetContents();
        foreach (var stack in contents)
        {
            var go = Instantiate(containerSlotPrefab);
            go.transform.SetParent(containerListParent, false);
            var slot = go.GetComponent<InventorySlotUI>();
            if (slot != null)
            {
                slot.Bind(stack, HandleTakeFromContainer);
            }
        }
    }

    private void ClearContainerList()
    {
        if (containerListParent == null) return;
        for (int i = containerListParent.childCount - 1; i >= 0; i--)
        {
            Destroy(containerListParent.GetChild(i).gameObject);
        }
    }

    private void RefreshPlayerInventory()
    {
        if (playerInventoryParent == null || playerInventorySlotPrefab == null)
            return;

        // Only destroy dynamically created slots (ones with InventorySlotUI component)
        List<Transform> toDestroy = new List<Transform>();
        for (int i = 0; i < playerInventoryParent.childCount; i++)
        {
            var child = playerInventoryParent.GetChild(i);
            if (child.GetComponent<InventorySlotUI>() != null)
            {
                toDestroy.Add(child);
            }
        }
        foreach (var t in toDestroy)
        {
            Destroy(t.gameObject);
        }

        if (InventoryManager.Instance == null)
        {
            Debug.LogWarning("[InventoryUI] InventoryManager not found");
            return;
        }

        switch (currentTab)
        {
            case InventoryTab.Abilities:
                PopulateAbilitiesList();
                break;
            case InventoryTab.Weapons:
                PopulateWeaponsList();
                break;
            default:
                PopulateMaterialsList();
                break;
        }
    }

    private void PopulateMaterialsList()
    {
        List<InventoryManager.InventoryStack> playerItems = InventoryManager.Instance.GetPlayerInventory();
        Debug.Log($"[InventoryUI] Player inventory has {playerItems.Count} stacks");

        foreach (var stack in playerItems)
        {
            var go = Instantiate(playerInventorySlotPrefab);
            go.transform.SetParent(playerInventoryParent, false);
            var label = go.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null && stack.material != null)
            {
                label.text = $"{stack.material.materialName} x{stack.amount}";
                Debug.Log($"[InventoryUI] Added slot: {stack.material.materialName} x{stack.amount}");
            }
        }
    }

    private void PopulateAbilitiesList()
    {
        List<AbilityData> abilities = InventoryManager.Instance.GetAbilities();
        Debug.Log($"[InventoryUI] Player abilities: {abilities.Count}");

        Dictionary<AbilityData, int> abilityCounts = new Dictionary<AbilityData, int>();
        foreach (var ability in abilities)
        {
            if (ability == null) continue;
            if (!abilityCounts.ContainsKey(ability))
                abilityCounts[ability] = 0;
            abilityCounts[ability] += 1;
        }

        foreach (var kvp in abilityCounts)
        {
            var go = Instantiate(playerInventorySlotPrefab);
            go.transform.SetParent(playerInventoryParent, false);
            var label = go.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
            {
                string countSuffix = kvp.Value > 1 ? $" x{kvp.Value}" : "";
                label.text = $"{kvp.Key.abilityName}{countSuffix}";
            }
        }
    }

    private void PopulateWeaponsList()
    {
        List<WeaponData> weapons = InventoryManager.Instance.GetWeapons();
        Debug.Log($"[InventoryUI] Player weapons: {weapons.Count}");

        foreach (var weapon in weapons)
        {
            var go = Instantiate(playerInventorySlotPrefab);
            go.transform.SetParent(playerInventoryParent, false);
            var label = go.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null && weapon != null)
            {
                label.text = weapon.weaponName;
            }
        }
    }

    private void HandleTakeFromContainer(ContainerInventory.ContainerStack stack)
    {
        if (currentContainer == null || stack == null) return;

        currentContainer.TransferToPlayer(stack);
        RefreshContainerList();
        RefreshPlayerInventory();
    }
}

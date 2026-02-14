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

    private ContainerInventory currentContainer;
    private bool isStandaloneMode = false; // True when opened with TAB (no container)
    private FPSController cachedController;

    void Start()
    {
        if (parentUIObject != null) parentUIObject.SetActive(false);

        cachedController = FindFirstObjectByType<FPSController>();
        
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => Close());
        }
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
    }

    public void Open(ContainerInventory container)
    {
        currentContainer = container;
        isStandaloneMode = false;
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

    private void HandleTakeFromContainer(ContainerInventory.ContainerStack stack)
    {
        if (currentContainer == null || stack == null) return;

        currentContainer.TransferToPlayer(stack);
        RefreshContainerList();
        RefreshPlayerInventory();
    }
}

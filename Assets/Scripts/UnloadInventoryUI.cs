using UnityEngine;
using TMPro;
using System.Linq;

public class UnloadInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject rootPanel;
    [SerializeField] private TextMeshProUGUI inventoryText;

    public void Show()
    {
        rootPanel.SetActive(true);
        UpdateInventoryDisplay();
        FreezePlayer(true);
    }

    private void UpdateInventoryDisplay()
    {
        var inventory = InventoryManager.Instance.GetPlayerInventory();
        if (inventory.Count == 0)
        {
            inventoryText.text = "No materials to unload";
            return;
        }

        var displayLines = inventory.Select(stack => 
            $"{stack.material.materialName}: {stack.amount}").ToList();
        inventoryText.text = "Materials to Unload:\n" + string.Join("\n", displayLines);
    }

    public void OnUnloadClicked()
    {
        UnloadPlayerInventoryToStash();
        rootPanel.SetActive(false);
        FreezePlayer(false);
    }

    private void UnloadPlayerInventoryToStash()
    {
        var inventory = InventoryManager.Instance.GetPlayerInventory();
        foreach (var stack in inventory)
        {
            if (stack.material != null && stack.amount > 0)
            {
                SessionState.Instance.AddMaterial(stack.material, stack.amount);
            }
        }
        InventoryManager.Instance.ClearInventory();
        Debug.Log("[UnloadInventoryUI] Materials transferred to ship stash");
    }

    void FreezePlayer(bool freeze)
    {
        // Simple version
        var controller = FindFirstObjectByType<FPSController>();
        if (controller != null)
            controller.frozen = freeze;

        Cursor.lockState = freeze ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = freeze;
    }
}
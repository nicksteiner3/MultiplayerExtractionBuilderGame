using System.Collections.Generic;
using UnityEngine;

public class ContainerInventory : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public class ContainerStack
    {
        public MaterialData material;
        public int amount;
    }

    [SerializeField] private List<ContainerStack> contents = new();
    [SerializeField] private InventoryUIController inventoryUI;

    public List<ContainerStack> GetContents() => contents;

    public void Interact()
    {
        if (inventoryUI == null)
        {
            inventoryUI = FindFirstObjectByType<InventoryUIController>(FindObjectsInactive.Include);
        }

        if (inventoryUI == null)
        {
            Debug.LogError("No InventoryUIController in scene.");
            return;
        }

        inventoryUI.Open(this);
    }

    public void TransferToPlayer(ContainerStack stack)
    {
        if (stack == null || stack.material == null || stack.amount <= 0) return;
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager not found.");
            return;
        }

        InventoryManager.Instance.AddToPlayer(stack.material, stack.amount);

        // Notify challenge system
        if (ChallengeManager.Instance != null)
        {
            ChallengeManager.Instance.OnObjectLooted();
        }

        contents.Remove(stack);
    }
}

using System.Collections.Generic;
using UnityEngine;

// Simple inventory manager for player materials/items (minimal scaffolding)
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [System.Serializable]
    public class InventoryStack
    {
        public MaterialData material;
        public int amount;
    }

    [SerializeField] private List<InventoryStack> playerInventory = new();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeStartingInventory();
    }

    private void InitializeStartingInventory()
    {
        // Get starting materials from SessionState
        var sessionState = SessionState.Instance;
        if (sessionState == null) return;

        // Add starting Bio-Fuel to player inventory
        if (sessionState.startingBioFuel != null && sessionState.startingBioFuelAmount > 0)
        {
            AddToPlayer(sessionState.startingBioFuel, sessionState.startingBioFuelAmount);
        }

        // Add starting Salvage Scrap to player inventory
        if (sessionState.startingSalvageScrap != null && sessionState.startingStashSalvage > 0)
        {
            AddToPlayer(sessionState.startingSalvageScrap, sessionState.startingStashSalvage);
        }

        // Add starting Ore to player inventory
        if (sessionState.startingOre != null && sessionState.startingOreAmount > 0)
        {
            AddToPlayer(sessionState.startingOre, sessionState.startingOreAmount);
        }
    }

    public List<InventoryStack> GetPlayerInventory()
    {
        return playerInventory;
    }

    public void AddToPlayer(MaterialData material, int amount)
    {
        if (material == null || amount <= 0) return;

        var stack = playerInventory.Find(s => s.material == material);
        if (stack == null)
        {
            stack = new InventoryStack { material = material, amount = 0 };
            playerInventory.Add(stack);
        }
        stack.amount += amount;
        Debug.Log($"[Inventory] Added {amount}x {material.materialName}. Total: {stack.amount}");
    }

    public bool RemoveFromPlayer(MaterialData material, int amount)
    {
        if (material == null || amount <= 0) return false;
        var stack = playerInventory.Find(s => s.material == material);
        if (stack == null || stack.amount < amount) return false;
        stack.amount -= amount;
        if (stack.amount <= 0)
            playerInventory.Remove(stack);
        return true;
    }
}

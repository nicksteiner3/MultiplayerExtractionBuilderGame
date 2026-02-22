using System.Collections.Generic;
using UnityEngine;

// Unified inventory manager for player materials, abilities, and weapons
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
    [SerializeField] private List<AbilityData> abilities = new();
    [SerializeField] private List<WeaponData> weapons = new();

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
        if (sessionState.startingSalvageScrap != null && sessionState.startingSalvageScrapAmount > 0)
        {
            AddToPlayer(sessionState.startingSalvageScrap, sessionState.startingSalvageScrapAmount);
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

    public bool HasMaterial(MaterialData material, int amount)
    {
        if (material == null || amount <= 0) return false;
        var stack = playerInventory.Find(s => s.material == material);
        return stack != null && stack.amount >= amount;
    }

    public void ClearInventory()
    {
        playerInventory.Clear();
        abilities.Clear();
        weapons.Clear();
        Debug.Log("[Inventory] Player inventory cleared (materials, abilities, weapons)");
    }

    // Ability Management
    public void AddAbility(AbilityData ability)
    {
        if (ability == null) return;
        abilities.Add(ability);
        Debug.Log($"[Inventory] Added ability: {ability.abilityName}");
    }

    public bool RemoveAbility(AbilityData ability)
    {
        if (ability == null) return false;
        bool removed = abilities.Remove(ability);
        if (removed)
        {
            Debug.Log($"[Inventory] Removed ability: {ability.abilityName}");
        }
        return removed;
    }

    public List<AbilityData> GetAbilities()
    {
        return new List<AbilityData>(abilities);
    }

    public bool HasAbility(AbilityData ability)
    {
        return abilities.Contains(ability);
    }

    // Weapon Management
    public void AddWeapon(WeaponData weapon)
    {
        if (weapon == null) return;
        weapons.Add(weapon);
        Debug.Log($"[Inventory] Added weapon: {weapon.weaponName}");
    }

    public bool RemoveWeapon(WeaponData weapon)
    {
        if (weapon == null) return false;
        bool removed = weapons.Remove(weapon);
        if (removed)
        {
            Debug.Log($"[Inventory] Removed weapon: {weapon.weaponName}");
        }
        return removed;
    }

    public List<WeaponData> GetWeapons()
    {
        return new List<WeaponData>(weapons);
    }

    public bool HasWeapon(WeaponData weapon)
    {
        return weapons.Contains(weapon);
    }
}

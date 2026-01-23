using System.Collections.Generic;
using UnityEngine;

public class SessionState : MonoBehaviour
{
    private List<AbilitySlot> equippedAbilities = new();

    public static SessionState Instance;
    public bool lastRunEndedInDeath = false;
    public bool hasEnteredPvpve = false;
    public bool returnedFromRun = false;

    // Tutorial progression
    public int tutorialStep = 0; // 0=Welcome, 1=PlaceReactor, 2=FuelReactor, 3=PowerFabricator, 4=Completed

    // Legacy salvage (being phased out, keeping for now)
    public int runSalvage;
    public int stashSalvage;

    [Header("Starting Materials (for testing)")]
    public MaterialData startingBioFuel;
    public int startingBioFuelAmount = 1;
    public MaterialData startingSalvageScrap;
    public int startingStashSalvage = 30;
    public MaterialData startingOre;
    public int startingOreAmount = 5;

    // Multi-material system
    private Dictionary<MaterialData, int> stashMaterials = new();

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("SessionState duplicate detected; destroying this instance. No starting materials added.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        StartWithMaterials_DEVONLY();
    }

    private void StartWithMaterials_DEVONLY() 
    {
        if (startingBioFuel != null && startingBioFuelAmount > 0)
        {
            AddMaterial(startingBioFuel, startingBioFuelAmount);
        }

        if (startingSalvageScrap != null && startingStashSalvage > 0)
        {
            AddMaterial(startingSalvageScrap, startingStashSalvage);
        }

        if (startingOre != null && startingOreAmount > 0)
        {
            AddMaterial(startingOre, startingOreAmount);
        }
    }

    // Called by pickups
    public void AddRunSalvage(int amount)
    {
        runSalvage += amount;
        Debug.Log($"Run Salvage: {runSalvage}");
    }

    // Called by unload button
    public void UnloadInventory()
    {
        stashSalvage += runSalvage;
        runSalvage = 0;
    }

    // Load abilities for the player
    public List<AbilitySlot> GetEquippedAbilities()
    {
        // Return copies so runtime cooldowns are independent
        List<AbilitySlot> copy = new List<AbilitySlot>();
        foreach (var slot in equippedAbilities)
        {
            copy.Add(new AbilitySlot
            {
                ability = slot.ability,
                cooldownRemaining = 0
            });
        }
        return copy;
    }

    public void AddAbilityToSession(AbilityData ability)
    {
        if (equippedAbilities.Exists(x => x.ability == ability))
            return;

        equippedAbilities.Add(new AbilitySlot
        {
            ability = ability,
            cooldownRemaining = 0
        });
    }

    public void RemoveAbilityFromSession(AbilityData ability)
    {
        equippedAbilities.RemoveAll(x => x.ability == ability);
    }

    public void ClearAbilities()
    {
        equippedAbilities.Clear();
    }

    // Material management
    public int GetMaterialCount(MaterialData material)
    {
        if (material == null) return 0;
        return stashMaterials.ContainsKey(material) ? stashMaterials[material] : 0;
    }

    public void AddMaterial(MaterialData material, int amount)
    {
        if (material == null || amount <= 0) return;

        if (!stashMaterials.ContainsKey(material))
            stashMaterials[material] = 0;

        stashMaterials[material] += amount;
        Debug.Log($"Added {amount}x {material.materialName}. Total: {stashMaterials[material]}");
    }

    public bool RemoveMaterial(MaterialData material, int amount)
    {
        if (material == null || amount <= 0) return false;

        int current = GetMaterialCount(material);
        if (current < amount)
        {
            Debug.LogWarning($"Insufficient {material.materialName}. Have: {current}, Need: {amount}");
            return false;
        }

        stashMaterials[material] -= amount;
        Debug.Log($"Removed {amount}x {material.materialName}. Remaining: {stashMaterials[material]}");
        return true;
    }

    public bool HasMaterial(MaterialData material, int amount)
    {
        return GetMaterialCount(material) >= amount;
    }

    public Dictionary<MaterialData, int> GetAllMaterials()
    {
        return new Dictionary<MaterialData, int>(stashMaterials);
    }

    public void ClearMaterials()
    {
        stashMaterials.Clear();
    }
}
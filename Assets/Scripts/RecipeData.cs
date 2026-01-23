using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialInput
{
    public MaterialData material;
    public int amount = 1;
}

[System.Serializable]
public class MaterialOutput
{
    public MaterialData material;
    public int amount = 1;
}

[CreateAssetMenu(menuName = "Crafting/Recipe")]
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public float craftTime;

    [Header("Legacy Salvage (Phase Out)")]
    public List<ItemCost> inputs; // Will be replaced by materialInputs
    public ItemOutput outputs;
    
    [Header("New Material System")]
    public List<MaterialInput> materialInputs = new();
    public List<MaterialOutput> materialOutputs = new();

    [Header("UI")]
    public AbilityUIItem prefabUIItem; // For abilities only
    public WeaponUIItem prefabWeaponUIItem; // For weapons only

    public enum MachineType
    {
        Fabricator,    // Abilities & weapons (simple recipes)
        Assembler      // Complex multi-input recipes (unlocked later)
    }

    [Header("Machine Type")]
    public MachineType requiredMachine = MachineType.Fabricator;
}

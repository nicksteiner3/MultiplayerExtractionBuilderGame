using System.Collections.Generic;
using UnityEngine;

public class SessionState : MonoBehaviour
{
    public static SessionState Instance;
    public bool lastRunEndedInDeath = false;
    public bool hasEnteredPvpve = false;

    // Temporary simple model
    public int runSalvage;
    public int stashSalvage;

    public bool returnedFromRun = false;

    public List<AbilitySlot> equippedAbilities = new();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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
        equippedAbilities.Add(new AbilitySlot
        {
            ability = ability,
            cooldownRemaining = 0
        });
    }

    public void ClearAbilities()
    {
        equippedAbilities.Clear();
    }
}
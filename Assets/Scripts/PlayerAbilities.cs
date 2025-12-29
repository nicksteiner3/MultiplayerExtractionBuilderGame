using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private List<AbilitySlot> equippedAbilities = new List<AbilitySlot>();

    private void Start()
    {
        if (SessionState.Instance != null)
        {
            // Populate this player's abilities from the session
            equippedAbilities = SessionState.Instance.GetEquippedAbilities();
        }
    }

    void Update()
    {
        foreach (var slot in equippedAbilities)
        {
            if (slot.ability == null) continue;

            if (slot.cooldownRemaining > 0)
                slot.cooldownRemaining -= Time.deltaTime;

            if (Input.GetKeyDown(slot.ability.activationKey) &&
                slot.cooldownRemaining <= 0)
            {
                slot.ability.Activate(gameObject);
                slot.cooldownRemaining = slot.ability.cooldown;
            }
        }
    }

    public void EquipAbility(AbilityData ability)
    {
        // Check if already equipped
        if (equippedAbilities.Any(s => s.ability == ability))
            return;

        var slot = new AbilitySlot { ability = ability, cooldownRemaining = 0 };

        equippedAbilities.Add(slot);

        if (SessionState.Instance != null)
        {
            SessionState.Instance.AddAbilityToSession(ability);
        }
    }

    public void ClearAbilities()
    {
        equippedAbilities.Clear();
        if (SessionState.Instance != null)
            SessionState.Instance.ClearAbilities();
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private List<WeaponData> equippedWeapons = new();
    public int maxWeapons = 2;

    void Start()
    {
        // Equip starter weapon on game start
        if (SessionState.Instance != null)
        {
            SessionState.Instance.EquipStarterWeapon(this);
        }
    }

    public void EquipWeapon(WeaponData weapon)
    {
        if (weapon == null) return;
        if (equippedWeapons.Any(w => w == weapon)) return;
        if (equippedWeapons.Count >= maxWeapons)
        {
            Debug.LogWarning("[PlayerWeapons] No free weapon slot.");
            return;
        }

        equippedWeapons.Add(weapon);
        Debug.Log($"[PlayerWeapons] Equipped {weapon.weaponName}");
        // TODO: Persist to SessionState when weapon persistence is added.
    }

    public void UnequipWeapon(WeaponData weapon)
    {
        if (weapon == null) return;
        if (!equippedWeapons.Remove(weapon)) return;

        Debug.Log($"[PlayerWeapons] Unequipped {weapon.weaponName}");
        // TODO: Persist to SessionState when weapon persistence is added.
    }

    public List<WeaponData> GetEquippedWeapons()
    {
        return equippedWeapons;
    }
}

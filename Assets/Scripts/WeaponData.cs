using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float damage = 10f;
    public float fireRate = 0.3f;
    public float range = 100f;
}

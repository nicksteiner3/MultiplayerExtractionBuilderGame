using UnityEngine;

[System.Serializable]
public class ItemCost
{
    public string itemId;
    public int amount;
}

[System.Serializable]
public class ItemOutput
{
    public AbilityData ability;
    public WeaponData weapon;
    public int amount;
}

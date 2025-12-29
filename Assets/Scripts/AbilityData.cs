using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    public string abilityName;
    public float cooldown;
    public KeyCode activationKey;

    public abstract void Activate(GameObject player);
}
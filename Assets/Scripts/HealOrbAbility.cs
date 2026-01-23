using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Heal Orb")]
public class HealOrbAbility : AbilityData
{
    public float healAmount = 25f;
    public GameObject healEffectPrefab;  // Visual effect on heal
    
    public override void Activate(GameObject player)
    {
        var health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Heal(healAmount);
            Debug.Log($"[Heal Orb] Healed {healAmount} HP");
            
            // Spawn visual effect
            if (healEffectPrefab != null)
            {
                Instantiate(healEffectPrefab, player.transform.position, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogWarning("[Heal Orb] PlayerHealth component not found!");
        }
    }
}

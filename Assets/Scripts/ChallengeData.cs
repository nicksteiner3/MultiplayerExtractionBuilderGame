using UnityEngine;

[CreateAssetMenu(menuName = "Progression/Challenge")]
public class ChallengeData : ScriptableObject
{
    public string challengeId;
    public string title;
    [TextArea(3, 5)]
    public string description;
    
    public enum ChallengeType
    {
        CraftAbility,      // Craft N abilities
        CraftWeapon,       // Craft N weapons
        KillPlayer,        // Kill N players
        KillBot,           // Kill N bots
        SurviveDeploy,     // Return from deploy alive (count=1)
        LootObjects        // Loot N objects
    }
    
    public ChallengeType type;
    public int targetCount = 1;  // How many to complete (e.g., kill 10 bots)
    
    [Header("Rewards")]
    public RecipeData unlockedRecipe;  // Optional: unlock a recipe on completion
    public int salvageReward = 0;
}

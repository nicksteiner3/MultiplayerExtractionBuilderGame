using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Progression/Milestone")]
public class MilestoneData : ScriptableObject
{
    public string milestoneId;
    public string title;
    [TextArea(3, 5)]
    public string description;
    
    public List<string> requiredChallengeIds = new();  // Challenge IDs that must be completed
    
    [Header("Rewards")]
    public List<RecipeData> unlockedRecipes = new();   // Recipes to unlock
    public int salvageReward = 0;
}

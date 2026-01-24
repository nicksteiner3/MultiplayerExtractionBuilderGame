using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public static ChallengeManager Instance;
    
    [SerializeField] private List<ChallengeData> activeChallenges = new();
    private Dictionary<string, int> challengeProgress = new();  // challengeId -> current count
    private HashSet<string> completedChallenges = new();

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("ChallengeManager duplicate detected; destroying.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void OnEnable()
    {
        // Initialize challenge tracking
        InitializeChallenges();
        
        // Register event listeners
        FabricatorMachine.OnAbilityCrafted += HandleAbilityCrafted;
        FabricatorMachine.OnWeaponCrafted += HandleWeaponCrafted;
        
        // TODO: Register combat system listeners (kill player, kill bot)
        // TODO: Register loot system listener
        // TODO: Register deploy survival listener
    }

    void OnDisable()
    {
        FabricatorMachine.OnAbilityCrafted -= HandleAbilityCrafted;
        FabricatorMachine.OnWeaponCrafted -= HandleWeaponCrafted;
    }

    public void InitializeChallenges()
    {
        challengeProgress.Clear();
        foreach (var challenge in activeChallenges)
        {
            if (!challengeProgress.ContainsKey(challenge.challengeId))
            {
                challengeProgress[challenge.challengeId] = 0;
            }
        }
        Debug.Log($"[ChallengeManager] Initialized {activeChallenges.Count} challenges");
    }

    public void IncrementProgress(string challengeId, int amount = 1)
    {
        if (!challengeProgress.ContainsKey(challengeId))
            return;

        if (completedChallenges.Contains(challengeId))
            return;  // Already completed, don't increment

        challengeProgress[challengeId] += amount;
        
        var challenge = activeChallenges.Find(c => c.challengeId == challengeId);
        if (challenge == null)
            return;

        Debug.Log($"[ChallengeManager] {challenge.title}: {challengeProgress[challengeId]}/{challenge.targetCount}");

        if (challengeProgress[challengeId] >= challenge.targetCount)
        {
            CompleteChallenge(challengeId);
        }
    }

    public void CompleteChallenge(string challengeId)
    {
        if (completedChallenges.Contains(challengeId))
            return;

        completedChallenges.Add(challengeId);
        var challenge = activeChallenges.Find(c => c.challengeId == challengeId);
        
        if (challenge != null)
        {
            Debug.Log($"[ChallengeManager] Challenge Completed: {challenge.title}");
            
            // Award recipe unlock
            if (challenge.unlockedRecipe != null)
            {
                Debug.Log($"[ChallengeManager] Unlocked recipe: {challenge.unlockedRecipe.recipeName}");
                // TODO: Add recipe to player's unlocked recipes
            }
            
            // Award salvage
            if (challenge.salvageReward > 0 && SessionState.Instance != null)
            {
                SessionState.Instance.stashSalvage += challenge.salvageReward;
                Debug.Log($"[ChallengeManager] +{challenge.salvageReward} salvage");
            }
            
            // Notify tutorial
            if (TutorialManager.Instance != null)
            {
                TutorialManager.Instance.OnChallengeCompleted(challengeId);
            }
            
            // Check if any milestones are now complete
            if (MilestoneManager.Instance != null)
            {
                MilestoneManager.Instance.CheckMilestoneCompletion();
            }
        }
    }

    public bool IsChallengeComplete(string challengeId)
    {
        return completedChallenges.Contains(challengeId);
    }

    // Event handlers
    private void HandleAbilityCrafted(RecipeData recipe)
    {
        // Find all "CraftAbility" challenges and increment them
        foreach (var challenge in activeChallenges)
        {
            if (challenge.type == ChallengeData.ChallengeType.CraftAbility && 
                !completedChallenges.Contains(challenge.challengeId))
            {
                IncrementProgress(challenge.challengeId);
            }
        }
    }

    private void HandleWeaponCrafted(RecipeData recipe)
    {
        // Find all "CraftWeapon" challenges and increment them
        foreach (var challenge in activeChallenges)
        {
            if (challenge.type == ChallengeData.ChallengeType.CraftWeapon && 
                !completedChallenges.Contains(challenge.challengeId))
            {
                IncrementProgress(challenge.challengeId);
            }
        }
    }

    public void OnPlayerKilled()
    {
        foreach (var challenge in activeChallenges)
        {
            if (challenge.type == ChallengeData.ChallengeType.KillPlayer && 
                !completedChallenges.Contains(challenge.challengeId))
            {
                IncrementProgress(challenge.challengeId);
            }
        }
    }

    public void OnBotKilled()
    {
        foreach (var challenge in activeChallenges)
        {
            if (challenge.type == ChallengeData.ChallengeType.KillBot && 
                !completedChallenges.Contains(challenge.challengeId))
            {
                IncrementProgress(challenge.challengeId);
            }
        }
    }

    public void OnObjectLooted()
    {
        foreach (var challenge in activeChallenges)
        {
            if (challenge.type == ChallengeData.ChallengeType.LootObjects && 
                !completedChallenges.Contains(challenge.challengeId))
            {
                IncrementProgress(challenge.challengeId);
            }
        }
    }

    public void OnDeploySurvived()
    {
        foreach (var challenge in activeChallenges)
        {
            if (challenge.type == ChallengeData.ChallengeType.SurviveDeploy && 
                !completedChallenges.Contains(challenge.challengeId))
            {
                IncrementProgress(challenge.challengeId, 1);
            }
        }
    }
}

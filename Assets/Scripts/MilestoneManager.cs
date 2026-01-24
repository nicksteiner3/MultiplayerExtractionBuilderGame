using System.Collections.Generic;
using UnityEngine;

public class MilestoneManager : MonoBehaviour
{
    public static MilestoneManager Instance;
    
    [SerializeField] private List<MilestoneData> milestones = new();
    private HashSet<string> completedMilestones = new();

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("MilestoneManager duplicate detected; destroying.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void OnEnable()
    {
        // Listen for challenge completions
        // Challenge tracking happens in ChallengeManager
    }

    public void CheckMilestoneCompletion()
    {
        if (ChallengeManager.Instance == null)
            return;

        foreach (var milestone in milestones)
        {
            if (completedMilestones.Contains(milestone.milestoneId))
                continue;  // Already completed

            // Check if all required challenges are done
            bool allChallengesComplete = true;
            foreach (var challengeId in milestone.requiredChallengeIds)
            {
                if (!ChallengeManager.Instance.IsChallengeComplete(challengeId))
                {
                    allChallengesComplete = false;
                    break;
                }
            }

            if (allChallengesComplete)
            {
                CompleteMilestone(milestone.milestoneId);
            }
        }
    }

    public void CompleteMilestone(string milestoneId)
    {
        if (completedMilestones.Contains(milestoneId))
            return;

        completedMilestones.Add(milestoneId);
        var milestone = milestones.Find(m => m.milestoneId == milestoneId);

        if (milestone != null)
        {
            Debug.Log($"[MilestoneManager] Milestone Completed: {milestone.title}");

            // Award recipe unlocks
            foreach (var recipe in milestone.unlockedRecipes)
            {
                Debug.Log($"[MilestoneManager] Unlocked recipe: {recipe.recipeName}");
                // TODO: Add to player's unlocked recipes list (track in SessionState)
            }

            // Award salvage
            if (milestone.salvageReward > 0 && SessionState.Instance != null)
            {
                SessionState.Instance.stashSalvage += milestone.salvageReward;
                Debug.Log($"[MilestoneManager] +{milestone.salvageReward} salvage");
            }

            // Notify tutorial
            if (TutorialManager.Instance != null)
            {
                TutorialManager.Instance.OnMilestoneCompleted();
            }
        }
    }
}

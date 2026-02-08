using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChallengeUI : MonoBehaviour
{
    [SerializeField] private GameObject parentPauseObjectUI;
    [SerializeField] private Transform challengeListParent;
    [SerializeField] private GameObject challengeEntryPrefab;
    [SerializeField] private Button closeButton;

    private Dictionary<string, GameObject> challengeEntries = new Dictionary<string, GameObject>();
    private FPSController fpsController;
    private bool playerWasFrozenLastFrame = false;

    private void Awake()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(ClosePanel);
    }

    private void Start()
    {
        fpsController = FindFirstObjectByType<FPSController>();
        RefreshChallenges();
        
        // Subscribe to challenge manager events
        if (ChallengeManager.Instance != null)
        {
            // Refresh UI when challenges update
            InvokeRepeating(nameof(RefreshChallenges), 1f, 1f);
        }
    }

    private void Update()
    {
        // Toggle panel open with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (parentPauseObjectUI != null && parentPauseObjectUI.activeSelf)
            {
                ClosePanel();
            }
            else
            {
                if (!playerWasFrozenLastFrame)
                    OpenPanel();
            }
        }
        playerWasFrozenLastFrame = fpsController.frozen;
    }

    private void OpenPanel()
    {
        if (parentPauseObjectUI != null)
        {
            parentPauseObjectUI.SetActive(true);
            if (fpsController != null)
                fpsController.FreezePlayer();
        }
    }

    private void ClosePanel()
    {
        if (parentPauseObjectUI != null)
        {
            parentPauseObjectUI.SetActive(false);
            if (fpsController != null)
                fpsController.UnfreezePlayer();
        }
    }

    private void RefreshChallenges()
    {
        if (ChallengeManager.Instance == null || challengeListParent == null)
            return;

        var activeChallenges = ChallengeManager.Instance.GetActiveChallenges();
        
        // Clear old entries that are no longer active
        List<string> toRemove = new List<string>();
        foreach (var kvp in challengeEntries)
        {
            if (!activeChallenges.Exists(c => c.challengeId == kvp.Key))
            {
                Destroy(kvp.Value);
                toRemove.Add(kvp.Key);
            }
        }
        foreach (var id in toRemove)
            challengeEntries.Remove(id);

        // Add or update entries
        foreach (var challenge in activeChallenges)
        {
            if (!challengeEntries.ContainsKey(challenge.challengeId))
            {
                CreateChallengeEntry(challenge);
            }
            else
            {
                UpdateChallengeEntry(challenge);
            }
        }
    }

    private void CreateChallengeEntry(ChallengeData challenge)
    {
        if (challengeEntryPrefab == null || challengeListParent == null)
            return;

        GameObject entry = Instantiate(challengeEntryPrefab, challengeListParent);
        challengeEntries[challenge.challengeId] = entry;
        
        UpdateChallengeEntry(challenge);
    }

    private void UpdateChallengeEntry(ChallengeData challenge)
    {
        if (!challengeEntries.ContainsKey(challenge.challengeId))
            return;

        GameObject entry = challengeEntries[challenge.challengeId];
        var texts = entry.GetComponentsInChildren<TextMeshProUGUI>();
        
        if (texts.Length >= 2)
        {
            // Title
            texts[0].text = challenge.title;
            
            // Progress
            int current = ChallengeManager.Instance.GetChallengeProgress(challenge.challengeId);
            texts[1].text = $"{current}/{challenge.targetCount}";
            
            // Mark completed
            if (current >= challenge.targetCount)
            {
                texts[0].color = Color.green;
                texts[1].text = "COMPLETE";
            }
        }
    }
}

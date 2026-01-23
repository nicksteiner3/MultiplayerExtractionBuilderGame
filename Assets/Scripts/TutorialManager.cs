using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public enum TutorialStep
    {
        Welcome,
        PlaceReactor,
        StartReactor,
        PlaceFabricator,
        CraftDash,
        EquipDash,
        Deploy,
        CompleteChallenge,
        CompleteMilestone,
        Completed
    }

    private TutorialStep currentStep = TutorialStep.Welcome;

    [Header("UI References")]
    public TMP_Text objectiveText;
    public GameObject objectivePanel;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        // Load saved progress
        if (SessionState.Instance != null)
        {
            currentStep = (TutorialStep)SessionState.Instance.tutorialStep;
        }

        UpdateObjectiveUI();
        
        // Register for scene load events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if player returned from first deploy
        if (SessionState.Instance != null && currentStep == TutorialStep.Deploy && SessionState.Instance.returnedFromRun)
        {
            OnReturnFromDeploy();
        }
    }

    private void Start()
    {
        if (currentStep == TutorialStep.Welcome)
        {
            ShowToast("Welcome to your ship. Let's get started.");
            Invoke(nameof(StartTutorial), 2f);
        }
    }

    private void StartTutorial()
    {
        AdvanceStep(TutorialStep.PlaceReactor);
    }

    public void OnReactorPlaced()
    {
        if (currentStep == TutorialStep.PlaceReactor)
        {
            ShowToast("Reactor placed! Now start it to bring systems online.");
            AdvanceStep(TutorialStep.StartReactor);
        }
    }

    public void OnReactorStarted()
    {
        if (currentStep == TutorialStep.StartReactor)
        {
            ShowToast("Power online! Now place a Fabricator.");
            AdvanceStep(TutorialStep.PlaceFabricator);
        }
    }

    public void OnFabricatorPlaced()
    {
        if (currentStep == TutorialStep.PlaceFabricator)
        {
            ShowToast("Fabricator placed! Now craft a Dash ability.");
            AdvanceStep(TutorialStep.CraftDash);
        }
    }

    public void OnDashCrafted()
    {
        if (currentStep == TutorialStep.CraftDash)
        {
            ShowToast("Dash crafted! Now equip it at the Equipment Terminal.");
            AdvanceStep(TutorialStep.EquipDash);
        }
    }

    public void OnDashEquipped()
    {
        if (currentStep == TutorialStep.EquipDash)
        {
            ShowToast("Dash equipped! Launch your first extraction from the Launch Terminal.");
            AdvanceStep(TutorialStep.Deploy);
        }
    }

    public void OnDeployStarted()
    {
        if (currentStep == TutorialStep.Deploy)
        {
            ShowToast("Deploying to the field. Good luck.");
            // Don't advance yet - wait for return
        }
    }

    public void OnReturnFromDeploy()
    {
        if (currentStep == TutorialStep.Deploy)
        {
            ShowToast("Welcome back! Now let's work toward your first milestone.");
            AdvanceStep(TutorialStep.CompleteChallenge);
            
            // Notify challenge system that player survived
            if (ChallengeManager.Instance != null)
            {
                ChallengeManager.Instance.OnDeploySurvived();
            }
        }
    }

    public void OnChallengeCompleted(string challengeId = "")
    {
        Debug.Log($"[TutorialManager] OnChallengeCompleted: {challengeId}");
        // Challenges complete as they're done; only advance to milestone step 
        // once ALL challenges are complete (handled by MilestoneManager)
        ShowToast("Challenge complete! Work toward your first milestone.");
    }

    public void OnMilestoneCompleted()
    {
        if (currentStep == TutorialStep.CompleteMilestone)
        {
            ShowToast("First milestone achieved! You're ready to build your ship.");
            AdvanceStep(TutorialStep.Completed);
        }
    }

    private void AdvanceStep(TutorialStep nextStep)
    {
        currentStep = nextStep;
        
        // Save progress
        if (SessionState.Instance != null)
        {
            SessionState.Instance.tutorialStep = (int)currentStep;
        }

        UpdateObjectiveUI();
    }

    private void UpdateObjectiveUI()
    {
        if (objectiveText == null) return;

        string objective = currentStep switch
        {
            TutorialStep.Welcome => "Welcome to your ship",
            TutorialStep.PlaceReactor => "Place a Reactor to generate power",
            TutorialStep.StartReactor => "Start the Reactor to generate power",
            TutorialStep.PlaceFabricator => "Place a Fabricator to craft abilities",
            TutorialStep.CraftDash => "Craft a Dash ability at the Fabricator",
            TutorialStep.EquipDash => "Equip Dash at the Equipment Terminal",
            TutorialStep.Deploy => "Deploy to the field",
            TutorialStep.CompleteChallenge => "Complete your first challenge",
            TutorialStep.CompleteMilestone => "Complete your first milestone",
            TutorialStep.Completed => "Tutorial complete!",
            _ => ""
        };

        objectiveText.text = objective;

        // Keep panel visible for testing (comment out to auto-hide on completion)
        // if (objectivePanel != null)
        // {
        //     objectivePanel.SetActive(currentStep != TutorialStep.Completed);
        // }
    }

    private void ShowToast(string message)
    {
        Debug.Log($"[Tutorial Toast] {message}");
        // TODO: Implement actual toast UI
    }

    public TutorialStep GetCurrentStep()
    {
        return currentStep;
    }

    public bool IsTutorialActive()
    {
        return currentStep != TutorialStep.Completed;
    }
}

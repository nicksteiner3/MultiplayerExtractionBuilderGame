using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public enum TutorialStep
    {
        Welcome,
        PlaceReactor,
        StartReactor,
        PlaceFabricator,
        InteractFabricator,
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

    private void Start()
    {
        // Load saved progress
        if (SessionState.Instance != null)
        {
            currentStep = (TutorialStep)SessionState.Instance.tutorialStep;
        }

        UpdateObjectiveUI();
        
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
            ShowToast("Fabricator placed! Interact with it to craft abilities.");
            AdvanceStep(TutorialStep.InteractFabricator);
        }
    }

    public void OnFabricatorInteracted()
    {
        if (currentStep == TutorialStep.InteractFabricator)
        {
            ShowToast("First 10 minutes complete! Next: craft abilities.");
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
            TutorialStep.InteractFabricator => "Interact with the Fabricator terminal",
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

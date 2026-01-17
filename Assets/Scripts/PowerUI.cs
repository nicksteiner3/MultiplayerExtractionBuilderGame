using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// UI panel for displaying power status and managing the reactor.
/// </summary>
public class PowerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI fuelText;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Button refuelButton;
    [SerializeField] private Button toggleGeneratorButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Image powerBar;
    [SerializeField] private Image fuelBar;

    private Reactor reactor;
    private PowerManager powerManager;
    private FPSController fpsController;

    private void Awake()
    {
        powerManager = PowerManager.Instance;
        fpsController = FindFirstObjectByType<FPSController>();

        if (refuelButton != null)
            refuelButton.onClick.AddListener(OnRefuelClicked);

        if (toggleGeneratorButton != null)
            toggleGeneratorButton.onClick.AddListener(OnToggleGeneratorClicked);

        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseClicked);
    }

    /// <summary>
    /// Set which reactor this UI should manage.
    /// Called by ReactorTerminal when opening the UI.
    /// </summary>
    public void SetReactor(Reactor reactor)
    {
        this.reactor = reactor;
    }

    private void OnEnable()
    {
        // Freeze player when UI opens
        if (fpsController != null)
            fpsController.FreezePlayer();
    }

    private void OnDisable()
    {
        // Unfreeze player when UI closes
        if (fpsController != null)
            fpsController.UnfreezePlayer();
    }

    private void Update()
    {
        UpdateDisplay();

        // Check for window closure (ESC key)
        if (gameObject.activeSelf)
            HandleWindowClosure();
    }

    private void HandleWindowClosure()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseWindow();
        }
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    private void UpdateDisplay()
    {
        if (powerManager == null) return;

        // Update power display
        float availablePower = powerManager.GetAvailablePower();
        float totalPower = powerManager.GetTotalPower();

        if (powerText != null)
            powerText.text = $"Power: {availablePower:F1} / {totalPower:F1}";

        // Update power bar
        if (powerBar != null)
            powerBar.fillAmount = totalPower > 0 ? availablePower / totalPower : 0;

        // Update generator display
        if (reactor != null)
        {
            float currentFuel = reactor.GetCurrentFuel();
            float maxFuel = reactor.GetMaxFuel();
            bool isRunning = reactor.IsRunning();

            if (fuelText != null)
                fuelText.text = $"Fuel: {currentFuel:F1} / {maxFuel:F1}";

            if (fuelBar != null)
                fuelBar.fillAmount = maxFuel > 0 ? currentFuel / maxFuel : 0;

            if (statusText != null)
                statusText.text = isRunning ? "Generator: RUNNING" : "Generator: IDLE";

            if (toggleGeneratorButton != null)
                toggleGeneratorButton.GetComponentInChildren<TextMeshProUGUI>().text = isRunning ? "Stop Generator" : "Start Generator";
        }
    }

    private void OnRefuelClicked()
    {
        if (reactor != null)
        {
            float refueled = reactor.Refuel(50f); // Refuel 50 salvage
            Debug.Log($"Refueled {refueled}");
        }
    }

    private void OnToggleGeneratorClicked()
    {
        if (reactor != null)
        {
            reactor.SetRunning(!reactor.IsRunning());
        }
    }

    private void OnCloseClicked()
    {
        CloseWindow();
    }
}

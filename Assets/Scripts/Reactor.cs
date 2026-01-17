using UnityEngine;

/// <summary>
/// Early-game power generator that consumes salvage to produce power.
/// Represents the manual, resource-intensive phase of power generation.
/// </summary>
public class Reactor : MonoBehaviour, IPowered
{
    [Header("Generator Settings")]
    private float fuelConsumptionPerSecond = 0.5f;   // Salvage per second when running
    private float powerOutputPerSecond = 25f;      // Power generated per second
    private float maxFuel = 100f;                  // Max fuel tank capacity
    
    private float currentFuel = 0f;
    private bool isRunning = false;
    private bool hasSufficientPower = true;

    public float PowerConsumption => 0f; // Generator doesn't consume power, it produces it

    private void Update()
    {
        if (!isRunning) return;

        // Check if we have fuel to burn
        if (currentFuel <= 0)
        {
            OnPowerLost();
            return;
        }

        // Consume fuel
        float fuelUsedThisFrame = fuelConsumptionPerSecond * Time.deltaTime;
        currentFuel = Mathf.Max(0, currentFuel - fuelUsedThisFrame);
    }

    /// <summary>
    /// Attempt to refuel from stash. Returns how much was actually added.
    /// </summary>
    public float Refuel(float amount)
    {
        int salvageNeeded = Mathf.CeilToInt(amount);
        int salvageAvailable = SessionState.Instance.stashSalvage;

        if (salvageAvailable <= 0)
        {
            Debug.Log("No salvage in stash to refuel");
            return 0f;
        }

        int refuelAmount = Mathf.Min(salvageNeeded, salvageAvailable);
        float spaceInTank = maxFuel - currentFuel;

        // Convert salvage to fuel (1:1 ratio for now, can adjust)
        float fuelAdded = Mathf.Min(refuelAmount, spaceInTank);
        currentFuel += fuelAdded;
        SessionState.Instance.stashSalvage -= Mathf.RoundToInt(fuelAdded);

        Debug.Log($"Reactor: Refueled {fuelAdded} (consumed {Mathf.RoundToInt(fuelAdded)} salvage)");
        return fuelAdded;
    }

    /// <summary>
    /// Start or stop the generator.
    /// </summary>
    public void SetRunning(bool running)
    {
        if (running && currentFuel <= 0)
        {
            Debug.Log("Cannot start generator: no fuel");
            return;
        }

        // Don't re-register if already running
        if (running == isRunning)
            return;

        isRunning = running;
        if (running)
        {
            // Register power output capacity when starting
            PowerManager.Instance.AdjustProducedPower(powerOutputPerSecond);
            Debug.Log($"Reactor started: +{powerOutputPerSecond} power capacity");
            OnPowerRestored();
        }
        else
        {
            // Unregister power output capacity when stopping
            PowerManager.Instance.AdjustProducedPower(-powerOutputPerSecond);
            Debug.Log($"Reactor stopped: -{powerOutputPerSecond} power capacity");
            OnPowerLost();
        }
    }

    public float GetCurrentFuel() => currentFuel;
    public float GetMaxFuel() => maxFuel;
    public bool IsRunning() => isRunning;

    public bool HasSufficientPower()
    {
        return true; // Generator itself doesn't need power
    }

    public void OnPowerLost()
    {
        if (isRunning)
        {
            // Remove power capacity when reactor stops/runs out of fuel
            PowerManager.Instance.AdjustProducedPower(-powerOutputPerSecond);
            Debug.Log($"Reactor out of fuel: -{powerOutputPerSecond} power capacity");
        }
        isRunning = false;
        hasSufficientPower = false;
    }

    public void OnPowerRestored()
    {
        isRunning = true;
        hasSufficientPower = true;
    }
}

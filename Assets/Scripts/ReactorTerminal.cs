using UnityEngine;

/// <summary>
/// Terminal for the reactor. Allows players to refuel and toggle the generator.
/// Opens a PowerUI panel on interaction for management.
/// </summary>
public class ReactorTerminal : MonoBehaviour, IInteractable
{
    private Reactor generator;
    private PowerUI powerUI;

    private void Awake()
    {
        // Find the generator on the ship (usually a child or sibling)
        generator = GetComponent<Reactor>();
        if (generator == null)
            Debug.LogError("ReactorTerminal: No Reactor script on same GameObject");

        // Find PowerUI panel (including inactive ones)
        var powerUIArray = FindObjectsByType<PowerUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        if (powerUIArray.Length > 0)
            powerUI = powerUIArray[0];
        else
            Debug.LogError("ReactorTerminal: No PowerUI found in scene");
    }

    public void Interact()
    {
        if (powerUI == null)
        {
            Debug.LogWarning("PowerUI not available");
            return;
        }

        // Tell PowerUI to manage this reactor
        powerUI.SetReactor(generator);

        // Show/activate the PowerUI panel
        powerUI.gameObject.SetActive(true);
        Debug.Log("Reactor terminal: Opened power management UI");
    }

    /// <summary>
    /// Refuel the generator from stash (call this from a "Refuel" button or auto-refuel)
    /// </summary>
    public void RefuelGenerator(float amount = 50f)
    {
        if (generator == null) return;

        float refueled = generator.Refuel(amount);
        Debug.Log($"Refueled {refueled} fuel from stash");
    }

    /// <summary>
    /// Get generator status for UI display.
    /// </summary>
    public (float current, float max, bool running) GetGeneratorStatus()
    {
        if (generator == null)
            return (0, 0, false);

        return (generator.GetCurrentFuel(), generator.GetMaxFuel(), generator.IsRunning());
    }
}

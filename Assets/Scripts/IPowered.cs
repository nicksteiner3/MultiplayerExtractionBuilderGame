using UnityEngine;

/// <summary>
/// Interface for machines/systems that consume or require power.
/// Allows PowerManager to track and gate machine operations.
/// </summary>
public interface IPowered
{
    /// <summary>
    /// Power consumption per second while active.
    /// </summary>
    float PowerConsumption { get; }

    /// <summary>
    /// Called when power drops below consumption needs.
    /// Machine should pause operations gracefully.
    /// </summary>
    void OnPowerLost();

    /// <summary>
    /// Called when power is restored or sufficient.
    /// Machine can resume operations.
    /// </summary>
    void OnPowerRestored();

    /// <summary>
    /// Returns true if this machine has sufficient power to operate.
    /// </summary>
    bool HasSufficientPower();
}

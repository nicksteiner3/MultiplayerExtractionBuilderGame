using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance;

    [Header("MVP Settings")]
    private float totalProducedPower = 0f;  // Starts at 0; reactors add power when running
    private float totalConsumedPower = 0f;

    private Dictionary<object, float> consumers = new(); // Tracks who is drawing how much

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Requests a certain amount of power. Returns true if granted, false if not enough available.
    /// </summary>
    public bool RequestPower(object consumer, float amount)
    {
        float available = totalProducedPower - totalConsumedPower;

        if (available >= amount)
        {
            if (consumers.ContainsKey(consumer))
                consumers[consumer] = amount;
            else
                consumers.Add(consumer, amount);

            totalConsumedPower += amount;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Releases previously requested power.
    /// </summary>
    public void ReleasePower(object consumer)
    {
        if (!consumers.ContainsKey(consumer))
            return;

        totalConsumedPower -= consumers[consumer];
        consumers.Remove(consumer);
    }

    public float GetTotalPower()
    {
        return totalProducedPower;
    }

    /// <summary>
    /// Optional: Returns current available power for display or decision making.
    /// </summary>
    public float GetAvailablePower()
    {
        return totalProducedPower - totalConsumedPower;
    }

    /// <summary>
    /// Optional: Adjust total power (upgrades, generators, etc.)
    /// </summary>
    public void AdjustProducedPower(float delta)
    {
        totalProducedPower += delta;
        totalProducedPower = Mathf.Max(0, totalProducedPower);
    }
}

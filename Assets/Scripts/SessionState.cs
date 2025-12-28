using UnityEngine;

public class SessionState : MonoBehaviour
{
    public static SessionState Instance;
    public bool lastRunEndedInDeath = false;
    public bool hasEnteredPvpve = false;

    // Temporary simple model
    public int runSalvage;
    public int stashSalvage;

    public bool returnedFromRun = false;

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

    // Called by pickups
    public void AddRunSalvage(int amount)
    {
        runSalvage += amount;
        Debug.Log($"Run Salvage: {runSalvage}");
    }

    // Called by unload button
    public void UnloadInventory()
    {
        stashSalvage += runSalvage;
        runSalvage = 0;
    }
}
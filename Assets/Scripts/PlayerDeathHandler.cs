using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerDeathHandler
{
    public static void HandleDeath(GameObject player)
    {
        if (SessionState.Instance == null)
            return;

        // Lose run inventory
        InventoryManager.Instance.ClearInventory();

        // Mark that we died (important distinction)
        SessionState.Instance.lastRunEndedInDeath = true;

        // Clear abilities
        SessionState.Instance.ClearAbilities();

        // Go back to ship
        SceneManager.LoadScene("ShipScene");
    }
}
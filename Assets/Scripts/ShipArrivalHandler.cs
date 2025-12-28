using UnityEngine;

public class ShipArrivalHandler : MonoBehaviour
{
    [SerializeField] private UnloadInventoryUI unloadInventoryUI;

    void Awake()
    {
        if (unloadInventoryUI == null)
        {
            unloadInventoryUI = FindObjectOfType<UnloadInventoryUI>();
        }
    }

    void Start()
    {
        if (SessionState.Instance == null)
            return;

        if (!SessionState.Instance.lastRunEndedInDeath &&
            SessionState.Instance.hasEnteredPvpve)
        {
            unloadInventoryUI.Show();
        }

        // Reset the flag once handled
        SessionState.Instance.lastRunEndedInDeath = false;
    }
}
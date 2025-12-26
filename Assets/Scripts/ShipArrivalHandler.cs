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
        if (SessionState.Instance.returnedFromRun)
        {
            unloadInventoryUI.Show();
        }
    }
}
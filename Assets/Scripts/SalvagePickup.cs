using UnityEngine;

public class SalvagePickup : MonoBehaviour, IInteractable
{
    [SerializeField] private int amount = 1;

    public void Interact(GameObject interactor)
    {
        SessionState.Instance.AddRunSalvage(amount);
        Destroy(gameObject);
    }
}
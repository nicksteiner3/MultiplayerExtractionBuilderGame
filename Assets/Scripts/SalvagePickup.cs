using UnityEngine;

public class SalvagePickup : MonoBehaviour, IInteractable
{
    [SerializeField] private int amount = 1;

    public void Interact()
    {
        SessionState.Instance.AddSalvage(amount);
        Destroy(gameObject);
    }
}
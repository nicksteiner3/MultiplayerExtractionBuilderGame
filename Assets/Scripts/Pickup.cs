using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] private MaterialData material;
    [SerializeField] private int amount = 1;

    public void Interact()
    {
        if (material != null)
        {
            InventoryManager.Instance.AddToPlayer(material, amount);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Pickup: No material assigned!");
        }
    }
}

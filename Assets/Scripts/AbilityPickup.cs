using UnityEngine;

public class AbilityPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private AbilityData ability;

    public void Interact(GameObject interactor)
    {
        var playerAbilities = interactor.GetComponent<PlayerAbilities>();
        if (playerAbilities == null) return;

        playerAbilities.EquipAbility(ability);
        Destroy(gameObject);
    }
}
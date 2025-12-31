using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    public KeyCode interactKey = KeyCode.E;
    public LayerMask interactableLayer;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange/*, interactableLayer*/))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
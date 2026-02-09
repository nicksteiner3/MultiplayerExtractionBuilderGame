using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    public KeyCode interactKey = KeyCode.E;
    public LayerMask interactableLayer;

    private IInteractable currentInteractable;

    public IInteractable GetCurrentInteractable() => currentInteractable;

    void Update()
    {
        CheckForInteractable();

        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    void CheckForInteractable()
    {
        var cam = GetComponentInChildren<Camera>();
        Vector3 origin = cam != null ? cam.transform.position : transform.position;
        Vector3 direction = cam != null ? cam.transform.forward : transform.forward;

        Ray ray = new Ray(origin, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            currentInteractable = hit.collider.GetComponent<IInteractable>();
        }
        else
        {
            currentInteractable = null;
        }
    }

    void TryInteract()
    {
        var cam = GetComponentInChildren<Camera>();
        Vector3 origin = cam != null ? cam.transform.position : transform.position;
        Vector3 direction = cam != null ? cam.transform.forward : transform.forward;

        Ray ray = new Ray(origin, direction);
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
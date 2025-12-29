using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtractionPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private string shipSceneName = "ShipScene";

    public void Interact(GameObject interactor)
    {
        SessionState.Instance.returnedFromRun = true;
        SceneManager.LoadScene(shipSceneName);
    }
}
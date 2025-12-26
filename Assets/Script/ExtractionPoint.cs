using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtractionPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private string shipSceneName = "ShipScene";

    public void Interact()
    {
        Debug.Log("Extracting back to ship...");
        SceneManager.LoadScene(shipSceneName);
    }
}
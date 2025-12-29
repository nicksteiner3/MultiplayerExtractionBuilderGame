using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] private string pvpveSceneName = "PvPvEScene";
    public void Interact(GameObject interactor)
    {
        SessionState.Instance.hasEnteredPvpve = true;
        SessionState.Instance.lastRunEndedInDeath = false;
        SceneManager.LoadScene(pvpveSceneName);
    }
}
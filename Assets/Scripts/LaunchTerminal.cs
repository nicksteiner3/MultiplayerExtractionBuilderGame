using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] private string pvpveSceneName = "PvPvEScene";
    public void Interact()
    {
        SessionState.Instance.hasEnteredPvpve = true;
        SessionState.Instance.lastRunEndedInDeath = false;
        
        // Notify tutorial on first deploy
        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.OnDeployStarted();
        }
        
        SceneManager.LoadScene(pvpveSceneName);
    }
}
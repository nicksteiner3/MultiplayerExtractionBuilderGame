using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] private string pvpveSceneName = "PvPvEScene";

    public void Interact()
    {
        SceneManager.LoadScene(pvpveSceneName);
    }
}
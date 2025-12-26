using UnityEngine;
using TMPro;

public class UnloadInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject rootPanel;
    [SerializeField] private TextMeshProUGUI runSalvageText;

    public void Show()
    {
        rootPanel.SetActive(true);
        runSalvageText.text =
            $"Unload {SessionState.Instance.runSalvage} Salvage";

        FreezePlayer(true);
    }

    public void OnUnloadClicked()
    {
        SessionState.Instance.UnloadInventory();
        rootPanel.SetActive(false);

        FreezePlayer(false);
    }

    void FreezePlayer(bool freeze)
    {
        // Simple version
        var controller = FindObjectOfType<FPSController>();
        if (controller != null)
            controller.frozen = freeze;

        Cursor.lockState = freeze ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = freeze;
    }
}
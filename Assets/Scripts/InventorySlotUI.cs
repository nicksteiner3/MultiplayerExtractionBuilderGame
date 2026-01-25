using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private Button takeButton;

    private ContainerInventory.ContainerStack boundStack;
    private System.Action<ContainerInventory.ContainerStack> onTake;

    public void Bind(ContainerInventory.ContainerStack stack, System.Action<ContainerInventory.ContainerStack> takeCallback)
    {
        boundStack = stack;
        onTake = takeCallback;
        Refresh();

        if (takeButton != null)
        {
            takeButton.onClick.RemoveAllListeners();
            takeButton.onClick.AddListener(() => OnTakeClicked());
        }
    }

    private void Refresh()
    {
        if (label == null || boundStack == null || boundStack.material == null)
            return;
        label.text = $"{boundStack.material.materialName} x{boundStack.amount}";
    }

    private void OnTakeClicked()
    {
        if (boundStack == null) return;
        onTake?.Invoke(boundStack);
    }
}

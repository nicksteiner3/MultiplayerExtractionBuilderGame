using UnityEngine;
using TMPro;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inventoryText;
    private static InventoryUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        UpdateInventoryDisplay();
    }

    private void UpdateInventoryDisplay()
    {
        var inventory = InventoryManager.Instance.GetPlayerInventory();
        if (inventory.Count == 0)
        {
            inventoryText.text = "Inventory: Empty";
            return;
        }

        var displayLines = inventory.Select(stack => 
            $"{stack.material.materialName}: {stack.amount}").ToList();
        inventoryText.text = "Inventory:\n" + string.Join("\n", displayLines);
    }
}
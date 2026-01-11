using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private TMP_Text costText;

    private BuildingData building;
    private ShipBuildingUI parentUI;

    public void Init(BuildingData building, ShipBuildingUI parent)
    {
        this.building = building;
        parentUI = parent;
        // Ensure TMP fields are assigned (try auto-find if inspector not wired)
        if (label == null)
        {
            label = GetComponentInChildren<TMP_Text>();
            if (label == null)
                Debug.LogWarning("BuildingButton: label not assigned and no TMP_Text child found.", gameObject);
        }

        if (costText == null)
        {
            var all = GetComponentsInChildren<TMP_Text>();
            foreach (var t in all)
            {
                if (t != label)
                {
                    costText = t;
                    break;
                }
            }
            if (costText == null)
                Debug.LogWarning("BuildingButton: costText not assigned and no secondary TMP_Text child found.", gameObject);
        }

        if (building != null)
        {
            Debug.Log($"BuildingButton.Init: {building.machineName}", gameObject);
            if (label != null) label.text = building.machineName;
            if (costText != null) costText.text = $"{building.salvageCost} Salvage";
        }

        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => parentUI.OnBuildingSelected(building));
        }
    }
}

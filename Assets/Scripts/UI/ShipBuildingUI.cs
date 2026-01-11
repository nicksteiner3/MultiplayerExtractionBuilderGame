using UnityEngine;
using TMPro;

public class ShipBuildingUI : MonoBehaviour
{
    [SerializeField] private GameObject buildingUiPanel;
    [SerializeField] private Transform buildingButtonsContainer;
    [SerializeField] private GameObject buildingButtonPrefab;
    [SerializeField] private string resourcesPath = "Buildings"; // Folder under Resources with BuildingData assets

    private void OnEnable()
    {
        PopulateBuildingList();
    }

    public void OpenBuildingMenu()
    {
        if (buildingUiPanel != null)
        {
            buildingUiPanel.SetActive(true);
            
            var controller = FindFirstObjectByType<FPSController>();
            if (controller != null)
                controller.FreezePlayer();
        }
    }

    public void CloseBuildingMenu()
    {
        if (buildingUiPanel != null)
            buildingUiPanel.SetActive(false);

        var controller = FindFirstObjectByType<FPSController>();
        if (controller != null)
            controller.UnfreezePlayer();
    }

    private void PopulateBuildingList()
    {
        if (buildingButtonsContainer == null || buildingButtonPrefab == null)
            return;

        // Clear existing
        for (int i = buildingButtonsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(buildingButtonsContainer.GetChild(i).gameObject);
        }

        // Load all BuildingData from Resources
        var buildings = Resources.LoadAll<BuildingData>(resourcesPath);
        if (buildings == null || buildings.Length == 0)
            return;

        // Create button for each building
        int index = 1;
        foreach (var building in buildings)
        {
            var buttonGo = Instantiate(buildingButtonPrefab, buildingButtonsContainer);
            var btn = buttonGo.GetComponent<BuildingButton>();
            if (btn != null)
            {
                btn.Init(building, this);
            }

            // Position vertically
            var rt = buttonGo.GetComponent<RectTransform>();
            if (rt != null)
            {
                var anchored = rt.anchoredPosition;
                anchored.y = -30f * index;
                rt.anchoredPosition = anchored;
            }

            index++;
        }
    }

    public void OnBuildingSelected(BuildingData building)
    {
        // Close menu and enter placement mode
        CloseBuildingMenu();
        ShipBuilder.Instance.EnterPlacementMode(building);
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShipBuildingUI : MonoBehaviour
{
    [SerializeField] private GameObject buildingUiPanel;
    [SerializeField] private Transform buildingButtonsContainer;
    [SerializeField] private VerticalLayoutGroup buildingListLayout;
    [SerializeField] private GameObject buildingButtonPrefab;
    [SerializeField] private string resourcesPath = "Buildings"; // Folder under Resources with BuildingData assets
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseBuildingMenu);
    }

    private void OnEnable()
    {
        PopulateBuildingList();
    }

    private void Update()
    {
        if (buildingUiPanel != null && buildingUiPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseBuildingMenu();
        }
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
        if (buildingListLayout == null || buildingButtonPrefab == null)
            return;

        Transform container = buildingListLayout.transform;

        // Configure layout group if assigned
        buildingListLayout.childControlHeight = false;
        buildingListLayout.childControlWidth = false;
        buildingListLayout.childForceExpandHeight = false;
        buildingListLayout.childForceExpandWidth = false;
        buildingListLayout.spacing = 10f;
        buildingListLayout.padding = new RectOffset(10, 10, 10, 10);

        // Clear existing (only dynamic building buttons)
        for (int i = container.childCount - 1; i >= 0; i--)
        {
            var child = container.GetChild(i);
            if (child.GetComponent<BuildingButton>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        // Load all BuildingData from Resources
        var buildings = Resources.LoadAll<BuildingData>(resourcesPath);
        if (buildings == null || buildings.Length == 0)
            return;

        // Create button for each building
        foreach (var building in buildings)
        {
            var buttonGo = Instantiate(buildingButtonPrefab, container);
            var btn = buttonGo.GetComponent<BuildingButton>();
            if (btn != null)
            {
                btn.Init(building, this);
            }
        }
    }

    public void OnBuildingSelected(BuildingData building)
    {
        // Close menu and enter placement mode
        CloseBuildingMenu();
        ShipBuilder.Instance.EnterPlacementMode(building);
    }
}

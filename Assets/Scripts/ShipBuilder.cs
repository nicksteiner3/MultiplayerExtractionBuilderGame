using UnityEngine;

public class ShipBuilder : MonoBehaviour
{
    public static ShipBuilder Instance { get; private set; }

    [Header("Placement")]
    [SerializeField] private LayerMask placementLayer; // Layer to raycast for placement points
    [SerializeField] private float placementRange = 100f;

    private BuildingData currentBuildingData;
    private bool inPlacementMode = false;
    private GameObject previewInstance;
    private FPSController fpsController;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        fpsController = FindFirstObjectByType<FPSController>();
    }

    private void Update()
    {
        if (!inPlacementMode) return;

        // Show preview at raycast position
        UpdatePreview();

        // Place on click
        if (Input.GetMouseButtonDown(0))
        {
            TryPlace();
        }

        // Cancel with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelPlacement();
        }
    }

    public void EnterPlacementMode(BuildingData buildingData)
    {
        if (buildingData == null)
            return;

        currentBuildingData = buildingData;
        inPlacementMode = true;

        // Create preview
        if (previewInstance == null && buildingData.prefab != null)
        {
            previewInstance = Instantiate(buildingData.prefab);
            // Disable colliders/interactables on preview
            SetPreviewMode(previewInstance, true);
        }
    }

    private void UpdatePreview()
    {
        if (previewInstance == null) return;

        Camera cam = null;
        if (fpsController != null)
            cam = fpsController.GetComponentInChildren<Camera>();
        if (cam == null)
            cam = Camera.main;

        if (cam == null)
        {
            // No camera available; hide preview and skip
            previewInstance.SetActive(false);
            return;
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, placementRange, placementLayer))
        {
            previewInstance.transform.position = hit.point;
            previewInstance.SetActive(true);
        }
        else
        {
            previewInstance.SetActive(false);
        }
    }

    private void TryPlace()
    {
        if (previewInstance == null || currentBuildingData == null)
            return;

        // Check salvage cost
        if (SessionState.Instance.stashSalvage < currentBuildingData.salvageCost)
        {
            Debug.LogWarning("Not enough salvage to build!");
            return;
        }

        // Consume salvage
        SessionState.Instance.stashSalvage -= currentBuildingData.salvageCost;

        // Place actual machine
        var actualMachine = Instantiate(currentBuildingData.prefab, previewInstance.transform.position, Quaternion.identity);
        SetPreviewMode(actualMachine, false);
        DontDestroyOnLoad(actualMachine);

        // Notify tutorial if Reactor or Fabricator placed
        if (TutorialManager.Instance != null)
        {
            if (actualMachine.GetComponent<Reactor>() != null)
            {
                TutorialManager.Instance.OnReactorPlaced();
            }
            else if (actualMachine.GetComponent<FabricatorMachine>() != null)
            {
                TutorialManager.Instance.OnFabricatorPlaced();
            }
        }

        // End placement
        CancelPlacement();
    }

    public void CancelPlacement()
    {
        inPlacementMode = false;

        if (previewInstance != null)
            Destroy(previewInstance);
        previewInstance = null;

        currentBuildingData = null;
    }

    private void SetPreviewMode(GameObject machine, bool isPreview)
    {
        // Disable colliders and interactive components
        foreach (var collider in machine.GetComponentsInChildren<Collider>())
            collider.enabled = !isPreview;

        foreach (var interactable in machine.GetComponentsInChildren<IInteractable>())
        {
            // Could disable interaction or tint preview, for now just disable collider
        }

        // Optional: tint preview to show it's not real
        if (isPreview)
        {
            foreach (var renderer in machine.GetComponentsInChildren<Renderer>())
            {
                var mat = renderer.material;
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.5f);
            }
        }
    }
}

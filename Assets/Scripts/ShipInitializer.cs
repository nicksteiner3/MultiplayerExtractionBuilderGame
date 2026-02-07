using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ShipInitializer : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only reconstruct if this is the ship scene
        // Adjust scene name to match your ship scene name
        if (scene.name != "ShipScene") return;

        // Rebuild buildings from SessionState placements
        if (SessionState.Instance == null)
        {
            Debug.LogError("[ShipInitializer] SessionState not found!");
            return;
        }

        List<SessionState.BuildingPlacement> placements = SessionState.Instance.GetPlacedBuildings();
        Debug.Log($"[ShipInitializer] Reconstructing {placements.Count} buildings");

        foreach (var placement in placements)
        {
            // Load building prefab from Resources
            BuildingData buildingData = Resources.Load<BuildingData>($"Buildings/{placement.buildingName}");
            if (buildingData == null)
            {
                Debug.LogError($"[ShipInitializer] Could not load building data for ID: {placement.buildingName}");
                continue;
            }

            // Instantiate the building
            GameObject building = Instantiate(buildingData.prefab, placement.position, placement.rotation);
            Debug.Log($"[ShipInitializer] Reconstructed {placement.buildingName} at {placement.position}");
        }
    }
}

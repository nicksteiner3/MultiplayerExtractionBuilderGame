using UnityEngine;

[CreateAssetMenu(menuName = "Building/Machine")]
public class BuildingData : ScriptableObject
{
    public string id; // Unique identifier for saving/loading (e.g., "Reactor", "Fabricator")
    public string machineName;
    public int salvageCost;
    public GameObject prefab; // Machine to instantiate
}

using UnityEngine;

[CreateAssetMenu(menuName = "Building/Machine")]
public class BuildingData : ScriptableObject
{
    public string machineName;
    public int salvageCost;
    public GameObject prefab; // Machine to instantiate
}

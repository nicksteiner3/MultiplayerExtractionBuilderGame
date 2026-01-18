using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Machine")]
public class MachineData : ScriptableObject
{
    public string machineName;
    [TextArea(2, 4)]
    public string description;
    
    [Header("Processing")]
    public MaterialData inputMaterial;
    public int inputAmount = 1;
    public MaterialData outputMaterial;
    public int outputAmount = 1;
    public float processTime = 10f;
    
    [Header("Power")]
    public float powerConsumption = 10f;
    
    [Header("Visuals")]
    public GameObject machinePrefab;
    public Sprite icon;
}

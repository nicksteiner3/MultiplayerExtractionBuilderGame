using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Material")]
public class MaterialData : ScriptableObject
{
    public string materialName;
    [TextArea(2, 4)]
    public string description;
    
    public enum MaterialTier
    {
        Tier0_Raw,
        Tier1_Processed,
        Tier2_Component,
        Tier3_Advanced
    }
    
    public MaterialTier tier;
    public Sprite icon;
}

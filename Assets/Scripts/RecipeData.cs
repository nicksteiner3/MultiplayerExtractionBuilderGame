using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Recipe")]
public class RecipeData : ScriptableObject
{
    public string recipeName;

    public float craftTime;

    public List<ItemCost> inputs;
    public ItemOutput outputs;

    public AbilityUIItem prefabUIItem;
}
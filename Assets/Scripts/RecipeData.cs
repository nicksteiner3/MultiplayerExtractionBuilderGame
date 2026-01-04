using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Recipe")]
public class RecipeData : ScriptableObject
{
    public string recipeName;

    public float craftTime;
    public float powerRequired;

    public List<ItemCost> inputs;
    public List<ItemOutput> outputs;
}
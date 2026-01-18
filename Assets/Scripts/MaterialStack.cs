using UnityEngine;

public struct MaterialStack
{
    public MaterialData material;
    public int quantity;

    public MaterialStack(MaterialData material, int quantity)
    {
        this.material = material;
        this.quantity = quantity;
    }

    public override string ToString()
    {
        return $"{material.materialName} x{quantity}";
    }
}

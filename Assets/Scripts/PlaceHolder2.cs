using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/PlaceHolder2")]
public class PlaceHolder2 : AbilityData
{
    public float dashForce = 10f;

    public override void Activate(GameObject player)
    {
        var controller = player.GetComponent<CharacterController>();
        Vector3 dashDir = player.transform.forward;
        controller.Move(dashDir * dashForce);
    }
}
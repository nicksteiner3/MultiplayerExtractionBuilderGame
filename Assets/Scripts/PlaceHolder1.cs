using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/PlaceHolder1")]
public class PlaceHolder1 : AbilityData
{
    public float dashForce = 10f;

    public override void Activate(GameObject player)
    {
        var controller = player.GetComponent<CharacterController>();
        Vector3 dashDir = player.transform.forward;
        controller.Move(dashDir * dashForce);
    }
}
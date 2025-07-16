using UnityEngine;

[CreateAssetMenu(fileName = "PickupXPRadiusUpgrade", menuName = "Upgrades/Pickup XP Radius")]
// set maxApplyCount = 5
public class PickupXPRadius : Upgrade
{
    float pickupRadiusIncrement = 1f;
    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);
        player.xpPickUpRadius += pickupRadiusIncrement;
    }

}

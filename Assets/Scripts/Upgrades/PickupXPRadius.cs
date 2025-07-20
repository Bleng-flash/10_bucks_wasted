using UnityEngine;

[CreateAssetMenu(fileName = "PickupXPRadiusUpgrade", menuName = "Upgrades/Pickup XP Radius")]
// set maxApplyCount = 5
public class PickupXPRadius : Upgrade
{
    [SerializeField] private float pickupRadiusIncrement = 0.75f;
    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);
        player.xpPickUpRadius += pickupRadiusIncrement;
    }

}

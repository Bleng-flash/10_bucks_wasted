using UnityEngine;

[CreateAssetMenu(fileName = "RepulsiveFieldUpgrade", menuName = "Upgrades/Repulsive Field")]
public class RepulsiveFieldUpgrade : Upgrade
{

    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);
        player.UnlockOrUpgradeAttack("RepulsiveField");
    }
}

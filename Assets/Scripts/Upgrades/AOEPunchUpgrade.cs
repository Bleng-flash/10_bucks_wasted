using UnityEngine;

[CreateAssetMenu(fileName = "AOEPunchUpgrade", menuName = "Upgrades/AOE Punch")]
public class AOEPunchUpgrade : Upgrade
{
    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);
        player.UnlockOrUpgradeAttack("AOEPunch");
    }
}

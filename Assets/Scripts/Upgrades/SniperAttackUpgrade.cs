using UnityEngine;

[CreateAssetMenu(fileName = "SniperAttackUpgrade", menuName = "Upgrades/Sniper Attack")]
public class SniperAttackUpgrade : Upgrade
{
    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);
        player.UnlockOrUpgradeAttack("SniperAttack");
    }
}

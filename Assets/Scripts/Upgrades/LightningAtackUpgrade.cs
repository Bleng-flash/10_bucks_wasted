using UnityEngine;

[CreateAssetMenu(fileName = "LightningAttackUpgrade", menuName = "Upgrades/Lightning Attack")]
public class LightningAttackUpgrade : Upgrade
{
    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);
        player.UnlockOrUpgradeAttack("LightningAttack");
    }
}

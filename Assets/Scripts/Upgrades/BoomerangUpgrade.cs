using UnityEngine;

[CreateAssetMenu(fileName = "BoomerangAttackUpgrade", menuName = "Upgrades/Boomerang Attack")]
public class BoomerangAttackUpgrade : Upgrade
{
    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);
        player.UnlockOrUpgradeAttack("BoomerangAttack");
    }
}

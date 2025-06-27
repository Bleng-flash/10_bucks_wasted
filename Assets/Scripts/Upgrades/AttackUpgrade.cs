using UnityEngine;

[CreateAssetMenu(fileName = "AttackUpgrade", menuName = "Upgrades/ATK")]
public class AttackUpgrade : Upgrade
{
    public float extraATK;
    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);
        player.IncreaseATKBy(extraATK);
    }
}

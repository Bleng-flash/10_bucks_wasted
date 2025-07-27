using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MaxHealthUpgrade", menuName = "Upgrades/Max Health")]
public class MaxHealthUpgrade : Upgrade
{
    public float extraMaxHP = 100f;

    // Takes player current health% = HP/maxHP and maintains this same health%
    // and increases maxHP (and thus HP accordingly)
    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);
        float extraHP = (float)Math.Round(player.GetHealthPercentage() * extraMaxHP, 2);
        // rounds to 2dp 
        player.IncreaseMaxHPBy(extraMaxHP);
        player.IncreaseHealthBy(extraHP);
    }
}

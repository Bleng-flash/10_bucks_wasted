using System;
using UnityEngine;

// Restores a given percentage of lost health
[CreateAssetMenu(fileName = "RestoreLostHealth", menuName = "Upgrades/Restore Lost Health")]
public class RestoreLostHealth : Upgrade
{
    public float percentageRestored = 0.5f; // {0.25, 0.5, 1}

    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);
        float extraHP = (float)Math.Round(percentageRestored * player.GetLostHealth(), 2);
        player.IncreaseHealthBy(extraHP);
    }
}

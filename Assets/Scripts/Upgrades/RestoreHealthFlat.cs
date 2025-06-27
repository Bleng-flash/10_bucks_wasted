using UnityEngine;

[CreateAssetMenu(fileName = "RestoreHealthFlat", menuName = "Upgrades/Restore Health Flat")]
public class RestoreHealthFlat : Upgrade
{
    public float extraHP;

    public override void ApplyUpgrade(PlayerScript player)
    {
        base.ApplyUpgrade(player);
        player.IncreaseHealthBy(extraHP);
    }
}

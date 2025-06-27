using UnityEngine;

[CreateAssetMenu(fileName = "AttackUpgrade", menuName = "Upgrades/ATK")]
public class AttackUpgrade : Upgrade
{
    public float extraATK;
    public override void ApplyUpgrade(PlayerScript player)
    {
        player.IncreaseATKBy(extraATK);
        Debug.Log($"Applied {upgradeName}, +{extraATK}ATK");
    }
}

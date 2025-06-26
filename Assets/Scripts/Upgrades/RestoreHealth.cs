using UnityEngine;

[CreateAssetMenu(fileName = "RestoreHealth", menuName = "Upgrades/RestoreHealth")]
public class RestoreHealth : Upgrade
{
    public float bonusHP;

    public override void ApplyUpgrade(PlayerScript player)
    {
        // player.IncreaseHealthBy(bonusHP); 
        Debug.Log("Health upgraded by " + bonusHP);
    }
}

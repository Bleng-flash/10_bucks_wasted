using UnityEngine;

/* 
    Player inherits from Entity
    In addition to stats and attacks provided by Entity, player has player health bar (KIV) as well
*/

public class PlayerScript : Entity
{
    // At start, initialise player.stats 
    // Initialise player.attacks with AOEPunch attack (starting basic attack)
    void Start()
    {
        this.stats.Initialise(this.stats.GetMaxHP(), this.stats.GetHealth(), this.stats.GetATK());
        attacks = new Attack[1];   // For now just put 1
        // add AOE punch somehow
    }

    public override void Die()
    {
        Debug.Log("You die!!");
        // Send out Death event to GameManager
        GameManager.Instance.OnPlayerDeath();
    }

    public float GetHealth()
    {
        return this.stats.GetHealth();
    }
}

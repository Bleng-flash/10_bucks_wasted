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
        this.stats.Initialise(100.0f, 100.0f, 5.0f);
        attacks = new Attack[1];   // For now just put 1
        // add AOE punch somehow
    }

    public override void Die()
    {
        Debug.Log("You die!!");
        // Send out Death event to GameManager
        GameManager.Instance.OnPlayerDeath();
    }
}

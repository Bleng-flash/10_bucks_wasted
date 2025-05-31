using Unity.VisualScripting;
using UnityEngine;

/* 
    Player inherits from Entity
    In addition to stats and attacks provided by Entity, player has player health bar (KIV) as well
*/

public class PlayerScript : Entity
{
    [SerializeField] // just for easier testing in editor
    private float maxHP;
    [SerializeField]
    private float HP;
    [SerializeField]
    private float ATK;
    private bool isDead;

    // At start, initialise player.stats with maxHP and starting ATK
    // Initialise player.attacks with AOEPunch attack (starting basic attack)
    void Start()
    {
        stats = new Attribute(maxHP, maxHP, ATK);
        attacks = new Attack[1];   // For now just put 1
        // add AOE punch somehow
    }

    public override void Die()
    {
        // Freeze player
        // Send out Death event to bring up GameOver UI
    }
}

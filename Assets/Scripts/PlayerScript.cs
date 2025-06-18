using UnityEngine;

/* 
    Player inherits from Entity
    In addition to stats and attacks provided by Entity, player has player health bar, 
    player XP bar and plaeyr XP (KIV) as well
*/

public class PlayerScript : Entity
{
    private int xpAmount;
    [SerializeField] private XpScript xpScript;
    [SerializeField] private float xpPickUpRadius = 2.0f;
    [SerializeField] private LayerMask xpLayer; 

    // At start, initialise player.stats 
    // Initialise player.attacks with AOEPunch attack (starting basic attack)
    void Start()
    {
        xpAmount = 0;
        this.stats.Initialise(this.stats.GetMaxHP(), this.stats.GetHealth(), this.stats.GetATK());
        attacks = new Attack[1];   // For now just put 1
        // add AOE punch somehow
    }

    void Update()
    {
        PickUpXp();
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

    // Overriding Takedamage to send out event whenever player receives damage
    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        GameManager.Instance.OnPlayerDamage();
    }

    // Pick up XP
    public void PickUpXp()
    {
        // Detects all objects will colliders that are in xp layer and add them to pickupXps
        Collider2D[] pickupXps = Physics2D.OverlapCircleAll(transform.position, xpPickUpRadius, xpLayer);

        foreach (Collider2D collider in pickupXps)
        {
            XpScript xp = collider.GetComponent<XpScript>();
            if (xp != null)
            {
                Debug.Log("Picked up " + xp.GetXpAmount() + " xp");
                xpAmount += xp.PickUpXp();
                Debug.Log("Current xp: " + xpAmount);
            }
        }
    }
}  

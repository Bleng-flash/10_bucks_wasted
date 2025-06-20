using UnityEngine;

/* 
    Player inherits from Entity
    In addition to stats and attacks provided by Entity, player has player health bar, 
    player XP bar and plaeyr XP (KIV) as well
*/

public class PlayerScript : Entity
{
    private int xpAmount;
    private int xpToNextLevel;
    private int level;

    [SerializeField] private XpScript xpScript;
    [SerializeField] private float xpPickUpRadius = 2.0f;
    [SerializeField] private LayerMask xpLayer;

    // At start, initialise player.stats 
    // Initialise player.attacks with AOEPunch attack (starting basic attack)
    void Start()
    {
        xpAmount = 0;
        xpToNextLevel = 100;
        level = 1;
        this.stats.Initialise(stats.GetMaxHP(), stats.GetHealth(), stats.GetATK());
        attacks = new Attack[1];   // For now just put 1
        // add AOE punch somehow
    }

    void Update()
    {
        PickUpXp();
        if (xpAmount >= xpToNextLevel)
        {
            LevelUp();
            Debug.Log("Next xp required to level up: " + xpToNextLevel);
            Debug.Log("Current level: " + level);
        }
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
        GameManager.Instance.OnPlayerDamage(stats.GetHealth(), stats.GetMaxHP());
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

    public void LevelUp()
    {
        xpAmount -= xpToNextLevel;
        level++;
        xpToNextLevel = (int) (xpToNextLevel * 1.5);    // Temporary formula for xp required to level up
    }
}  

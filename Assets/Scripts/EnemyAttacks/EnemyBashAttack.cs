using Unity.VisualScripting;
using UnityEngine;

// Simple attack that deals damage when enemy comes into contact with player

public class EnemyBashAttack : NonAutoAttack
{
    private bool playerInRange = false;
    private PlayerScript player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise(this.cooldown, this.damage);
    }

    // Enemy attacks are non-auto attacks
    protected override bool TargetInRange()
    {
        return playerInRange;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            Debug.Log("Collision!");
            playerInRange = true;
            player = collision.gameObject.GetComponent<PlayerScript>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            playerInRange = false;
        }
    }

    protected override void PerformAttack()
    {
        Debug.Log("Bash Attack Player for " + damage + " damage!");
        player.TakeDamage(this.damage);
    }
}

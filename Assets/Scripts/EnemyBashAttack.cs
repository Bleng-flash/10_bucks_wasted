using Unity.VisualScripting;
using UnityEngine;

// Simple attack that deals damage when enemy comes into contact with player

public class EnemyBashAttack : Attack
{
    private bool playerInRange = false;
    private PlayerScript player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise(this.cooldown, this.damage, true);
    }

    protected override bool CanAttack()
    {
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision!");
            playerInRange = true;
            player = collision.gameObject.GetComponent<PlayerScript>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    protected override void PerformAttack()
    {
        if (playerInRange)
        {
            Debug.Log("Hit Player for " + damage + " damage!");
            player.TakeDamage(this.damage);
        }
    }
}

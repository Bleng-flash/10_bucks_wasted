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
        Entity ownerEntity = GetComponent<Entity>();
        if (ownerEntity == null)
        {
            Debug.LogError("Entity component missing on EnemyBashAttack GameObject.");
            return;
        }
        Initialise(this.cooldown, this.damage, true, ownerEntity);
    }

    // Enemy attacks are non-auto attacks
    protected override bool CanAttack()
    {
        return playerInRange;
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
        Debug.Log("Hit Player for " + damage + " damage!");
        player.TakeDamage(this.damage);
    }
}

using Unity.VisualScripting;
using UnityEngine;

// Simple attack that deals damage when enemy comes into contact with player

public class EnemyBashAttack : NonAutoAttack
{
    private bool playerInRange = false;
    private PlayerScript player;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise(this.cooldown, this.damage);
        animator = GetComponentInParent<Animator>();
    }

    // Enemy attacks are non-auto attacks
    protected override bool TargetInRange()
    {
        return playerInRange;
    }

    // This method is called when its parent collider's CollisionEnter2D method is invoked
    public void OnParentCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Check for collision!");
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            Debug.Log("Collision!");
            playerInRange = true;
            player = collision.gameObject.GetComponent<PlayerScript>();
        }
    }

    // This method is called when its parent collider's CollisionExit2D method is invoked
    public void OnParentCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Check for leaving collision!");
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            playerInRange = false;
        }
    }

    protected override void PerformAttack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("Bash Attack Player for " + damage + " damage!");
        player.TakeDamage(this.damage);
    }
}

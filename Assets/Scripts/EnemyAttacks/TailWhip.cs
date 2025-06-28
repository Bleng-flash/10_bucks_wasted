using System.Collections;
using UnityEngine;

public class TailWhip : NonAutoAttack
{
    [SerializeField] private float attackRadius = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise(this.cooldown, this.damage);
    }

    // TailWhip is similar to AOEpunch, just 360 degrees 
    protected override void PerformAttack()
    {
        Debug.Log("Starting attack!");
        StartCoroutine(DelayedAttack());

    }
    private IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(delayToAttack);     // This delays the code below until time is over, while letting other scripts run

        Debug.Log("Attacking player!");
        
        // Detects player
        Collider2D playerObject = Physics2D.OverlapCircle(owner.transform.position, attackRadius, targetLayer);
        if (playerObject != null)
        {
            PlayerScript player = playerObject.GetComponent<PlayerScript>();
            if (player != null)
            {
                Debug.Log($"TailWhip {playerObject.name} with {this.damage} damage!");
                player.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("PlayerScript missing on: " + playerObject.name);
            }
        }
        else
        {
            Debug.Log("Player escaped before taking damage!");
        }
    }

    protected override bool TargetInRange()
    {
        Collider2D playerObject = Physics2D.OverlapCircle(owner.transform.position, attackRadius, targetLayer);
        return playerObject != null;
    }
}

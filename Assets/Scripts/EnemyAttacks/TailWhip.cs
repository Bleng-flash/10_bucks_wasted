using System.Collections;
using UnityEngine;

public class TailWhip : NonAutoAttack
{
    [SerializeField] private float attackRadius = 2.0f;
    [SerializeField] private Animator tailAnimator;
    [SerializeField] private TailWhipOverlay tailOverlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise(this.cooldown, this.damage);
    }

    // TailWhip is similar to AOEpunch, just 360 degrees 
    protected override void PerformAttack()
    {
        animator.SetTrigger("TailWhip");
        Debug.Log("Starting attack!");
        StartCoroutine(DelayedAttack());

    }

    // Using Coroutine for delayed attack (meaning damage is taken by player only after delayToAttack ends)
    private IEnumerator DelayedAttack()
    {
        EnemyMovement enemyMovement = GetComponentInParent<EnemyMovement>();
        enemyMovement?.SetAttacking(true);      // Tells enemyMovement to stop while enemy attacking

        yield return new WaitForSeconds(delayToAttack);     // This delays the code below until time is over, while letting other scripts run

        // Before damaging player, check if enemy died between initiating attack and end of delay
        if (owner.IsDead())
        {
            Debug.Log("Enemy died before attack!");
            yield break;
        }

        Debug.Log("Attacking player!");

        // Play tail whip animation
        if (tailOverlay != null)
        {
            tailOverlay.PrepareAnimation();
            tailAnimator.Play("tail whip attack", 0, 0f);
            Debug.Log("tail whip animation playing!");
        }
        else
        {
            Debug.Log("No tailOverlay!");
        }

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

        enemyMovement?.SetAttacking(false);
    }

    protected override bool TargetInRange()
    {
        Collider2D playerObject = Physics2D.OverlapCircle(owner.transform.position, attackRadius, targetLayer);
        return playerObject != null;
    }
    public override void Recalculate()
    {
        return;
    }
}

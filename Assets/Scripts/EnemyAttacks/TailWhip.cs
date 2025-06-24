using UnityEngine;

public class TailWhip : NonAutoAttack
{
    [SerializeField] private float attackRadius = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise(this.cooldown, this.damage);
    }

    // TailWhip is similar to AOEpunch, just 360 degrees 
    protected override void PerformAttack()
    {
        // Detects player
        Collider2D playerObject = Physics2D.OverlapCircle(transform.position, attackRadius, targetLayer);

        PlayerScript player = playerObject.GetComponent<PlayerScript>();
            if (player != null)
            {
                Debug.Log($"Hit {playerObject.name} with {this.damage} damage!");
                player.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("PlayerScript missing on: " + playerObject.name);
            }
    }

    protected override bool TargetInRange()
    {
        Collider2D playerObject = Physics2D.OverlapCircle(transform.position, attackRadius, targetLayer);
        return playerObject != null;
    }
}

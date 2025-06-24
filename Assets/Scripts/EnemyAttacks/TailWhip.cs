using UnityEngine;

public class TailWhip : NonAutoAttack
{
    [SerializeField] private float attackRadius = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // TailWhip is similar to AOEpunch, just 360 degrees 
    protected override void PerformAttack()
    {
        // Detects player (Physics2D.OverlapCircleAll returns a Collider2D array)
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, attackRadius, targetLayer);

        // Should only have 1 player at most
        foreach (Collider2D collider in players)
        {
            PlayerScript player = collider.GetComponent<PlayerScript>();
            if (player != null)
            {
                Debug.Log($"Hit {collider.name} with {this.damage} damage!");
                player.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("PlayerScript missing on: " + collider.name);
            }
        }
    }
}

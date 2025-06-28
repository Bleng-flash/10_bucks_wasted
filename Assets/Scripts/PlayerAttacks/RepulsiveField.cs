using UnityEngine;

public class RepulsiveField : AutoAttack
{
    [SerializeField] private float attackRadius = 2.0f;
    [SerializeField] private float force = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise(this.cooldown, this.damage);
    }

    protected override void PerformAttack()
    {
        Vector2 attackOrigin = transform.position;

        // Detects all objects will colliders that are in enemy layer and add them to hitColliders
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackOrigin, attackRadius, targetLayer);
        Debug.Log($"Enemies hit: {hitColliders.Length}");

        foreach (Collider2D collider in hitColliders)
        {
            Rigidbody2D enemyBody = collider.GetComponent<Rigidbody2D>();
            EnemyMovement enemyMovement = collider.GetComponent<EnemyMovement>();

            if (enemyBody != null)
            {
                Debug.Log($"Pushing {collider.name} with {this.force} force!");
                Vector2 direction = (enemyBody.transform.position - transform.position).normalized;
                enemyMovement.Repel();
                enemyBody.AddForce(direction * force, ForceMode2D.Impulse);     // Pushes enemies in direction with specified force
            }
            else
            {
                Debug.LogWarning("Rigidbody2D missing on: " + collider.name);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}

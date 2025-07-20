using UnityEngine;

public class RepulsiveField : AutoAttack
{
    [SerializeField] private float attackRadius = 2.0f;
    [SerializeField] private float force = 3f; // push force
    [SerializeField] private Animator fieldAnimator;
    [SerializeField] private RepulsiveFieldOverlay fieldOverlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise(this.cooldown, this.damage);
    }

    protected override void PerformAttack()
    {
        Vector2 attackOrigin = transform.position;
        // Play repulsive field animation
        if (fieldOverlay != null)
        {
            fieldOverlay.PrepareAnimation();
            fieldAnimator.Play("repulsive field attack", 0, 0f);
            Debug.Log("Field pushing animation playing!");
        }
        else
        {
            Debug.Log("No fieldOverlay!");
        }

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

    public override void Recalculate()
    {
        return;
    }
    public override void UpgradeAttack()
    {
        int tier = upgradeData.ApplyCount; // 0 to 5 (inclusive)

        switch (tier)
        {
            case 0:
                break;
            case 1: // base case
                cooldown = 3f;
                force = 3f;
                break;
            case 2:
                force = 3.5f;
                break;
            case 3:
                cooldown = 2.5f;
                break;
            case 4:
                force = 4f;
                break;
            case 5:
                cooldown = 2f;
                break;
            default:
                Debug.LogWarning("Invalid value: applyCount for RepulsiveField");
                break;
        }

        Recalculate();
    }
}

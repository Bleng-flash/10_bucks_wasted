using UnityEngine;


// Basic starting attack for player character
public class AOEPunch : AutoAttack
{
    [SerializeField] private float attackRadius = 2.0f;
    [SerializeField] private float attackAngle = 90f;       // Half-angle — so 90 = 180° cone
    private PlayerMovement playerMovement;
    [SerializeField] private Animator punchAnimator;
    [SerializeField] private PunchOverlay punchOverlay;

    void Start()
    {
        // Debug.Log($"AOEPunch Start() — Cooldown: {this.cooldown}, Damage: {this.damage}");
        Initialise(this.cooldown, this.damage);
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    protected override void PerformAttack()
    {
        Vector2 attackOrigin = transform.position;
        Vector2 attackDirection = playerMovement.FacingDirection;     // This will point to wherever player is facing
        if (attackDirection == Vector2.zero) attackDirection = Vector2.right;   // Failsafe if vector2 is zero

        // Plays punch animation
        if (punchAnimator != null)
        {
            punchAnimator.transform.position = attackOrigin;
            punchOverlay.PrepareSlashAnimation();
            punchAnimator.Play("slash attack", 0, 0f);
        }
        else
        {
            Debug.Log("No punchAnimator found!");
        }

        // Detects all objects will colliders that are in enemy layer and add them to hitColliders
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackOrigin, attackRadius, targetLayer);

        foreach (Collider2D collider in hitColliders)
        {
            Vector2 directionToTarget = ((Vector2)collider.transform.position - attackOrigin).normalized;
            float angle = Vector2.Angle(attackDirection, directionToTarget);

            if (angle <= attackAngle)
            {
                EnemyScript enemy = collider.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    Debug.Log($"Hit {collider.name} with {this.damage} damage!");
                    enemy.TakeDamage(damage);
                }
                else
                {
                    Debug.LogWarning("EnemyScript missing on: " + collider.name);
                }
            }
        }
    }

    // Visualise attack range in scene view (not runtime)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position;
        Vector2 direction = playerMovement.FacingDirection;
        if (direction == Vector2.zero) direction = Vector2.right;

        // Draw semicircle arc
        int segments = 20;
        float step = (attackAngle * 2f) / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = -attackAngle + step * i;
            Vector3 dir = Quaternion.Euler(0, 0, angle) * direction;
            Gizmos.DrawLine(origin, origin + dir * attackRadius);
        }
    }

    protected override void OnUpdate()
    {
        DrawAOEArea();
    }

    // Draws range of AOE punch in game view (runtime)
    private void DrawAOEArea()
    {
        Vector2 origin = transform.position;
        Vector2 attackDir = playerMovement.FacingDirection;
        float angle = attackAngle;
        float radius = attackRadius;
        
        // Draw center direction
        Debug.DrawRay(origin, attackDir * radius, Color.red);

        // Draw arc with multiple lines
        int segments = 20;
        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = -angle + (2 * angle * i / segments);
            Vector2 dir = Quaternion.Euler(0, 0, currentAngle) * attackDir;
            Debug.DrawRay(origin, dir * radius, Color.yellow);
        }
    }

}

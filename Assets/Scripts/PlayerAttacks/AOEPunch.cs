using System.Collections;
using UnityEngine;


// Basic starting attack for player character
public class AOEPunch : AutoAttack
{
    [SerializeField] private float attackRadius = 2.0f;
    [SerializeField] private float attackAngle = 90f;       // Half-angle — so 90 = 180° cone
    private PlayerMovement playerMovement;
    [SerializeField] private Animator punchAnimator;
    [SerializeField] private PunchOverlay punchOverlay;
    private Vector2 originalOffset;
    private float damageMultiplier = 1f; // damage = damageMultiplier * player ATK

    void Start()
    {
        // Debug.Log($"AOEPunch Start() — Cooldown: {this.cooldown}, Damage: {this.damage}");
        Initialise(this.cooldown, this.damage);
        playerMovement = GetComponentInParent<PlayerMovement>();
        originalOffset = punchOverlay.transform.localPosition;
    }

    protected override void PerformAttack()
    {
        Vector2 attackOrigin = transform.position;
        Vector2 attackDirection = playerMovement.FacingDirection.normalized;     // This will point to wherever player is facing
        if (attackDirection == Vector2.zero) attackDirection = Vector2.right;   // Failsafe if vector2 is zero

        // Plays punch animation
        if (punchAnimator != null)
        {
            // Rotate animation to face attack direction, while keeping offset in front of player
            float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
            Vector2 rotatedOffset = Quaternion.Euler(0, 0, angle) * originalOffset;
            punchOverlay.transform.localPosition = rotatedOffset;
            punchOverlay.transform.localRotation = Quaternion.Euler(0, 0, angle);

            punchOverlay.PrepareSlashAnimation();
            punchAnimator.Play("slash attack", 0, 0f);
        }
        else
        {
            Debug.Log("No punchAnimator found!");
        }

        // Detects all objects with colliders that are in enemy layer and add them to hitColliders
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

    public override void Recalculate()
    {
        damage = Mathf.Max(0, damageMultiplier * owner.getATK());
    }

    public override void UpgradeAttack()
    {
        int tier = upgradeData.ApplyCount; // 0 to 5 (inclusive)

        switch (tier)
        {
            case 0:
                break;
            case 1: // base case
                damageMultiplier = 1f;
                cooldown = 2f;
                break;
            case 2:
                damageMultiplier = 1.5f;
                break;
            case 3:
                damageMultiplier = 2f;
                break;
            case 4:
                cooldown = 1f;
                break;
            case 5:
                damageMultiplier = 3f;
                break;
            default:
                Debug.LogWarning("Invalid value: applyCount for AOEPunch");
                break;
        }

        Recalculate();
    }

}

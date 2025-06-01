using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// Basic starting attack for player character
public class AOEPunch : Attack
{
    [SerializeField] private float AOEPunchCooldown = 2.0f; // in seconds

    [SerializeField] private float AOEPunchDamage = 10;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float attackAngle = 90f;       // Half-angle — so 90 = 180° cone
    [SerializeField] private LayerMask enemyLayer;          // Detects all objects in enemy layer
    private Vector2 currentFacingDirection = Vector2.right; // Consider adding this to Attack class instead

    void Start()
    {
        Debug.Log($"AOEPunch Start() — Cooldown: {AOEPunchCooldown}, Damage: {AOEPunchDamage}");
        Initialise(AOEPunchCooldown, AOEPunchDamage, true);
    }

    protected override bool CanAttack()
    {
        return true;
    }

    public void UpdateFacingDirection(Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            currentFacingDirection = moveInput.normalized;
        }
    }

    protected override void PerformAttack()
    {
        Vector2 attackOrigin = transform.position;
        Vector2 attackDirection = currentFacingDirection;

        // Detects all objects will colliders that are in enemy layer and add them to hitColliders
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackOrigin, attackRadius, enemyLayer);
        Debug.Log("Enemies in circle: " + hitColliders.Length);

        foreach (Collider2D collider in hitColliders)
        {
            Vector2 directionToTarget = ((Vector2)collider.transform.position - attackOrigin).normalized;
            float angle = Vector2.Angle(attackDirection, directionToTarget);

            if (angle <= attackAngle)
            {
                EnemyScript enemy = collider.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    Debug.Log($"Hit {collider.name} with {AOEPunchDamage} damage!");
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
        Vector2 direction = Application.isPlaying ? currentFacingDirection : Vector2.right;
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
        Vector2 attackDir = currentFacingDirection;
        float angle = 90f;
        float radius = 5f;

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

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Boomerang Script is the logic for a prefab of a boomerang, 
// which will be put into BoomerangAttack to spawn boomerangs
public class BoomerangScript : MonoBehaviour
{
    // Using public properties to set the private fields (instead of multiple setter methods)
    public float Speed { get; set; }
    public float MaxDistance { get; set; }
    public float Damage { get; set; }
    public float HitCooldown { get; set; }
    public LayerMask EnemyLayer { get; set; }

    private Vector2 startPosition1;
    private Vector2 startPosition2;
    private Vector2 direction;
    private Transform player;

    // Track last time enemy was hit, enemies can only be damaged by boomerang once every HitCooldown
    private Dictionary<EnemyScript, float> lastHitTime = new Dictionary<EnemyScript, float>();

    private enum BoomerangPhase
    {
        Outward,
        ReturnToPlayer1,
        Behind,
        ReturnToPlayer2
    }

    private BoomerangPhase phase = BoomerangPhase.Outward;

    public void ThrowBoomerang(Vector2 direction, Transform player)
    {
        this.direction = direction.normalized;
        this.player = player;
        startPosition1 = transform.position;
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("BoomerangScript: Player not found");
            return;
        }

        switch (phase)
        {
            case BoomerangPhase.Outward:
                transform.position += (Vector3)(direction * Speed * Time.deltaTime);

                if (Vector2.Distance(startPosition1, transform.position) >= MaxDistance)
                {
                    phase = BoomerangPhase.ReturnToPlayer1;
                }
                break;

            case BoomerangPhase.ReturnToPlayer1:
                Vector2 returnDir1 = ((Vector2)player.position - (Vector2)transform.position).normalized;
                transform.position += (Vector3)(returnDir1 * Speed * Time.deltaTime);

                if (Vector2.Distance(player.position, transform.position) < 0.1f)
                {
                    // Cache current position to start behind throw
                    startPosition2 = transform.position;
                    direction = -direction; // Reverse direction to throw behind
                    phase = BoomerangPhase.Behind;
                }
                break;

            case BoomerangPhase.Behind:
                transform.position += (Vector3)(direction * Speed * Time.deltaTime);

                if (Vector2.Distance(startPosition2, transform.position) >= MaxDistance)
                {
                    phase = BoomerangPhase.ReturnToPlayer2;
                }
                break;

            case BoomerangPhase.ReturnToPlayer2:
                Vector2 returnDir2 = ((Vector2)player.position - (Vector2)transform.position).normalized;
                transform.position += (Vector3)(returnDir2 * Speed * Time.deltaTime);

                if (Vector2.Distance(player.position, transform.position) < 0.1f)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject, EnemyLayer))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                if (!lastHitTime.ContainsKey(enemy) || Time.time - lastHitTime[enemy] >= HitCooldown)
                {
                    enemy.TakeDamage(Damage);
                    lastHitTime[enemy] = Time.time;
                }
            }
        }
    }

    bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return ((1 << obj.layer) & mask) != 0;
    }
}

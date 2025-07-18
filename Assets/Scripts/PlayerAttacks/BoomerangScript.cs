using System.Collections.Generic;
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
    public LayerMask enemyLayer { get; set; }

    private Vector2 startPosition1;
    private Vector2 startPosition2;
    private Vector2 direction;
    private bool returning1 = false;
    private bool returning2 = false;
    private Transform player;

    // Track last time enemy was hit, enemies can only be damaged by boomerang once every HitCooldown
    private Dictionary<EnemyScript, float> lastHitTime = new Dictionary<EnemyScript, float>();

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

        if (!returning1)
        {
            transform.position += (Vector3)(direction * Speed * Time.deltaTime);

            if (Vector2.Distance(startPosition1, transform.position) >= MaxDistance)
            {
                returning1 = true;
            }
        }
        else if (returning1 && !returning2)
        {
            startPosition2 = transform.position;
            Vector2 returnDir = (player.position - transform.position).normalized;
            transform.position += (Vector3)(returnDir * Speed * Time.deltaTime);

            if (Vector2.Distance(startPosition2, transform.position) >= MaxDistance)
            {
                returning2 = true;
            }
        }
        else
        {
            Vector2 returnDir = (player.position - transform.position).normalized;
            transform.position += (Vector3)(returnDir * Speed * Time.deltaTime);

            if (Vector2.Distance(player.position, transform.position) < 0.1f)
            {
                Destroy(gameObject); // End of attack, boomerang returns to player
            }
        }
    }
}

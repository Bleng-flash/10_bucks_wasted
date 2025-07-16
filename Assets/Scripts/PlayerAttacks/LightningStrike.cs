using UnityEngine;

// Lightning Strike is a prefab of a single, self-contained lightning strike, which will be used
// by Lightning Attack (which is the actual attack consisting of many lightning strike prefabs)
public class LightningStrike : AutoAttack
{
    [SerializeField] private float radius = 2f;

    // Used as an animation event to deal damage in a specific frame
    public void DoDamage()
    {
        // Detects all objects will colliders that are in enemy layer and add them to hitColliders
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        foreach (Collider2D collider in hitColliders)
        {
            EnemyScript enemy = collider.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                Debug.Log($"Lightning Strike {collider.name} with {this.damage} damage!");
                enemy.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("EnemyScript missing on: " + collider.name);
            }
        }
    }

    // Used as an animation event in the last frame of animation to delete lightning strike
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected override void PerformAttack()
    {
        return;
    }
    public override void Recalculate()
    {
        return;
    }
}

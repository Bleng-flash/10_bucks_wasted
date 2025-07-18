using UnityEngine;

// Lightning Strike is a prefab of a single, self-contained lightning strike, which will be used
// by Lightning Attack (which is the actual attack consisting of many lightning strike prefabs)
public class LightningStrike : MonoBehaviour
{
    public float Damage { get; set; }
    public LayerMask TargetLayer { get; set; }
    [SerializeField] private float radius = 2f;

    // Used as an animation event to deal damage in a specific frame
    public void DoDamage()
    {
        // Detects all objects with colliders that are in enemy layer and add them to hitColliders
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, TargetLayer);
        foreach (Collider2D collider in hitColliders)
        {
            EnemyScript enemy = collider.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                Debug.Log($"Lightning Strike {collider.name} with {Damage} damage!");
                enemy.TakeDamage(Damage);
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
}

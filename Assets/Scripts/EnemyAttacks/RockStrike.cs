using Unity.VisualScripting;
using UnityEngine;

// Rock Strike is a prefab of a single, self-contained rock strike, which will be used
// by Rock Attack (which is the actual attack consisting of many rock strike prefabs)
public class RockStrike : MonoBehaviour
{
    public float Damage { get; set; }
    public LayerMask TargetLayer { get; set; }
    [SerializeField] private float radius = 2f;
    

    // Used as an animation event to deal damage in a specific frame
    public void DoDamage()
    {
        // Detects player
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, TargetLayer);
        if (collider != null)
        {
            PlayerScript player = collider.GetComponent<PlayerScript>();
            if (player != null)
            {
                Debug.Log($"Rock Strike {collider.name} with {Damage} damage!");
                player.TakeDamage(Damage);
            }
            else
            {
                Debug.LogWarning("PlayerScript missing on: " + collider.name);
            }
        }
    }

    // Used as an animation event in the last frame of animation to delete rock strike
    public void DestroySelf()
    {
        Destroy(transform.parent.gameObject);
    }
}

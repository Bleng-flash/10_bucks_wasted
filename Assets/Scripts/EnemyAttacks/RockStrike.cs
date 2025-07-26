using Unity.VisualScripting;
using UnityEngine;

// Lightning Strike is a prefab of a single, self-contained lightning strike, which will be used
// by Lightning Attack (which is the actual attack consisting of many lightning strike prefabs)
public class RockStrike : MonoBehaviour
{
    public float Damage { get; set; }
    public LayerMask TargetLayer { get; set; }
    [SerializeField] private float radius = 2f;
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float spinSpeed = 100f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
    }

    // Used as an animation event to deal damage in a specific frame
    public void DoDamage()
    {
        // Detects player
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, TargetLayer);
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

    // Used as an animation event in the last frame of animation to delete lightning strike
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

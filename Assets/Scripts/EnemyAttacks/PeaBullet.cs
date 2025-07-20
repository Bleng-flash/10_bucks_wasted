using UnityEngine;
using UnityEngine.Pool;

public class PeaBullet : MonoBehaviour
{
    // Using public properties to set the private fields (instead of multiple setter methods)
    public float Speed { get; set; }
    public float Damage { get; set; }
    public LayerMask EnemyLayer { get; set; }
    public Vector2 Direction { get; set; }
    public BulletPool Pool { get; set; }
    public LayerMask BorderLayer { get; set; }

    void Update()
    {
        transform.position += (Vector3)(Direction * Speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject, EnemyLayer))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player != null && !player.IsDead())
            {
                player.TakeDamage(Damage);
                Pool.ReturnBullet(gameObject);    // Returns bullet to pool
            }
        }
        else if (IsInLayerMask(other.gameObject, BorderLayer))
        {
            Pool.ReturnBullet(gameObject);          // Return bullet to pool if it hits border
        }
    }

    bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return ((1 << obj.layer) & mask) != 0;
    }

    // Needed for object pooling, since it applies each time the object is enabled (taken from pool)
    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(AutoReturn), 5f); // return if no hit after 5s
    }

    void AutoReturn()
    {
        Pool.ReturnBullet(gameObject);
    }
}

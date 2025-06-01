using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float movementSpeed;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Fixed Update better for Rigidbody movement cause it depends on real time instead of frame rate
    void FixedUpdate()
    {
        // Move towards player
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
    }
    public void SetPlayerTarget(Transform player)
    {
        this.player = player;
    }
}

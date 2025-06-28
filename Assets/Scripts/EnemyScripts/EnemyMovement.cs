using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float movementSpeed;
    private Rigidbody2D rb;
    private bool isAttacking = false;
    private Animator animator;
    private EnemyScript enemy;

    // Below fields are for repulsive field attack, to stop enemy movement when repelled
    [SerializeField] private float repelDuration = 0.5f;
    private float repelTimer = 0f;
    private bool isRepelled = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<EnemyScript>();
    }

    // Fixed Update better for Rigidbody movement cause it depends on real time instead of frame rate
    void FixedUpdate()
    {
        // Stop moving if attacking or dead
        if (isAttacking || enemy.IsDead())
        {
            animator.SetBool("isWalking", false);
            return;
        }

        // Have the enemy stop moving when repelled 
        // (seperate from above logic because one is attacking and one is being attacked)
        if (isRepelled)
        {
            repelTimer -= Time.fixedDeltaTime;
            if (repelTimer <= 0.0)
            {
                isRepelled = false;
            }
            animator.SetBool("isWalking", false);
            return;
        }

        // Move towards player
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
        animator.SetBool("isWalking", true);
    }
    public void SetPlayerTarget(Transform player)
    {
        this.player = player;
    }

    public void Repel()
    {
        isRepelled = true;
        repelTimer = repelDuration;
    }

    public void SetAttacking(bool isAttacking)
    {
        this.isAttacking = isAttacking;
    }
}

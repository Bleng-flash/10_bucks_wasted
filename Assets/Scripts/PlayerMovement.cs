using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    [SerializeField]
    private float movementSpeed = 5f;
    private Rigidbody2D rb;

    // Method matches user input with input actions key bindings
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Use fixed update for rigidbody movement
    void FixedUpdate()
    {
        // Move player in direction based on input
        Vector2 direction = new Vector2(moveInput.x, moveInput.y);
        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);

        // Rotate the player to face movement direction
        if (moveInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}

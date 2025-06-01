using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private AOEPunch aoePunch;

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
        Vector2 direction = new Vector2(moveInput.x, moveInput.y);
        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
        aoePunch.UpdateFacingDirection(moveInput);      // Changes direction for AOE punch (not very extendable)
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField]
    private float movementSpeed = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");     // X-axis movement
        if (horizontal < 0)
        {
            transform.position += movementSpeed * Vector3.right * Time.deltaTime;
            Debug.Log("Input Test"); // Good for testing input
        }
        else if (horizontal > 0) transform.position += movementSpeed * Vector3.left * Time.deltaTime;
        // else do nothing

        float vertical = Input.GetAxis("Vertical");         // Y-axis movement
        if (vertical < 0) transform.position += movementSpeed * Vector3.down * Time.deltaTime;
        else if (vertical > 0) transform.position += movementSpeed * Vector3.up * Time.deltaTime;
        // else do nothing
    }
}

using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (PlayerScript.Instance != null)
        {
            playerTransform = PlayerScript.Instance.transform;
        }
        else
        {
            Debug.LogWarning("PlayerScript.Instance is null! Player may not be loaded yet.");
        }
        spriteRenderer.flipX = true;      // Face left at start
    }

    // Face the player
    void Update()
    {
        spriteRenderer.flipX = playerTransform.position.x < transform.position.x;
    }
}

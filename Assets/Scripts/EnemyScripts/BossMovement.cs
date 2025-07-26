using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Transform playerTransform;
    void Start()
    {
        if (PlayerScript.Instance != null)
        {
            playerTransform = PlayerScript.Instance.transform;
        }
        else
        {
            Debug.LogWarning("PlayerScript.Instance is null! Player may not be loaded yet.");
        }
        transform.localScale = new Vector3(-1, 1, 1);      // Face left at start
    }

    // Face the player
    void Update()
    {
        float dir = playerTransform.position.x - transform.position.x;
        if (dir <= 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // facing left
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // facing right
        }
    }
}

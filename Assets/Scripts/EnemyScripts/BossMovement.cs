using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    void Start()
    {
        transform.position = Vector2.left;      // Face left at start
    }

    // Face the player
    void Update()
    {
        float dir = player.position.x - transform.position.x;
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

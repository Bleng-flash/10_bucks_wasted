using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards player every frame
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0) * Time.deltaTime;
    }
}

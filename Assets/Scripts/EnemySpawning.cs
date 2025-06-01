using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnInterval = 3.0f;   // in seconds
    [SerializeField]
    private int spawnCount = 1; // number of enemies spawned per cycle
    private float timer = 0.0f; // time in seconds since last spawn

    // Border widths and heights
    public float width = 60.0f;
    public float height = 40.0f;


    public void SetSpawnInterval(float interval)
    {
        this.spawnInterval = interval;
    }

    // Update is called once per frame
    void Update()
    {
        this.timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemies();
            this.timer = 0.0f;
        }

    }

    private Vector2 GetRandomBorderPosition()
    {   
        float halfWidth = width / 2.0f;
        float halfHeight = height / 2.0f;

        int edge = Random.Range(0, 4);
        float x = 0.0f;
        float y = 0.0f;

        switch (edge)
        {
            case 0: // Top
                x = Random.Range(-halfWidth, halfWidth);
                y = halfHeight;
                break;
            case 1: // Bottom
                x = Random.Range(-halfWidth, halfWidth);
                y = -halfHeight;
                break;
            case 2: // Left
                x = -halfWidth;
                y = Random.Range(-halfHeight, halfHeight);
                break;
            case 3: // Right
                x = halfWidth;
                y = Random.Range(-halfHeight, halfHeight);
                break;
        }

        return new Vector2(x, y);
    }


    private void SpawnEnemies()
    {
        if (this.spawnCount <= 0) return;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 spawnPos = GetRandomBorderPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            // the 2nd argument expects a Vector3 but Unity implicitly converts Vector2 into
            // Vector3 by setting default z = 0

            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            if (movement != null)
            {
                movement.SetPlayerTarget(player);
            }
        }
        
    }
}

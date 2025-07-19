using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public Transform player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 3.0f;   // in seconds
    [SerializeField] private int spawnCount = 1; // number of enemies spawned per cycle
    [SerializeField] private float HPMultiplier;
    [SerializeField] private float ATKMultiplier;
    [SerializeField] private float XPDropMultiplier;
    [SerializeField] private float scoreMultiplier;

    private float timer = 0.0f; // time in seconds since last spawn

    // Border widths and heights
    private float width = 60.0f;
    private float height = 40.0f;
    int enemyLayer;


    void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    public void SetSpawnInterval(float interval)
    {
        spawnInterval = interval;
    }
    public void SetSpawnCount(int count)
    {
        spawnCount = count;
    }
    public void SetHPMultiplier(float val)
    {
        HPMultiplier = val;
    }
    public void SetATKMultiplier(float val)
    {
        ATKMultiplier = val;
    }
    public void SetXPMultiplier(float val)
    {
        XPDropMultiplier = val;
    }
    public void SetScoreMultiplier(float val)
    {
        scoreMultiplier = val;
    }

    // Update is called once per frame
    void Update()
    {
        // Stop spawner if player dies
        if (!GameManager.Instance.isPlayerAlive)
        {
            return;
        }
        if (player == null)
        {
            PlayerScript player = FindFirstObjectByType<PlayerScript>();
            this.player = player.transform;
        }
        
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemies();
            timer = 0.0f;
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
        if (spawnCount <= 0) return;

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
            enemy.layer = enemyLayer;

            // scales enemy stats based on their base stats 
            // (base stats is the stats we saved in the prefab for that enemy type in Unity inspector)
            EnemyScript enemyStats = enemy.GetComponent<EnemyScript>();
            if (enemyStats != null)
            {
                enemyStats.ScaleStats(HPMultiplier, ATKMultiplier, XPDropMultiplier, scoreMultiplier);
            }
        }
        
    }
}

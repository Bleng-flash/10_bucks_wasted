using UnityEngine;

// Enemy inherits from entity, contains hp and atk
public class EnemyScript : Entity
{
    [SerializeField] private int xpAmount;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.stats.Initialise(20.0f, 20.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Die()
    {
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(1);
        GameManager.Instance.xpSpawner.DropXp(xpAmount, transform.position);
        Debug.Log("Enemy killed!");
    }
}
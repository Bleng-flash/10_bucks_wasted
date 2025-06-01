using UnityEngine;

// Enemy inherits from entity, contains hp and atk
public class EnemyScript : Entity
{


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
        Debug.Log("Enemy killed!");
    }
}

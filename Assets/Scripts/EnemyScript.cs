using UnityEngine;

// Enemy inherits from entity, contains hp and atk
public class EnemyScript : Entity
{
    [SerializeField] // just for easier testing in editor
    private float maxHP;
    [SerializeField]
    private float HP;
    [SerializeField]
    private float ATK;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = new Attribute(maxHP, HP, ATK);
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

using Unity.VisualScripting;
using UnityEngine;

// Enemy inherits from entity, contains hp and atk
public class EnemyScript : Entity
{
    [SerializeField] private int xpAmount; // xp amount dropped by this enemy upon death
    [SerializeField] private int score; // score that the player gets when killing this enemy
    private Animator animator;

    void Awake()
    {
        Initialise(20.0f, 20.0f, 5.0f);
        team = Team.Enemy;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Die()
    {
        animator.SetTrigger("Die");
        ScoreManager.Instance.AddScore(score);
        GameManager.Instance.xpSpawner.DropXp(xpAmount, transform.position);
        Debug.Log("Enemy killed!");
    }

    // This method is called as an event in the last frame of the death animation, which then destroys the object
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }

}
using System;
using Unity.VisualScripting;
using UnityEngine;

// Enemy inherits from entity, contains hp and atk
public class EnemyScript : Entity
{
    [SerializeField] private float xpAmount; // xp amount dropped by this enemy upon death
    [SerializeField] private float score; // score that the player gets when killing this enemy
    private Animator animator;

    void Awake()
    {
        Initialise(maxHP, HP, ATK);
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
        GetComponent<Collider2D>().enabled = false;     // Turn off collider to stop collisions
        ScoreManager.Instance.AddScore(score);
        GameManager.Instance.xpSpawner.DropXp(xpAmount, transform.position);
        Debug.Log("Enemy killed!");
    }

    // This method is called as an event in the last frame of the death animation, which then destroys the object
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }

    // Called upon spawning
    public void ScaleStats(float HPMultiplier, float ATKMultiplier,
            float XPDropMultiplier, float scoreMultiplier)
    {
        maxHP *= HPMultiplier;
        HP = maxHP;
        ATK *= ATKMultiplier;
        xpAmount *= XPDropMultiplier;
        score *= scoreMultiplier;
    }

}
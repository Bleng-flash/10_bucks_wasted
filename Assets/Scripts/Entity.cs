using UnityEngine;

/* 
Entity is an abstract class used to encapsulate the player and mobs/boss
 (anything that has health, can attack and is attackable) 
Player and Enemies will inherit from Entity
*/
public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected float maxHP;
    [SerializeField] protected float HP;
    [SerializeField] protected float ATK;
    private Animator animator;
    protected bool isDead;
    public Team team;

    public enum Team
    {
        Player,
        Enemy
    }

    public void Initialise(float maxHP, float HP, float ATK)
    {
        this.maxHP = maxHP;
        this.HP = HP;
        this.ATK = ATK;
        this.isDead = false;
        animator = GetComponent<Animator>();
    }

    public void IncreaseHealthBy(float inc)
    {
        HP = Mathf.Min(maxHP, HP + inc);  // cannot exceed maxHP
        GameManager.Instance.OnPlayerDamage(HP, maxHP);
    }
    public void DecreaseHealthBy(float dec)
    {
        HP = Mathf.Max(0, HP - dec);      // Ensure minimum is 0
        // Debug.Log("Current HP: " + HP);
        CheckDeath();
    }

    public void IncreaseMaxHPBy(float inc)
    {
        maxHP += inc;
        GameManager.Instance.OnPlayerDamage(HP, maxHP);
    }

    public void RestoreAllHealth()
    {
        HP = maxHP;
    }
    public void SetMaxHealthTo(float newMaxHP)
    {
        maxHP = newMaxHP;
    }
    public float getHealthPercentage()
    {
        return HP / maxHP;
    }
    public float getLostHealth()
    {
        return maxHP - HP;
    }
    public void IncreaseATKBy(float inc)
    {
        ATK += inc;
        // ConfigureActiveAttacks();
    }
    public void DecreaseATKBy(float dec)
    {
        ATK = Mathf.Max(0, ATK - dec);      // Ensure minimum is 0
        // ConfigureActiveAttacks();
    }
    public float getATK()
    {
        return ATK;
    }

    // called when the entity takes damage (decrease health)
    public void CheckDeath()
    {
        if (HP <= 0.0f)
        {
            // Avoids death animation loops by repeating Die() every Update check
            if (isDead) return;

            isDead = true;
            Die();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public virtual void TakeDamage(float dmg)
    {
        if (isDead) return;     // Don't trigger take damage animation when enemy is dying
        animator.SetTrigger("TakeDamage");
        DecreaseHealthBy(dmg);
    }

    public abstract void Die();

    public bool IsEnemyTo(Entity other)
    {
        return this.team != other.team;
    }
    public void ConfigureActiveAttacks()
    {
        Attack[] attacks = GetComponentsInChildren<Attack>();
        foreach (Attack attack in attacks)
        {
            attack.Recalculate();
        }
    } 
}

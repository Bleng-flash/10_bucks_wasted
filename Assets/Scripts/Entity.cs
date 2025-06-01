using UnityEngine;

/* 
Entity is an abstract class used to encapsulate the player and mobs/boss
 (anything that has health, can attack and is attackable) 
Player and Enemies will inherit from Entity
*/
public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected Attribute stats;
    [SerializeField]
    protected Attack[] attacks; // stores the attacks owned by this entity

    public void TakeDamage(float dmg)
    {
        this.stats.DecreaseHealthBy(dmg);
        if (stats.CheckDeath())
        {
            Die();
        }
    }

    public abstract void Die();
}
